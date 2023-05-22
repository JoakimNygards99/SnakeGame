using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeGame.DataBaseFiles
{
    public class PlayerScoreModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Score { get; set; }
        public string Date { get; set; }
    }
}
