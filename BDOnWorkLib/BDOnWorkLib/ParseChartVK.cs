using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using System.Drawing;

namespace BDOnWorkLib
{
    public class ParseChartVK : Panel
    {
        private Chart ChartVK = new Chart();

        private double dMaxSPI = 0;
        private double dMinSPI = 100;
        private double dMaxPower = 0;
        private double dMinPower = 100;

        private Label MessageLabel = new Label();
        private readonly string DirectionFile;
        private readonly int NumberVKinChart;  //  т.к. в одном файле могут быть 2 катушки (а могут и не быть), то нужно указать номер этой катушки в файле

        public ParseChartVK (string directionFile, int numberVKinChart)
        {
            DirectionFile = directionFile;
            NumberVKinChart = numberVKinChart;
            this.Size = new Size(1100, 300);
            CreateMessage();
            VisibleChart();

        }

        //  Метод создания информационного сообщения
        private void CreateMessage ()
        {
            MessageLabel.Font = new Font("Microsoft Sans Serif", 50);
            MessageLabel.Text = "Выполняется состовление графиков";
            MessageLabel.Size = this.Size;
            MessageLabel.Top = this.Top + 30;
            MessageLabel.Left = this.Left + 50;
            MessageLabel.Parent = this;
        }

        //  Метода парсинга файла
        private async void VisibleChart ()
        {
            try
            {
                await Task.Run(() => { CreateChart(); });
                //  Если небыло исключения, значит графики построились
                MessageLabel.Visible = false;
                ChartVK.Parent = this;
                ChartVK.Left = 0;
            }
            catch
            {
                MessageBox.Show("Возникла ошибка на операции визуализации графика ВК. Обратитесь к рукожопу, который делал эту программу", "Рукожёп детектед", MessageBoxButtons.OK);
                throw;
            }

        }

        //  Метод создания диаграмм
        private void CreateChart()
        {
            CreateDateChart(DirectionFile);
            GC.Collect(); // при запросе точек диаграммы выделяется большой объём памяти. Его надо очистить, чтобы не мешался.
            CreateAreaChart();
        }

        //  Метод нахождения данных диаграммы
        private void CreateDateChart(string directionFile)
        {
            try
            {
                //  Запрос в файл с данными для получения точек, по каторым строится диаграмма
                InteractionWithBase oCon = new InteractionWithBase(directionFile);
                oCon.SettingConnectToBD();
                string spiVK = String.Format("ER{0}(dB)", NumberVKinChart);
                string powerVK = String.Format("POW{0}(mW)", NumberVKinChart);
                Dictionary<string, object> fildsOfVK = new Dictionary<string, object>();
                fildsOfVK.Add("Time (s)", null);
                fildsOfVK.Add(spiVK, null);
                fildsOfVK.Add(powerVK, null);
                List<SensitiveElement> PointsOfChart = QueriesToBD.SelectFromBD(oCon, "Лист1", new SensitiveElement());

                //  Настройка диаграмм
                Series SChartSPI_SPI = new Series("СПИ");
                SChartSPI_SPI.ChartType = SeriesChartType.Line;
                Series SChartPower = new Series("Мощьность");
                SChartPower.ChartType = SeriesChartType.Line;

                foreach (SensitiveElement s in PointsOfChart)
                {
                    //  Составление точек диаграммы СПИ
                    //  Основные параметры для составления точек
                    double time = (double)s.Filds["Time (s)"];
                    double spi = (double)s.Filds[spiVK];
                    double power = (double)s.Filds[powerVK];

                    //  Поиск минималных и максимальных точкд для отображения диаграмм
                    if (dMaxSPI < spi) dMaxSPI = spi;
                    if (dMinSPI > spi) dMinSPI = spi;
                    if (dMaxPower < power) dMaxPower = power;
                    if (dMinPower > power) dMinPower = power;

                    //  Диаграмма СПИ
                    DataPoint DPspi = new DataPoint(time, spi);
                    //  Диаграмма Мощности
                    DataPoint DPpower = new DataPoint(time, power);

                    //  Добавление точек к шкале диаграммы
                    SChartSPI_SPI.Points.Add(DPspi);
                    SChartPower.Points.Add(DPpower);
                }


                //  Добавление графиков к диаграммам
                ChartVK.Series.Add(SChartSPI_SPI);
                ChartVK.Series.Add(SChartPower);
            }
            catch
            {
                MessageBox.Show("Возникла ошибка на операции составления графиков ВК. Обратитесь к рукожопу, который делал эту программу", "Рукожёп детектед", MessageBoxButtons.OK);
                throw;
            }
        }

        //  Метод настройки внешнего вида диаграмм
        private void CreateAreaChart()
        {
            //  добавление заголовков
            ChartVK.Titles.Add("Диаграмма ВК");

            //  добавление областей диаграммы
            ChartVK.ChartAreas.Add("СПИ/Мощьность");

            // Добавление легенды
            Legend LegendSPI = new Legend("СПИ/Мощьность");
            LegendSPI.Docking = Docking.Top;
            ChartVK.Legends.Add(LegendSPI);

            //  Настройка шкалы
            ChartVK.ChartAreas[0].AxisX.Title = "Время c.";
            ChartVK.ChartAreas[0].AxisY.Title = "СПИ";
            ChartVK.ChartAreas[0].AxisY2.Title = "Мощьность";
            ChartVK.ChartAreas[0].AxisX.MajorGrid.Enabled = false;
            ChartVK.ChartAreas[0].AxisY.MajorGrid.Enabled = false;
            ChartVK.ChartAreas[0].AxisY2.MajorGrid.Enabled = false;
            ChartVK.ChartAreas[0].AxisY2.Maximum = Math.Round(dMaxPower, 3) + 0.01;
            ChartVK.ChartAreas[0].AxisY2.Minimum = Math.Round(dMinPower, 3) - 0.01;
            ChartVK.ChartAreas[0].AxisY.Maximum = Math.Round(dMaxSPI, 0) + 10;
            ChartVK.ChartAreas[0].AxisY.Minimum = Math.Round(dMinSPI, 0) - 10;
            ChartVK.Series[1].YAxisType = AxisType.Secondary;
        }

    }
}
