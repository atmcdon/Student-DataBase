using System.Runtime.Versioning;

namespace StudentDbApp
{
    


    //The Student class represents a single record in the database for a student
    //in the school. We'll determine what parameters we want to store in that record .

    //the student class is an example of encapsulation, the first "feature" or mechanism of 
    // any OO language. it provides a software component that holds all the STATE and the BEHAVIOR having
    // to do with an object of that type.

    //Encapsulation is covered in CH10
    // Inheritance - ch11
    //Poly morphism - CH12

    //object has a defualt tosting method
    //implicitly call object class
    internal class Student //: object
    {
        //what attributes do we want to store

        //this is a normal student 
        //this is a generic student 
        // Grad student will need to 

        public string FirstName { get; set; }
        
        public string LastName { get; set; }

        //intuitively chosen as the promary key for a record

        public string EmailAddress { get; set; }

        public double GradePtAvg { get; set; }



        // fully specified ctor (full spec)
        public Student(string fName, string lName, string email, double gpa)
        {
            FirstName = fName;
            LastName = lName;
            EmailAddress = email;
            GradePtAvg = gpa;
            
        }

        //do nothing no-arg (smae as default)
        public Student()
        {
        }

        //need a way for a student obj to pring itself
        // original override of the ToString for the user
        public override string ToString()
        {
            // 1 declare temp string
            string str = string.Empty;

            // 2 build up that string with the info from this object

            str += "**********Student Rec******************\n";
            str += $"First: {FirstName}\n";
            str += $" Last: {LastName}\n";
            str += $"Email: {EmailAddress}\n";
            str += $"  GPA: {GradePtAvg}\n";
            


            // 3 return the string
            return str;
        }

        //we need a way for a student obj to print itself
        //formatted for the output file - only data, no labels, etc
        // virtual looks for other final verions of this method.
        public virtual string ToStringForOutputFile()
        {
            // 1 declare temp string
            string str = string.Empty;

            // 2 build up that string with the info from this object

            str += $"{FirstName}\n";
            str += $"{LastName}\n";
            str += $"{EmailAddress}\n";
            str += $"{GradePtAvg:F2}";
           
            // 3 return the string
            return str;
        }
    }

    

   
}