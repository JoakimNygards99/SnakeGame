using SnakeGame.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeGame
{
    public class Food : IFood
    {
        //public Position foodPos = new Position();
        //Canvas canvas = new Canvas();
        public IPosition _foodPos { get; set; }
        Random rnd = new Random();
        private ICanvas _canvas;
        
        public Food(IPosition foodPosInput, ICanvas canvasInput)
        {
            _foodPos = foodPosInput;
            _canvas = canvasInput;
            _foodPos.x = rnd.Next(5, _canvas.getWidth());
            _foodPos.y = rnd.Next(5, _canvas.getHeight());
        }

        //Denna används för testning
        public Food(IPosition position)
        {
            //test constructor
            position.x = 10;
            position.y = 10;
            _foodPos = position;

        }

        public void drawFood()
        {
            Console.SetCursorPosition(_foodPos.x, _foodPos.y);
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.Write("🌭");
        }

        public IPosition foodLocation()
        {
            return _foodPos;
        }

        public void foodNewLocation(IMine mine)
        {
            _foodPos.x = rnd.Next(5, _canvas.getWidth());
            _foodPos.y = rnd.Next(5, _canvas.getHeight());

            //Om maten sätts där det redan finns en mina så körs metoden igen
            foreach (var item in mine.mineList)
            {
                foreach (var mineLocation in item)
                {
                    if(_foodPos.x == mineLocation.Key && _foodPos.y == mineLocation.Value)
                    {
                        foodNewLocation(mine);
                    }
                }
            }
        }
    }
}
