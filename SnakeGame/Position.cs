using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SnakeGame.Interfaces;

namespace SnakeGame
{
    public class Position : IPosition
    {
        public int x { get; set; }
        public int y { get; set; }

        public Position()
        {

        }

        public Position(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public override string ToString()
        {
            return $"{x},{y}";
        }
    }
}
