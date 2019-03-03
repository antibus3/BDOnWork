using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;
using System.Threading;

namespace BDOnWorkLib
{
    public class ParseParfjonov : Panel
    {

        public Bitmap _primalDrawing { private set; get; } //  исходное изображение
        public PictureBox _chartDrawing { private set; get; }  //  Изображение графика
        public string _initialDrift { private set; get; }
        private Label Info;

        public ParseParfjonov () { }

        public ParseParfjonov (string patchOfFile)
        {
            try
            {
                ThisSetting();
                _primalDrawing = new Bitmap(patchOfFile);
                CreateInfo();
            }
            catch
            {
                Info.Text = "График не найден.";
                return;
            }
            try
            {
                CreatePicture();
                AsyncStartParse();
            }
            catch
            {
                Info.Text = "График не сформирован.";
            }

        }
        //  Метод ностройки родительской панели
        private void ThisSetting ()
        {
            this.Width = 358;
            this.Height = 165;
        }

        //  метод настройки изображения графика
        private void CreatePicture ()
        {
            _chartDrawing = new PictureBox();
            _chartDrawing.Width = 358;
            _chartDrawing.Height = 165;
            _chartDrawing.Location = new Point(0, 0);
            _chartDrawing.Parent = this;
        }

        //  настройка строки информации
        private void CreateInfo ()
        {
            Info = new Label();
            Info.AutoSize = true;
            Info.Text = "Загрузка";
            Info.Parent = this;
            Info.Location = new Point(3, 66);
            Info.Font = new Font(FontFamily.GenericSansSerif, 20);
            Info.Visible = true;
        }

        private async void AsyncStartParse ()
        {
            try
            {
                _chartDrawing.Image = await Task.Run(() => ParseChart());
                Info.Visible = false;
            }
            catch
            {
                throw new Exception();
            }
        }

        private Bitmap ParseChart ()
        {
            Thread.Sleep(3000);
            // образец, который соответствует заголовку графика
            List<string> sampleColor = new List<string> () {
                "ff903aff", "ff0000ff", "ff0000ff", "ff0000ff", "ffb6ffff",
                "ff903aff", "ff0000ff", "ff0000ff", "ff0000ff", "ffb6ffff"
                                                           };
            int beginYChart = 0; //  Переменная, означающая, когда начинается строка с графиком
            for (int i = 0; i < _primalDrawing.Height; i++)
            {
                /*  т.к. я не знаю, где именно будет нужный график, но знаю, что будет написано в ряду с названием графика
                 *  то ищу все пиксели без белый в каждом ряду и сравниваю с образцом
                */
                List<Color> notWhiteColor = new List<Color>();
                for (int j = 0; j < _primalDrawing.Width; j++)
                {
                    if (_primalDrawing.GetPixel(j, i).Name != "ffffffff")
                        notWhiteColor.Add(_primalDrawing.GetPixel(j, i));
                }
                bool equalSample = true; //  Эта переменная указывает, найден ли конец заголовка перед графиком
                if (notWhiteColor.Count == 10)
                {
                    for (int j = 0; j < notWhiteColor.Count; j++)
                    {
                        if (notWhiteColor[j].Name != sampleColor[j]) { equalSample = false; break; }
                    }
                } else equalSample = false;
                if (equalSample)
                {
                    beginYChart = i + 7; //  +7 потому что, от заголовка до самого графика 7 пикилей
                    break;
                }
            }
            if (beginYChart ==0 ) throw (new Exception("График не найден"));  //  в этом случае график не найден и выдать исключние 
            int beginXChart = 0; //  откуда начинается график по оси х
            //  поиск, с какого пикселя начинается график
            for (int j = 0; j < _primalDrawing.Width; j++)
                if (_primalDrawing.GetPixel(j, beginYChart).Name != "ffffffff")
                {
                    beginXChart = j; break;
                }
            int heightChart = 165; //  размер графика по оси у
            int widthChart = 358; //  размер графика по оси x
            //  Создать новый битмап, который будет содержать только нужный график
            Bitmap resultBitMap = new Bitmap(widthChart, heightChart);
            int ii = 0;
            int jj = 0;
            for (int i = beginYChart; i < beginYChart + heightChart; i++)
            {
                for (int j = beginXChart; j < beginXChart + widthChart; j++)
                {
                    
                    resultBitMap.SetPixel(jj++, ii, _primalDrawing.GetPixel(j, i));
                }
                ii++; jj = 0;
            }

            //  далее найти велечину дрейа
            try
            {
                _initialDrift = ParseValue(beginYChart + heightChart);
            }catch
            {
                _initialDrift = "";
            }
            return resultBitMap;
        }

        
        private string ParseValue (int numberBeginRow)
        {
            //  Образец, равноценный букывм "Др" (Дрейф). На эти буквы больше ничего не начинавется.
            List<string> sampleColor = new List<string>() {
                "ffb66600", "ff000000", "ff000000", "ff000000", "ff000000", "ff000000", "ff000000","ff006690",
                "ff903a00", "ff000000", "ff000000", "ff0066b6", "ffffffff", "ffffffff"
                                                           };
            string result = "";
            for (int i = numberBeginRow; i < _primalDrawing.Height; i++)
            {
                List<Color> RowColor = new List<Color>();
                //  Записать в лист строку цветов, за исключением первых белых (пустых) 
                for (int j = 0; j < _primalDrawing.Width; j++)
                {
                    if (_primalDrawing.GetPixel(j, i).Name != "ffffffff")
                    {
                        for (int jj = j; jj < _primalDrawing.Width; jj++)
                            RowColor.Add(_primalDrawing.GetPixel(jj, i));
                        break;
                    }
                }
                //  посмотреть, является ли начало листа словом "Дрейф"
                bool sampleFound = true;
                if (RowColor.Count == 0) sampleFound = false;
                else
                for (int j = 0; j < 14; j++)
                    if (sampleColor[j] != RowColor[j].Name) sampleFound = false;
                //  Если найдена нужная строка, тогда найти на ней первое число, стоящее после заголовка (оно и будет искомым дрейфом)
                //  Если в пределах 10 пиксилей небудет ничего, значит числа нет
                if (sampleFound) { result = ParseNumber(RowColor); break; }
            }

            return result;
        }
        
        private string ParseNumber (List<Color> rowColor)
        {
            //  каждое число 6 писилей в длину. Сравниваем строку.
            int beginPixelNumber = 41; //  После это пикселя кончается слово "Дрейф"
            //  ищем начало числа

            rowColor.RemoveRange(0, beginPixelNumber); //  Удаляем слово "Дрейф"
            string voidNumber = "ffffffff ffffffff ffffffff ffffffff ffffffff ffffffff ";
            string actualString = "";
            do
            {
                rowColor.RemoveAt(0);
                //  Находим 6 пикселей (возможное число)
                actualString = "";
                for (int i = 0; i < 6; i++)
                    actualString += rowColor[i].Name + " "; 
            } while (actualString.Equals(voidNumber));
            bool endNumber = false;
            string result = "";
            while (!endNumber)  //  пока не закончилось число
            {
                //  Состовление предпологаемого числа (6 пикселей)
                actualString = "";
                for (int i = 0; i < 6; i++)
                    actualString += rowColor[i].Name + " ";

                //  проверка на конец числа
                if (actualString.Equals("ffffffff ffffffff ffffffff ffffffff ffffffff ffffffff ")) break;
                //  Проверка на точку
                if (CodeNumber.Point.Equals(rowColor[0].Name+ " " + rowColor[1].Name+ " " + rowColor[2].Name + " "))
                {
                    result += ".";
                    rowColor.RemoveRange(0, 3);
                    continue;
                }

                //  прверка на число
                string FindNumber = FindNumberInPixel(actualString);
                if (FindNumber != "")
                {
                    rowColor.RemoveRange(0, 6);
                    result += FindNumber;
                }
                else rowColor.RemoveAt(0);
            }
            return result;
        }

        //  сравнение пикселей с образцами на предмет числа
        private string FindNumberInPixel (string stringNumber)
        {
            //  Проверка на цифру 0
            if (CodeNumber.n0.Equals(stringNumber))
                return "0";

            //  Проверка на цифру 1
            if (CodeNumber.n1.Equals(stringNumber))
                return "1";

            //  Проверка на цифру 2
            if (CodeNumber.n2.Equals(stringNumber))
                return "2";

            //  Проверка на цифру 3
            if (CodeNumber.n3.Equals(stringNumber))
                return "3";

            //  Проверка на цифру 4
            if (CodeNumber.n4.Equals(stringNumber))
                return "4";

            //  Проверка на цифру 5
            if (CodeNumber.n5.Equals(stringNumber))
                return "5";

            //  Проверка на цифру 6
            if (CodeNumber.n6.Equals(stringNumber))
                return "6";

            //  Проверка на цифру 7
            if (CodeNumber.n7.Equals(stringNumber))
                return "7";

            //  Проверка на цифру 8
            if (CodeNumber.n8.Equals(stringNumber))
                return "8";

            //  Проверка на цифру 9
            if (CodeNumber.n9.Equals(stringNumber))
                return "9";
            return "";
        }

    }

    //  Образцы цифр
    public static class CodeNumber
    {
        public const string Point = "ffffb666 ff00003a ff90dbff ";
        public const string n0 = "ffffffff ffffb666 ff000000 ff00003a ff90dbff ffffffff ";
        public const string n1 = "ffffffff ffdb903a ff000000 ff000000 ff0066b6 ffffffff ";
        public const string n2 = "ffdb903a ff000000 ff000000 ff000000 ff000066 ffb6ffff ";
        public const string n3 = "ffdb903a ff000000 ff000000 ff003a90 ffdbffff ffffffff ";
        public const string n4 = "ffffffff ffffffff ffffffff ffb66600 ff66b6ff ffffffff ";
        public const string n5 = "ffffb666 ff000000 ff000000 ff0066b6 ffffffff ffffffff ";
        public const string n6 = "ffffffff ffffb666 ff000000 ff000000 ff66b6ff ffffffff ";
        public const string n7 = "ffffffff ffffffff ffb66666 ffb6ffff ffffffff ffffffff ";
        public const string n8 = "ffffffff ffdb903a ff000000 ff000000 ff0066b6 ffffffff ";
        public const string n9 = "ffffb666 ff000000 ff003a90 ffdbffff ffffffff ffffffff ";
    }
}
