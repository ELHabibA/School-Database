using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolUppgiftDB
{
    internal class Courses
    {
    
        public int id;
        public string title;
        public Decimal price;

     
        public static List<Courses> course = new List<Courses>();

       
        public Courses(int id, string title, decimal price)
        {
            this.id = id;
            this.title = title;
            this.price = price;

          
            course.Add(this);
        }
    }
}

