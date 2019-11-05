using System;

namespace TaskDB
{
    class Job
    {
        public Guid id { get; set; }
        public string Name { get; set; }

        public string Description { get; set; }

        public string Tag { get; set; }

        public DateTime CreationDate { get; set; }

        public DateTime? Date { get; set; }
    }
}