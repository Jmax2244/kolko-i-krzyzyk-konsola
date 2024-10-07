using System;
using System.IO;

class KolkoKrzyzyk
{
    static char[,] plansza = new char[5, 5];
    static char gracz = 'X';
    static char komputer = 'O';


    static string sciezka = "C:\\Users\\3988200\\Desktop\\wynik\\wynik.txt"; 

    static void Main(string[] args)
    {
    
        PokazOstatniegoZwyciezce();

     
        bool czyGraTrwa = true;
        while (czyGraTrwa)
        {
            WyczyscPlansze();
            Graj();
            czyGraTrwa = CzyChceszKontynuowac();
        }
    }

    static void PokazOstatniegoZwyciezce()
    {
        if (File.Exists(sciezka))
        {
            string ostatniZwyciezca = File.ReadAllText(sciezka);
            Console.WriteLine($"Ostatnia gra wygrał: {ostatniZwyciezca}");
        }
        else
        {
            Console.WriteLine("Brak wyników z poprzednich gier.");
        }
    }

    static void WyczyscPlansze()
    {
        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < 5; j++)
            {
                plansza[i, j] = '-';
            }
        }
    }

    static void WyswietlPlansze()
    {
        Console.Clear();
        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < 5; j++)
            {
                Console.Write(plansza[i, j] + " ");
            }
            Console.WriteLine();
        }
    }

    static void Graj()
    {
        bool graTrwa = true;
        while (graTrwa)
        {
            WyswietlPlansze();
            RuchGracza();
            if (SprawdzWygrana(gracz))
            {
                WyswietlPlansze();
                Console.WriteLine("Gratulacje! Wygrałeś!");
                ZapiszZwyciezce("Człowiek");
                break;
            }
            if (CzyRemis())
            {
                WyswietlPlansze();
                Console.WriteLine("Remis!");
                break;
            }

            RuchKomputera();
            if (SprawdzWygrana(komputer))
            {
                WyswietlPlansze();
                Console.WriteLine("Komputer wygrał!");
                ZapiszZwyciezce("Komputer");
                break;
            }
        }
    }

    static void RuchGracza()
    {
        int wiersz, kolumna;
        bool poprawnyRuch = false;

        while (!poprawnyRuch)
        {
            Console.WriteLine("Twój ruch! Podaj numer wiersza (1-5) i kolumny (1-5): ");
            wiersz = int.Parse(Console.ReadLine()) - 1;
            kolumna = int.Parse(Console.ReadLine()) - 1;

            if (wiersz >= 0 && wiersz < 5 && kolumna >= 0 && kolumna < 5 && plansza[wiersz, kolumna] == '-')
            {
                plansza[wiersz, kolumna] = gracz;
                poprawnyRuch = true;
            }
            else
            {
                Console.WriteLine("Nieprawidłowy ruch, spróbuj ponownie.");
            }
        }
    }

    static void RuchKomputera()
    {
        Random rnd = new Random();
        int wiersz, kolumna;
        bool poprawnyRuch = false;

        while (!poprawnyRuch)
        {
            wiersz = rnd.Next(0, 5);
            kolumna = rnd.Next(0, 5);

            if (plansza[wiersz, kolumna] == '-')
            {
                plansza[wiersz, kolumna] = komputer;
                poprawnyRuch = true;
            }
        }

        Console.WriteLine("Komputer wykonał ruch.");
    }

    static bool SprawdzWygrana(char znak)
    {
      
        for (int i = 0; i < 5; i++)
        {
            if (SprawdzLinie(znak, i, 0, 0, 1) || SprawdzLinie(znak, 0, i, 1, 0))
            {
                return true;
            }
        }

      
        if (SprawdzLinie(znak, 0, 0, 1, 1) || SprawdzLinie(znak, 0, 4, 1, -1))
        {
            return true;
        }

        return false;
    }

    static bool SprawdzLinie(char znak, int startWiersz, int startKolumna, int deltaWiersz, int deltaKolumna)
    {
        for (int i = 0; i < 5; i++)
        {
            if (plansza[startWiersz + i * deltaWiersz, startKolumna + i * deltaKolumna] != znak)
            {
                return false;
            }
        }
        return true;
    }

    static bool CzyRemis()
    {
        foreach (char pole in plansza)
        {
            if (pole == '-')
                return false;
        }
        return true;
    }

    static void ZapiszZwyciezce(string zwyciezca)
    {
        File.WriteAllText(sciezka, zwyciezca);
    }

    static bool CzyChceszKontynuowac()
    {
        Console.WriteLine("Czy chcesz zagrać ponownie? (t/n)");
        string odpowiedz = Console.ReadLine();
        return odpowiedz.ToLower() == "t";
    }
}
