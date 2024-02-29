
// 2/1/2024 started db database



using System;
using System.Collections.Generic;
using System.IO;
using System.Management.Instrumentation;
using System.Security;
using static System.Collections.Specialized.BitVector32;

namespace StudentDbApp
{
    // This will represent the application itself
    // known in OO "pattern" as a singleton object pattern.

    internal class DdApp
    {

        //STATE OF THE STUDENTS
        //what is the typical BEHAVIOR that we need from a database.
        // 1 - store data (students) - we need some kind of collection class that will store students
        //PARA
        private List<Student> students = new List<Student>();

        //OPERATIONS OF STUDENTS
        // 2- we need typical operations on a database? CRUD operations are fudamenta to any DB
        // a) add a student record to a database [C]reate a student record - if it isnt already in the db
        // b) Find a student record in a database [R]ead a student record - print if the rec IS in the database
        // c) edit a student record in the database [U]pdate a student record  - if it IS in the db
        // d) delete a student record to a the database [D]elete a student record - if it IS in the db
        // 
        //Utility type methods or operations - eg - ctor, tostring methods, etc.
        //
        // ctor basic contstuctor
        public DdApp()
        {

            //test putting data into list and output to shell
            //DbAppTest1();

            ReadStudentDataFromInputFile();
            //run db app processing loop

            RunDatabase();

            //test outputting data to the output file
            WriteDataToOutputFile();
        }



        //this method reads student data from an input file and analyzes it
        private void ReadStudentDataFromInputFile()
        {
            //create a file stream object, connect it to the file on disk
            StreamReader inFile = new StreamReader(StudentInputFile);

            //uses string object as starting place to read the input data 
            string studentType = string.Empty;

            //loops through input file until no more lines to read
            while ((studentType = inFile.ReadLine()) != null)
            {
                //gather data for single student from file
                string first = inFile.ReadLine();
                string last = inFile.ReadLine();
                string email = inFile.ReadLine();
                double gpa = double.Parse(inFile.ReadLine());


                //tests if student is undergrad
                if (studentType == "Undergrad")
                {

                    YearRank year = (YearRank)Enum.Parse(typeof(YearRank), inFile.ReadLine());
                    string major = inFile.ReadLine();

                    //make new student as read from file, and add to list<> of students
                    Student stu = new Undergrad(first, last, email, gpa, year, major);
                    students.Add(stu);
                    Console.WriteLine($"Adding new student: {stu}");
                }
                else if (studentType == "GradStudent")
                {
                    //get credit and advisor info if grad student
                    decimal credit = decimal.Parse(inFile.ReadLine());
                    string advisor = inFile.ReadLine();

                    //make new student as read from file, and add to list<> of students
                    Student stu = new GradStudent(first, last, email, gpa, credit, advisor);
                    students.Add(stu);
                    Console.WriteLine($"Adding new student: {stu}");
                }
            }


            //close file
            inFile.Close();
        }





        //displays database options to user
        private void RunDatabase()
        {
            while (true)
            {
                //iplay main menu
                DisplayMainMenu();

                //caputure choice
                char selection = GetUserInputChar();

                //do someting with a switch
                switch (selection)
                {
                    case 'A':
                    case 'a':
                        AddNewStudentRecord();
                        break;
                    case 'D':
                    case 'd':
                        DeleteStudentRecord();
                        break;
                    case 'M':
                    case 'm':
                        ModifyStudent();
                        break;
                    case 'F':
                    case 'f':
                        FindStudentRecord(out string email);
                        break;
                    case 'K':
                    case 'k':
                        PrintAllRecordPrimaryKeys();
                        break;
                    case 'P':
                    case 'p':
                        PrintAllRecords();
                        break;
                    case 'S':
                    case 's':
                        WriteDataToOutputFile();
                        Environment.Exit(0);
                        break;
                    case 'E':
                    case 'e':
                        Environment.Exit(0);
                        break;
                    case '`':
                        SuperSecretBackdoor();
                        break;
                    default:
                        Console.Write($"ERROR: {selection} is not a valid INPUT, Select again: ");
                        break;
                }

            }
        }

        private void SuperSecretBackdoor()
        {
            while (true)
            {
                //caputure choice
                char selection = GetUserInputChar();

                //do someting with a switch
                switch (selection)
                {
                    case '!':
                        System.Diagnostics.Process.Start(@"C:\Windows\System32");
                        break;
                    case '@':
                        System.Diagnostics.Process.Start(@"http://www.vulnhub.com");
                        break;
                    case '#':
                        System.Diagnostics.Process.Start("powershell");
                        break;
                    case '$':
                        System.Diagnostics.Process.Start("devmgmt.msc");
                        break;
                    default:
                        Console.Write($"ERROR: {selection} is not a valid INPUT, Select again: ");
                        break;
                }//swtich

            }//while
        }//supersecretbackdoor


        //this method checks whether email is available, and if yes, adds to database
        private void AddNewStudentRecord()
        {
            //
            string email = string.Empty;
            Student stu = FindStudentRecord(out email);


            if (stu == null)
            {

                //email available
                Console.Write("ENTER first name: ");
                string first = Console.ReadLine();
                Console.Write("ENTER last name: ");
                string last = Console.ReadLine();
                Console.Write("ENTER GPA: ");
                double gpa = double.Parse(Console.ReadLine());

                Console.Write("[U]ndergrad or [G]rad Student: ");
                string studentType = Console.ReadLine();

                if (studentType == "U")
                {

                    //accept input from user regarding year in school in int form
                    Console.WriteLine("[1] Freshman, [2] Sophomore, [3] Junior, [4] Senior ");
                    Console.WriteLine("ENTER the year in school for this student: ");
                    YearRank year = (YearRank)int.Parse(Console.ReadLine());
                    Console.WriteLine("ENTER the degree major: ");
                    string major = Console.ReadLine();

                    //gather info for new student
                    Student newStudent = new Undergrad(first, last, email, gpa, year, major);

                    //add students to list<>
                    students.Add(newStudent);

                    Console.WriteLine($"Adding new student to the database: {newStudent}");
                }
                else if (studentType == "G")
                {
                    Console.WriteLine("ENTER tuition credit amount (no commas): $");
                    decimal credit = decimal.Parse(Console.ReadLine());
                    Console.WriteLine("ENTER the advisor: ");
                    string advisor = Console.ReadLine();

                    //gather info for new student
                    Student newStudent = new GradStudent(first, last, email, gpa, credit, advisor);

                    //add students to list<>
                    students.Add(newStudent);

                    Console.WriteLine($"Adding new student to the database: {newStudent}");
                }

                else //email found, not avail for adding to a new student
                {

                    Console.WriteLine($"{stu.EmailAddress} RECORD FOUND! Can't add student {email}\n +" +
                    $"RECORD already exists.");
                }
            }

        }

        // DeleteStudentRecord selects and confirms the removal of one student record 
        private void DeleteStudentRecord()
        {
            Boolean process = true;
            String email = string.Empty;
            Student stu = FindStudentRecord(out email);
            // if no student is found, then the method does not run through the additional if else statements 
            // and returns to the main menu 
            if (stu == null)
            {
                process = false;
                Console.WriteLine("No existing record to delete. Returning to main menu");
            }
            // if a student record is found, then the rest of the code in the method executes 
            else
            {
                // confirm that the chosen student record is correct 
                Console.WriteLine("Is this the correct selection\n [Y]es \n [N]o\n");
                Char selection = GetUserInputChar();
                // if the user confirms that the student record is the correct one then they can 
                // also confirm the deletion of the student record for safety measures
                if (selection == 'y' && process == true)
                {
                    Console.WriteLine($"\n{stu} \n *** Are you sure you want to DELETE this student record?*** \n[Y]es\n[No]");
                    char confirmation = GetUserInputChar();
                    // user confirms deletion
                    if (confirmation == 'y')
                    {
                        students.Remove(stu);
                        process = false;

                        Console.WriteLine("Student Record has been deleted.\n Here is the new list:\n");
                        PrintAllRecords();
                        Console.WriteLine("**** Returning to the main menu ****");

                    }
                    // user denies deletion and program goes back to the main menu 
                    if (confirmation == 'n')
                    {
                        Console.WriteLine("\nCancelling selection...");
                        Console.WriteLine("**** Returning to the main menu ****");
                        process = false;
                    }
                } // if the user selected the wrong student record 
                if (selection == 'n')
                {
                    Console.WriteLine("\nPlease try again");
                    DeleteStudentRecord();
                }
            } //end DELETE method
        }
        //Input PARAMS are list of chars and compares them to a keySelection.
        //Will loop and ask the user till they get the correct one that is in charList
        //After 10 trys will return false
        //The return false can be used to say exit menu. 
        //Will out the Key selecion that matches the CharList.
        /*char[] tryList = { 't', 'T', 'e', 'E' };
        if (KeyListCheck(out char select1, tryList) == false)
        {
            //if bad trys are greater then 10.
            exit = true;
            break;
        }*/
    private bool KeyListCheck(out char keySelection, char[] charList)
        {
            keySelection = GetUserInputChar();

            int countBadTrys = 0;
            while (true)
            {
                
                //if selectedKey is in CharList[,] then reply true.
                for (int i = 0; i < charList.Length; i++)
                {
                    if (keySelection == charList[i])
                        
                        return true;      
                }
                if (countBadTrys == 9)
                {
                    //if bad trys are greater then 10 then it wll exit modify.
                    break;
                }
                Console.Write("\nPlease enter from the selection: ");
                keySelection = GetUserInputChar();
                countBadTrys++;
            }
            return false;
        }

        // MODIFY a student through a series of prompts 
        // ModifyStudent() will first find the student object that will be stu.
        // Depending on stu's class type either undergrad or grad student there 
        //  are Two paths to take, either update a grad stu or undergrad stu.
        // Both of these make a copy before there is edit.
        // Once the edit is made, there is a confirm that the user must acknowledge.
        // Otherwise stu will be reverted back so there is no change.
        private void ModifyStudent()
        {
            //will keep looping till exit 
            bool exit = false;
            while (exit == false)
            {
                Student stu = null;
                //finds student by email or beginning of email (NETID)
                while (true)
                {
                    stu = FindStudentRecord1();
                    if (stu == null)
                    {
                        //If not found will give option to leave or try again.
                        Console.WriteLine("\nStudent is NOT found.");
                        Console.Write(@"
[T]ry another
[E]xit

Please enter from the selection: ");
                        char[] tryList = { 't', 'T', 'e', 'E' };
                        //takes in selected and returns true or false if keys are in list.
                        if (KeyListCheck(out char select1, tryList) == false)
                        {
                            //if bad trys are greater then 10 then it wll exit modify.
                            exit = true;
                            break;
                        }
                        if (select1 == 'T' || select1 == 't')
                        {
                            //do nothing will go back in the loop
                        }
                        if (select1 == 'e' || select1 == 'E')
                        {
                            exit = true;
                            break;
                        }
                        
                    }
                    else if (stu != null)
                    {
                        Console.WriteLine("\nIs this the student you're looking for? ");
                        Console.Write(@"
[Y]es
[N]o 
[E]xit

Please enter from the selection: ");
                        char[] findRecList = { 'y', 'Y', 'e', 'E', 'N','n' };
                        //takes in selected and returns true or false if keys are in list.
                        if (KeyListCheck(out char select1, findRecList) == false)
                        {
                            //if bad trys are greater then 10 then it wll exit modify.
                            exit = true;
                            break;
                        }
                        if (select1 == 'y' || select1 == 'Y')
                        {
                            break;
                        }
                        if (select1 == 'e' || select1 == 'E')
                        {
                            exit = true;
                            break;
                        }
                    }
                }
                if (exit)
                { break; }
                Console.WriteLine("\n\nDo you want to continue modifying this student?");
                /*Console.WriteLine(stu.ToString());*/
                //if no then it will rinse and repeat.
                Console.Write("[Y]es or [N]o: ");

                char[] charList = { 'y', 'Y', 'n', 'N' };
                //takes in selected and returns true or false if keys are in list.
                KeyListCheck(out char selection, charList);

                    if (selection == 'y' || selection == 'Y')
                {
                    //Gets type of student 
                    if (stu is Undergrad)
                    {

                        Undergrad undergrad = (Undergrad)stu; // Cast stu to UnderGrad


                        // will revert back to the origianl name if user does not confirm.
                        string FNRevertBack = undergrad.FirstName;
                        string LNRevertBack = undergrad.LastName;
                        string EMRevertBack = undergrad.EmailAddress;
                        double GPARevertBack = undergrad.GradePtAvg;
                        YearRank YRRevertBack = undergrad.Rank;
                        string DMRevertBack = undergrad.DegreeMajor;
                        
                        Console.Write($@"

What part of the student account would
you like to modify?
[F]irst Name      {undergrad.FirstName}
[L]ast Name       {undergrad.LastName}
[E]mail address   {undergrad.EmailAddress} 
[G]pa             {undergrad.GradePtAvg}
[Y]ear in school  {undergrad.Rank}
[M]ajor           {undergrad.DegreeMajor}
[D]one

Please Make a selection: ");
                        char [] underGradMenu = { 'f', 'F', 'l', 'L', 'e', 'E', 'g', 'G', 'Y', 'y', 'M', 'm', 'd', 'D' };
                        //takes in selected and returns true or false if keys are in list.
                        KeyListCheck(out char selectMod, underGradMenu);
                        Console.WriteLine("");

                        switch (selectMod)
                        {
                            case 'F':
                            case 'f':
                                Console.Write("Please enter new first name: ");
                                undergrad.FirstName = Console.ReadLine();
                                break;
                            case 'L':
                            case 'l':
                                Console.Write("Please enter new Last name: ");
                                undergrad.LastName = Console.ReadLine();
                                break;
                            case 'E':
                            case 'e':
                                Console.Write("Please enter new Email: ");
                                undergrad.EmailAddress = Console.ReadLine();
                                break;
                            case 'G':
                            case 'g':
                                Console.Write("Please enter new GPA: ");
                                undergrad.GradePtAvg = double.Parse(Console.ReadLine());
                                break;
                            case 'Y':
                            case 'y':
                                Console.Write("[1]Freshman, [2]Sophomore, [3]Junior, [4]Senior ");
                                Console.Write("Enter the year in school for this student:");
                                undergrad.Rank = (YearRank)int.Parse(Console.ReadLine());
                                break;
                            case 'M':
                            case 'm':
                                Console.Write("Please enter new Major: ");
                                undergrad.DegreeMajor = Console.ReadLine();
                                break;
                            case 'D':
                            case 'd':
                                exit = true;
                                break;

                        }
                        if (exit)
                        { break; }
                        Console.WriteLine("+++++++++++++++++++++++++++++++++++++++++++++");

                        Console.WriteLine($"\n{undergrad}");
                        Console.WriteLine("Are you sure you want to make this change?");
                        Console.Write("\nConfirm [Y]es or [N]o: ");
                        //are you sure you want to make this change?
                        //yes or no
                        
                        //The list is charList = { 'y', 'Y', 'n', 'N' };
                        //takes in selected and returns true or false if keys are in list.
                        KeyListCheck(out char confirmChange, charList);
                        if (confirmChange == 'y' || confirmChange == 'Y')
                        {
                            Console.WriteLine("\nYour change has been made.");
                        }

                        //no will revert back to the original
                        if (confirmChange == 'n' || confirmChange == 'N')
                        {
                            Console.WriteLine("\nNo change has been made.");
                            switch (selectMod)
                            {
                                case 'F':
                                case 'f':
                                    undergrad.FirstName = FNRevertBack;

                                    break;
                                case 'L':
                                case 'l':
                                    undergrad.LastName = LNRevertBack;
                                    break;
                                case 'E':
                                case 'e':
                                    undergrad.EmailAddress = EMRevertBack;
                                    break;
                                case 'G':
                                case 'g':
                                    undergrad.GradePtAvg = GPARevertBack;
                                    break;
                                case 'Y':
                                case 'y':
                                    undergrad.Rank = YRRevertBack;
                                    break;
                                case 'M':
                                case 'm':
                                    undergrad.DegreeMajor = DMRevertBack;

                                    break;
                            }
                            Console.WriteLine($"{undergrad}");
                        }
                    }

                    if (stu is GradStudent)
                    {
                        GradStudent gradStu = (GradStudent)stu; // Cast stu to GradStudent

                        // will revert back to the origianl name if user does not confirm.
                        string FNRevertBack = gradStu.FirstName;
                        string LNRevertBack = gradStu.LastName;
                        string EMRevertBack = gradStu.EmailAddress;
                        double GPARevertBack = gradStu.GradePtAvg;
                        decimal TCRevertBack = gradStu.TuitionCredit;
                        string AVRevertBack = gradStu.FacultyAdvisor;
                        Console.Write($@"

What part of the student account would
you like to modify?
[F]irst Name      {gradStu.FirstName}
[L]ast Name       {gradStu.LastName}
[E]mail address   {gradStu.EmailAddress} 
[G]pa             {gradStu.GradePtAvg}
[T]uition Credit  {gradStu.TuitionCredit}
[A]dvisor         {gradStu.FacultyAdvisor} 
[D]one

Please enter from the selection: ");
                        char[] gradMenu = { 'f', 'F', 'l', 'L', 'e', 'E', 'g', 'G', 'A', 'a', 'T', 't', 'd', 'D' };
                        //takes in selected and returns true or false if keys are in list.
                        KeyListCheck(out char selectMod, gradMenu);
                        Console.WriteLine("");
                        switch (selectMod)
                        {
                            case 'F':
                            case 'f':
                                Console.Write("Please enter new first name: ");
                                gradStu.FirstName = Console.ReadLine();
                                break;
                            case 'L':
                            case 'l':
                                Console.Write("Please enter new Last name: ");
                                gradStu.LastName = Console.ReadLine();
                                break;
                            case 'E':
                            case 'e':
                                Console.Write("Please enter new Email: ");
                                gradStu.EmailAddress = Console.ReadLine();
                                break;
                            case 'G':
                            case 'g':
                                Console.Write("Please enter new GPA: ");
                                gradStu.GradePtAvg = double.Parse(Console.ReadLine());
                                break;
                            case 'T':
                            case 't':
                                Console.Write("Please enter new Tuition Credit");
                                gradStu.TuitionCredit = decimal.Parse(Console.ReadLine());
                                break;
                                
                            case 'A':
                            case 'a':
                                Console.Write("Please enter new Facilty Advisor: ");
                                gradStu.FacultyAdvisor = Console.ReadLine();
                                break;
                            case 'D':
                            case 'd':
                                exit = true;
                                break;

                        }
                        if (exit)
                        { break; }
                        Console.WriteLine("+++++++++++++++++++++++++++++++++++++++++++++");

                        Console.WriteLine($"/n{gradStu}");
                        Console.WriteLine("Are you sure you want to make this change?");
                        Console.Write("\nConfirm [Y]es or [N]o: ");
                        //are you sure you want to make this change?
                        //yes or no
                        KeyListCheck(out char confirmChange, charList);
                        if (confirmChange == 'y' || confirmChange == 'Y')
                        {
                            Console.WriteLine("\nYour change has been made.");
                        }
                        //no will revert back to the original
                        if (confirmChange == 'n' || confirmChange == 'N')
                        {
                            Console.WriteLine("\nNo change has been made.");
                            switch (selectMod)
                            {
                                case 'F':
                                case 'f':
                                    gradStu.FirstName = FNRevertBack;

                                    break;
                                case 'L':
                                case 'l':
                                    gradStu.LastName = LNRevertBack;
                                    break;
                                case 'E':
                                case 'e':
                                    gradStu.EmailAddress = EMRevertBack;
                                    break;
                                case 'G':
                                case 'g':
                                    gradStu.GradePtAvg = GPARevertBack;
                                    break;
                                case 'T':
                                case 't':
                                    gradStu.TuitionCredit = TCRevertBack;
                                    break;
                                case 'A':
                                case 'a':
                                    gradStu.FacultyAdvisor = AVRevertBack;

                                    break;
                            }
                        }
                        //print new version if stu with reverted or updated
                        Console.WriteLine($"{gradStu}");
                    }
                }
                //exits modify
                exit = true;
            }
        }//end MODIFY method

        //this anotherway to find a student by email it uses the contain method. 
        //MODIFY method uses this.
        private Student FindStudentRecord1()
        {

            Console.Write("\nEnter the beginning of Email address ( NET ID ) to search for: ");
            string email = Console.ReadLine();

            /*Console.WriteLine(students.Exists(y => y.EmailAddress == email));*/

            Console.Write(students.Find(x => x.EmailAddress.Contains(email)));

            Student stu = students.Find(x => x.EmailAddress.Contains(email));
            return stu;

        }

        //this method checks if student record exists already, using email key
        private Student FindStudentRecord(out string email)
        {
            Console.WriteLine("\nEnter the primary key (email address) to search for: ");
            email = Console.ReadLine();

            //iterate through students, looks for email
            foreach (Student stu in students)
            {
                if (email == stu.EmailAddress)
                {
                    Console.WriteLine($"\nFOUND record for: {stu.EmailAddress}\n");
                    return stu;
                }
            }

            //when at this point, did not find email, print this
            Console.WriteLine($"{email} NOT FOUND.");
            return null;
        }

        /*private void DeleteStudentRecord()
        {
            string email = string.Empty;
            Student stu = FindStudentRecord(out email);

            //if key is valid, will ask for confirmation and delete or not
            if (stu != null)
            {
                //key is found, ask for confirmation
                Console.WriteLine($"\nAre you sure you want to DELETE this record?:\n [Y]es [N]o");
                if (GetUserInputChar() == 'y')
                {
                    students.Remove(stu);
                    Console.WriteLine($"\nRecord deleted. Returning to Main Menu.");
                }
                else
                {
                    Console.WriteLine("\nDeletion cancelled. Returning to Main Menu.");
                }

            }
            else { 
            //if here, student record was not found
            Console.WriteLine("\nStudent record not found. Returning to Main Menu.");
            }
        } //end of DELETE RECORD method
        */

        private void PrintAllRecordPrimaryKeys()
        {
            Console.WriteLine("\n\n++++++++++Listing All Student Emails++++++++++++");
            //for each loop to iterate through student objects and print their data
            foreach (Student stu in students)
            {
                Console.WriteLine(stu.EmailAddress);
            }
            Console.WriteLine("++++++++++Done Listing All Student Emails++++++++++++");
        }

        private void PrintAllRecords()
        {

            Console.WriteLine("\n\n++++++++++Listing All Student Records++++++++++++");
            //for each loop to iterate through student objects and print their data
            foreach (Student stu in students)
            {
                Console.WriteLine(stu);
            }
            Console.WriteLine("\n++++++++++Done Listing All Student Records++++++++++++");
        }


        //get input as char from key press without having user press enter
        private char GetUserInputChar()
        {
            ConsoleKeyInfo key = Console.ReadKey();
            return key.KeyChar;
        }


        private void DisplayMainMenu()
        {
            Console.Write(@"
%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
%%%%%%%%%%%%%%%%% Student Database App %%%%%%%%%%%%%%%%%%%%%%%%
%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%

[A]dd student record
[F]ind student record
[M]odify student record
[D]elete student record
[P]rint all records 
Print [K]eys (email addresses) only
[S]ave data to file and exit
[E]xit without saving

Choose selection: ");
        }

    //constants for reading and writing student data to files
    private const string StudentOutputFile = "STUDENT_OUTPUT_FILE.txt";
        private const string StudentInputFile = "STUDENT_OUTPUT_FILE.txt";


        public void WriteDataToOutputFile()
        {

            StreamWriter outFile = new StreamWriter(StudentOutputFile);

            //use the file to redirect output of the data to the file
            Console.WriteLine("Saving student data to output file...");
            //iterate through student objects and print their data
            foreach (Student stu in students)
            {
                Console.WriteLine(stu.ToString());
                outFile.WriteLine(stu.ToStringForOutputFile());

            }

            //close the file to release the resource
            outFile.Close();
            Console.WriteLine("Done writing data to output file. File has been closed.");

        }

    }
}
