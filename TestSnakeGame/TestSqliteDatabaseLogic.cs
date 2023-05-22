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

        [Fact]
        public void TestSavePlayer()
        {
            using (var mock = AutoMock.GetLoose())
            {
                var player = GetFakePlayers()[0];
                string sql = "insert into Player (Name, Score, Date) values (@Name, @Score, @Date)";

                mock.Mock<ISqliteDataAccess>().Setup(x => x.SaveData(player, sql)).Returns("Success");

                var test = mock.Create<PlayerScoreDataAccess>();

                var expected = "Success";
                var actual = test.SavePlayer(player);

                //Kollar att den retunerar rätt värde
                Assert.Equal(expected, actual);

                mock.Mock<ISqliteDataAccess>().Verify(x => x.SaveData(player, sql), Times.Exactly(1));

            }
        }

        [Fact]
        public void TestCleanTable()
        {
            using (var mock = AutoMock.GetLoose())
            {
                string sql = "Delete from Player";
                mock.Mock<ISqliteDataAccess>().Setup(x => x.CleanDatabase(sql)).Returns("Success");

                var test = mock.Create<PlayerScoreDataAccess>();

                var expected = "Success";
                var actual = test.CleanTable();

                Assert.Equal(expected, actual);

                mock.Mock<ISqliteDataAccess>().Verify(x => x.CleanDatabase(sql), Times.Exactly(1));
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
