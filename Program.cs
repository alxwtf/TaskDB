using System;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Collections.Generic;
namespace TaskDB
{
    class Program
    {
        static void Main(string[] args)
        {
            var db = new Context();
            var Actions = new Actions(db);
            var answer = 0;
            do
            {
                System.Console.WriteLine("1. Добавить задачу\n2. Посмотреть список задач");
                System.Console.WriteLine("3. Найти задачу по названию,тегу\n4. Посмотреть конкретную задачу");
                System.Console.WriteLine("5. Установить тег на задачу\n6. Удалить задачу");
                System.Console.WriteLine("7. Выход");
                int.TryParse(Console.ReadLine(), out answer);
                switch (answer)
                {
                    case 1:
                        {
                            Actions.Add();
                            break;
                        }
                    case 2:
                        {
                            Console.Clear();
                            if (db.Jobs.Count() != 0)
                                Actions.History(db.Jobs.ToList());
                            else Console.WriteLine("Задачи не найдены");
                            break;
                        }
                    case 3: { Actions.Search(); break; }
                    case 4: { Actions.TaskInfo(); break; }
                    case 5: { Actions.setTag(); break; }
                    case 6: { Actions.DeleteTask(); break; }
                    default: break;
                }
            } while (answer != 7);
        }
    }
}
