using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Newtonsoft.Json;
using System.Net;
using Microsoft.Win32;
using System.IO;

namespace Pogodynka
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class DashBoard : Window
    {
        
        public DashBoard()
        {
            InitializeComponent();
        }

        string APIKey = "c599fe69d63596c49356a4ec0502276d";

        public BitmapImage Image { get; private set; }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            getWeather();
        }
        /// <summary>
        /// Getting data and images from api
        /// </summary>
        void getWeather()
        {
            using (WebClient web = new WebClient())
            {
                string url = string.Format("https://api.openweathermap.org/data/2.5/weather?q={0}&appid={1}",TBCity.Text, APIKey );
                var json = web.DownloadString(url);
                PogodynkaApp.nest pogodynkaInfo = JsonConvert.DeserializeObject < PogodynkaApp.nest>(json);
                var image = new Image();

                var fullFilePath = @"http://openweathermap.org/img/w/" + pogodynkaInfo.weather[0].icon +".png";
                BitmapImage bitmap = new BitmapImage();
                bitmap.BeginInit();
                bitmap.UriSource = new Uri(fullFilePath, UriKind.Absolute);
                bitmap.EndInit();
                image.Source = bitmap;
                wrappanel1.Children.Add(image);

                txtCondition.Text = pogodynkaInfo.weather[0].main;
                txtDetails.Text = pogodynkaInfo.weather[0].description;
                txtSunset.Text = convertDateTime(pogodynkaInfo.sys.sunset).ToShortTimeString();
                txtSunrise.Text = convertDateTime(pogodynkaInfo.sys.sunrise).ToShortTimeString();
                txtWindspeed.Text = pogodynkaInfo.wind.speed.ToString();
                txtPressure.Text = pogodynkaInfo.main.pressure.ToString();
                txtTemp.Text = pogodynkaInfo.main.temp.ToString() + "°";
            }
        }
        /// <summary>
        /// Time converter from api to date
        /// </summary>
        /// <param name="sec"></param>
        /// <returns></returns>
        DateTime convertDateTime(long sec)
        {
            DateTime day = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc).ToLocalTime();
            day = day.AddSeconds(sec).ToLocalTime();
            return day;


        }
        /// <summary>
        /// Save to file button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            if (saveFileDialog.ShowDialog() == true)
                File.WriteAllText(saveFileDialog.FileName, txtDetails.Text + txtCondition.Text +" "+ txtSunset.Text + " " + txtSunrise.Text + " " + txtWindspeed.Text + " " + txtPressure.Text + " " + txtTemp.Text);
        }
    }
}
