using SnakeGame.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeGame
{
    public class Mine : IMine
    {
        public IPosition _minePos { get; set; }
        Random rnd = new Random();
        private ICanvas _canvas;

        public List<Dictionary<int, int>> mineList { get; set; }

        public Mine(IPosition minePosInput, ICanvas canvasInput)
        {
            mineList = new List<Dictionary<int, int>>();
            _minePos = minePosInput;
            _canvas = canvasInput;
            for(int i = 0; i < 6; i++)
            {
                int x = rnd.Next(5, _canvas.getWidth());
                int y = rnd.Next(5, _canvas.getHeight());
                var myDict = new Dictionary<int, int>();
                myDict.Add(x, y);
                mineList.Add(myDict);
            }
        }

        public void drawMine()
        {
            foreach(var mine in mineList)
            {
                foreach (var m in mine)
                {
                    Console.SetCursorPosition(m.Key, m.Value);
                    Console.OutputEncoding = System.Text.Encoding.UTF8;
                    Console.Write("💣");
                }
            } 
        }
    }
}
