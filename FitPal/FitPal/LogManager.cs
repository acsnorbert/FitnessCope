using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace FitPal
{
    class LogManager
    {
        public List<DailyLog> Logs { get; private set; } = new List<DailyLog>();


        public List<CustomRecipe> Recipes { get; private set; } = new List<CustomRecipe>();

        private const string LogsFile = "logs.txt";
        private const string RecipesFile = "recipes.txt";

        public DailyLog GetOrCreateLog(DateTime date)
        {
            var log = Logs.FirstOrDefault(l => l.Date.Date == date.Date);

            if (log == null)
            {
                log = new DailyLog(date);
                Logs.Add(log);
            }

            return log;
        }


        public void SaveLogs()
        {
            List<string> lines = new List<string>();

            foreach (var log in Logs)
            {

                lines.Add($"DATE|{log.Date:yyyy-MM-dd}|{log.Steps}");


                foreach (var item in log.Items)
                {
                    if (item is CustomRecipe r)
                    {

                        string safeRecipe = (r.Steps ?? "").Replace("|", "/");
                        lines.Add($"FOOD|{r.Name}|{r.Calories}|{r.Protein}|{r.Fat}|{r.Carbs}|{safeRecipe}");
                    }
                    else
                    {
                        lines.Add($"FOOD|{item.Name}|{item.Calories}|{item.Protein}|{item.Fat}|{item.Carbs}|");
                    }
                }
            }

            DataStorage.SaveLines(LogsFile, lines);
        }


        public void LoadLogs()
        {
            var lines = DataStorage.LoadLines(LogsFile);
            if (lines.Count == 0) return;

            Logs.Clear();

            DailyLog current = null;

            foreach (var line in lines)
            {
                var parts = line.Split('|');

                if (parts.Length == 0) continue;

                if (parts[0] == "DATE")
                {

                    if (parts.Length >= 3 && DateTime.TryParse(parts[1], out DateTime date))
                    {
                        int steps = 0;
                        int.TryParse(parts[2], out steps);

                        current = GetOrCreateLog(date);
                        current.Steps = steps; 
                    }
                }
                else if (parts[0] == "FOOD" && current != null)
                {

                    string name = parts.Length > 1 ? parts[1] : "Ismeretlen";
                    int cal = (parts.Length > 2 && int.TryParse(parts[2], out int tcal)) ? tcal : 0;
                    double p = (parts.Length > 3 && double.TryParse(parts[3], out double tp)) ? tp : 0;
                    double f = (parts.Length > 4 && double.TryParse(parts[4], out double tf)) ? tf : 0;
                    double c = (parts.Length > 5 && double.TryParse(parts[5], out double tc)) ? tc : 0;
                    string recipe = parts.Length > 6 ? parts[6] : "";

                    FoodItem food;
                    if (string.IsNullOrWhiteSpace(recipe))
                    {
                        food = new BasicFood(name, cal, p, f, c);
                    }
                    else
                    {

                        food = new CustomRecipe(name, cal, p, f, c, recipe);

                        if (!Recipes.Any(r => r.Name == name && r.Steps == recipe))
                            Recipes.Add((CustomRecipe)food);
                    }

                    current.AddFood(food);
                }
            }
        }


        public void SaveRecipes()
        {

            var lines = new List<string>();
            foreach (var r in Recipes)
            {
                string safeRecipe = (r.Steps ?? "").Replace("|", "/");
                lines.Add($"{r.Name}|{r.Calories}|{r.Protein}|{r.Fat}|{r.Carbs}|{safeRecipe}");
            }

            DataStorage.SaveLines(RecipesFile, lines);
        }


        public void LoadRecipes()
        {
            var lines = DataStorage.LoadLines(RecipesFile);
            if (lines.Count == 0) return;

            Recipes.Clear();

            foreach (var line in lines)
            {
                var parts = line.Split('|');
                if (parts.Length < 6) continue;
                string name = parts[0];
                int cal = int.TryParse(parts[1], out int tc) ? tc : 0;
                double p = double.TryParse(parts[2], out double tp) ? tp : 0;
                double f = double.TryParse(parts[3], out double tf) ? tf : 0;
                double c = double.TryParse(parts[4], out double tc2) ? tc2 : 0;
                string recipe = parts[5];

                var r = new CustomRecipe(name, cal, p, f, c, recipe);
                Recipes.Add(r);
            }
        }
    }
}
