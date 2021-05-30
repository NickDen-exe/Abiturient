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

namespace Abiturients
{
    /// <summary>
    /// Логика взаимодействия для SelectDataWindow.xaml
    /// </summary>
    public partial class SelectDataWindow : Window
    {
        string input { get; set; } = String.Empty;

        public SelectDataWindow(string input)
        {
            InitializeComponent();

            this.input = input;
            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                List<Student> students = new List<Student>();

                if (input == "Пошук зарахованих абітурієнтів")
                {
                    students = MainWindow.dataAccess.SelectData(Mark_Select.Text);
                }
                else
                {
                    students = MainWindow.dataAccess.SelectData(Mark_Select.Text, School_Select.Text);
                }
                if(students.Count == 0)
                {
                    throw new Exception("Такі абітурієнти не знайдені");
                }
                MainWindow.selected = students;
                MessageBox.Show("Пошук виконано успішно)0)");

                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Помилка");
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            School_Select.Visibility = (input == "Пошук зарахованих абітурієнтів") ? Visibility.Hidden : Visibility.Visible;
        }
    }
}
