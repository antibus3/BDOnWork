using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using System.Drawing;
using System.Threading.Tasks;
using System.Threading;


namespace BDOnWorkLib
{
    public class ParseChart : Panel
    {
        private Chart ChartSPI = new Chart();
        private Chart ChartLoss = new Chart();
        private Chart ChartPower = new Chart();       

        private double dMaxSPI = 0;
        private double dMinSPI = 100;
        private double dMaxPower = 0;
        private double dMinPower = 100;
        private double dMaxKD = 0;
        private double dMinKD = 100;
        private double dMaxLoss = 0;
        private double dMinLoss = 100;

        private Label MessageLabel = new Label();
        private readonly string DirectionFile;



        public ParseChart ()
        {
        }

        public ParseChart (string directionFile)
        {
            try
            {
                DirectionFile = directionFile;
                this.Size = new Size(1100, 300);
                CreateMessage();
                VisibleChart();


            }
            catch
            {
                MessageLabel.Text = "Графики не могут быть сформированы";
            }
         }

        //  Метод создания сообщения
        private void CreateMessage ()
        {
            MessageLabel.Font = new Font("Microsoft Sans Serif", 50);
            MessageLabel.Text = "Выполняется состовление графиков";
            MessageLabel.Size = this.Size;
            MessageLabel.Top = this.Top + 30;
            MessageLabel.Left = this.Left + 50;
            MessageLabel.Parent = this;

        }

        //  Метод, отображающий диаграммы
        private async void VisibleChart()
        {
            try
            {
                await Task.Run(() => { CreateChart(); });
                //  Если небыло исключения, значит графики построились
                MessageLabel.Visible = false;
                ChartSPI.Parent = this;
                ChartSPI.Left = 0;
                ChartLoss.Parent = this;
                ChartLoss.Left = ChartSPI.Right + 10;
                ChartPower.Parent = this;
                ChartPower.Left = ChartLoss.Right + 10;
            } catch
            {
                MessageBox.Show("Возникла ошибка на операции визуализации графика СИОМ. Обратитесь к рукожопу, который делал эту программу", "Рукожёп детектед", MessageBoxButtons.OK);
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
        
        //  Метод заполнения диаграмм точками.
        private void CreateDateChart (string directionFile)
        {
            try
            {
                //  Запрос в файл с данными для получения точек, по каторым строится диаграмма
                InteractionWithBase oCon = new InteractionWithBase(directionFile);
                oCon.SettingConnectToBD();
                List<SensitiveElement> PointsOfChart = QueriesToBD.SelectFromBD(oCon, "Паспорт СИОМ", new SensitiveElement());

                //  Настройка диаграмм
                Series SChartSPI_SPI1 = new Series("СПИ 1");
                SChartSPI_SPI1.ChartType = SeriesChartType.Line;
                Series SChartSPI_SPI2 = new Series("СПИ 2");
                SChartSPI_SPI2.ChartType = SeriesChartType.Line;
                Series SChartTemperature = new Series("Температура");
                SChartTemperature.ChartType = SeriesChartType.Line;
                Series SChartLoss = new Series("Потери");
                SChartLoss.ChartType = SeriesChartType.Line;
                Series SChartKD = new Series("Коофицент деления");
                SChartKD.ChartType = SeriesChartType.Line;
                Series SChartPower1 = new Series("Мощьность 1");
                SChartPower1.ChartType = SeriesChartType.Line;
                Series SChartPower2 = new Series("Мощьность 2");
                SChartPower2.ChartType = SeriesChartType.Line;

                foreach (SensitiveElement s in PointsOfChart)
                {
                    //  Составление точек диаграммы СПИ
                    //  Основные параметры для составления точек
                    double time = (double)s.Filds["Time (s)"];
                    double spi1 = (double)s.Filds["ER1(dB)"];
                    double spi2 = (double)s.Filds["ER2(dB)"];
                    double power1 = (double)s.Filds["POW1(mW)"];
                    double power2 = (double)s.Filds["POW2(mW)"];
                    double temperature = (double)s.Filds["Темп#, °С"];
                    double kd = (double)s.Filds["КД по лев# кан#, %"];
                    double loss = (double)s.Filds["Потери, дБ"];

                    //  Поиск минималных и максимальных точкд для отображения диаграмм
                    if (dMaxSPI < spi1) dMaxSPI = spi1;
                    if (dMaxSPI < spi2) dMaxSPI = spi1;
                    if (dMinSPI > spi1) dMinSPI = spi1;
                    if (dMinSPI > spi2) dMinSPI = spi2;
                    if (dMaxPower < power1) dMaxPower = power1;
                    if (dMaxPower < power2) dMaxPower = power2;
                    if (dMinPower > power1) dMinPower = power1;
                    if (dMinPower > power2) dMinPower = power2;
                    if (dMaxKD < kd) dMaxKD = kd;
                    if (dMinKD > kd) dMinKD = kd;
                    if (dMaxLoss < loss) dMaxLoss = loss;
                    if (dMinLoss > loss) dMinLoss = loss;

                    //  Диаграмма СПИ
                    DataPoint DPspi1 = new DataPoint(time, spi1);
                    DataPoint DPspi2 = new DataPoint(time, spi2);
                    DataPoint DPtemperature = new DataPoint(time, temperature);
                    //  Диаграмма Потери\КД
                    DataPoint DPloss = new DataPoint(time, loss);
                    DataPoint DPkd = new DataPoint(time, kd);
                    //  Диаграмма Мощности
                    DataPoint DPpower1 = new DataPoint(time, power1);
                    DataPoint DPpower2 = new DataPoint(time, power2);

                    //  Добавление точек к шкале диаграммы
                    SChartSPI_SPI1.Points.Add(DPspi1);
                    SChartSPI_SPI2.Points.Add(DPspi2);
                    SChartTemperature.Points.Add(DPtemperature);
                    SChartPower1.Points.Add(DPpower1);
                    SChartPower2.Points.Add(DPpower2);
                    SChartKD.Points.Add(DPkd);
                    SChartLoss.Points.Add(DPloss);
                }


                //  Добавление графиков к диаграммам
                ChartSPI.Series.Add(SChartSPI_SPI1);
                ChartSPI.Series.Add(SChartSPI_SPI2);
                ChartSPI.Series.Add(SChartTemperature);
                ChartLoss.Series.Add(SChartLoss);
                ChartLoss.Series.Add(SChartKD);
                ChartPower.Series.Add(SChartPower1);
                ChartPower.Series.Add(SChartPower2);
            } catch
            {
                MessageBox.Show("Возникла ошибка на операции составления графиков СИОМ. Обратитесь к рукожопу, который делал эту программу", "Рукожёп детектед", MessageBoxButtons.OK);
                throw;
            }
        }

        //  Метод настройки внешнего вида диаграмм
        private void CreateAreaChart ()
        {
            //  добавление заголовков
            ChartSPI.Titles.Add("Диаграмма СПИ");
            ChartLoss.Titles.Add("Диаграмма Потери/КД");
            ChartPower.Titles.Add("Диаграмма Мощьности");

            //  добавление областей диаграммы
            ChartSPI.ChartAreas.Add("СПИ");
            ChartLoss.ChartAreas.Add("Потери/КД");
            ChartPower.ChartAreas.Add("Мощьность");


            // Добавление легенды
            Legend LegendSPI = new Legend("СПИ");
            LegendSPI.Docking = Docking.Top;
            ChartSPI.Legends.Add(LegendSPI);
            Legend LegendLoss = new Legend("Потери/КД");
            LegendLoss.Docking = Docking.Top;
            ChartLoss.Legends.Add(LegendLoss);
            Legend LegendPower = new Legend("Мощность");
            LegendPower.Docking = Docking.Top;
            ChartPower.Legends.Add(LegendPower);

            //  Настройка шкалы
            ChartSPI.ChartAreas[0].AxisX.Title = "Время c.";
            ChartSPI.ChartAreas[0].AxisY.Title = "Температура";
            ChartSPI.ChartAreas[0].AxisY2.Title = "СПИ";
            ChartSPI.ChartAreas[0].AxisX.MajorGrid.Enabled = false;
            ChartSPI.ChartAreas[0].AxisY.MajorGrid.Enabled = false;
            ChartSPI.ChartAreas[0].AxisY2.MajorGrid.Enabled = false;
            ChartSPI.ChartAreas[0].AxisY2.Maximum = Math.Round(dMaxSPI, 0) + 10;
            ChartSPI.ChartAreas[0].AxisY2.Minimum = Math.Round(dMinSPI, 0) - 10;
            ChartSPI.Series[1].YAxisType = AxisType.Secondary;

            ChartLoss.ChartAreas[0].AxisX.Title = "Время c.";
            ChartLoss.ChartAreas[0].AxisY.Title = "Потери дБ";
            ChartLoss.ChartAreas[0].AxisY2.Title = "КД %";
            ChartLoss.ChartAreas[0].AxisX.MajorGrid.Enabled = false;
            ChartLoss.ChartAreas[0].AxisY.MajorGrid.Enabled = false;
            ChartLoss.ChartAreas[0].AxisY2.MajorGrid.Enabled = false;
            ChartLoss.ChartAreas[0].AxisY.Maximum = Math.Round(dMaxLoss, 2) + 0.1;
            ChartLoss.ChartAreas[0].AxisY.Minimum = Math.Round(dMinLoss, 2) - 0.1;
            ChartLoss.ChartAreas[0].AxisY2.Maximum = Math.Round(dMaxKD, 2) + 0.2;
            ChartLoss.ChartAreas[0].AxisY2.Minimum = Math.Round(dMinKD, 2) - 0.2;
            ChartLoss.Series[1].YAxisType = AxisType.Secondary;

            ChartPower.ChartAreas[0].AxisX.Title = "Время c.";
            ChartPower.ChartAreas[0].AxisY.Title = "Мощность мВт";
            ChartPower.ChartAreas[0].AxisX.MajorGrid.Enabled = false;
            ChartPower.ChartAreas[0].AxisY.MajorGrid.Enabled = false;
            ChartPower.ChartAreas[0].AxisY.Maximum = Math.Round(dMaxPower, 2) + 0.005;
            ChartPower.ChartAreas[0].AxisY.Minimum = Math.Round(dMinPower, 2) - 0.005;
        }



    }
}
