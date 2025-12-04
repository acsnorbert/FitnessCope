using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
    

namespace FitPal
{
    abstract class FoodItem
    {
        public string Name { get; protected set; }
        public int Calories { get; protected set; }
        public double Protein { get; protected set; }
        public double Fat { get; protected set; }
        public double Carbs { get; protected set; }

        public FoodItem(string name, int calories, double protein, double fat, double carbs)
        {
            Name = name;
            Calories = calories;
            Protein = protein;
            Fat = fat;
            Carbs = carbs;
        }

        public virtual string GetInfo()
        {
            return $"{Name} - {Calories} kcal (P: {Protein}g, Zs: {Fat}g, CH: {Carbs}g)";
        }
    }
}

