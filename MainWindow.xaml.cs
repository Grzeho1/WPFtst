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
using System.Data.SqlClient;
using System.Data.Common;
using Microsoft.SqlServer.Server;
using System.Data;
using WPFtest2.Pages;

namespace WPFtest2
{
    /// <summary>
    /// Interakční logika pro MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            
            InitializeComponent();
        }
        SqlConnection con = new SqlConnection(@"Data Source=TOMAS;Initial Catalog=Projekt;Integrated Security=True");


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            

            
           
            

            try
            {
                String querry = "SELECT * FROM  Login WHERE login = '" + textBoxLogin.Text + "' AND heslo='" + textBoxHeslo.Text + "'";
                SqlDataAdapter adapter = new SqlDataAdapter(querry, con);

                DataTable dt = new DataTable();
                adapter.Fill(dt);


                if (dt.Rows.Count > 0)
                {
                    DataRow row = dt.Rows[0];
                    Uzivatel uzivatel = new Uzivatel();


                    uzivatel.Login = row["login"].ToString();
                    uzivatel.Heslo = row["heslo"].ToString();
                    uzivatel.Role = row["role"].ToString();

                    Global.PrihlasenyUzivatel = uzivatel;

                    AppMain appMain = new AppMain();
                    appMain.Show();
                    this.Close();
                    
                }
                  


                else
                {
                    MessageBox.Show("Chybné jméno nebo heslo", "Chyba");

                }
            }
            catch
            {
                MessageBox.Show("Spojení ztraceno", "Chyba");
            }
            finally
            {
                con.Close();
            }

          
        }

        private void tlacitkoPrihlas_GotFocus(object sender, RoutedEventArgs e)
        {

        }

        private void btn_exit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            AppMain appMain = new AppMain();
            appMain.Show();
            this.Close();
            
        }
    }
}
