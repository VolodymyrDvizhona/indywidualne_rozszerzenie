using BazaZplytami;
using System;
using System.Collections.Generic;
using System.IO;

namespace Program
{
    class Program
    {
        static List<Baza.Plyta> bazaPlyt = new List<Baza.Plyta>();

        static void Main(string[] args)
        {
            char c;
            do
            {
                c = Menu();
                switch (c)
                {
                    case 'a':
                    case 'A':
                        DodajPlyte();
                        break;
                    case 'b':
                    case 'B':
                        WypiszTytulyPlyt();
                        break;
                    case 'c':
                    case 'C':
                        WyswietlSzczegolyPlyty();
                        break;
                    case 'd':
                    case 'D':
                        ZapiszBazeDoPliku();
                        break;
                }
            }
            while (c != 'k' && c != 'K');
        }

        static char Menu()
        {
            Console.Clear();
            Console.WriteLine("\n\t\tA - Dodaj płytę");
            Console.WriteLine("\n\t\tB - Wypisz wszystkie tytuły płyt");
            Console.WriteLine("\n\t\tC - Wyświetl szczegóły płyty");
            Console.WriteLine("\n\t\tD - Zapisz bazę do pliku");
            Console.WriteLine("\n\t\tK - Zakończ program");
            return Console.ReadKey().KeyChar;
        }

        static void DodajPlyte()
        {
            Baza.Plyta plyta = new Baza.Plyta("Nieznany tytul", "CD");
            plyta.WprowadzPlyte();
            bazaPlyt.Add(plyta);
        }

        static void WypiszTytulyPlyt()
        {
            Console.Clear();
            Console.WriteLine("Lista tytułów płyt:");
            for (int i = 0; i < bazaPlyt.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {bazaPlyt[i].Tytul}");
            }
            Console.WriteLine("Naciśnij dowolny klawisz aby kontynuować...");
            Console.ReadKey();
        }

        static void WyswietlSzczegolyPlyty()
        {
            Console.Clear();
            Console.WriteLine("Podaj numer płyty do wyświetlenia szczegółów:");
            int numerPlyty;
            if (int.TryParse(Console.ReadLine(), out numerPlyty) && numerPlyty > 0 && numerPlyty <= bazaPlyt.Count)
            {
                var plyta = bazaPlyt[numerPlyty - 1];
                Console.WriteLine($"Szczegóły płyty {plyta.Tytul}:");
                plyta.WypiszPlyteSzczegoly();
            }
            else
            {
                Console.WriteLine("Niepoprawny numer płyty.");
            }
            Console.Write("Chcesz wyświetlić szczegóły konkretnego utworu? (T/N): ");
            char tn = Console.ReadKey().KeyChar;
            if (tn == 'T' || tn == 't')
            {
                bazaPlyt[numerPlyty - 1].WypiszUtworSzczegoly();
            }

            Console.WriteLine("\nNaciśnij dowolny klawisz aby kontynuować...");
            Console.ReadKey();
        }

        static void ZapiszBazeDoPliku()
        {
            using (StreamWriter sw = new StreamWriter("baza.txt"))
            {
                foreach (var plyta in bazaPlyt)
                {
                    sw.WriteLine("Tytul: " + plyta.Tytul);
                    sw.WriteLine("Typ: " + plyta.Typ);
                    sw.WriteLine("Czas trwania: " + plyta.TotalCzas + " minut");
                    sw.WriteLine("\n");
                    foreach (var utwor in plyta.Utwory)
                    {
                        sw.WriteLine(utwor.Nazwa + " " + utwor.CzasTrwania + ":" + utwor.Sekundy.ToString("D2"));
                        sw.WriteLine(string.Join(", ", utwor.Wykonawcy));
                        sw.WriteLine(utwor.Kompozytor);
                        sw.WriteLine("ID: " + utwor.Nr);
                        sw.WriteLine("\n");
                    }
                }
            }
            Console.WriteLine("Baza została zapisana do pliku.");
            Console.WriteLine("Naciśnij dowolny klawisz aby kontynuować...");
            Console.ReadKey();
        }
    }
}

