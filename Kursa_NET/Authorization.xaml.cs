using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Abiturient
{
    /// <summary>
    /// Логика взаимодействия для Authorization.xaml
    /// </summary>
    public partial class Authorization : Window
    {

        public Authorization()
        {
            InitializeComponent();
        }
        public bool loged_in { get; set; }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            loged_in = MainWindow.dataAccess.CheckLogin(login_box.Text, password_box.Password);
            if (loged_in)
            {
                this.Close();
            }
            else { 
            
                MessageBox.Show("Лошара, ты куда звонишь блять");
            }
        }
    }
}