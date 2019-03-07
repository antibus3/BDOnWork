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

        

        public ParseChartVK (string directionFile)
        {
            DirectionFile = directionFile;
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
        private void VisibleChart ()
        {


        }

        //  Метод нахождения данных диаграммы
        private void CreateDateChart(string directionFile)
        {
            try
            {
                //  Запрос в файл с данными для получения точек, по каторым строится диаграмма
                InteractionWithBase oCon = new InteractionWithBase(directionFile);
                oCon.SettingConnectToBD();
                List<SensitiveElement> PointsOfChart = QueriesToBD.SelectFromBD(oCon, "Лист1", new SensitiveElement());

                //  Настройка диаграмм
                Series SChartSPI_SPI1 = new Series("СПИ");
                SChartSPI_SPI1.ChartType = SeriesChartType.Line;
                Series SChartTemperature = new Series("Температура");
                SChartTemperature.ChartType = SeriesChartType.Line;
                Series SChartPower1 = new Series("Мощьность");
                SChartPower1.ChartType = SeriesChartType.Line;

                foreach (SensitiveElement s in PointsOfChart)
                {
                    //  Составление точек диаграммы СПИ
                    //  Основные параметры для составления точек
                    double time = (double)s.Filds["Time (s)"];
                    double spi1 = (double)s.Filds["ER1(dB)"];
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
            }
            catch
            {
                MessageBox.Show("Возникла ошибка на операции составления графиков СИОМ. Обратитесь к рукожопу, который делал эту программу", "Рукожёп детектед", MessageBoxButtons.OK);
                throw;
            }
        }

    }
}
