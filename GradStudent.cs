﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentDbApp
{
    internal class GradStudent : Student
    {

        public decimal TuitionCredit { get; set; }

        public string FacultyAdvisor { get; set; }

        public GradStudent(string first, string last, string email, double gpa, decimal credit, string advisor)

            //refers to base class
            : base(first, last, email, gpa)
        {
            TuitionCredit = credit;
            FacultyAdvisor = advisor;
        }

        public override string ToStringForOutputFile()
        {
            string str = this.GetType().Name + "\n";
            str += base.ToStringForOutputFile() + "\n";
            str += $"{TuitionCredit}\n";
            str += $"{FacultyAdvisor}";

            return str;


        }
        public override string ToString()
        {
            //return base class to string method + other additinal arguments
            return base.ToString() + $"Credit: {TuitionCredit}\nFacAdv: {FacultyAdvisor}\n";
        }
    }
}
