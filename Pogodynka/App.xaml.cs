using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Pogodynka
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
    }
    class Pogodynka {
    public class coord
    {
        double lon { get; set; }
        double lat { get; set; }
    }
    public class weather
    {
        string main { get; set; }
        string description { get; set; }
        string icon { get; set; }
    }
    public class main
    {
        double temp { get; set; }
        double pressure { get; set; }
        double humidity { get; set; }
    }
    public class wind
    {
        double speed { get; set; }

    }
    public class sys
    {
        long sunrise { get; set; }
        long sunset { get; set; }
    }

    public class nest
    {
        public coord coord { get; set; }
        public List <weather> weather { get; set; }
        public main main { get; set; }
        public wind wind { get; set; }
        public sys sys { get; set; }
    }
    }
}
