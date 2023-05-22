using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeGame.DataBaseFiles
{
    public class PlayerScoreDataAccess
    {
        ISqliteDataAccess _database;

        public PlayerScoreDataAccess(ISqliteDataAccess database)
        {
            _database = database;
        }

        public List<PlayerScoreModel> LoadPlayers()
        {
            string sql = "select * from Player";
            var output = _database.LoadData<PlayerScoreModel>(sql);

            return output;
        }   

        public string SavePlayer(PlayerScoreModel player)
        {
            string sql = "insert into Player (Name, Score, Date) values (@Name, @Score, @Date)";
            _database.SaveData(player, sql);
            return "Success";
        }

        public string CleanTable()
        {
            string sql = "Delete from Player";
            _database.CleanDatabase(sql);
            return "Success";
        }

    }
}
