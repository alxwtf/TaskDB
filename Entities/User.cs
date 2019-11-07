using System;
using System.Collections.Generic;
using System.Linq;

namespace TaskDB
{
    public class User
    {
        public int id { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public List<Job> Jobs { get; set; }

    }
}