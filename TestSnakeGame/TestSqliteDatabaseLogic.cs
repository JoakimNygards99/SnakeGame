using SnakeGame.DataBaseFiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using Autofac.Extras.Moq;
using System.Numerics;

namespace TestSnakeGame
{
    public class TestSqliteDatabaseLogic
    {
        [Fact]
        public void TestLoadPlayers()
        {
            //Tillåter oss att mocka flera olika object utan att skapa nya instanser av mock
            using (var mock = AutoMock.GetLoose())
            {
                mock.Mock<ISqliteDataAccess>()
                .Setup(X => X.LoadData<PlayerScoreModel>("select * from Player")).Returns(GetFakePlayers());


                var test = mock.Create<PlayerScoreDataAccess>();

                //Det vi vill ha är våran fakade lista med fake products
                var expected = GetFakePlayers();

                //Samlar datan från metoden LoadPlayers
                var actual = test.LoadPlayers();

                //Kollar om metoden LoadPlayers faktiskt retunerar ett värde
                Assert.True(actual != null);

                Assert.Equal(expected.Count(), actual.Count());

                for (int i = 0; i < expected.Count(); i++)
                {
                    Assert.Equal(actual[i].Name, expected[i].Name);
                    Assert.Equal(actual[i].Score, expected[i].Score);
                    Assert.Equal(actual[i].Date, expected[i].Date);
                }
            }
        }

        private List<PlayerScoreModel> GetFakePlayers()
        {
            var players = new List<PlayerScoreModel>
            {
                new PlayerScoreModel
                {
                   Name = "Jocke",
                   Score = 1,
                   Date = DateTime.Now.ToString()

                },
                new PlayerScoreModel
                {
                   Name = "Pontus",
                   Score = 100,
                   Date = DateTime.Now.ToString()
                },
                new PlayerScoreModel
                {
                   Name = "Göran",
                   Score = 99,
                   Date = DateTime.Now.ToString()
                },
                new PlayerScoreModel
                {
                    Name = "Björn",
                   Score = 78,
                   Date = DateTime.Now.ToString()
                }
            };
            return players;
        }

    }
}
