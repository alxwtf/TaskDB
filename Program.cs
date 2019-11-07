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
            var user = new Users(db);
            var userid = 0;
            if (db.Users.Count() != 0)
            {
                userid = user.ReadUser();
            }
            else
            {
                user.AddUser();
                userid = user.ReadUser();
            }
            var Actions = new Actions(db, userid);
            var answer = 0;
            System.Console.WriteLine($"Вы зашли под пользователем - {userid}"); 
            do
            {
                System.Console.WriteLine("1. Добавить задачу\n2. Посмотреть список задач");
                System.Console.WriteLine("3. Найти задачу по названию,тегу\n4. Посмотреть конкретную задачу");
                System.Console.WriteLine("5. Установить тег на задачу\n6. Удалить задачу");
                System.Console.WriteLine("7. Переключиться на другого пользователя\n8. Создать нового пользователя");
                System.Console.WriteLine("9. Выход");
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
                            var jobcount = db.Jobs.Where(x => x.userid == userid).Count();
                            if (jobcount != 0)
                            {
                                var list = db.Jobs.Where(x => x.userid == userid).ToList();
                                Actions.History(list);
                            }
                            else Console.WriteLine("Задачи не найдены");
                            break;
                        }
                    case 3: { Actions.Search(); break; }
                    case 4: { Actions.TaskInfo(); break; }
                    case 5: { Actions.setTag(); break; }
                    case 6: { Actions.DeleteTask(); break; }
                    case 7:
                        {
                            userid = user.ReadUser();
                            System.Console.WriteLine($"Вы зашли под пользователем - {userid}"); 
                            break;
                        }
                    case 8: { user.AddUser(); break; }
                    default: break;
                }
            } while (answer != 9);
        }
    }
}
