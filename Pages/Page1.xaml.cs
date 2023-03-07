using Newtonsoft.Json;
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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WPFtest2.Class;


namespace WPFtest2.Pages
{
    /// <summary>
    /// Interakční logika pro Page1.xaml
    /// </summary>
    public partial class Page1 : Page
    {
        public Page1()
        {
            #region /* Data k aktualnímu počasí*/
            string city ="Prague";
            
            InitializeComponent();
            Weather weather = new Weather();
            Weather weatherdata= WeatherService.GetWeather(city);
            

            var temp = weatherdata.MainInfo.Temperature;
            var main = weatherdata.WeatherInfo[0].Description;
            var location = weatherdata.Location;
            textBlokPocasi.Text = "Teplota: " + temp.ToString() + "°C";
            textBlockpocasi.Text = "Stav: " + main.ToString();
            textBlockLocation.Text = location.ToString().ToUpper();
            #endregion



            #region /* data  k aktualni cene BTC */   
            List<Crypto> cryptoList = cryptoService.GetCrypto();                     
            if (cryptoList.Count > 0)                                                                    //kontrola jestli list obsahuje alespon jeden prvek v seznamu cryptolist
            {
                textBlockcrypto.Text ="Aktuální cena BTC: " +  cryptoList[0].CurrentPrice.ToString() +" $" ;
            }
            #endregion






            if (Global.LogedUser != null)
            {
                lbl_pocetMailu.Content = Global.LogedUser.Unread;
            }
            else
            {
                lbl_pocetMailu.Content = 0; 
            }

        }





    }
}
