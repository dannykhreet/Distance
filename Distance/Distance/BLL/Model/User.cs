using System;
using System.Collections.Generic;
using System.Text;

namespace Distance.BLL.Model
{
   public enum Gender
    {
        Male,
        Famele
    }
  public  class User :ModelBase
    {
        public String Email { get; set; }
        public String Password { get; set; }
        public String Name { get; set; }
        public int TelephoneNumber { get; set; }
        public Gender Gender { get; set; }

    }
}
