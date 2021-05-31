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

namespace Abiturients
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
        private WriteInWord writeInWord = new WriteInWord();
        private Student selectedStudent = new Student();
        private List<Student> abiturients = new List<Student>();

        public MainWindow()
        {
            InitializeComponent();
        }

        public static void ErrorShow(string Message, string Header = "Помилка")
        {
            MessageBox.Show(Message, Header, MessageBoxButton.OK, MessageBoxImage.Warning);
        }

        public void MainForm_Loaded(object sender, RoutedEventArgs e)
        {
            this.Hide();
            authorization.ShowDialog();
            if (authorization.loged_in == true)
            {
                this.Show();
                LoadData();
            }
            else
            {
                this.Close();
            }
        }

        private void LoadData()
        {
            try
            {
                List<Student> result = dataAccess.GetStudents();

                DataTable.Items.Clear();
                abiturients.Clear();

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
                ErrorShow(ex.Message);
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
                MessageBox.Show(ex.Message);
                return false;
            }
        }

        private void DataTable_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selectedStudent = (Student)DataTable.SelectedItem;

            if(selectedStudent != null)
            {
                Name.Text = selectedStudent.fullName;
                Mark1.Text = Convert.ToString(selectedStudent.marks[0]);
                Mark2.Text = Convert.ToString(selectedStudent.marks[1]);
                Mark3.Text = Convert.ToString(selectedStudent.marks[2]);
                School.Text = selectedStudent.school;
            }

        }

        private void TextBox_TextChanged(object sender, RoutedEventArgs e)
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
                selectDataWindow.ShowDialog();
                SelectData.SelectedItem = null;
                if (selected.Count != 0)
                {
                    DisplayList(selected);
                }
            }

        }

        private void ClearEditForm()
        {
            Name.Text = String.Empty;
            Mark1.Text = String.Empty;
            Mark2.Text = String.Empty;
            Mark3.Text = String.Empty;
            School.Text = String.Empty;
        }

        private bool UserInputOk()
        {
            return !String.IsNullOrWhiteSpace(Name.Text) && !String.IsNullOrWhiteSpace(Mark1.Text) &&
                !String.IsNullOrWhiteSpace(Mark2.Text) && !String.IsNullOrWhiteSpace(Mark3.Text) &&
                !String.IsNullOrWhiteSpace(School.Text) &&
                Convert.ToInt32(Mark1.Text) > 0 && Convert.ToInt32(Mark1.Text) < 100 &&
                Convert.ToInt32(Mark2.Text) > 0 && Convert.ToInt32(Mark2.Text) < 100 &&
                Convert.ToInt32(Mark3.Text) > 0 && Convert.ToInt32(Mark3.Text) < 100;
        }

        private bool DeleteAbiturient(Student student)
        {
            try
            {
                if (dataAccess.DeleteFromBase(student))
                {
                    return true;
                }
                else
                {
                    ErrorShow("Не вдалось видалити абітурієнта з бази даних");
                    return false;
                }
            }
            catch
            {
                ErrorShow("Не вдалось видалити абітурієнта");
                return false;
            }
        }

        private void AddStudent()
        {
            try
            {
                Student student = new Student(Name.Text, new int[3] { Convert.ToInt32(Mark1.Text), Convert.ToInt32(Mark2.Text), Convert.ToInt32(Mark3.Text) }, School.Text);

                int result = dataAccess.AddToBase(student);
                if (result == 1)
                {
                    LoadData();
                    ClearEditForm();
                }
                else if (result == 0)
                {
                    throw new Exception("Не вдалось додати абітурієнта");
                }
                else
                {
                    throw new Exception("Такий абітурієнт вже існує");
                }
            }
            catch (Exception ex)
            {
                ErrorShow(ex.Message);
            }

        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!UserInputOk())
                {
                    throw new Exception("Неправильні вхідні дані");
                }
                if (DataTable.SelectedItem == null)
                {
                    AddStudent();
                }
                else
                {
                    if (DeleteAbiturient(selectedStudent))
                    {
                        AddStudent();
                    }
                    else
                    {
                        throw new Exception("Не вдалось відредагувати запис");
                    }
                }
                ClearEditForm();
                DataTable.SelectedItem = null;
            }
            catch (Exception ex)
            {
                ErrorShow(ex.Message);
            }
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                selectedStudent = (Student)DataTable.SelectedItem;
                if (selectedStudent == null)
                {
                    throw new Exception();
                }

                DeleteAbiturient(selectedStudent);

                LoadData();
                ClearEditForm();
                DataTable.SelectedItem = null;
            }
            catch
            {
                ErrorShow("Ви не обрали абітурієнта", "Помилка!");
            }

        }

        private void ReportButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (selected.Count == 0)
                {
                    throw new Exception("Не вдалося створити звіт, здійсніть спочатку пошук абітурієнтів");
                }
                if (writeInWord.WriteData(selected))
                {
                    MessageBox.Show("Інформація була успішно записана");
                }
            }
            catch (Exception ex)
            {
                ErrorShow(ex.Message, "Помилка");
            }
        }

        private void TextBox_FocusableChanged(object sender, RoutedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            string text = String.Empty;

            switch (textBox.Name)
            {
                case "Name":
                    text = textNames[0];
                    break;
                case "Mark1":
                    text = textNames[1];
                    break;
                case "Mark2":
                    text = textNames[2];
                    break;
                case "Mark3":
                    text = textNames[3];
                    break;
                case "School":
                    text = textNames[4];
                    break;
            }

            if (text == textBox.Text)
            {
                textBox.Text = String.Empty;
            }
        }

        private void MainForm_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            App.Current.Shutdown();
        }
    }
}
