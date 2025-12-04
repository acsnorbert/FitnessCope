using System;
using System.Collections.Generic;

namespace FitPal
{

    class User
    {
        public string Name { get; set; } = "";
        public int Age { get; set; } = 0;
        public double Weight { get; set; } = 0; //kg
        public double Height { get; set; } = 0; //cm

        private const string FileName = "user.txt";

        public void Save()
        {

            string text = $"{Name}|{Age}|{Weight}|{Height}";
            DataStorage.SaveText(FileName, text);
        }

        public void Load()
        {
            string raw = DataStorage.LoadText(FileName);
            if (string.IsNullOrWhiteSpace(raw)) return;

            var parts = raw.Split('|');
            if (parts.Length >= 1) Name = parts[0];
            if (parts.Length >= 2 && int.TryParse(parts[1], out int a)) Age = a;
            if (parts.Length >= 3 && double.TryParse(parts[2], out double w)) Weight = w;
            if (parts.Length >= 4 && double.TryParse(parts[3], out double h)) Height = h;
        }

        public override string ToString()
        {
            return $"Név: {Name}\nKor: {Age}\nTestsúly: {Weight} kg\nMagasság: {Height} cm";
        }
    }
}
