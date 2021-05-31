using Microsoft.VisualStudio.TestTools.UnitTesting;
using Abiturients;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Abiturients.Tests
{
    [TestClass()]
    public class MainWindowTests
    {
        [TestMethod()]
        public void SearchX()
        {
            List<Student> expected = new List<Student>()
            {
                new Student( "Денисенко Микита Сергійович", new int[3]{ 4, 5, 3}, "21"),
                new Student("Веретельніков Микола Вадимович", new int[3] { 2, 2, 3 }, "19")
            };

            DataAccess dataAccess = new DataAccess();
            List<Student> actual = dataAccess.SelectData("");

            for (int i = 0; i < expected.Count; i++)
            {
                Assert.AreEqual(expected[i].fullName, actual[i].fullName);
                Assert.AreEqual(expected[i].marks[0], actual[i].marks[0]);
                Assert.AreEqual(expected[i].marks[1], actual[i].marks[1]);
                Assert.AreEqual(expected[i].marks[2], actual[i].marks[2]);
                Assert.AreEqual(expected[i].school, actual[i].school);
            }
        }

        [TestMethod()]
        public void SearchXY()
        {
            List<Student> expected = new List<Student>()
            {
                new Student( "Денисенко Микита Сергійович", new int[3]{ 4, 5, 3}, "21"),
                new Student("Веретельніков Микола Вадимович", new int[3] { 2, 2, 3 }, "19")
            };

            DataAccess dataAccess = new DataAccess();
            List<Student> actual = dataAccess.SelectData("", "");

            for (int i = 0; i < expected.Count; i++)
            {
                Assert.AreEqual(expected[i].fullName, actual[i].fullName);
                Assert.AreEqual(expected[i].marks[0], actual[i].marks[0]);
                Assert.AreEqual(expected[i].marks[1], actual[i].marks[1]);
                Assert.AreEqual(expected[i].marks[2], actual[i].marks[2]);
                Assert.AreEqual(expected[i].school, actual[i].school);
            }
        }

        [TestMethod()]
        public void LoadDataTest()
        {
            List<Student> expected = new List<Student>()
            {
                new Student( "Денисенко Микита Сергійович", new int[3]{ 4, 5, 3}, "21"),
                new Student("Веретельніков Микола Вадимович", new int[3] { 2, 2, 3 }, "19")
            };

            DataAccess dataAccess = new DataAccess();
            List<Student> actual = dataAccess.GetStudents();

            for (int i = 0; i < expected.Count; i++)
            {
                Assert.AreEqual(expected[i].fullName, actual[i].fullName);
                Assert.AreEqual(expected[i].marks[0], actual[i].marks[0]);
                Assert.AreEqual(expected[i].marks[1], actual[i].marks[1]);
                Assert.AreEqual(expected[i].marks[2], actual[i].marks[2]);
                Assert.AreEqual(expected[i].school, actual[i].school);
            }
        }

    }
}