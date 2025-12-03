using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitPal
{
    class BasicFood : FoodItem
    {
        public BasicFood(string name, int calories, double protein, double fat, double carbs)
            : base(name, calories, protein, fat, carbs)
        {
        }

        public override string GetInfo()
        {
            return "[Alap étel] " + base.GetInfo();
        }
    }

}
