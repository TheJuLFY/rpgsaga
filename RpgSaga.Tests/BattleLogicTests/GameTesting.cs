namespace RPGSagaUnitTests.BattleLogicTests
{
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.Extensions.Hosting;
    using Moq;
    using RpgSaga.Core.BattleLogic;
    using RpgSaga.Core.Entities;
    using RpgSaga.Core.Interfaces;
    using Xunit;

    public class GameTesting
    {
        public static bool VerifyHeroes(List<Hero> heroes)
        {
            Assert.NotNull(heroes);
            Assert.Equal(2, heroes.Count);

            foreach (Hero hero in heroes)
            {
                Assert.NotNull(hero);
            }

            return true;
        }

        [Fact]
        public async Task Round_Starts_With_NotNull_List()
        {
            // Arrange
            var loggerMock = new Mock<ILogger>();
            var hostApplicationLifetimeMock = new Mock<IHostApplicationLifetime>();
            var roundMock = new Mock<IRound>();
            var heroLoaderMock = new Mock<IHeroLoader>();

            var sut = new Game(
                        loggerMock.Object,
                        hostApplicationLifetimeMock.Object,
                        roundMock.Object,
                        heroLoaderMock.Object);

            var randomNumberGeneratorMock = new Mock<IRandomNumberGenerator>();
            var eventLoggerMock = new Mock<IEventLogger>();

            heroLoaderMock
                .Setup(x => x.LoadHeroes())
                .Returns(new List<Hero>
                {
                    new Barbarian(eventLoggerMock.Object, randomNumberGeneratorMock.Object),
                    new Barbarian(eventLoggerMock.Object, randomNumberGeneratorMock.Object),
                });

            roundMock
                .Setup(x => x.StartRound(It.IsAny<List<Hero>>()))
                .Returns(new List<Hero> { new Barbarian(eventLoggerMock.Object, randomNumberGeneratorMock.Object) });

            // Act
            await sut.StartAsync(CancellationToken.None);

            // Assert
            roundMock.Verify(x => x.StartRound(It.Is<List<Hero>>(heroes => VerifyHeroes(heroes))), Times.Once);
        }
    }
}