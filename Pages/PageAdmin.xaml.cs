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
using WPFtest2.Class;

namespace WPFtest2.Pages
{
    /// <summary>
    /// Interakční logika pro PageAdmin.xaml
    /// </summary>
    public partial class PageAdmin : Page
    {
        public PageAdmin()
        {
            
            InitializeComponent();
            Uzivatel uzivatel = new Uzivatel();
            List<Uzivatel> uzivatels = uzivatel.GetAll();
            UsersListView.ItemsSource = uzivatels;
        }
            
            

    }
}
