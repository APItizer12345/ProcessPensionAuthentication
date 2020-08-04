using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProcessPension
{
    public class ClientInput
    {
        
        public string name { get; set; }

        public DateTime dateOfBirth { get; set; }
       
        public string pan { get; set; }
       
        public string aadharNumber { get; set; }
        
        public int pensionType { get; set; }
        
    }


    public class ValueforCalCulation
    {
        public int bankType { get; set; }
        public int salaryEarned { get; set; }
        public int allowances { get; set; }
        public int pensionType { get; set; }
    }
}
