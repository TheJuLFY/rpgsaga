namespace RPGSagaUnitTests.EffectsTests
{
    using System.Linq;
    using Moq;
    using RpgSaga.Core.Effects;
    using RpgSaga.Core.Entities;
    using RpgSaga.Core.Interfaces;
    using Xunit;

    public class GetRegularDamageTesting
    {
        [Theory]
        [InlineData(100, 5, 95)]
        [InlineData(120, 10, 110)]
        [InlineData(80, 20, 60)]
        public void Get_Regular_Damage(int initialHp, int damage, int expectedHp)
        {
            // Arrange
            var eventLoggerMock = new Mock<IEventLogger>();
            var randomNumberGeneratorMock = new Mock<IRandomNumberGenerator>();
            var sut = new GetRegularDamage(damage, eventLoggerMock.Object);

            Hero hero = new Witcher(eventLoggerMock.Object, randomNumberGeneratorMock.Object);
            hero.SetupHero("TestHero");
            hero.Hp = initialHp;
            hero.Effects.Add(sut);

            // Act
            hero.Effects.First().GetEffect(hero);

            // Assert
            Assert.Equal(hero.Hp, expectedHp);
        }

        [Fact]
        public void Check_Not_Changes_Duration()
        {
            // Arrange
            var eventLoggerMock = new Mock<IEventLogger>();
            var randomNumberGeneratorMock = new Mock<IRandomNumberGenerator>();
            var sut = new GetRegularDamage(5, eventLoggerMock.Object);

            Hero hero = new Witcher(eventLoggerMock.Object, randomNumberGeneratorMock.Object);
            hero.SetupHero("TestHero");
            hero.Effects.Add(sut);

            int durationBefore = hero.Effects.First().Duration;

            // Act
            hero.Effects.First().GetEffect(hero);

            // Assert
            int durationAfter = hero.Effects.First().Duration;
            Assert.Equal(durationAfter, durationBefore);
        }
    }
}