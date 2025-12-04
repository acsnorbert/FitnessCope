using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitPal
{
    class Program
    {
        static void Main(string[] args)
        {

            LogManager manager = new LogManager();
            manager.LoadRecipes(); 
            manager.LoadLogs();   

            User user = new User();
            user.Load();

            MenuController menu = new MenuController(manager, user);
            menu.RunMenu();

            manager.SaveLogs();
            manager.SaveRecipes();
            user.Save();

            Console.WriteLine("Minden adatod elmentve. Szevasz!");
        }
    }
}

