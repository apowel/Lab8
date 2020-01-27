using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;

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
                    "(enter a number 1-" + studentDirectory.directory.Count() + "): ");
                Student selectedStudent = SelectStudent(studentDirectory);
                Console.WriteLine("What would you like to know about this student? " +
                    @"enter ""hometown"", ""favorite food"", ""id"", or ""name"": ");
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
                Console.WriteLine("enter a number 1-"+ studentDirectory.directory.Count());
                return SelectStudent(studentDirectory);
            }
            catch(OverflowException e)
            {
                Console.WriteLine(e.StackTrace);
                Console.WriteLine(e.Message);
                Console.WriteLine("enter a number 1-" + studentDirectory.directory.Count());
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
            string info = "";
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
                SelectInfo(selectedStudent);
            }
            selectedStudent.GetInfo(info);
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
                Console.Clear();
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
            public string FavoriteFood { get; set; }

            //This method exists to allow for easier implementation of new Student properties.
            public void GetInfo(string info)
            {
                PropertyInfo[] properties = this.GetType().GetProperties();
                bool infoAgain = true;
                string infoNoSpace = Regex.Replace(info, @"\s+", "");
                foreach (var property in properties)
                {
                    string[] propertyName = property.ToString().Split();
                    if (propertyName[1].ToString().ToLower() == infoNoSpace)
                    {
                        Console.WriteLine("The " + info + " of " + this.Name + " is " +
                        this.GetType().GetProperty(propertyName[1]).GetValue(this, null).ToString());
                        infoAgain = false;
                    }
                }
                while(infoAgain)
                {
                    Console.WriteLine("That was not a valid input");
                    infoAgain = false;
                    SelectInfo(this);
                }
            }
        }


        public class StudentDirectory
        {
            public List<Student> directory = new List<Student>()
            {
                new Student() { Id = 1, Name = "Jake Collins", HomeTown = "Corona, CA", FavoriteFood = "Sushi"},
                new Student() { Id = 2, Name = "Andrew Waltman", HomeTown = "Grand Rapids, MI", FavoriteFood = "Burgers"},
                new Student() { Id = 3, Name = "Albert Ngoudjou", HomeTown = "Bafoussam", FavoriteFood = "Lasagna"},
                new Student() { Id = 4, Name = "Tommy Waalkes", HomeTown = "Raleigh, NC", FavoriteFood = "Chicken Curry"},
                new Student() { Id = 5, Name = "Austin Powel", HomeTown = "Blissfield, MI", FavoriteFood = "Spaghetti"},
                new Student() { Id = 6, Name = "Dylan Rule", HomeTown = "Newport, NH", FavoriteFood = "Poutine"},
            };
            public Student GetStudent(int id)
            {
                return directory.FirstOrDefault(e => e.Id == id);
            }
        }
    }
}
