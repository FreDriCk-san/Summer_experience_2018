using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;

namespace Notebook
{
    class MainConsole
    {

        static void Main(string[] args)
        {
            string temp = null;
            int num = -1;

            // Task for taking all information from server (IList)
            var tAll = SelectAll();
            tAll.Wait();

            var dbList = tAll.Result;

            // Task for adding some info into the Database
            var adding = AddInfo(dbList);
            adding.Wait();

            Console.WriteLine("1) Type a number;");
            Console.WriteLine("2) Type 'all' to see all information from Database;");
            Console.WriteLine("3) Type 'name' to search a user by his(her) name;");
            Console.WriteLine("4) Type 'add' to add new user;");
            Console.WriteLine("Please, enter one of your decisions:");

            temp = Console.ReadLine();

            // If number was entered
            if (int.TryParse(temp, out num))
            {
                // Task for searching info by user's Id
                var tSearch = SearchById(Convert.ToInt32(temp));
                tSearch.Wait();

                var user = tSearch.Result;
                Info(user);
            }
            else
            {
                switch (temp)
                {
                    case "all":
                        // Display all data from DataBase
                        foreach (var item in dbList)
                        {
                            Console.WriteLine("//-----------------");
                            Console.WriteLine("Id: " + item.Id +
                            "\nName: " + item.Name +
                            "\nSurname: " + item.Surname +
                            "\nBirthday: " + item.Birthday +
                            "\nAddress: " + item.Address);
                        };
                        break;

                    case "name":
                        // Search info by name
                        Console.WriteLine("Enter name:");
                        temp = Console.ReadLine();

                        // Task for searching info by user's Name
                        var tName = SearchByName(temp);
                        tName.Wait();

                        var user = tName.Result;
                        Info(user);
                        break;

                    case "add":
                        // Add user
                        // Task for adding new user into Database
                        var tAdd = AddUser();
                        tAdd.Wait();

                        if (tAdd.Result)
                        {
                            Console.WriteLine("Added successfull!");
                        }
                        break;

                    default:
                        Console.WriteLine("Incorrect input! Try again later...");
                        break;
                }
            }

            Console.ReadKey();
        }


        // Add some info into Database (asynchronous)
        private static async Task AddInfo(IList<Models.User> dbInfo)
        {
            try
            {
                // Create new Users
                IList<Models.User> newUsers = new List<Models.User>()
                {
                    new Models.User { Name = "John", Surname = "Smith", Birthday = new DateTime(1996, 09, 25), Address = "St. Gorman 1" },
                    new Models.User { Name = "Sarah", Surname = "Smith", Birthday = new DateTime(1989, 11, 06), Address = "St. Germany 14" },
                    new Models.User { Name = "Bob", Surname = "Parker", Birthday = new DateTime(1999, 03, 15), Address = "Center Park" },
                    new Models.User { Name = "Kim", Surname = "Gray", Birthday = new DateTime(1985, 06, 30), Address = "Prokupe st." },
                    new Models.User { Name = "Paul", Surname = "Larcraft", Birthday = new DateTime(1979, 02, 19), Address = "Baimble 12" },
                    new Models.User { Name = "Remy", Surname = "Blackwood", Birthday = new DateTime(2000, 05, 10), Address = "Library" },
                    new Models.User { Name = "Bob", Surname = "Redkliff", Birthday = new DateTime(1999, 04, 11), Address = "Eston st." }
                };

                using (Context.ContextDB db = new Context.ContextDB())
                {
                    // EnsureCreated - creates DataBase if it does not exist. Else, this method doing nothing
                    db.Database.EnsureCreated();

                    // Add new Users to server. First of all, we must avoid inserting same data in the DataBase
                    if(dbInfo.Count == 0 || null == dbInfo)
                    {
                        // if Database have nothing - set new Info into it
                        db.Users.AddRange(newUsers);
                    }
                    else
                    {
                        // if server have something, start searching for coincidence
                        for (int i = 0; i < newUsers.Count; ++i)
                        {
                            // in current situation, 'Birthday' - is like a primary key (because the chance of it coincidence is very small)
                            if (!newUsers[i].Birthday.Equals(dbInfo[i].Birthday))
                            {
                                db.Users.Add(newUsers[i]);
                            }
                        }
                    }

                    // Save changes to DataBase
                    await db.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }


        // Add new user in Database (asynchronous)
        private static async Task<bool> AddUser()
        {
            var user = new Models.User();
            string temp = null;

            // Enter and save Name (no need to parse)
            Console.WriteLine("Enter Name:");
            user.Name = Console.ReadLine();

            // Enter and save Surname (no need to parse)
            Console.WriteLine("Enter Surname:");
            user.Surname = Console.ReadLine();

            // Enter and save Birthday (parse)
            DateTime dateTime;
            Console.WriteLine("Enter Birthday (yyyy-mm-dd hh:mm:ss):");
            temp = Console.ReadLine();
            // Check, if input text matches the format
            if (!DateTime.TryParseExact(temp, "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture, DateTimeStyles.None, out dateTime))
            {
                Console.WriteLine("Incorrect date format!");
                return false;
            }
            user.Birthday = dateTime;

            // Enter and save Address (no need to parse)
            Console.WriteLine("Enter Address:");
            user.Address = Console.ReadLine();

            try
            {
                using (Context.ContextDB db = new Context.ContextDB())
                {
                    db.Users.Add(user);
                    await db.SaveChangesAsync();
                    return true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return false;
        }


        // Search info about user by Id (asynchronous)
        private static async Task<Models.User> SearchById(int id)
        {
            try
            {
                using (Context.ContextDB db = new Context.ContextDB())
                {
                    var user = await (db.Users.FindAsync(id));
                    return user;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return null;
        }


        // Select all users from Database (asynchronous)
        private static async Task<IList<Models.User>> SelectAll()
        {
            try
            {
                using(Context.ContextDB db = new Context.ContextDB())
                {
                    var list = await (db.Users.ToListAsync());
                    return list;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return null;
        }


        // Search info about user by name (asynchronous)
        private static async Task<Models.User> SearchByName(string name)
        {
            try
            {
                using (Context.ContextDB db = new Context.ContextDB())
                {
                    var user = await (db.Users.FirstOrDefaultAsync(u => u.Name == name));
                    return user;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return null;
        }


        // Display info about user
        private static void Info(Models.User user)
        {
            try
            {
                if (null != user)
                {
                    Console.WriteLine("//-----------------");
                    Console.WriteLine("Id: " + user.Id +
                        "\nName: " + user.Name +
                        "\nSurname: " + user.Surname +
                        "\nBirthday: " + user.Birthday +
                        "\nAddress: " + user.Address);
                }
                else
                {
                    Console.WriteLine("This user does not exist!");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
