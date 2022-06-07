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
        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {

        }
        void getWeather()
        {
            using (WebClient web = new WebClient())
            {
                string url = string.Format("https://api.openweathermap.org/data/2.5/weather?q={0}&appid={1}",TBCity.Text, APIKey );
                var json = web.DownloadString(url);
                Pogodynka.nest pogodynkaInfo = JsonConvert.DeserializeObject < Pogodynka.nest>(json);
                
            }
        }
    }
}
