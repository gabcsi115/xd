using System;
using System.Globalization;

namespace GLS_CLI
{
    public class AutóAdatok
    {
        // Property-k, melyek csak lekérdezhetők
        public DateTime Dátum { get; private set; }
        public string SofőrNeve { get; private set; }
        public int NapiKilométer { get; private set; }
        public int KézbesítettCsomagokSzáma { get; private set; }
        public int NapiFogyasztásLiterben { get; private set; }

        // Paraméteres konstruktor, amely az állomány egy sorából inicializálja az objektumot
        public AutóAdatok(string sor)
        {
            try
            {
                string[] adatok = sor.Split(';');
                if (adatok.Length >= 5)
                {
                    Dátum = DateTime.Parse(adatok[0], CultureInfo.InvariantCulture);
                    SofőrNeve = adatok[1];
                    NapiKilométer = int.Parse(adatok[2]);
                    KézbesítettCsomagokSzáma = int.Parse(adatok[3]);
                    NapiFogyasztásLiterben = int.Parse(adatok[4]);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Hiba az adatok beolvasásakor: {ex.Message}");
                throw;
            }
        }

        // Másodlagos konstruktor teszteléshez
        public AutóAdatok(DateTime dátum, string sofőrNeve, int napiKilométer, int kézbesítettCsomagokSzáma, int napiFogyasztásLiterben)
        {
            Dátum = dátum;
            SofőrNeve = sofőrNeve;
            NapiKilométer = napiKilométer;
            KézbesítettCsomagokSzáma = kézbesítettCsomagokSzáma;
            NapiFogyasztásLiterben = napiFogyasztásLiterben;
        }

        // Átlagos napi fogyasztás kiszámítása liter/100 km-ben
        public static double ÁtlagosFogyasztásSzámítás(int fogyasztás, int megtettKm)
        {
            if (fogyasztás <= 0 || megtettKm <= 0)
            {
                return 0;
            }

            return (fogyasztás * 100.0) / megtettKm;
        }
    }
}