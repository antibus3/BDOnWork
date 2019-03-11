using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace BDOnWorkLib
{
    public class ParseKoffer : Panel
    {
        public Bitmap primalDrawing { get; private set; }  //  исходное изображение
        public PictureBox chartDrawing { get; private set; }  //  Изображение графика

        private Label _info;

        public ParseKoffer (string patchOfFile)
        {
            try
            {
                ThisSetting();
                CreateInfo();
                primalDrawing = new Bitmap(patchOfFile);
            }catch
            {
                _info.Text = "График не найден";
                return;
            }

            try
            {
                RunParseChart();
            }
            catch
            {
                _info.Text = "График не сформирован";
                return;
            }
            
        }

        //  Метод настройки родительского элемента
        private void ThisSetting ()
        {
            this.Height = 165;
            this.Width = 358;
        }

        //  Метод создания и настройки строки информации
        private void CreateInfo ()
        {
            _info = new Label();
            _info.AutoSize = true;
            _info.Text = "Загрузка";
            _info.Parent = this;
            _info.Location = new Point(3, 66);
            _info.Font = new Font(FontFamily.GenericSansSerif, 20);
            _info.Visible = true;
        }

        //  Метод создания и настройки основного графика
        private void CreateChart (Bitmap scalableDrawing)
        {
            chartDrawing = new PictureBox();
            chartDrawing.Width = this.Width;
            chartDrawing.Height = this.Height;
            chartDrawing.Location = new Point(0, 0);
            chartDrawing.Parent = this;
            chartDrawing.Image = new Bitmap(scalableDrawing, new Size(chartDrawing.Width, chartDrawing.Height));
            _info.Visible = false;
        }

        private async void RunParseChart ()
        {
            int endChart;
            Bitmap scalableDrawing = await Task.Run(() => ParseChart(out endChart));
            CreateChart(scalableDrawing);

        }

        //  Метод парсинга графика
        private Bitmap ParseChart (out int endChart)
        {
            int beginChart = FindRowBeginChart();
            endChart = FindRowEndChart(beginChart);
            Bitmap tempchartDrawing = new Bitmap(primalDrawing.Width, endChart - beginChart);
            int yactual = 0;
            for (int y = beginChart; y < endChart; y++)
            {
                for (int x = 0; x < primalDrawing.Width; x++)
                    tempchartDrawing.SetPixel(x, yactual, primalDrawing.GetPixel(x, y));
                yactual++;
            }
            return tempchartDrawing;
        }

        // Метод поиска начала графика
        private int FindRowBeginChart ()
        {
            for (int i = 0; i < primalDrawing.Height; i++)
            {

                List<Color> notVoidColorsRow = CompilationRowColors(i);

                int countPixelBeginChart = 0;
                if (notVoidColorsRow.Count != 0)
                    if (notVoidColorsRow[0].Name == "ffa0a0a0")
                        foreach (Color notVoidcolor in notVoidColorsRow)
                            if (notVoidcolor.Name == "ffa0a0a0") countPixelBeginChart++; //  Подсчитать количество пикселей, соответствующих верхней границе графика
                if (countPixelBeginChart > 1000)
                    return i;
            }
            throw new Exception("Начало графика не найдено");
        }

        //  Метод поиска конца графика
        private int FindRowEndChart(int beginChart)
        {
            for (int i = beginChart; i < primalDrawing.Height; i++)
            {
                List<Color> notVoidColorsRow = CompilationRowColors(i);
                if (notVoidColorsRow.Count != 0)
                    if (CompilationNamePixel(notVoidColorsRow, 23) == CodeNumber.vog)
                        return i - 21; //  т.к. слово "Вог" находится ниже конца графика на 21 пиксел, то вычитаем их
            }
            throw new Exception("Не найден конец графика");
        }

        //  Составим список пикселей, содержащихся в строке за сиключением пустых
        private List<Color> CompilationRowColors (int numberRow)
        {
            List<Color> result = new List<Color>();
            for (int i = 0; i < primalDrawing.Width; i++ )
            {
                if (primalDrawing.GetPixel(i, numberRow).Name != CodeNumber.abyys)
                    result.Add(primalDrawing.GetPixel(i, numberRow));
            }
            return result;
        }

        //  Метод состовления строки из имён пикселей (для сравнения)
        private string CompilationNamePixel (List<Color> row, int countPixels)
        {
            if (row.Count <= countPixels) return "";
            string result = "";
            for (int i = 0; i < countPixels; i++)
                result += row[i].Name + " ";
            return result;
        }

        //  Метод парсинга значений класса и коэфицентов
        private void ParseValue (int endChart)
        {
            List<Color> notVoidColorsRow = new List<Color>();
            for (int i = 0; i < primalDrawing.Width; i++)  
                notVoidColorsRow.Add(primalDrawing.GetPixel(i, endChart + 21));  //  т.к. я вычел пиксели от слова "Вог" до конца графика, то добавляю их
            FindClass(notVoidColorsRow);

        }

        private int FindClass(List<Color> notVoidColorsRow)
        {
            while (notVoidColorsRow.Count >= 36)  //  36 - это количество пикселей в слове Класс
            {
                if (notVoidColorsRow[0].Name != "fff0ab60")
                {
                    notVoidColorsRow.RemoveAt(0);
                    continue;
                }
                string findPixels = CompilationNamePixel(notVoidColorsRow, 36);
                if (findPixels == CodeNumber.numberClass)  //  найти слово "класс"
                    break;
                notVoidColorsRow.RemoveAt(0);
            }
            if (notVoidColorsRow.Count <= 36) throw new Exception("Класс не найден");
            notVoidColorsRow.RemoveRange(0, 36);

            //  поиск белой области, но которой написан номер класса
            int j = 0;
            while (j < 200)  //  200 - примерное число, на котором уже должен быть точно определён класс
            {
                if (notVoidColorsRow[0].Name == "ffffffff") break;
                notVoidColorsRow.RemoveAt(0); j++;
                if (j >= 200) throw new Exception("Класс не найден");
            }
            //  поиск в этой белой области начала числа
            while (j < 200)
            {
                if (notVoidColorsRow[0].Name != "ffffffff") break;
                notVoidColorsRow.RemoveAt(0); j++;
                if (j >= 200) throw new Exception("Класс не найден");
            }
            string s = CompilationNamePixel(notVoidColorsRow, 20);

            return 0;
        }
    }
}
