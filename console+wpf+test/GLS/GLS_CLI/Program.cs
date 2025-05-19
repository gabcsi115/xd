using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace GLS_CLI
{
    class Program
    {
        static void Main(string[] args)
        {
            List<AutóAdatok> autóAdatokLista = new List<AutóAdatok>();

            try
            {
                // 1. Feladat: Fájl beolvasása
                string[] sorok = File.ReadAllLines("C:\\Users\\Gabcsi\\Desktop\\GLS\\GLS_CLI\\GLS.txt");
                foreach (string sor in sorok)
                {
                    try
                    {
                        AutóAdatok adat = new AutóAdatok(sor);
                        autóAdatokLista.Add(adat);
                    }
                    catch (Exception)
                    {
                        // Hibás sor esetén folytatjuk a következővel
                        continue;
                    }
                }

                Console.WriteLine("A GLS.txt fájl adatai sikeresen beolvasva!");

                // 2. Feladat: Napok számának meghatározása
                int napokSzáma = autóAdatokLista.Count;
                Console.WriteLine($"Az autó használatban töltött napjainak száma: {napokSzáma}");

                // 3. Feladat: Különböző sofőrök megszámlálása
                int különbözőSofőrökSzáma = autóAdatokLista.Select(a => a.SofőrNeve).Distinct().Count();
                Console.WriteLine($"Különböző sofőrök száma: {különbözőSofőrökSzáma}");

                // 4. Feladat: Összes megtett kilométer kiszámítása
                int összesKilométer = autóAdatokLista.Sum(a => a.NapiKilométer);
                Console.WriteLine($"Az összes megtett kilométer: {összesKilométer} km");

                // 6. Feladat: Átlagos fogyasztás kiszámítása liter/100 km alapján
                double átlagosFogyasztás = AutóAdatok.ÁtlagosFogyasztásSzámítás(
                    autóAdatokLista.Sum(a => a.NapiFogyasztásLiterben),
                    autóAdatokLista.Sum(a => a.NapiKilométer));
                Console.WriteLine($"Átlagos fogyasztás: {átlagosFogyasztás:F2} liter/100 km");

                // 7. Feladat: Legtöbbször vezető sofőr meghatározása
                var sofőrStatisztika = autóAdatokLista
                    .GroupBy(a => a.SofőrNeve)
                    .Select(g => new { Sofőr = g.Key, Napok = g.Count() })
                    .OrderByDescending(g => g.Napok)
                    .FirstOrDefault();

                if (sofőrStatisztika != null)
                {
                    Console.WriteLine($"A legtöbbet vezető sofőr: {sofőrStatisztika.Sofőr}, napok száma: {sofőrStatisztika.Napok}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Hiba történt: {ex.Message}");
            }

            Console.WriteLine("\nNyomj Enter-t a befejezéshez...");
            Console.ReadLine();
        }
    }
}