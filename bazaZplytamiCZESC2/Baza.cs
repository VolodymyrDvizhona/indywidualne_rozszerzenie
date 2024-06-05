using System;
using System.Collections.Generic;
using static BazaZplytami.Baza;

namespace BazaZplytami
{
    public class Baza
    {
        public struct Utwor
        {
            public string Nazwa;
            public double CzasTrwania;
            public int Sekundy;
            public List<string> Wykonawcy;
            public string Kompozytor;
            public double Nr;

            public Utwor(string nazwa, double czasTrwania, int sekundy, string kompozytor, double nr, List<string> wykonawcy = null)
            {
                Nazwa = nazwa;
                CzasTrwania = czasTrwania;
                Sekundy = sekundy;
                Kompozytor = kompozytor;
                Nr = nr;
                Wykonawcy = wykonawcy ?? new List<string>();
            }
        }

        public struct Plyta
        {
            public string Tytul;
            public string Typ;
            public double TotalCzas;
            public List<Utwor> Utwory;
            public List<string> Wszystkie;

            public Plyta(string tytul, string typ)
            {
                Tytul = tytul;
                Typ = typ;
                TotalCzas = 0;
                Utwory = new List<Utwor>();
                Wszystkie = new List<string>();
            }

            public void WprowadzPlyte()
            {
                do
                {
                    try
                    {
                        Console.Write("Podaj tytuł płyty: ");
                        Tytul = Console.ReadLine();

                        Console.Write("Podaj typ płyty (CD/DVD): ");
                        Typ = Console.ReadLine();
                        if (Typ.ToLower() != "cd" && Typ.ToLower() != "dvd")
                        {
                            throw new Exception("Niepoprawny typ dysku");
                        }

                        for (int i = 0; i < 30; i++)
                        {
                            Console.WriteLine($"\nWprowadzanie utworu {i + 1}:");
                            var utwor = new Utwor();

                            utwor.Nr = i + 1;

                            Console.Write("Podaj tytul utworu: ");
                            utwor.Nazwa = Console.ReadLine();

                            Console.Write("Podaj czas trwania utworu (w minutach): ");
                            utwor.CzasTrwania = Convert.ToDouble(Console.ReadLine());

                            Console.Write("Podaj czas trwania utworu (w sekundach): ");
                            utwor.Sekundy = Convert.ToInt32(Console.ReadLine());

                            int dodatkoweMinuty = utwor.Sekundy / 60;
                            int pozostaleSekundy = utwor.Sekundy % 60;
                            utwor.CzasTrwania += dodatkoweMinuty;
                            utwor.Sekundy = pozostaleSekundy;

                            TotalCzas += utwor.CzasTrwania + dodatkoweMinuty;

                            Console.Write("Podaj kompozytora utworu: ");
                            utwor.Kompozytor = Console.ReadLine();

                            Console.Write("Podaj liczbę wykonawców utworu: ");
                            int liczbaWykonawcow = Convert.ToInt32(Console.ReadLine());
                            utwor.Wykonawcy = new List<string>();
                            for (int j = 0; j < liczbaWykonawcow; j++)
                            {
                                Console.Write($"Podaj wykonawcę {j + 1}: ");
                                string lokalna = Console.ReadLine();
                                utwor.Wykonawcy.Add(lokalna);
                                Wszystkie.Add(lokalna);
                            }

                            Utwory.Add(utwor);

                            Console.Write("\nCzy chcesz dodać kolejny utwór? (T/N): ");
                            string odpowiedz = Console.ReadLine();
                            if (odpowiedz.ToLower() != "t")
                            {
                                break;
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                    break;
                }
                while (true);
            }

            public void WypiszUtworSzczegoly()
            {
                while (true)
                {
                    Console.Write("\nPodaj numer utworu: ");
                    int index = Convert.ToInt32(Console.ReadLine());
                    index--;
                    if (index >= 0 && index < Utwory.Count)
                    {
                        Utwor utwor = Utwory[index];
                        Console.WriteLine($"\nSzczegóły utworu {index + 1}:");
                        Console.WriteLine($"Tytul: {utwor.Nazwa}, Czas trwania: {utwor.CzasTrwania}:{utwor.Sekundy:D2}");
                        Console.Write("Wykonawca(-y): ");
                        foreach (var wykonawca in utwor.Wykonawcy)
                        {
                            Console.Write($"{wykonawca}, ");
                        }
                        Console.WriteLine($"\nKompozytor: {utwor.Kompozytor}");
                    }
                    else
                    {
                        Console.WriteLine("Niepoprawny numer utworu.");
                    }

                    Console.Write("\nChcesz wyswietlic szczegóły kolejnego utworu? (T/N): ");
                    char odpowiedz1 = Console.ReadKey().KeyChar;
                    if (odpowiedz1 != 't' && odpowiedz1 != 'T')
                    {
                        break;
                    }
                }
            }

            public void WypiszPlyteSzczegoly()
            {
                Console.Clear();
                Console.WriteLine($"Tytuł płyty: {Tytul}");
                Console.WriteLine($"Typ płyty: {Typ}");
                Console.WriteLine($"Czas trwania płyty: {TotalCzas} minut");
                Console.WriteLine("\nLista utworów:");
                foreach (var utwor in Utwory)
                {
                    Console.WriteLine($"{utwor.Nr}. {utwor.Nazwa} {utwor.CzasTrwania}:{utwor.Sekundy:D2}");
                }
                Console.WriteLine($"Lista wykonawców na płycie {Tytul}:");
                foreach (var wykonawca in Wszystkie)
                {
                    Console.WriteLine($"- {wykonawca}");
                }
            }
        }
    }
}
