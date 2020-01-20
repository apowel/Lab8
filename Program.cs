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
                Console.WriteLine("Which student would you like to learn more about? (enter a number 1-20): ");
                Student selectedStudent = SelectStudent(studentDirectory);
                Console.WriteLine("Student " + selectedStudent.id + " is" + selectedStudent.name + ".");
                Console.WriteLine(@"What would you like to know about this student? enter ""hometown"", or ""favorite food"": ");
                SelectInfo(selectedStudent);
                again = Again();
            }

        }

        public static Student SelectStudent(StudentDirectory studentDirectory)
        {
            if (Int32.TryParse(Console.ReadLine(), out int idInput) && idInput > 0 && idInput < 21)
            { 
                return studentDirectory.GetStudent(idInput);
            }
            else
            { 
                Console.WriteLine("That student does not exist.  Please try again. (enter a number 1-20): ");
                return SelectStudent(studentDirectory);
            }
        }
        public static void SelectInfo(Student selectedStudent)
        {
            string info = Console.ReadLine().ToLower().Trim();
            if (info == "hometown")
            {
                Console.WriteLine(selectedStudent.name + " is from " + selectedStudent.homeTown + ".");
            }
            else if (info == "favorite food")
            {
                Console.WriteLine("The favorite food of " + selectedStudent.name + " is " + selectedStudent.food);
            }
            else
            {
                Console.WriteLine("Please try again.");
                SelectInfo(selectedStudent);
            }
        }
        public static bool Again()
        {
            Console.WriteLine(@"Would you like to know more about another student? (enter ""yes"" or ""no"")");
            string againInput = Console.ReadLine().ToLower().Trim();
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
            public int id { get; set; }
            public string name { get; set; }
            public string homeTown { get; set; }
            public string food { get; set; }
        }

        public class StudentDirectory
        {
            public List<Student> studentDirectory = new List<Student>()
            {
                new Student() { id = 1, name = "Name1", homeTown = "City A", food = "Apple"},
                new Student() { id = 2, name = "Name2", homeTown = "City B", food = "Banana"},
                new Student() { id = 3, name = "Name3", homeTown = "City C", food = "Fish"},
                new Student() { id = 4, name = "Name4", homeTown = "City D", food = "Sushi"},
                new Student() { id = 5, name = "Name5", homeTown = "City E", food = "Hamburgers"},
                new Student() { id = 6, name = "Name6", homeTown = "City F", food = "Spaghetti"},
                new Student() { id = 7, name = "Name7", homeTown = "City G", food = "Beans"},
                new Student() { id = 8, name = "Name8", homeTown = "City H", food = "Taco Bell"},
                new Student() { id = 9, name = "Name9", homeTown = "City I", food = "Bread"},
                new Student() { id = 10, name = "Name10", homeTown = "City J", food = "BBQ"},
                new Student() { id = 11, name = "Name11", homeTown = "City A", food = "Steak"},
                new Student() { id = 12, name = "Name12", homeTown = "City B", food = "Pho"},
                new Student() { id = 13, name = "Name13", homeTown = "City C", food = "Soup"},
                new Student() { id = 14, name = "Name14", homeTown = "City D", food = "Pizza"},
                new Student() { id = 15, name = "Name15", homeTown = "City E", food = "Tacos"},
                new Student() { id = 16, name = "Name16", homeTown = "City F", food = "Dumplings"},
                new Student() { id = 17, name = "Name17", homeTown = "City G", food = "PB&J"},
                new Student() { id = 18, name = "Name18", homeTown = "City H", food = "Blueberries"},
                new Student() { id = 19, name = "Name19", homeTown = "City I", food = "Apple"},
                new Student() { id = 20, name = "Name20", homeTown = "City J", food = "Chicken"},
            };
            public Student GetStudent(int id)
            {
                return studentDirectory.FirstOrDefault(e => e.id == id);
            }
        }
    }
}
