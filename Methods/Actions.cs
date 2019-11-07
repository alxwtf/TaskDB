using System.Collections.Generic;
using System;
using System.Linq;

namespace TaskDB
{
    class Actions
    {
        private Context _db;
        private int _userid {get; set; }
        public Actions(Context db,int userid)
        {
            _db = db;
            _userid = userid;
        }
        public void Add()
        {
            Console.Clear();
            var id = Guid.NewGuid();
            Console.WriteLine("Название задачи");
            var Name = Console.ReadLine();
            Console.WriteLine("Описание задачи");
            var Description = Console.ReadLine();
            Console.WriteLine("Введите тэги через пробел");
            var Tags = Console.ReadLine();
            var CreationDate = DateTime.Now.Date;
            System.Console.WriteLine("Введите дату завершения");
            DateTime? Date = DateTime.Parse(Console.ReadLine());
            _db.Jobs.Add(new Job()
            {
                id = id,
                Name = Name,
                Description = Description,
                Tag = Tags,
                CreationDate = CreationDate,
                Date = Date,
                userid = _userid
            });
            _db.SaveChanges();
        }

        public void History(List<Job> query)
        {
            var count = 0;
            foreach (var Job in query)
            {
                if (query.Count > 1)
                {
                    Console.WriteLine($"\nЗадача №{count++}");
                }
                Console.WriteLine($"ID: {Job.id}");
                Console.WriteLine($"Название задачи: {Job.Name}");
                Console.WriteLine($"Описание задачи: {Job.Description}");
                Console.WriteLine($"Тэги: {Job.Tag}");
                Console.WriteLine($"Дата создания: {Job.CreationDate.ToString("dd.MM.yyyy")}");
                if (Job.Date != null)
                {
                    Console.WriteLine($"Дата завершения: {Job.Date.Value.ToString("dd.MM.yyyy")}");
                }
            }
            Console.WriteLine();
        }
        public void Search()
        {
            Console.Clear();
            Console.WriteLine("Вести поиск по\n1. Названию\n2. Тэгу");
            int.TryParse(Console.ReadLine(), out var answer);
            switch (answer)
            {
                case 1:
                    {
                        Console.WriteLine("Введите название Задачи");
                        var name = Console.ReadLine();
                        var query = _db.Jobs.Where(x => x.Name == name && x.userid==_userid).ToList();
                        if (query.Count() != 0) History(query);
                        else Console.WriteLine("Задачи не найдены");
                        break;
                    }
                case 2:
                    {
                        Console.WriteLine("Введите Тэг(и)");
                        var tags = Console.ReadLine();
                        var query = _db.Jobs.Where(x => x.Tag.Contains(tags) && x.userid==_userid).ToList();
                        if (query.Count() != 0) History(query);
                        else Console.WriteLine("Задачи не найдены");
                        break;
                    }
                default: break;
            }
        }
        public void TaskInfo()
        {
            Console.Clear();
            if (_db.Jobs.Count() != 0)
            {
                var GuidList = _db.Jobs.Select(x => new { x.id, x.userid }).Where(x=>x.userid==_userid).ToList();
                var count = 1;
                foreach (var item in GuidList)
                {
                    System.Console.WriteLine($"{count++}. { item.id}");
                }
                Console.WriteLine($"Введите ID(Guid) Задачи");
                var task = Console.ReadLine();
                var query = _db.Jobs.Where(x => x.id.ToString().Contains(task) && x.userid==_userid).ToList();
                History(query);
            }
            else Console.WriteLine("Задач на данный момент нет");
        }
        public void setTag()
        {
            Console.Clear();
            var query = _db.Jobs.ToList();
            if (query.Count() != 0)
            {
                History(query);
                Console.WriteLine($"Введите ID(Guid) Задачи");
                var guid = Console.ReadLine();
                Console.WriteLine("Введите новые тэги через пробел");
                var setTag = _db.Jobs.Where(x => x.id.ToString().Contains(guid) && x.userid==_userid).FirstOrDefault();
                setTag.Tag = Console.ReadLine();
                _db.SaveChanges();
            }
            else System.Console.WriteLine("Задач нет\n");
        }
        public void DeleteTask()
        {
            Console.Clear();
            var query = _db.Jobs.Select(x => new { x.id, x.Name, x.Description, x.userid })
                        .Where(x=>x.userid==_userid)
                        .ToList();
            if (query.Count != 0)
            {
                foreach (var job in query)
                {
                    System.Console.WriteLine($"Guid Задачи:{job.id}");
                    System.Console.WriteLine($"Название задачи:{job.Name}");
                    System.Console.WriteLine($"Описание:{job.Description}\n");
                }
                System.Console.WriteLine($"Введите Guid задачи\nдля отмены нажмите enter");
                var guid = "";
                do
                {
                    guid = Console.ReadLine();
                    if (guid != "")
                    {
                        var toRemove = _db.Jobs.Where(x => x.id.ToString().Contains(guid) && x.userid==_userid).FirstOrDefault();
                        _db.Jobs.Remove(toRemove);
                        _db.SaveChanges();
                        System.Console.WriteLine("Успешно удалено\n");
                        break;
                    }
                    else { System.Console.WriteLine("Отмена\n"); }
                } while (guid != "");
            }
            else { System.Console.WriteLine("Пусто\n"); }
        }
    }
}