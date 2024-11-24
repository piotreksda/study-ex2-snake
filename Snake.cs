using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

class Program
{
    static void Main()
    {
        Console.WindowHeight = 16;
        Console.WindowWidth = 32;
        int screenWidth = Console.WindowWidth;
        int windowHeight = Console.WindowHeight;
        Random randomNumber = new Random();
        Pixel pixel = new Pixel
        {
            XPos = screenWidth / 2,
            YPos = windowHeight / 2,
            Colour = ConsoleColor.Red
        };

        string movement = "RIGHT";
        List<int> fields = new List<int>();
        int score = 0;
        List<int> fieldsPositions = new List<int>();
        fieldsPositions.Add(pixel.XPos);
        fieldsPositions.Add(pixel.YPos);
        Obstacle obstacle = new Obstacle
        {
            XPos = randomNumber.Next(1, screenWidth),
            YPos = randomNumber.Next(1, windowHeight),
            Character = "#",
            Colour = ConsoleColor.DarkRed
        };
        
        while (true)
        {
            Console.Clear();
            //Draw Obstacle
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.SetCursorPosition(obstacle.XPos, obstacle.YPos);
            Console.Write(obstacle.Character);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.SetCursorPosition(pixel.XPos, pixel.YPos);
            Console.Write("■");
            
            Console.ForegroundColor = ConsoleColor.White;
            for (int i = 0; i < screenWidth; i++)
            {
                Console.SetCursorPosition(i, 0);
                Console.Write("■");
            }
            for (int i = 0; i < screenWidth; i++)
            {
                Console.SetCursorPosition(i, windowHeight - 1);
                Console.Write("■");
            }

            for (int i = 0; i < windowHeight; i++)
            {
                Console.SetCursorPosition(0, i);
                Console.Write("■");
            }

            for (int i = 0; i < windowHeight; i++)
            {
                Console.SetCursorPosition(screenWidth - 1, i);
                Console.Write("■");
            }

            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("Score: " + score);
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("H");
            for (int i = 0; i < fields.Count(); i++)
            {
                Console.SetCursorPosition(fields[i], fields[i + 1]);
                Console.Write("■");
            }
            //Draw Snake
            Console.SetCursorPosition(pixel.XPos, pixel.YPos);
            Console.Write("■");
            Console.SetCursorPosition(pixel.XPos, pixel.YPos);
            Console.Write("■");
            Console.SetCursorPosition(pixel.XPos, pixel.YPos);
            Console.Write("■");
            Console.SetCursorPosition(pixel.XPos, pixel.YPos);
            Console.Write("■");
            ConsoleKeyInfo info = Console.ReadKey();
            //Game Logic
            movement = info.Key switch
            {
                ConsoleKey.UpArrow => "UP",
                ConsoleKey.DownArrow => "DOWN",
                ConsoleKey.LeftArrow => "LEFT",
                ConsoleKey.RightArrow => "RIGHT",
                _ => movement
            };
            
            switch (movement)
            {
                case "UP":
                    pixel.YPos--;
                    break;
                case "DOWN":
                    pixel.YPos++;
                    break;
                case "LEFT":
                    pixel.XPos--;
                    break;
                case "RIGHT":
                    pixel.XPos++;
                    break;
            }

            //Hindernis treffen
            if (pixel.XPos == obstacle.XPos && pixel.YPos == obstacle.YPos)
            {
                score++;
                obstacle.XPos= randomNumber.Next(1, screenWidth);
                obstacle.YPos = randomNumber.Next(1, windowHeight);
            }

            fieldsPositions.Insert(0, pixel.XPos);
            fieldsPositions.Insert(1, pixel.YPos);
            fieldsPositions.RemoveAt(fieldsPositions.Count - 1);
            fieldsPositions.RemoveAt(fieldsPositions.Count - 1);
            //Kollision mit Wände oder mit sich selbst
            if (pixel.XPos == 0 || pixel.XPos == screenWidth - 1 || pixel.YPos == 0 || pixel.YPos == windowHeight - 1)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.SetCursorPosition(screenWidth / 5, windowHeight / 2);
                Console.WriteLine("Game Over");
                Console.SetCursorPosition(screenWidth / 5, windowHeight / 2 + 1);
                Console.WriteLine("Dein Score ist: " + score);
                Console.SetCursorPosition(screenWidth / 5, windowHeight / 2 + 2);
                Environment.Exit(0);
            }
            for (int i = 0; i < fields.Count(); i += 2)
            {
                if (pixel.XPos == fields[i] && pixel.YPos == fields[i + 1])
                {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.SetCursorPosition(screenWidth / 5, windowHeight / 2);
                    //???
                    Console.SetCursorPosition(screenWidth / 5, windowHeight / 2 + 1);
                    Console.WriteLine("Dein Score ist: " + score);
                    Console.SetCursorPosition(screenWidth / 5, windowHeight / 2 + 2);
                    Environment.Exit(0);
                }
            }

            Thread.Sleep(50);
        }
    }
}




