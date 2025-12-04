using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace FitPal
{
    class CustomRecipe : FoodItem
    {
        public string Steps { get; private set; }

        public CustomRecipe(string name, int calories, double protein, double fat, double carbs, string steps)
            : base(name, calories, protein, fat, carbs)
        {
            Steps = steps;
        }

        public override string GetInfo()
        {
            return $"[Recept] {Name} - {Calories} kcal (P: {Protein}g, Zs: {Fat}g, CH: {Carbs}g)\nElkészítés: {Steps}";
        }
    }
}

