namespace RPGSagaUnitTests.EffectsTests
{
    using System.Linq;
    using Moq;
    using RpgSaga.Core.Effects;
    using RpgSaga.Core.Entities;
    using RpgSaga.Core.Interfaces;
    using Xunit;

    public class SkipMoveTesting
    {
        [Fact]
        public void Skip_Move()
        {
            // Arrange
            var eventLoggerMock = new Mock<IEventLogger>();
            var randomNumberGeneratorMock = new Mock<IRandomNumberGenerator>();
            var sut = new SkipMove(1, eventLoggerMock.Object);

            Hero hero = new Witcher(eventLoggerMock.Object, randomNumberGeneratorMock.Object);
            hero.SetupHero("TestHero");
            hero.Effects.Add(sut);
            bool before = hero.ShouldSkipMove;

            // Act
            hero.Effects.First().GetEffect(hero);

            // Assert
            bool after = hero.ShouldSkipMove;
            Assert.NotEqual(after, before);
        }

        [Fact]
        public void Check_Changes_Duration()
        {
            // Arrange
            var eventLoggerMock = new Mock<IEventLogger>();
            var randomNumberGeneratorMock = new Mock<IRandomNumberGenerator>();
            var sut = new SkipMove(1, eventLoggerMock.Object);

            Hero hero = new Witcher(eventLoggerMock.Object, randomNumberGeneratorMock.Object);
            hero.SetupHero("TestHero");
            hero.Effects.Add(sut);

            int durationBefore = hero.Effects.First().Duration;

            // Act
            hero.Effects.First().GetEffect(hero);

            // Assert
            int durationAfter = hero.Effects.First().Duration;
            Assert.NotEqual(durationAfter, durationBefore);
        }
    }
}
