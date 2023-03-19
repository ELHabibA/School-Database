using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolUppgiftDB
{
    internal class Enrollment
    {

        public int course_id;
        public int student_id;
        public decimal price;

   
        public static List<Enrollment> enrollment = new List<Enrollment>();

   
        public Enrollment(int course_id, int student_id, decimal price)
        {
            this.course_id = course_id;
            this.student_id = student_id;
            this.price = price;

          
            enrollment.Add(this);
        }
    }
}
