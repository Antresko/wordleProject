using System;
using System.Linq;
using System.Text;
using System.IO;

namespace Wordle
{
    class Wordle
    {

        private string word;
        private string wordChecked;
        static private string track = "D:\\WordleProject\\Wordle\\words_1_cleaned1.txt";
        private string[] theRigthWords = File.ReadAllLines(track);
        public int steps = 0;
        private bool fine = true;
        public bool activeGame = false;

        int slicz;


        public string Word
        {
            get { return word; }
            set { word = value; }
        }

        public string WordCh
        {
            get { return wordChecked; }
            set { wordChecked = value; }
        }

        public void EndGame()
        {
            activeGame = false;
        }

        public void SetWord(string typedWord)
        {
            if (!activeGame)
            {
                if (string.IsNullOrWhiteSpace(typedWord))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("*** Slowo nie moze byc puste! ***");
                    Console.ResetColor();
                    return;
                }

                activeGame = true;
                word = typedWord.ToUpper();

                Console.WriteLine("** Gra rozpoczęta. Powodzenia! **");
                Console.WriteLine("** Długość słowa to " + word.Length + " **\n");

                Console.Write("  ");
                for (int i = 0; i < word.Length; i++)
                {
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.Write(" [ ]");
                }
                Console.ResetColor();
                Console.WriteLine();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("*** Gra już istnieje! ***");
                Console.ResetColor();
            }
        }

        private void CheckWord(string wordToCheck)
        {
            fine = true;
            bool repeat = false;

            if (!activeGame)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("*** Gra nie jest rozpoczęta! ***");
                Console.ResetColor();
                fine = false;
                return;
            }

            wordChecked = wordToCheck.ToUpper();

            if (string.IsNullOrWhiteSpace(wordChecked))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("*** Wymagane podanie słowa! ***");
                Console.ResetColor();
                fine = false;
                return;
            }

            if (wordChecked.Length != word.Length)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("*** Słowa nie mają takiej samej długości! ***");
                Console.ResetColor();
                fine = false;
                return;
            }
            Console.WriteLine("Dlugosc tablicy slow to: " + theRigthWords.Length);
            wordToCheck = wordToCheck.ToLower();

            if (!File.Exists(track))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("*** Problem z dostępem do bazy słów, przepraszamy ***");
                Console.ResetColor();
                return;
            }

            for (int s = 0; s < theRigthWords.Length; s++)
            {
                // Console.WriteLine(theRigthWords[s]);
                if (wordToCheck == theRigthWords[s])
                {
                    repeat = true;
                    break;
                }
                slicz = s;
            }
            Console.WriteLine("Ilosc przeszukanych slow: " + slicz);
            if (!repeat)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("*** Wpisane słowo nie istnieje! ***");
                Console.ResetColor();
                fine = false;
                return;
            }


            foreach (char c in wordChecked)
            {

                if (!char.IsLetter(c))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("*** Słowo musi się składać tylko z liter! ***");
                    Console.ResetColor();
                    fine = false;
                    break;
                }
            }
        }

        public void CompareWords(string wordToCheck)
        {
            CheckWord(wordToCheck);
            if (!fine) return;

            steps++;
            Console.Write("   ");

            for (int i = 0; i < wordChecked.Length; i++)
            {
                bool presense = false;
                for (int j = 0; j < word.Length; j++)
                {
                    if (wordChecked[i] == word[j])
                    {
                        if (i == j)
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.DarkYellow;
                        }
                        Console.Write(" " + wordChecked[i] + " ");
                        Console.ResetColor();
                        presense = true;
                        break;
                    }
                }

                if (!presense)
                {
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write(" " + wordChecked[i] + " ");
                    Console.ResetColor();
                }
            }
            Console.WriteLine();
        }

        public void Results()
        {
            if (word == wordChecked)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("\n**************************");
                Console.WriteLine("*** Zgadłeś/aś! Brawo! ***\n");

                Console.WriteLine("* Główne słowo to '" + word + "' * Ilość prób: " + steps + " *");
                Console.ResetColor();
            }
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            Console.InputEncoding = Encoding.UTF8;
            Console.OutputEncoding = Encoding.UTF8;

            Wordle wordle = new Wordle();

            wordle.SetWord("poscig");

            string userInput;
            while (wordle.Word != wordle.WordCh)
            {
                Console.Write("\n* Podaj słowo: ");
                userInput = Console.ReadLine();
                wordle.CompareWords(userInput);
            }

            wordle.Results();
            wordle.EndGame();

            Console.WriteLine("Naciśnij dowolny klawisz aby zakończyć...");
            Console.ReadKey();
        }
    }
}

