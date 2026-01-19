// See https://aka.ms/new-console-template for more informationusing System;
using System.Collections.Generic;
using System.Linq;

class Book
{
    public string Title { get; set; }
    public string Author { get; set; }
    public int Year { get; set; }
    public string Genre { get; set; }

    public override string ToString()
    {
        return $"{Title} – {Author} ({Year}), Genre: {Genre}";
    }
}

class Program
{
    static List<Book> library = new List<Book>();

    static void Main()
    {
        bool running = true;

        while (running)
        {
            Console.WriteLine("\n--- Kotikirjasto ---");
            Console.WriteLine("1. Lisää kirja");
            Console.WriteLine("2. Poista kirja");
            Console.WriteLine("3. Näytä kaikki kirjat");
            Console.WriteLine("4. Näytä kirjat genren mukaan");
            Console.WriteLine("5. Hae kirja (nimi tai kirjoittaja)");
            Console.WriteLine("0. Lopeta");

            Console.Write("Valinta: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    AddBook();
                    break;
                case "2":
                    RemoveBook();
                    break;
                case "3":
                    ShowAllBooks();
                    break;
                case "4":
                    ShowBooksByGenre();
                    break;
                case "5":
                    SearchBooks();
                    break;
                case "0":
                    running = false;
                    break;
                default:
                    Console.WriteLine("Virheellinen valinta.");
                    break;
            }
        }
    }

    // 🔹 Lisää kirja
    static void AddBook()
    {
        Console.Write("Nimi: ");
        string title = Console.ReadLine();

        Console.Write("Kirjoittaja: ");
        string author = Console.ReadLine();

        Console.Write("Julkaisuvuosi: ");
        int year = int.Parse(Console.ReadLine());

        Console.Write("Genre: ");
        string genre = Console.ReadLine();

        library.Add(new Book
        {
            Title = title,
            Author = author,
            Year = year,
            Genre = genre
        });

        Console.WriteLine("Kirja lisätty.");
    }

    // 🔹 Poista kirja
    static void RemoveBook()
    {
        Console.Write("Anna poistettavan kirjan nimi: ");
        string title = Console.ReadLine();

        var book = library.FirstOrDefault(b =>
            b.Title.Equals(title, StringComparison.OrdinalIgnoreCase));

        if (book != null)
        {
            library.Remove(book);
            Console.WriteLine("Kirja poistettu.");
        }
        else
        {
            Console.WriteLine("Kirjaa ei löytynyt.");
        }
    }

    // 🔹 Näytä kaikki kirjat
    static void ShowAllBooks()
    {
        if (library.Count == 0)
        {
            Console.WriteLine("Kirjasto on tyhjä.");
            return;
        }

        foreach (var book in library)
        {
            Console.WriteLine(book);
        }
    }

    // 🔹 Näytä kirjat genren mukaan
    static void ShowBooksByGenre()
    {
        Console.Write("Anna genre: ");
        string genre = Console.ReadLine();

        var results = library.Where(b =>
            b.Genre.Equals(genre, StringComparison.OrdinalIgnoreCase));

        if (!results.Any())
        {
            Console.WriteLine("Ei kirjoja tässä genressä.");
            return;
        }

        foreach (var book in results)
        {
            Console.WriteLine(book);
        }
    }

    // 🔹 Hae kirja nimellä tai kirjoittajalla
    static void SearchBooks()
    {
        Console.Write("Hakusana: ");
        string query = Console.ReadLine();

        var results = library.Where(b =>
            b.Title.Contains(query, StringComparison.OrdinalIgnoreCase) ||
            b.Author.Contains(query, StringComparison.OrdinalIgnoreCase));

        if (!results.Any())
        {
            Console.WriteLine("Ei hakutuloksia.");
            return;
        }

        foreach (var book in results)
        {
            Console.WriteLine(book);
        }
    }
}