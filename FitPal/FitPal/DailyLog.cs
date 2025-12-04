using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace FitPal
{
    class DailyLog
    {
        public DateTime Date { get; set; }
        public List<FoodItem> Items { get; set; } = new List<FoodItem>();
        public int Steps { get; set; }

        public DailyLog(DateTime date)
        {
            Date = date;
            Steps = 0;
        }

        public void AddFood(FoodItem food)
        {
            Items.Add(food);
        }

        public void AddSteps(int steps)
        {
            Steps += steps;
        }

        public string GetDailyStats()
        {
            int calories = Items.Sum(i => i.Calories);
            double protein = Items.Sum(i => i.Protein);
            double fat = Items.Sum(i => i.Fat);
            double carbs = Items.Sum(i => i.Carbs);

            return
                $"Dátum: {Date.ToShortDateString()}\n" +
                $"Összes kalória: {calories} kcal\n" +
                $"Fehérje: {protein} g\n" +
                $"Zsír: {fat} g\n" +
                $"Szénhidrát: {carbs} g\n" +
                $"Lépésszám: {Steps} lépés\n";
        }
    }
}
