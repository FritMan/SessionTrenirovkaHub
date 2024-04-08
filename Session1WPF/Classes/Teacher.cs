using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Session1WPF.Data
{
    public partial class Teacher
    {
        public string FIO 
        { 
            get
            {
                return $"{Surname} {Name} {Patronymic}";
            }
        }
    }
}
