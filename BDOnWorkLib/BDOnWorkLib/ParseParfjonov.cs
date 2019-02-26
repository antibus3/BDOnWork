using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace BDOnWorkLib
{
    public class ParseParfjonov : Panel
    {

        private readonly Bitmap _primalDrawing; //  исходное изображение
        private readonly Bitmap _chartDrawing;

        public ParseParfjonov () { }

        public ParseParfjonov (string patchOfFile)
        {
            _primalDrawing = new Bitmap(patchOfFile);
            ParseChart();
        }

        private Bitmap ParseChart ()
        {
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
            return resultBitMap;
        }

    }
}
