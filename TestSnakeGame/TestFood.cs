using Moq;
using SnakeGame;
using SnakeGame.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace TestSnakeGame
{
    public class TestFood
    {
        [Fact]
        public void Test_Food_FoodLocationShouldReturnFoodPos()
        {
            var food = new Food(new Position());

            IPosition location = food.foodLocation();

            var actual = location.ToString();

            var expected = "10,10";

            Assert.Equal(actual, expected);
        }



        [Fact]
        public void Test_Food_FoodNewLocationShouldGenerateCorrectPosition()
        {
            Mock<ICanvas> mockCanvas = new Mock<ICanvas>();

            mockCanvas.Setup(p => p.getWidth()).Returns(10);
            mockCanvas.Setup(p => p.getHeight()).Returns(10);

            Food food = new Food(new Position(), mockCanvas.Object);
            food.foodNewLocation(new Mine(new Position(), mockCanvas.Object));

            Assert.InRange(food._foodPos.x, 5, 10);
            Assert.InRange(food._foodPos.y, 5, 10);
        }

        [Fact]
        public void Test_Food_foodNewLocationShouldBeCalledTwice()
        {
            Mock<ICanvas> mockCanvas = new Mock<ICanvas>();

            mockCanvas.Setup(p => p.getWidth()).Returns(10);
            mockCanvas.Setup(p => p.getHeight()).Returns(10);

            Mine mine = new Mine(new Position(), mockCanvas.Object);

            Mock<IFood> food = new Mock<IFood>();

            food.SetupAllProperties();

            int timesCalled = 0;

            mine.mineList.Add(new Dictionary<int, int>());
            mine.mineList.Add(new Dictionary<int, int>());
            mine.mineList[0].Add(0, 10);
            mine.mineList[1].Add(0, 10);

            food.Setup(p => p.foodNewLocation(mine)).Callback(() =>
            {
                if (mine.mineList[0][0] == mockCanvas.Object.getWidth() && mine.mineList[1][0] == mockCanvas.Object.getHeight())
                {
                    timesCalled++;
                }
            });

            food.Object.foodNewLocation(mine);

            Assert.NotEqual(0, timesCalled);
        }
    }
}
