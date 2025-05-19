using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace LolCLI
{
    class Program
    {
        static List<Hos> heroes = new List<Hos>();

        static void Main(string[] args)
        {
            ReadCSV("champions2017.csv");

            Console.WriteLine($"2. Feladat: Az állományban {heroes.Count} hős található");

            Hos selectedHero = null;
            do
            {
                Console.Write("3. Feladat: Kérem adja meg a hős nevét: ");
                var heroName = Console.ReadLine();
                selectedHero = heroes.FirstOrDefault(h => h.Name.Equals(heroName, StringComparison.OrdinalIgnoreCase));
            } while (selectedHero == null);

            Console.WriteLine($"{selectedHero.Name} adatai: HP:{selectedHero.Hp.ToString("N2", CultureInfo.GetCultureInfo("hu-HU"))}; Kategória: {selectedHero.Category}");

            var maxHp = 0.0;
            Hos maxHero = null;
            foreach (var hero in heroes)
            {
                var currentHp = hero.Hp + 15 * hero.HpPerLevel;
                if (currentHp > maxHp)
                {
                    maxHp = currentHp;
                    maxHero = hero;
                }
            }
            Console.WriteLine($"5. Feladat: 15. szinten a legnagyobb HP-vel rendelkező hős {maxHero.Name}; HP={maxHp.ToString("N2", CultureInfo.GetCultureInfo("hu-HU"))}");

            var categories = heroes.GroupBy(h => h.Category).OrderBy(g => g.Key);
            using (var writer = new StreamWriter("teljes.txt"))
            {
                foreach (var category in categories)
                {
                    writer.WriteLine(category.Key);
                    foreach (var hero in category.OrderBy(h => h.Name))
                        writer.WriteLine(hero.Name);
                }
            }
        }

        static void ReadCSV(string path)
        {
            foreach (var line in File.ReadLines(path).Skip(1))
                heroes.Add(new Hos(line));
        }

        public static double HpErtek(string heroName, int level)
        {
            if (level < 1 || level > 18)
                return -1;

            var hero = heroes.FirstOrDefault(h => h.Name.Equals(heroName, StringComparison.OrdinalIgnoreCase));
            return hero == null ? -1 : hero.Hp + level * hero.HpPerLevel;
        }
    }
}