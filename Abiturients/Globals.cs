using System;
using System.Collections.Generic;
using System.Text;

namespace Abiturients
{
    public class Student
    {
        public string school { get; set; }

        public int[] marks { get; set; } = new int[3];

        public string fullName { get; set; }

        public Student() { }

        public Student(string fullName, int[] marks, string school)
        {
            this.fullName = fullName;
            this.marks = marks;
            this.school = school;
        }
    }
}