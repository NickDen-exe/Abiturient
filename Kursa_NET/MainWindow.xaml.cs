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

namespace Abiturient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static List<Student> selected = new List<Student>();
        
        List<string> textNames = new List<string>()
        {
            "ПІБ",
            "Перша оцінка",
            "Друга оцінка",
            "Третя оцінка",
            "Школа"
        };

        static public DataAccess dataAccess = new DataAccess();
        public static Authorization authorization = new Authorization();

        private List<Student> abiturients = new List<Student>();

        public MainWindow()
        {
            InitializeComponent();
        }


        private void MainForm_Loaded(object sender, RoutedEventArgs e)
        {
            //this.Hide();
            //authorization.ShowDialog();
            //if (authorization.loged_in == true)
            //{
            //    this.Show();
                LoadData();
            //}
            //else
            //{
            //    this.Close();
            //}
        }

        private void LoadData()
        {
            try
            {
                List<Student> result = dataAccess.GetStudents();
                if(result != null)
                {
                    abiturients = result;
                    foreach(Student element in abiturients)
                    {
                        DataTable.Items.Add(element);
                    }
                }
                else
                {
                    throw new Exception("База даних пуста");
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Помилка", MessageBoxButton.OK, MessageBoxImage.Warning);

            }
        }

        bool DisplayList(List<Student> students)
        {
            try
            {
                DataTable.Items.Clear();
                foreach (var student in students)
                {
                    DataTable.Items.Add(student);
                }
                return true;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Помилка");
                return false;
            }
        }

        private void DataTable_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Name_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            if(String.IsNullOrWhiteSpace(textBox.Text))
            {
                switch (textBox.Name)
                {
                    case "Name":
                        textBox.Text = textNames[0];
                        break;
                    case "Mark1":
                        textBox.Text = textNames[1];
                        break;
                    case "Mark2":
                        textBox.Text = textNames[2];
                        break;
                    case "Mark3":
                        textBox.Text = textNames[3];
                        break;
                    case "School":
                        textBox.Text = textNames[4];
                        break;
                }
                textBox.Opacity = 0.7;
            }
            else
            {
                textBox.Opacity = 1;
            }
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(SelectData.SelectedItem != null)
            {
                ComboBoxItem item = (ComboBoxItem)SelectData.SelectedItem;
                SelectDataWindow selectDataWindow = new SelectDataWindow(item.Content.ToString());
                selectDataWindow.Show();
                SelectData.SelectedItem = null;
                if (selected.Count != 0)
                {
                    DisplayList(selected);
                }
            }

        }
    }
}
