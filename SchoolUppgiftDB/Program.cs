using MySql.Data.MySqlClient;
using Org.BouncyCastle.Utilities;
using System;
using System.Xml.Linq;

namespace SchoolUppgiftDB
{
    internal class Program
    {
        static void Main(string[] args)
        {


            //Iniitera variabeler deklaration
            string server = "LOCALHOST";
            string database = "school";
            string username = "root";
            string pass = "pebbles5";

            string strConn = $"SERVER={server};DATABASE={database};UID={username};PASSWORD={pass};";

            //Establera koppling till Databas
            MySqlConnection conn = new MySqlConnection(strConn);

            // ConsoleKeyInfo input;
            string input = "";
            //Meny
       while (input != "0") {

                Console.Clear();

                //Skriva ut en meny för användaren
                Console.WriteLine("Välj ditt val för DB funktion!");
                Console.WriteLine("------------------------------");

                Console.WriteLine();

                Console.WriteLine("1. Show students");
                Console.WriteLine("2. Show Instructors");
                Console.WriteLine("3. Show courses");

                Console.WriteLine();

                Console.WriteLine("4. Enroll a student to a course");

                Console.WriteLine();

                Console.WriteLine("5. Add a student");
                Console.WriteLine("6. Add an instructor");
                Console.WriteLine("7. Add a course");

                Console.WriteLine();

                Console.WriteLine("8. Delete a student");
                Console.WriteLine("9. Delete an instructor");
                Console.WriteLine("10. Delete a course");

                Console.WriteLine();

                Console.WriteLine("11. Show how many courses every student is enrolled in");
                Console.WriteLine("12. Show students and courses table");

                Console.WriteLine("0. Avsluta");

               
                input = Console.ReadLine();

                //Ta värdet till en SwitchCase
                switch (input)
                {
                    case "1":
                       
                        Console.Clear();
                        ShowStudents(conn);
                        break;
                    case "2":
                        Console.Clear();
                        ShowInstructors(conn);
                        break;
                    case "3":
                        Console.Clear();
                        ShowCourses(conn);
                        break;

                    case "4":
                        Console.Clear();
                        EnrollToCourse(conn);
                        break;
                    case "5":
                        Console.Clear();
                        AddStudent(conn);
                        break;
                    case "6":
                        Console.Clear();
                        AddInstructor(conn);
                        break;
                    case "7":
                        Console.Clear();
                        AddCourse(conn);
                        break;
                    case "8":
                        Console.Clear();
                        DeleteStudent(conn);
                        break;
                    case "9":
                        Console.Clear();
                        DeleteInstructor(conn);
                        break;
                    case "10":
                        Console.Clear();
                        DeleteCourse(conn);
                        break;
                    case "11":
                        Console.Clear();
                        ShowNumberOfCourses(conn);
                        break;
                    case "12":
                        Console.Clear();
                        ShowStudentsAndCourses(conn);
                        break;
                    case "0":
                        break;
                    default:
                        Console.WriteLine("Du har matat in ett felaktigt värde. (Press any key to continue...)");
                        Console.ReadKey();
                        break;
                       
                }


            } ;

        }

        public static void AddStudent(MySqlConnection conn)
        {
            //Hämta data från användare
            Console.Write("Mata in student förnamn: ");
            string first_name = Console.ReadLine();

            Console.Write("Mata in student efternamn: ");
            string last_name = Console.ReadLine();

            Console.Write("Mata in student email: ");
            string email = Console.ReadLine();

            // SQL Querry för INSERT
            string sqlQuerry = $"CALL add_student('{first_name}', '{last_name}', '{email}');";

            // Skapa MySQLCOmmand objekt
            conn.Open();
            MySqlCommand cmd = new MySqlCommand(sqlQuerry, conn);

            //Exekvera MySQLCommand
            cmd.ExecuteReader();

            //Stänga DB koppling
            conn.Close();
        }

        public static void ShowStudents(MySqlConnection conn)
        {
            // SQL Querry för INSERT
            string sqlQuerry = "CALL students_select();";

            // Skapa MySQLCOmmand objekt
            conn.Open();
            MySqlCommand cmd = new MySqlCommand(sqlQuerry, conn);

            //Exekvera MySQLCommand. Spara resultat i reader
            MySqlDataReader reader = cmd.ExecuteReader();

            //Tömma Persons list
            Students.student.Clear();

            Console.WriteLine("Student ID\tFirst name\tLast Name\t\tEmail");
            Console.WriteLine("-----------\t----------\t--------\t\t----");
            //While Loop för att skriva ut resultatet till Konsol
            while (reader.Read())
            {
                //Skriv ut person till Konsol
                Console.WriteLine($"{reader["student_id"]}\t\t{reader["first_name"]}\t\t{reader["last_name"]}\t\t{reader["email"]}");

                //Spara data till Lista
                new Students(Convert.ToInt32(reader["student_id"]), reader["first_name"].ToString(), reader["last_name"].ToString());
            }

            //Stänga DB koppling
            conn.Close();

            Console.WriteLine("Data Fetched successfully! Press any key to continue");
            Console.ReadKey();
        }

        public static void DeleteStudent(MySqlConnection conn)
        {
            //Om ingen data har hämtats, hämta data
            if (Students.student.Count == 0)
            {
                Console.WriteLine("Here Are students to choose from. Press Any key to continue to the deleteing Prosses!");
                Console.WriteLine();
                ShowStudents(conn);
                Console.Clear();
            }

            int count = 1;

            foreach (Students student in Students.student)
            {
                Console.WriteLine($"{count}. {student.first_name} - {student.last_name} ");
                count++;
            }


            Console.WriteLine("Välja student som du vill radera?");
            int input = Convert.ToInt32(Console.ReadLine());

            int SelectID = Students.student[input - 1].id;

            string sqlQuerry = $"Call delete_student({SelectID});";

            // Skapa MySQLCommand obejkt
            conn.Open();
            MySqlCommand cmd = new MySqlCommand(sqlQuerry, conn);

            //EXEKVERA MySQLCommand
            cmd.ExecuteReader();

            //Sträng DB koppling
            conn.Close();

            Console.Clear();
            ShowStudents(conn);

        }


        public static void AddInstructor(MySqlConnection conn)
        {
            //Hämta data från användare
            Console.Write("Mata in instructor namn: ");
            string name = Console.ReadLine();



            // SQL Querry för INSERT
            string sqlQuerry = $"CALL add_instructor('{name}');";

            // Skapa MySQLCOmmand objekt
            conn.Open();
            MySqlCommand cmd = new MySqlCommand(sqlQuerry, conn);

            //Exekvera MySQLCommand
            cmd.ExecuteReader();

            //Stänga DB koppling
            conn.Close();
        }

        public static void ShowInstructors(MySqlConnection conn)
        {

            string sqlQuerry = "CALL instructors_select();";

           
            conn.Open();
            MySqlCommand cmd = new MySqlCommand(sqlQuerry, conn);

           
            MySqlDataReader reader = cmd.ExecuteReader();

          
            Instructors.instructor.Clear();

         
           
            while (reader.Read())
            {

                Console.WriteLine($" {reader["instructor_id"]}   -  " +
                    $"  {reader["name"]}");


                new Instructors(Convert.ToInt32(reader["instructor_id"]), reader["name"].ToString());
            }


            conn.Close();

            Console.WriteLine("Data Fetched successfully! Press any key to continue");
            Console.ReadKey();
        }

        public static void DeleteInstructor(MySqlConnection conn)
        {
            //Om ingen data har hämtats, hämta data
            if (Instructors.instructor.Count == 0)
            {
                Console.WriteLine("Here Are Instructors to choose from. Press Any key to continue to the deleteing Prosses!");
                ShowInstructors(conn);
                Console.Clear();
            }

            int count = 1;

            foreach (Instructors student in Instructors.instructor)
            {
                Console.WriteLine($"{count}. {student.name}");
                count++;
            }


            Console.WriteLine("Välja 'instructor' som du vill radera?");
            int input = Convert.ToInt32(Console.ReadLine());

            int SelectID = Instructors.instructor[input - 1].id;

            string sqlQuerry = $"Call delete_instructor({SelectID});";

            conn.Open();
            MySqlCommand cmd = new MySqlCommand(sqlQuerry, conn);

            cmd.ExecuteReader();
 
            conn.Close();

            Console.Clear();
            ShowInstructors(conn);

        }

        public static void EnrollToCourse(MySqlConnection conn)
        {
            if (Courses.course.Count == 0)
            {
                Console.WriteLine("Here Are Courses to choose from. Press Any key to continue to the enrollment Prosses!");
                Console.WriteLine();
                ShowCourses(conn);
                Console.Clear();
            }

            int count1 = 1;

            foreach (Courses course in Courses.course)
            {
                Console.WriteLine($"{count1}. {course.title}");
                count1++;
            }

            Console.Write("Mata in Course ID : ");
            int course_id = int.Parse(Console.ReadLine());

            int count2 = 1;

            if (Students.student.Count == 0)
            {
                Console.WriteLine("Here Are Students to choose from. Press Any key to complete the enrollment Prosses!");
                Console.WriteLine();
                ShowStudents(conn);
                Console.Clear();
            }

            foreach (Students student in Students.student)
            {
                Console.WriteLine($"{count2}. {student.first_name} - {student.last_name} ");
                count2++;
            }
       

            Console.Write("Mata in Student_ID : ");
            int student_id = int.Parse(Console.ReadLine());

            Console.Write("Mata in kurs pris : ");
            decimal price = decimal.Parse(Console.ReadLine());

            string sqlQuerry = $"CALL enroll_to_course('{course_id}', '{student_id}', '{price}');";

            conn.Open();
            MySqlCommand cmd = new MySqlCommand(sqlQuerry, conn);

            cmd.ExecuteReader();

            conn.Close();
        }

        public static void ShowCourses(MySqlConnection conn)
        {

            string sqlQuerry = "CALL courses_select();";


            conn.Open();
            MySqlCommand cmd = new MySqlCommand(sqlQuerry, conn);


            MySqlDataReader reader = cmd.ExecuteReader();


            Courses.course.Clear();



            while (reader.Read())
            {

                Console.WriteLine($" {reader["course_id"]}   -  " +
                    $"  {reader["title"]}" + $"  {reader["price"]}");


                new Courses(Convert.ToInt32(reader["course_id"]), reader["title"].ToString(), Convert.ToDecimal(reader["course_id"]));
            }


            conn.Close();

            Console.WriteLine("Data Fetched successfully! Press any key to continue");
            Console.ReadKey();
        }

        public static void AddCourse(MySqlConnection conn)
        {
            //Hämta data från användare
            Console.Write("Mata in kurs titel: ");
            string title = Console.ReadLine();

            Console.Write("Mata in kurs pris: ");
            decimal price = decimal.Parse(Console.ReadLine());

            Console.Write("Mata in instructor ID: ");
            int instructor_id = int.Parse(Console.ReadLine());


            string sqlQuerry = $"CALL add_course('{title}', {price}, {instructor_id});";

            // Skapa MySQLCOmmand objekt
            conn.Open();
            MySqlCommand cmd = new MySqlCommand(sqlQuerry, conn);

            //Exekvera MySQLCommand
            cmd.ExecuteReader();

            //Stänga DB koppling
            conn.Close();
        }

        public static void DeleteCourse(MySqlConnection conn)
        {
            //Om ingen data har hämtats, hämta data
            if (Courses.course.Count == 0)
            {
                Console.WriteLine("Here Are Courses to choose from. Press Any key to continue to the deleting Prosses!");
                Console.WriteLine();
                ShowCourses(conn);
                Console.Clear();
            }

            int count = 1;

            foreach (Courses course in Courses.course)
            {
                Console.WriteLine($"{count}. {course.title}");
                count++;
            }


            Console.WriteLine("Välja kurs som du vill radera?");
            int input = Convert.ToInt32(Console.ReadLine());

            int SelectID = Courses.course[input - 1].id;

            string sqlQuerry = $"Call delete_course({SelectID});";

            conn.Open();
            MySqlCommand cmd = new MySqlCommand(sqlQuerry, conn);

            cmd.ExecuteReader();

            conn.Close();

            Console.Clear();
            ShowInstructors(conn);

        }

        public static void ShowNumberOfCourses(MySqlConnection conn)
        {

            string sqlQuerry = "SELECT * FROM school.number_of_courses;";


            conn.Open();
            MySqlCommand cmd = new MySqlCommand(sqlQuerry, conn);


            MySqlDataReader reader = cmd.ExecuteReader();



            while (reader.Read())
            {

                Console.WriteLine($" {reader["first_name"]} " +
                    $"  {reader["last_name"]}" + $" :  Has enrolled  {reader["number"]} courses");

            }


            conn.Close();

            Console.WriteLine("Data Fetched successfully! Press any key to continue");
            Console.ReadKey();
        }

        public static void ShowStudentsAndCourses(MySqlConnection conn)
        {

            string sqlQuerry = "SELECT * FROM school.list_of_students_and_courses;";


            conn.Open();
            MySqlCommand cmd = new MySqlCommand(sqlQuerry, conn);


            MySqlDataReader reader = cmd.ExecuteReader();



            while (reader.Read())
            {

                Console.WriteLine($" {reader["first_name"]} " +
                    $"  {reader["last_name"]}" + $" --  {reader["course_id"]} ");

            }


            conn.Close();

            Console.WriteLine("Data Fetched successfully! Press any key to continue");
            Console.ReadKey();
        }

    }
}