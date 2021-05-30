using System;
using System.Collections.Generic;
using Microsoft.Office.Interop.Word;

namespace Abiturients
{
    class WriteInWord
    {
        Application wordApp;
        Document wordDoc;

        string filePath;

        public bool WriteData(List<Student> selected)
        {
            try
            {
                filePath = AppDomain.CurrentDomain.BaseDirectory.ToString();
                wordApp = new Application();

                wordDoc = wordApp.Documents.Add(filePath + "\\Шаблон_Пошуку.dotx");
            }
            catch (Exception ex)
            {
                MainWindow.ErrorShow(ex.Message + char.ConvertFromUtf32(13) +
                    "Помістіть файл Шаблон_Пошуку.dotx" + char.ConvertFromUtf32(13) +
                    "у каталог із ехе-файлом програми і повторіть збереження", "Помилка");
            }

            try
            {
                ReplaceText(selected);
                wordDoc.Save();
            }
            catch (Exception ex)
            {
                MainWindow.ErrorShow(ex.Message + char.ConvertFromUtf32(13) +
                    "Помилка збереження відібраних даних", "Помилка");
                return false;
            }
            return true;
        }

        private void ReplaceText(List<Student> selected)
        {
            if (selected == null)
            {
                return;
            }
            for (int i = 0; i < selected.Count; i++)
            {
                wordDoc.Tables[1].Rows.Add();
                wordDoc.Tables[1].Cell(2 + i, 1).Range.Text = Convert.ToString(i + 1);
                wordDoc.Tables[1].Cell(2 + i, 2).Range.Text = selected[i].fullName;
            }
        }
    }
}
