using Xunit;
using SnakeGame;
using Xunit.Sdk;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using Xunit.Abstractions;
using Moq;
using System.ComponentModel;
using System.Reflection.Metadata;
using Castle.Components.DictionaryAdapter.Xml;
using SnakeGame.Interfaces;

namespace TestSnake
{
    public class TestSnake
    {
        [Fact]
        public void Test_Snake_SnakeMoveShouldDecrementY()
        {

            Snake snake = new Snake(new Position());

            snake.moveSnake(new Position());

            int expected = 9;

            Assert.Equivalent(expected, snake.y);
        }

        [Fact]
        public void Test_Snake_SnakeMoveShouldIncrementY()
        {

            Snake snake = new Snake(new Position());

            snake.dir = 'd';

            snake.moveSnake(new Position());

            int expected = 11;

            Assert.Equivalent(expected, snake.y);
        }
        [Fact]
        public void Test_Snake_SnakeMoveShouldDecrementX()
        {


            Snake snake = new Snake(new Position());

            snake.key = 'a';

            snake.dir = 'l';

            snake.moveSnake(new Position());

            int expected = 9;

            Assert.Equivalent(expected, snake.x);
        }
        [Fact]
        public void Test_Snake_SnakeMoveShouldIncrementX()
        {

            Snake snake = new Snake(new Position());

            snake.key = 'd';

            snake.moveSnake(new Position());

            int expected = 11;

            Assert.Equivalent(expected, snake.x);
        }
        [Fact]
        public void Test_Snake_EatShouldIncrementScore()
        {
            var snakeMock = new Mock<ISnake>();

            snakeMock.SetupAllProperties();

            var food = new Food(new Position());

            var position = new Position();

            snakeMock.Object.score = 0;

            snakeMock.Setup(p => p.eat(position, food)).Callback(() =>
            {
                snakeMock.Object.score += 1;
            });

            snakeMock.Object.eat(position, food);

            Assert.Equal(1, snakeMock.Object.score);
        }


        [Fact]
        public void Test_Snake_IsDeadShouldThrowSnakeExceptionIfHitsSelf()
        {
            Snake snake = new Snake(new Position());

            snake.snakeBody.Add(new Position(10, 10));

            snake.snakeBody.Add(new Position(11, 10));

            snake.snakeBody.Add(new Position(11, 11));

            snake.snakeBody.Add(new Position(10, 11));

            snake.snakeBody.Add(new Position(10, 10));

            Assert.Throws<SnakeException>(() => snake.isDead());
        }

        [Fact]
        public void Test_Snake_hitWallShouldThrowSnakeException()
        {

            Snake snake = new Snake(new Position());
            Assert.Throws<SnakeException>(() => snake.hitWall(new Canvas("nodraw")));
        }

        [Theory]
        [InlineData("1")]
        [InlineData("2")]
        [InlineData("3")]
        [InlineData("4")]
        [InlineData("5")]
        [InlineData("PP")]
        public void Test_Correct_Color(string choice)
        {

            var mock = new Mock<ISnake>();
            mock.SetupAllProperties();
            Dictionary<string, string> colordict = new Dictionary<string, string>() { { "1", "Green" }, { "2", "Red" }, { "3", "Yellow" }, { "4", "Blue" }, { "5", "White" }, { "PP", "White" } };


            string result = "";
            mock.Setup(p => p.drawSnake(It.IsAny<string>())).Callback((string choice) =>
            {
                switch (choice)
                {
                    case "1":
                        result = "Green";
                        break;
                    case "2":
                        result = "Red";
                        break;
                    case "3":
                        result = "Yellow";
                        break;
                    case "4":
                        result = "Blue";
                        break;
                    default:
                        result = "White";
                        break;
                }
            });

            mock.Object.drawSnake(choice);

            Assert.Equal(result, colordict[choice]);
        }

        [Fact]
        public void Test_Snake_hitMineShouldThrowSnakeException()
        {
            Snake snake = new Snake(new Position());
            Mock<ICanvas> mockCanvas = new Mock<ICanvas>();
            mockCanvas.Setup(p => p.getWidth()).Returns(10);
            mockCanvas.Setup(p => p.getHeight()).Returns(10);
            Mine mine = new Mine(new Position(), mockCanvas.Object);
            mine.mineList[0].Add(0, 10);
            mine.mineList[1].Add(0, 10);
            snake.snakeBody[snake.snakeBody.Count - 1] = new Position(0, 10);

            Assert.Throws<SnakeException>(() => snake.hitMine(mine));
        }
    }
}