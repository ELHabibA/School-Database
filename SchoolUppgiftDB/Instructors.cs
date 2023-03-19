using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolUppgiftDB
{
    internal class Instructors
    {
      
        public int id;
        public string name;
       


        public static List<Instructors> instructor = new List<Instructors>();

     
        public Instructors(int id, string name)
        {
            this.id = id;
            this.name = name;

            
            instructor.Add(this);
        }
    }
}

