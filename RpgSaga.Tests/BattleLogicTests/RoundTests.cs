namespace RPGSagaUnitTests.BattleLogicTests
{
    using System.Collections.Generic;
    using Moq;
    using RpgSaga.Core.BattleLogic;
    using RpgSaga.Core.Entities;
    using RpgSaga.Core.Interfaces;
    using Xunit;

    public class RoundTests
    {
        [Fact]
        public void Round_Return_NotNull_List()
        {
            // Arrange
            var eventLoggerMock = new Mock<IEventLogger>();
            var randomNumberGeneratorMock = new Mock<IRandomNumberGenerator>();
            var duelMock = new Mock<IDuel>();
            var sut = new Round(eventLoggerMock.Object, duelMock.Object, randomNumberGeneratorMock.Object);

            Hero hero1 = new Undead(eventLoggerMock.Object, randomNumberGeneratorMock.Object);
            Hero hero2 = new Undead(eventLoggerMock.Object, randomNumberGeneratorMock.Object);
            List<Hero> heroes = new List<Hero> { hero1, hero2 };

            // Act
            List<Hero> winners = sut.StartRound(heroes);

            // Assert
            Assert.NotNull(winners);
        }
    }
}
