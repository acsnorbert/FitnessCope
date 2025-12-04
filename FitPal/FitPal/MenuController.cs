using System;
using System.Linq;

namespace FitPal
{
    class MenuController
    {
        private LogManager manager;
        private User user;

        public MenuController(LogManager m, User u)
        {
            manager = m;
            user = u;
        }

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
                    case "5": EditUser(); break;
                    case "6": ShowUser(); break;
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
            Console.WriteLine("5. Felhasználói adatok szerkesztése");
            Console.WriteLine("6. Felhasználói adatok megtekintése");
            Console.WriteLine("0. Kilépés");
            Console.Write("Választás: ");
        }

        private void AddFoodMenu()
        {
            try
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
                    if (!manager.Recipes.Any(r => r.Name == name && r.Steps == steps))
                        manager.Recipes.Add((CustomRecipe)food);
                }
                else
                {
                    food = new BasicFood(name, cal, p, f, c);
                }

                log.AddFood(food);
                Console.WriteLine("Étel hozzáadva.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Hiba a bevitel során: " + ex.Message);
            }
        }

        private void AddStepsMenu()
        {
            try
            {
                Console.Write("Add meg a napot (YYYY-MM-DD): ");
                DateTime date = DateTime.Parse(Console.ReadLine());

                var log = manager.GetOrCreateLog(date);

                Console.Write("Lépésszám (+): ");
                int steps = int.Parse(Console.ReadLine());

                log.AddSteps(steps);
                Console.WriteLine("Lépésszám hozzáadva.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Hiba a bevitel során: " + ex.Message);
            }
        }

        private void ShowDailySummary()
        {
            try
            {
                Console.Write("Add meg a napot (YYYY-MM-DD): ");
                DateTime date = DateTime.Parse(Console.ReadLine());

                var log = manager.GetOrCreateLog(date);

                Console.WriteLine(log.GetDailyStats());
            }
            catch (Exception ex)
            {
                Console.WriteLine("Hiba: " + ex.Message);
            }
        }

        private void ShowAllLogs()
        {
            foreach (var log in manager.Logs.OrderBy(l => l.Date))
            {
                Console.WriteLine(log.GetDailyStats());

                foreach (var item in log.Items)
                {
                    Console.WriteLine(item.GetInfo());
                }
                Console.WriteLine("-------------------------");
            }


            if (manager.Recipes.Count > 0)
            {
                Console.WriteLine("\n-- Mentett receptek --");
                foreach (var r in manager.Recipes)
                {
                    Console.WriteLine(r.GetInfo());
                    Console.WriteLine();
                }
            }
        }

        private void EditUser()
        {
            Console.Write("Név: ");
            user.Name = Console.ReadLine();

            Console.Write("Kor: ");
            if (int.TryParse(Console.ReadLine(), out int age)) user.Age = age;

            Console.Write("Testsúly (kg): ");
            if (double.TryParse(Console.ReadLine(), out double w)) user.Weight = w;

            Console.Write("Magasság (cm): ");
            if (double.TryParse(Console.ReadLine(), out double h)) user.Height = h;

            user.Save();
            Console.WriteLine("Felhasználói adatok mentve.");
        }

        private void ShowUser()
        {
            Console.WriteLine("\n-- Felhasználó --");
            Console.WriteLine(user.ToString());
        }
    }
}
