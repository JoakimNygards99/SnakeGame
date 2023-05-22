using SnakeGame.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace SnakeGame
{
    public class Snake
    {
        ConsoleKeyInfo KeyInfo = new ConsoleKeyInfo();
        public char key = 'w';
        public char dir = 'u';

        public List<IPosition> snakeBody { get; set; }

        public int x { get; set; }
        public int y { get; set; }
        public int score { get; set; }

        public Snake(IPosition position)
        {
            x = 10;
            y = 10;
            score = 0;

            snakeBody = new List<IPosition>();
            position.x = x;
            position.y = y;
            snakeBody.Add(position);
        }

        public void drawSnake(string choice)
        {
            switch (choice)
            {
                case "1":
                    Console.ForegroundColor = ConsoleColor.Green;
                    break;
                case "2":
                    Console.ForegroundColor = ConsoleColor.Red;
                    break;
                case "3":
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    break;
                case "4":
                    Console.ForegroundColor = ConsoleColor.Blue;
                    break;
                default:
                    break;
            }
            foreach (IPosition pos in snakeBody)
            {
                Console.SetCursorPosition(pos.x, pos.y);
                Console.Write("O");
            }
            Console.ForegroundColor = ConsoleColor.White;
        }

        public void Input()
        {
            if (Console.KeyAvailable)
            {
                KeyInfo = Console.ReadKey(true);
                key = KeyInfo.KeyChar;
            }
        }

        private void direction()
        {
            if (key == 'w' && dir != 'd')
            {
                dir = 'u';
            }
            else if (key == 's' && dir != 'u')
            {
                dir = 'd';
            }
            else if (key == 'd' && dir != 'l')
            {
                dir = 'r';
            }
            else if (key == 'a' && dir != 'r')
            {
                dir = 'l';
            }
        }
        public void moveSnake(IPosition position)
        {
            direction();

            if (dir == 'u')
            {
                y--;
            }
            else if (dir == 'd')
            {
                y++;
            }
            else if (dir == 'r')
            {
                x++;
            }
            else if (dir == 'l')
            {
                x--;
            }

            position.x = x;
            position.y = y;
            snakeBody.Add(position);
            snakeBody.RemoveAt(0);
            Thread.Sleep(50);
        }
        public void hitMine(IMine mine)
        {
            IPosition head = snakeBody[snakeBody.Count - 1];

                foreach (var item in mine.mineList)
                {
                    foreach (var i in item)
                    {
                        if (head.x == i.Key && head.y == i.Value)
                        {
                            throw new SnakeException("YOU LOST");
                        }
                    }
                }
        }

        public void eat(IPosition food, IFood f, IPosition position, IMine mine)
        {
            IPosition head = snakeBody[snakeBody.Count - 1];

            if (head.x == food.x && head.y == food.y)
            {
                position.x = x;
                position.y = y;
                snakeBody.Add(position);
                f.foodNewLocation(mine);
                score++;
            }
        }
        public void isDead()
        {
            IPosition head = snakeBody[snakeBody.Count - 1];

            for (int i = 0; i < snakeBody.Count - 2; i++)
            {
                IPosition sb = snakeBody[i];

                if (head.x == sb.x && head.y == sb.y)
                {
                    throw new SnakeException("YOU LOST");
                }
            }
        }
        public void hitWall(ICanvas canvas)
        {
            IPosition head = snakeBody[snakeBody.Count - 1];
            var width = canvas.getWidth();
            var height = canvas.getHeight();

            if (head.x >= width || head.x <= 0 || head.y >= height || head.y <= 0)
            {
                throw new SnakeException("YOU LOST");
            }
        }
    }
}