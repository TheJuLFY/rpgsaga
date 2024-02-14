namespace RPGSagaUnitTests.BattleLogicTests
{
    using Moq;
    using RpgSaga.Core.BattleLogic;
    using RpgSaga.Core.Entities;
    using RpgSaga.Core.Interfaces;
    using Xunit;

    public class DuelTests
    {
        [Theory]
        [InlineData(10, 0, "TestHero1")]
        [InlineData(0, 10, "TestHero2")]
        public void Winner_Returned_Correct(int hero1Hp, int hero2Hp, string expected)
        {
            // Arrange
            var eventLoggerMock = new Mock<IEventLogger>();
            var loggerMock = new Mock<ILogger>();
            var randomNumberGeneratorMock = new Mock<IRandomNumberGenerator>();
            var sut = new Duel(eventLoggerMock.Object, loggerMock.Object, randomNumberGeneratorMock.Object);

            Hero hero1 = new Undead(eventLoggerMock.Object, randomNumberGeneratorMock.Object);
            hero1.SetupHero("TestHero1");
            hero1.Hp = hero1Hp;

            Hero hero2 = new Undead(eventLoggerMock.Object, randomNumberGeneratorMock.Object);
            hero2.SetupHero("TestHero2");
            hero2.Hp = hero2Hp;

            // Act
            Hero winner = sut.StartDuel(hero1, hero2);

            // Assert
            Assert.Equal(expected, winner.Name);
        }

        [Theory]
        [InlineData(5, 1)]
        [InlineData(2, 6)]
        public void Random_Winner_Is_NotNull(int hero1Luck, int hero2Luck)
        {
            // Arrange
            var eventLoggerMock = new Mock<IEventLogger>();
            var randomNumberGeneratorMock = new Mock<IRandomNumberGenerator>();
            var loggerMock = new Mock<ILogger>();
            var sut = new Duel(eventLoggerMock.Object, loggerMock.Object, randomNumberGeneratorMock.Object);

            Hero hero1 = new Undead(eventLoggerMock.Object, randomNumberGeneratorMock.Object);
            hero1.SetupHero("TestHero1");
            hero1.Hp = 0;

            Hero hero2 = new Undead(eventLoggerMock.Object, randomNumberGeneratorMock.Object);
            hero2.SetupHero("TestHero2");
            hero2.Hp = 0;

            randomNumberGeneratorMock
                .SetupSequence(x => x.CreateRandomNumber(1, 7))
                .Returns(hero1Luck)
                .Returns(hero2Luck);

            // Act
            Hero winner = sut.StartDuel(hero1, hero2);

            // Assert
            Assert.NotNull(winner);
        }

        [Fact]
        public void FailedHero_Proceeded_In_Duel()
        {
            // Arrange
            var eventLoggerMock = new Mock<IEventLogger>();
            var randomNumberGeneratorMock = new Mock<IRandomNumberGenerator>();
            var loggerMock = new Mock<ILogger>();
            var sut = new Duel(eventLoggerMock.Object, loggerMock.Object, randomNumberGeneratorMock.Object);

            Hero hero1 = new FailedHero(eventLoggerMock.Object, randomNumberGeneratorMock.Object);
            hero1.SetupHero("TestHero1");
            hero1.Hp = 100;

            Hero hero2 = new Undead(eventLoggerMock.Object, randomNumberGeneratorMock.Object);
            hero2.SetupHero("TestHero2");
            hero2.Hp = 100;

            // Act
            Hero winner = sut.StartDuel(hero1, hero2);

            // Assert
            Assert.Equal(hero2, winner);
        }
    }
}
