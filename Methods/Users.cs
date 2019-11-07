using System;
using System.Linq;

namespace TaskDB
{
    class Users
    {
        private Context _db;

        public Users(Context db)
        {
            _db = db;
        }
        public void AddUser()
        {
            System.Console.WriteLine("Введите имя Нового пользователя:");
            var username = Console.ReadLine();
            System.Console.WriteLine("Введите пароль Нового пользователя:");
            var password = ReadPassword();
            System.Console.WriteLine("Повторите пароль Нового пользователя:");
            var repeat = ReadPassword();
            if (password != repeat)
            {
                do
                {
                    System.Console.WriteLine("Пароли не совпадают\nвведите новый пароль снова");
                    password = ReadPassword();
                    System.Console.WriteLine("Повторите пароль Нового пользователя:");
                    repeat = ReadPassword();
                } while (password != repeat);
            }
            _db.Users.Add(new User
            {
                username = username,
                password = password
            });
            _db.SaveChanges();
        }
        public int ReadUser()
        {
            System.Console.WriteLine("Введите имя пользователя");
            var username = Console.ReadLine();
            var password = ReadPassword();
            var id = 0;
            var existinguser = _db.Users.FirstOrDefault(x => x.username == username && x.password == password);
            if (existinguser != null)
            {
                id = existinguser.id;
            }
            else
            {
                do
                {
                    System.Console.WriteLine("Логин и пароль не найдены\n пожалуйста введите снова");
                    System.Console.WriteLine("Логин");
                    username = Console.ReadLine();
                    System.Console.WriteLine("Пароль");
                    username = ReadPassword();
                    existinguser = _db.Users.FirstOrDefault(x => x.username == username && x.password == password);
                } while (existinguser == null);
                id = existinguser.id;
            };
            return id;
        }

        private string ReadPassword()
        {
            var password = "";
            ConsoleKeyInfo info = Console.ReadKey(true);
            while (info.Key != ConsoleKey.Enter)
            {
                if (info.Key != ConsoleKey.Backspace)
                {
                    Console.Write("*");
                    password += info.KeyChar;
                }
                else
                {
                    if (!string.IsNullOrEmpty(password))
                    {
                        password = password.Substring(0, password.Length - 1);
                        var pos = Console.CursorLeft;
                        Console.SetCursorPosition(pos - 1, Console.CursorTop);
                        Console.Write(" ");
                        Console.SetCursorPosition(pos - 1, Console.CursorTop);
                    }
                }
                info = Console.ReadKey(true);
            }
            System.Console.WriteLine();
            return password;
        }
    }
}