using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SnakeGame.Interfaces;

namespace SnakeGame
{
    public class Canvas : ICanvas
    {
        public int Width { get; set; }
        public int Height { get; set; }

        //Sätter height och width på spelbrädet
        public Canvas()
        {
            Width = Console.WindowWidth - 10;
            Height = Console.WindowHeight - 10;

            Console.CursorVisible = false;
        }

        public Canvas(string constructorForTest)
        {

        }


        public int getWidth()
        {
            return Width;
        }

        public int getHeight()
        {
            return Height;
        }

        //Skriver ut boarden
        public void drawCanvas()
        {
            Console.Clear();

            for (int i = 0; i < Width; i++)
            {
                Console.SetCursorPosition(i, 0);
                Console.Write("#");
            }

            for (int i = 0; i < Width; i++)
            {
                Console.SetCursorPosition(i, Height);
                Console.Write("#");
            }

            for (int i = 0; i < Height; i++)
            {
                Console.SetCursorPosition(0, i);
                Console.Write("#");
            }

            for (int i = 0; i < Height; i++)
            {
                Console.SetCursorPosition(Width, i);
                Console.Write("#");
            }
        }
    }
}