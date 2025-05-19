using System.Globalization;

namespace LolCLI
{
    public class Hos
    {
        public string Name { get; private set; }
        public string Title { get; private set; }
        public string Category { get; private set; }
        public string Tag { get; private set; }
        public string Blurb { get; private set; }
        public double Hp { get; private set; }
        public double HpPerLevel { get; private set; }
        public double Armor { get; private set; }
        public double AttackRange { get; private set; }
        public double AttackDamage { get; private set; }

        public Hos(string csvLine)
        {
            var parts = csvLine.Split(';');
            Name = parts[0];
            Title = parts[1];
            Category = parts[2];
            Tag = parts[3];
            Blurb = parts[4];
            Hp = double.Parse(parts[5], CultureInfo.GetCultureInfo("hu-HU"));
            HpPerLevel = double.Parse(parts[6], CultureInfo.GetCultureInfo("hu-HU"));
            Armor = double.Parse(parts[7], CultureInfo.GetCultureInfo("hu-HU"));
            AttackRange = double.Parse(parts[8], CultureInfo.GetCultureInfo("hu-HU"));
            AttackDamage = double.Parse(parts[9], CultureInfo.GetCultureInfo("hu-HU"));
        }
    }
}