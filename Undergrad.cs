using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentDbApp
{

    public enum YearRank
    {
        //One years rank is represented the students 
        // progress though program
        Freshman = 1,
        Sophomore = 2,
        Junior = 3,
        Senior = 4
    }
    // undergrad is a derived entity of student.
    // undergrad is a kind of student
    internal class Undergrad : Student
    {
        public YearRank Rank { get; set; }

        public string DegreeMajor { get; set; }

        public Undergrad(string first, string last, string email, double gpa, YearRank rank, string degreeMajor)
        
            //refers to base class
            : base(first, last, email, gpa)
        {
            Rank = rank;
            DegreeMajor = degreeMajor;
        }
        //can only override a virtual 
        public override string ToStringForOutputFile()
        {
            string str = this.GetType().Name + "\n";
            str += base.ToStringForOutputFile() + "\n";
            str += $"{Rank}\n";
            str += $"{DegreeMajor}";

            return str;

        }

        public override string ToString()
        {
            //return base class to string method + other additinal arguments
            return base.ToString() + $" Year: {Rank}\n Major: {DegreeMajor}\n";
        }
    }
}
