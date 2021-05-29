using System;
using System.Collections.Generic;
using System.Text;

namespace Abiturient
{
    public class Student
    {
        public string school { get; set; }

        public int[] marks { get; set; } = new int[3];

        public string fullName { get; set; }

        public Student(string fullName, int[] marks, string school)
        {
            this.fullName = fullName;
            this.marks = marks;
            this.school = school;
        }
    }
}