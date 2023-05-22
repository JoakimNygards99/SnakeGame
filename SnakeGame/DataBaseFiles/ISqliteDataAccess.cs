using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeGame.DataBaseFiles
{
    public interface ISqliteDataAccess
    {
        List<T> LoadData<T>(string sql);
        string SaveData<T>(T data, string sql);
        string CleanDatabase(string sql);
    }
}
