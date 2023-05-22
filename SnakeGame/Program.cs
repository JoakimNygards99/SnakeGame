using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SnakeGame;
using SnakeGame.DataBaseFiles;
using SnakeGame.Interfaces;

namespace SnakeGame
{
    class Program
    {
        static void Main(string[] args)
        {
          
            
            bool finished = false;
            PlayerScoreDataAccess DataAccess = new PlayerScoreDataAccess(new SqliteDataAccess());
            PlayerScoreModel currentPlayer = new PlayerScoreModel();

            Position startSnakePosition = new Position();
            Position foodPosition = new Position();

            Position eatSnakePosition = new Position();
            Canvas canvas = new Canvas();

            Snake snake = new Snake(startSnakePosition);
            Food food = new Food(foodPosition ,canvas);
            Mine mine = new Mine(new Position() ,canvas);


            //######################################StartGame############################################################################################################################

            MenuDisplay.DisplayMenu();
            string menuChoice = Console.ReadLine();
            if(menuChoice == "2")
            {
                var players = DataAccess.LoadPlayers();
                foreach(var p in players)
                {
                    Console.WriteLine($"Name: {p.Name}, Score: {p.Score}, Date: {p.Date}");
                }
            }

            else if(menuChoice == "1")
            {
                Console.Write("Enter your PlayerName: ");
                string playerName = Console.ReadLine();
                currentPlayer.Name = playerName;

                Console.Clear();
                MenuDisplay.DisplayStartGameMenu();
                string choiceColor = Console.ReadLine();
                Console.Read();
                while (!finished)
                {
                    
                    try
                    {

                        canvas.drawCanvas();
                        Console.SetCursorPosition(90, 20);
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write("SCORE : {0}", snake.score);
                        Console.ForegroundColor = ConsoleColor.White;
                        snake.Input();
                        food.drawFood();
                        mine.drawMine();

                        snake.drawSnake(choiceColor);
                        Position snakeMovePosition = new Position();
                        snake.moveSnake(snakeMovePosition);
                        snake.eat(food.foodLocation(), food, eatSnakePosition, mine);

                        snake.hitMine(mine);
                        snake.isDead();
                        snake.hitWall(canvas);
                    }
                    catch (SnakeException e)
                    {
                        Console.Clear();
                        Console.WriteLine(e.Message);

                        Console.WriteLine("Restart y/n");

                        string c = Console.ReadLine();

                        switch (c)
                        {
                            case "y":

                                foodPosition = new Position();
                                food = new Food(foodPosition, canvas);
                                mine = new Mine(new Position(), canvas);
                                snake.x = 10;
                                snake.y = 10;
                                snake.score = 0;
                                snake.snakeBody.RemoveRange(0, snake.snakeBody.Count - 1);
                                break;

                            case "n":
                                finished = true;
                                currentPlayer.Score = snake.score;
                                currentPlayer.Date = DateTime.Now.ToString();
                                DataAccess.SavePlayer(currentPlayer);
                                break;
                        }
                    }
                }

                
            }
            


        }
    }
}