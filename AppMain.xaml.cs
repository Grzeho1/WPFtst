using System;
using System.Collections.Generic;
using System.Drawing;
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
using System.Windows.Shapes;
using WPFtest2.Pages;

namespace WPFtest2
{
    /// <summary>
    /// Interakční logika pro AppMain.xaml
    /// </summary>
    public partial class AppMain : Window
    {
        public AppMain()
        {
            InitializeComponent();
        }

        private void Buttona_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            
            Page1 page1= new Page1();
            Page2 page2 = new Page2();
            
        }

        private void ListViewItem_Selected(object sender, RoutedEventArgs e)
        {
            Page1 page1= new Page1();
            Main.Navigate(page1);
            obr_home.Source= new BitmapImage(new Uri(@"/Resources/home_over.png",UriKind.Relative));

        }

        private void ListViewItem_Selected_1(object sender, RoutedEventArgs e)
        {
            Page2 page2 = new Page2();
            Main.Navigate(page2);
            obr_home.Source = new BitmapImage(new Uri(@"/Resources/home.png", UriKind.Relative));
            
        }

        private void ListViewItem_Selected_2(object sender, RoutedEventArgs e)
        {

        }

        private void Main_Loaded(object sender, RoutedEventArgs e)
        {

            if (Global.LogedUser != null)
            {
                lbl_uzivatel.Content = Global.LogedUser.Login;
            }
            else
            {
                lbl_uzivatel.Content = "Guest";
                
                 
            }
            
        }

        
    }
}
