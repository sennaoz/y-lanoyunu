using System;
using System.Collections.Generic;
using System.Threading;

class YilanOyunu
{
    static int boardWidth = 30;
    static int boardHeight = 20;
    static int yilanX = boardWidth / 2;
    static int yilanY = boardHeight / 2;
    static List<(int x, int y)> yilanParcalari = new List<(int x, int y)>();
    static int yemX, yemY;
    static int skor = 0;
    static bool oyunBitti = false;
    static string yon = "SAĞ";
    static Random random = new Random();

    static void Main(string[] args)
    {
        Console.CursorVisible = false;
        IlkYemiOlustur();
        while (!oyunBitti)
        {
            YilanHareket();
            EkraniCiz();
            if (yilanX == yemX && yilanY == yemY)
            {
                skor++;
                YemiYediktenSonra();
                IlkYemiOlustur();
            }
            if (YilanKendineCarptiMi())
            {
                oyunBitti = true;
            }
            Thread.Sleep(100); // Yılanın hızını ayarlamak için
        }
        Console.Clear();
        Console.WriteLine("Oyun Bitti! Skorunuz: " + skor);
    }

    static void EkraniCiz()
    {
        Console.Clear();
        for (int i = 0; i < boardHeight; i++)
        {
            for (int j = 0; j < boardWidth; j++)
            {
                if (i == 0 || i == boardHeight - 1 || j == 0 || j == boardWidth - 1)
                {
                    Console.Write("#"); // Duvarlar
                }
                else if (i == yilanY && j == yilanX)
                {
                    Console.Write("O"); // Yılanın başı
                }
                else if (i == yemY && j == yemX)
                {
                    Console.Write("@"); // Yem
                }
                else if (YilanParcasiMi(j, i))
                {
                    Console.Write("o"); // Yılanın vücudu
                }
                else
                {
                    Console.Write(" ");
                }
            }
            Console.WriteLine();
        }
        Console.WriteLine("Skor: " + skor);
    }

    static void YilanHareket()
    {
        if (Console.KeyAvailable)
        {
            var key = Console.ReadKey(true).Key;
            switch (key)
            {
                case ConsoleKey.UpArrow:
                    if (yon != "AŞAĞI") yon = "YUKARI";
                    break;
                case ConsoleKey.DownArrow:
                    if (yon != "YUKARI") yon = "AŞAĞI";
                    break;
                case ConsoleKey.LeftArrow:
                    if (yon != "SAĞ") yon = "SOL";
                    break;
                case ConsoleKey.RightArrow:
                    if (yon != "SOL") yon = "SAĞ";
                    break;
            }
        }

        yilanParcalari.Insert(0, (yilanX, yilanY));

        switch (yon)
        {
            case "YUKARI":
                yilanY--;
                break;
            case "AŞAĞI":
                yilanY++;
                break;
            case "SOL":
                yilanX--;
                break;
            case "SAĞ":
                yilanX++;
                break;
        }

        if (yilanParcalari.Count > skor + 1)
        {
            yilanParcalari.RemoveAt(yilanParcalari.Count - 1);
        }

        if (yilanX == 0 || yilanX == boardWidth - 1 || yilanY == 0 || yilanY == boardHeight - 1)
        {
            oyunBitti = true;
        }
    }

    static void IlkYemiOlustur()
    {
        yemX = random.Next(1, boardWidth - 1);
        yemY = random.Next(1, boardHeight - 1);
    }

    static bool YilanParcasiMi(int x, int y)
    {
        foreach (var parca in yilanParcalari)
        {
            if (parca.x == x && parca.y == y)
            {
                return true;
            }
        }
        return false;
        
    }


    static void YemiYediktenSonra()
    {
        yilanParcalari.Add((yilanX, yilanY));
    }

    static bool YilanKendineCarptiMi()
    {
        for (int i = 1; i < yilanParcalari.Count; i++)
        {
            if (yilanParcalari[i].x == yilanX && yilanParcalari[i].y == yilanY)
            {
                return true;
            }
        }
        return false;
    }
}
