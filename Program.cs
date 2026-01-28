using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;

class Program
{
    static List<Kirja> kirjat = new List<Kirja>();
    const string tiedosto = "kirjat.json";

    static void Main()
    {
        LataaTiedostosta();

        while (true)
        {
            Console.WriteLine("\n--- Kotikirjasto ---");
            Console.WriteLine("1) Lisää kirja");
            Console.WriteLine("2) Poista kirja");
            Console.WriteLine("3) Näytä kaikki kirjat");
            Console.WriteLine("4) Näytä kirjat genren mukaan");
            Console.WriteLine("5) Etsi kirja");
            Console.WriteLine("6) Tallenna ja lopeta");
            Console.Write("Valinta: ");

            string? valinta = Console.ReadLine();

            switch (valinta)
            {
                case "1": LisaaKirja(); break;
                case "2": PoistaKirja(); break;
                case "3": NaytaKaikki(); break;
                case "4": NaytaGenrenMukaan(); break;
                case "5": EtsiKirja(); break;
                case "6": TallennaJaLopeta(); return;
                default: Console.WriteLine("Virheellinen valinta."); break;
            }
        }
    }

    static void LisaaKirja()
    {
        Console.Write("Kirjan nimi: ");
        string? nimi = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(nimi))
        {
            Console.WriteLine("Nimi ei voi olla tyhjä.");
            return;
        }

        Console.Write("Kirjoittaja: ");
        string? kirjoittaja = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(kirjoittaja))
        {
            Console.WriteLine("Kirjoittaja ei voi olla tyhjä.");
            return;
        }

        Console.Write("Julkaisuvuosi: ");
        string? vuosiSyote = Console.ReadLine();
        if (!int.TryParse(vuosiSyote, out int vuosi))
        {
            Console.WriteLine("Virheellinen vuosi.");
            return;
        }

        Console.Write("Genre: ");
        string? genre = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(genre))
        {
            Console.WriteLine("Genre ei voi olla tyhjä.");
            return;
        }

        kirjat.Add(new Kirja(nimi, kirjoittaja, vuosi, genre));
        Console.WriteLine("Kirja lisätty!");
    }

    static void PoistaKirja()
    {
        Console.Write("Anna poistettavan kirjan nimi: ");
        string? nimi = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(nimi))
        {
            Console.WriteLine("Nimi ei voi olla tyhjä.");
            return;
        }

        var kirja = kirjat.FirstOrDefault(k => 
            k.Nimi.Equals(nimi, StringComparison.OrdinalIgnoreCase));

        if (kirja == null)
        {
            Console.WriteLine("Kirjaa ei löytynyt.");
            return;
        }

        kirjat.Remove(kirja);
        Console.WriteLine("Kirja poistettu.");
    }

    static void NaytaKaikki()
    {
        if (!kirjat.Any())
        {
            Console.WriteLine("Ei kirjoja.");
            return;
        }

        foreach (var k in kirjat)
            Console.WriteLine(k);
    }

    static void NaytaGenrenMukaan()
    {
        Console.Write("Anna genre: ");
        string? genre = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(genre))
        {
            Console.WriteLine("Genre ei voi olla tyhjä.");
            return;
        }

        var tulokset = kirjat.Where(k => 
            k.Genre.Equals(genre, StringComparison.OrdinalIgnoreCase));

        if (!tulokset.Any())
        {
            Console.WriteLine("Ei kirjoja tällä genrellä.");
            return;
        }

        foreach (var k in tulokset)
            Console.WriteLine(k);
    }

    static void EtsiKirja()
    {
        Console.Write("Anna hakusana (nimi tai kirjoittaja): ");
        string? haku = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(haku))
        {
            Console.WriteLine("Hakusana ei voi olla tyhjä.");
            return;
        }

        haku = haku.ToLower();

        var tulokset = kirjat.Where(k =>
            k.Nimi.ToLower().Contains(haku) ||
            k.Kirjoittaja.ToLower().Contains(haku));

        if (!tulokset.Any())
        {
            Console.WriteLine("Ei osumia.");
            return;
        }

        foreach (var k in tulokset)
            Console.WriteLine(k);
    }

    static void TallennaJaLopeta()
    {
        string json = JsonSerializer.Serialize(kirjat, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(tiedosto, json);
        Console.WriteLine("Tallennettu. Heippa!");
    }

    static void LataaTiedostosta()
    {
        if (File.Exists(tiedosto))
        {
            string json = File.ReadAllText(tiedosto);
            var lista = JsonSerializer.Deserialize<List<Kirja>>(json);

            if (lista != null)
                kirjat = lista;

            Console.WriteLine("Kirjat ladattu tiedostosta.");
        }
    }
}

class Kirja
{
    public string Nimi { get; set; }
    public string Kirjoittaja { get; set; }
    public int Vuosi { get; set; }
    public string Genre { get; set; }

    public Kirja(string nimi, string kirjoittaja, int vuosi, string genre)
    {
        Nimi = nimi;
        Kirjoittaja = kirjoittaja;
        Vuosi = vuosi;
        Genre = genre;
    }

    public override string ToString()
    {
        return $"{Nimi} ({Vuosi}) – {Kirjoittaja}, Genre: {Genre}";
    }
}
