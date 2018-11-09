using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Store.Models
{
    public class CheckBoxModel
    {
        private static int Static_Id = 0;
        private static object locker = new object();
        public CheckBoxModel()
        {
            Id = Static_Id;
            Static_Id += 1;
        }

        public static void RefreshId()
        {
            lock (locker)
            {
                Static_Id = 0;
            }
        }

        public bool IsChecked { get; set; }
        public string Value { get; set; }
        public int Id { get; private set; }
    }
}