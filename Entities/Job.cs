using System;
using System.Collections.Generic;

namespace TaskDB
{
    public class Job
    {
        public Guid id { get; set; }
        public string Name { get; set; }

        public string Description { get; set; }

        public string Tag { get; set; }

        public DateTime CreationDate { get; set; }

        public DateTime? Date { get; set; }

        public int userid { get; set; }
    }
}