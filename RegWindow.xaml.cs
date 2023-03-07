using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
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

namespace WPFtest2
{
    /// <summary>
    /// Interakční logika pro RegWindow.xaml
    /// </summary>
    public partial class RegWindow : Window
    {
        public RegWindow()
        {
            InitializeComponent();
            checkLogin();
        }
            // conection string
        SqlConnection con = new SqlConnection(@"Data Source=TOMAS;Initial Catalog=Projekt;Integrated Security=True");

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            {
                con.Open();
                string login,email ;
                System.Security.SecureString pass = txtHeslo.SecurePassword;
                System.Security.SecureString passZnovu = txtHeslo_Copy.SecurePassword;
                System.Security.SecureString emailheslo = txtHesloMail.SecurePassword;
                string passString = new System.Net.NetworkCredential(string.Empty,pass).Password;                                    //převedeni ze secure pass do stringu
                string passMailString = new System.Net.NetworkCredential(string.Empty, emailheslo).Password;
                string passZnovulString = new System.Net.NetworkCredential(string.Empty, passZnovu).Password;
                login = txtLogin.Text;
                //heslo = txtHeslo.SecurePassword;
                email = txtEmail.Text;
                //potvrzeniHesla = txtHeslo_Copy.Text;




                try
                {
                    var emaill = new System.Net.Mail.MailAddress(email);

                    if (checkLogin() == true || login != "" || passString != "" || passString != passZnovulString)

                        if (checkLogin() == true)
                        {
                            MessageBox.Show("Uzivatel už existuje");
                        }
                        else if (login == "" || passString == "")
                        {
                            MessageBox.Show("Nevyplneno", "chyba");
                        }
                        else if (passString != passZnovulString)
                        {
                            MessageBox.Show("hesla se neshodují", "chyba");
                        }




                        else
                        {

                            string query = "INSERT INTO Login (login, heslo, email,emailheslo,role) VALUES (@login, @hesloString, @email, @hesloMailString, @role)";
                            SqlCommand cmd = new SqlCommand(query, con);

                            cmd.Parameters.AddWithValue("@login", login);
                            cmd.Parameters.AddWithValue("@hesloString", passString);
                            cmd.Parameters.AddWithValue("@email", email);
                            cmd.Parameters.AddWithValue("@hesloMailString", passMailString);
                            cmd.Parameters.AddWithValue("role", "user");

                            MessageBox.Show("Registrace proběhla uspěšně", ":)");
                            txtHeslo.Clear();
                            txtLogin.Clear();
                            txtHeslo_Copy.Clear();
                            txtHesloMail.Clear();
                            txtEmail.Clear();

                            cmd.ExecuteNonQuery();


                            MainWindow mainWindow = new MainWindow();
                            mainWindow.Show();
                            this.Close();
                        }

                }

                catch (SqlException)
                {
                    MessageBox.Show("Chyba spojení", "Chyba");

                }
                catch (FormatException)
                {
                    MessageBox.Show("Email neni ve správném formátu");
                }

                finally
                {
                    con.Close();

                }
            }
        }

            private Boolean checkLogin()
        {
            string login= txtLogin.Text;
            Boolean loginFree = false;

            String kontrolaDuplicity = "SELECT * FROM Login WHERE login = '" + login + "'";


            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = kontrolaDuplicity;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = cmd;
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)

            {
                loginFree = true;
            }
            return loginFree;


        }

        private void btn_back_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }
    }
}
