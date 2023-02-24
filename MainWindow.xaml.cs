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
using System.Drawing.Design;
using System.Security.Cryptography.X509Certificates;
using System.Diagnostics.Eventing.Reader;

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
        
        SqlConnection con = new SqlConnection(@"Data Source=TOMAS;Initial Catalog=Projekt;Integrated Security=True"); // connection string


        private void Button_Click(object sender, RoutedEventArgs e)
        {

            // nacteni uzivatele

            try
            {
                String querry = "SELECT * FROM  Login WHERE login = '" + textBoxLogin.Text + "' AND heslo='" + textBoxHeslo.Text + "'";
                SqlDataAdapter adapter = new SqlDataAdapter(querry, con);

                DataTable dt = new DataTable();
                adapter.Fill(dt);
                

                if (dt.Rows.Count > 0)
                {
                    DataRow row = dt.Rows[0];
                    Uzivatel user = new Uzivatel();
                    AppMain appMain = new AppMain();

                    user.Login = row["login"].ToString();
                    user.Pass = row["heslo"].ToString();
                    user.Role = row["role"].ToString().Substring(0,5);
                    user.Email = row["email"].ToString() ?? "";
                    user.EmailPass = row["emailheslo"].ToString() ?? "";
                    
                    Global.LogedUser = user;
                    var btnAdmin = appMain.FindName("btnAdmin") as Button;     // Nalezení tlačítka v okně

                    // kontrola jestli je danný uživatel admin a ¨zviditelnení admin tlacitka
                    bool isAdmin = user.IsAdmin(user);

                    if (isAdmin==true)
                    {
                        
                        btnAdmin.Visibility = Visibility.Visible;

                    }
                    else 
                    {

                        btnAdmin.Visibility = Visibility.Collapsed;

                    }

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

        private void tlacitkoPrihlas_MouseEnter(object sender, MouseEventArgs e)
        {
            ((Button)sender).Foreground = Brushes.Red;
        }

        private void tlacitkoPrihlas_MouseLeave(object sender, MouseEventArgs e)
        {
            ((Button)sender).Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFBAC8FF"));
        }

        private void tlacitkoRegistrace_Click(object sender, RoutedEventArgs e)
        {
            RegWindow regWindow = new RegWindow(); 
            regWindow.Show();
            this.Close();
        }
    }
}
