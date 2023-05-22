using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeGame.Interfaces
{
    public interface ISnake
    {
        public int score { get; set; }
        List<IPosition> snakeBody { get; set; }
        void drawSnake(string colour);
        void Input();
        void direction();
        void moveSnake();
        void eat(IPosition food, IFood f);
        void isDead();
        void hitWall(ICanvas canvas);
    }
}
