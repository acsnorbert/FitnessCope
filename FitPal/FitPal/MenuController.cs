using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitPal
{
    using System;
    using System.Linq;

    class MenuController
    {
        private LogManager manager = new LogManager();

        public void RunMenu()
        {
            while (true)
            {
                ShowMenu();
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1": AddFoodMenu(); break;
                    case "2": AddStepsMenu(); break;
                    case "3": ShowDailySummary(); break;
                    case "4": ShowAllLogs(); break;
                    case "0": return;
                    default: Console.WriteLine("Érvénytelen választás."); break;
                }
            }
        }

        protected void ShowMenu()
        {
            Console.WriteLine("\n---- FITLOG MENÜ ----");
            Console.WriteLine("1. Ételfelvétel adott naphoz");
            Console.WriteLine("2. Lépésszám hozzáadása naphoz");
            Console.WriteLine("3. Napi összesítés megtekintése");
            Console.WriteLine("4. Teljes napló kiírása");
            Console.WriteLine("0. Kilépés");
            Console.Write("Választás: ");
        }

        private void AddFoodMenu()
        {
            Console.Write("Add meg a napot (YYYY-MM-DD): ");
            DateTime date = DateTime.Parse(Console.ReadLine());

            var log = manager.GetOrCreateLog(date);

            Console.Write("Étel neve: ");
            string name = Console.ReadLine();

            Console.Write("Kalória: ");
            int cal = int.Parse(Console.ReadLine());

            Console.Write("Fehérje (g): ");
            double p = double.Parse(Console.ReadLine());

            Console.Write("Zsír (g): ");
            double f = double.Parse(Console.ReadLine());

            Console.Write("Szénhidrát (g): ");
            double c = double.Parse(Console.ReadLine());

            Console.Write("Recept? (i/n): ");
            bool recipe = Console.ReadLine().ToLower() == "i";

            FoodItem food;

            if (recipe)
            {
                Console.Write("Elkészítés leírása: ");
                string steps = Console.ReadLine();
                food = new CustomRecipe(name, cal, p, f, c, steps);
            }
            else
            {
                food = new BasicFood(name, cal, p, f, c);
            }

            log.AddFood(food);
            Console.WriteLine("Étel hozzáadva.");
        }

        private void AddStepsMenu()
        {
            Console.Write("Add meg a napot (YYYY-MM-DD): ");
            DateTime date = DateTime.Parse(Console.ReadLine());

            var log = manager.GetOrCreateLog(date);

            Console.Write("Lépésszám (+): ");
            int steps = int.Parse(Console.ReadLine());

            log.AddSteps(steps);
            Console.WriteLine("Lépésszám hozzáadva.");
        }

        private void ShowDailySummary()
        {
            Console.Write("Add meg a napot (YYYY-MM-DD): ");
            DateTime date = DateTime.Parse(Console.ReadLine());

            var log = manager.GetOrCreateLog(date);

            Console.WriteLine(log.GetDailyStats());
        }

        private void ShowAllLogs()
        {
            foreach (var log in manager.Logs.OrderBy(l => l.Date))
            {
                Console.WriteLine(log.GetDailyStats());
            }
        }
    }

}
