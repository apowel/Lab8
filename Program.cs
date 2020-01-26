using System;
using System.Collections.Generic;
using System.Linq;

namespace Lab8
{
    class Program
    {
        static void Main(string[] args)
        {
            StudentDirectory studentDirectory = new StudentDirectory();
            bool again = true;
            Console.Write("Welcome to the class. ");
            while (again == true)
            {
                Console.WriteLine("Which student would you like to learn more about? " +
                    "(enter a number 1-" + studentDirectory.Directory.Count() + "): ");
                Student selectedStudent = SelectStudent(studentDirectory);
                Console.WriteLine("What would you like to know about this student? " +
                    @"enter ""hometown"", or ""favorite food"": ");
                SelectInfo(selectedStudent);
                again = Again();
            }

        }

        public static Student SelectStudent(StudentDirectory studentDirectory)
        {
            string idInput = Console.ReadLine().ToLower();
            int id;
            try
            {
                id = Int32.Parse(idInput);
            }
            catch(FormatException e)
            {
                Console.WriteLine(e.StackTrace);
                Console.WriteLine("enter a number 1-"+ studentDirectory.Directory.Count());
                return SelectStudent(studentDirectory);
            }
            catch(OverflowException e)
            {
                Console.WriteLine(e.StackTrace);
                Console.WriteLine(e.Message);
                Console.WriteLine("enter a number 1-" + studentDirectory.Directory.Count());
                return SelectStudent(studentDirectory);
            }

            try
            {
                Console.WriteLine("Student " + id + " is " + 
                studentDirectory.GetStudent(id).Name + ".");
                return studentDirectory.GetStudent(id);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
                Console.WriteLine("That's not a student, try again.");
                return SelectStudent(studentDirectory);
            }
        }
        public static void SelectInfo(Student selectedStudent)
        {
            string info;
            try
            {
                info = Console.ReadLine().ToLower().Trim();
                //This should never trigger an exception, but users figure out how to break stuff.
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine(e.StackTrace);
                Console.WriteLine("That input broke something.");
                throw;
            }
            
            if (info == "hometown")
            {
                Console.WriteLine(selectedStudent.Name + " is from " + 
                    selectedStudent.HomeTown + ".");
            }
            else if (info == "favorite food")
            {
                Console.WriteLine("The favorite food of " + selectedStudent.Name + 
                    " is " + selectedStudent.Food);
            }
            else
            {
                Console.WriteLine("Please try again.");
                SelectInfo(selectedStudent);
            }
        }
        public static bool Again()
        {
            Console.WriteLine("Would you like to know more about another student? " +
                @"(enter ""yes"" or ""no"")");
            string againInput = "";
            try
            {
                againInput = Console.ReadLine().ToLower().Trim();
                //This should never trigger an exception, but users figure out how to break stuff.
            }
            catch(Exception e)
            {
                Console.WriteLine(e.StackTrace);
                Console.WriteLine("That input broke something.");
                return Again();
            }
            if (againInput == "yes")
            {
                return true;
            }
            else if (againInput == "no")
            {
                return false;
            }
            else
            {
                return Again();
            }
        }

        public class Student
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string HomeTown { get; set; }
            public string Food { get; set; }
        }

        public class StudentDirectory
        {
            public List<Student> Directory = new List<Student>()
            {
                new Student() { Id = 1, Name = "Jake Collins", HomeTown = "Corona, CA", Food = "Sushi"},
                new Student() { Id = 2, Name = "Andrew Waltman", HomeTown = "Grand Rapids, MI", Food = "Burgers"},
                new Student() { Id = 3, Name = "Albert Ngoudjou", HomeTown = "Bafoussam", Food = "Lasagna"},
                new Student() { Id = 4, Name = "Tommy Waalkes", HomeTown = "Raleigh, NC", Food = "Chicken Curry"},
                new Student() { Id = 5, Name = "Austin Powel", HomeTown = "Blissfield, MI", Food = "Spaghetti"},
                new Student() { Id = 6, Name = "Dylan Rule", HomeTown = "Newport, NH", Food = "Poutine"},
            };
            public Student GetStudent(int id)
            {
                return Directory.FirstOrDefault(e => e.Id == id);
            }
        }
    }
}
