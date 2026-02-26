using System;

namespace HangMan
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string[] words = {"orbit","mango","lantern","whisper","pixel","drift","cactus","velvet", "echo","harbor","spark","nebula","timber","ripple","compass","fable","crystal","breeze","glyph","meadow","quartz","amber","lunar","pollen","voyage","inkwell","saffron","mythic","thistle","zephyr"};
            Random random = new Random();
            string word = words[random.Next(words.Length)];
            //Console.WriteLine(word);
            int lives = 6;
            HashSet<char> guessed = new HashSet<char>();
            while (lives > 0)
            {
                Console.WriteLine($"Lives: {lives}");
                Console.WriteLine("Guessed letters: ");
                foreach (char c in guessed)
                {
                    Console.Write(c + " ");
                }
                Console.WriteLine("Word: ");
                for (int i = 0; i < word.Length; i++)
                {
                    if (guessed.Contains(word[i]))
                    {
                        Console.Write($"{word[i]} ");
                    }
                    else
                    {
                        Console.Write("_ ");
                    }
                }
                Console.WriteLine();
                Console.WriteLine("Enter a letter: ");
                string input = Console.ReadLine();
                if (string.IsNullOrEmpty(input)) continue;
                char guess = input[0];
                if (!char.IsLetter(guess))
                {
                    Console.WriteLine("Please enter a valid letter.");
                    continue;
                }
                if (guessed.Contains(guess))
                {
                    Console.WriteLine("Already guessed. retry");
                    continue;
                }
                guessed.Add(guess);
                if (!word.Contains(guess))
                {
                    lives--;
                    Console.WriteLine("Wrong guess");
                }
                bool won = true;
                foreach (char c in word)
                {
                    if (!guessed.Contains(c))
                    {
                        won = false;
                        break;
                    }
                }

                if (won)
                {
                    Console.WriteLine("You won!");
                    Console.WriteLine($"The word was: {word}");
                    break;
                }

            }


        }
    }
}

