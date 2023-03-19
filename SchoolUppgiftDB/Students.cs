using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolUppgiftDB
{
    internal class Students
    {
        //Attribut
        public int id;
        public string first_name;
        public string last_name;

        //Statisk lista
        public static List<Students> student = new List<Students>();

        //Konstruktor
        public Students(int id, string first_name, string last_name)
        {
            this.id = id;
            this.first_name = first_name;
            this.last_name = last_name;

            //Add THIS objekt to list
            student.Add(this);
        }
    }
}

