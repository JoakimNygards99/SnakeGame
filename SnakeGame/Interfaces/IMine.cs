using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeGame.Interfaces
{
    public interface IMine
    {
        void drawMine();
        List<Dictionary<int, int>> mineList { get; set;}


    }
}
