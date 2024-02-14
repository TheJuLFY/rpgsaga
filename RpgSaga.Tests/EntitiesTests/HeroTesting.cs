namespace RPGSagaUnitTests.EntitiesTests
{
    using System.Collections.Generic;
    using Moq;
    using RpgSaga.Core.Effects;
    using RpgSaga.Core.Entities;
    using RpgSaga.Core.Interfaces;
    using RpgSaga.Core.Skills;
    using Xunit;

    public class HeroTesting
    {
        [Theory]
        [InlineData(10, 10)]
        [InlineData(0, 0)]
        [InlineData(-10, 0)]
        public void Check_HeroHp_Setup(int newHp, int savedHp)
        {
            // Arrange
            var eventLoggerMock = new Mock<IEventLogger>();
            var randomNumberGeneratorMock = new Mock<IRandomNumberGenerator>();
            Hero hero = new Undead(eventLoggerMock.Object, randomNumberGeneratorMock.Object);

            // Act
            hero.Hp = newHp;

            // Assert
            Assert.Equal(savedHp, hero.Hp);
        }

        [Theory]
        [InlineData(10, 10)]
        [InlineData(50, 50)]
        [InlineData(100, 100)]
        public void Hero_Skip_Move(int currentHp, int expectedHp)
        {
            // Arrange
            var eventLoggerMock = new Mock<IEventLogger>();
            var randomNumberGeneratorMock = new Mock<IRandomNumberGenerator>();

            Hero hero1 = new Undead(eventLoggerMock.Object, randomNumberGeneratorMock.Object);
            hero1.ShouldSkipMove = true;

            Hero hero2 = new Undead(eventLoggerMock.Object, randomNumberGeneratorMock.Object);
            hero2.Hp = currentHp;

            // Act
            hero1.DoMove(hero2);

            // Assert
            Assert.Equal(expectedHp, hero2.Hp);
        }

        [Theory]
        [InlineData(10, 5, 5)]
        [InlineData(50, 10, 40)]
        [InlineData(100, 20, 80)]
        public void Hero_Hit_Enemy(int currentHp, int damage, int expectedHp)
        {
            // Arrange
            var eventLoggerMock = new Mock<IEventLogger>();
            var randomNumberGeneratorMock = new Mock<IRandomNumberGenerator>();

            Hero hero1 = new Undead(eventLoggerMock.Object, randomNumberGeneratorMock.Object);
            hero1.SetupHero("TestHero1");
            hero1.Power = damage;
            hero1.Skills = new List<ISkill> { new UsualPunch(eventLoggerMock.Object) };

            Hero hero2 = new Undead(eventLoggerMock.Object, randomNumberGeneratorMock.Object);
            hero2.Hp = currentHp;

            // Act
            hero1.DoMove(hero2);

            // Assert
            Assert.Equal(expectedHp, hero2.Hp);
        }

        [Theory]
        [InlineData(10, 5, 5)]
        [InlineData(50, 10, 40)]
        [InlineData(100, 20, 80)]
        public void Hero_Get_Effects(int currentHp, int damage, int expectedHp)
        {
            // Arrange
            var eventLoggerMock = new Mock<IEventLogger>();
            var randomNumberGeneratorMock = new Mock<IRandomNumberGenerator>();
            var sut = new Undead(eventLoggerMock.Object, randomNumberGeneratorMock.Object);

            sut.SetupHero("TestHero");
            sut.Hp = currentHp;
            sut.Effects = new List<IEffect> { new GetRegularDamage(damage, eventLoggerMock.Object) };

            Hero enemy = new Undead(eventLoggerMock.Object, randomNumberGeneratorMock.Object);

            // Act
            sut.DoMove(enemy);

            // Assert
            Assert.Equal(expectedHp, sut.Hp);
        }

        [Theory]
        [InlineData(10, 100)]
        [InlineData(50, 100)]
        [InlineData(100, 100)]
        public void Hero_Refresh_Hp(int currentHp, int expectedHp)
        {
            // Arrange
            var eventLoggerMock = new Mock<IEventLogger>();
            var randomNumberGeneratorMock = new Mock<IRandomNumberGenerator>();
            var sut = new Undead(eventLoggerMock.Object, randomNumberGeneratorMock.Object);

            sut.SetupHero("TestHero");
            sut.Hp = currentHp;
            sut.StartHp = expectedHp;

            // Act
            sut.RefreshAfterFight();

            // Assert
            Assert.Equal(expectedHp, sut.Hp);
        }

        [Fact]
        public void Hero_Refresh_Effects()
        {
            // Arrange
            var eventLoggerMock = new Mock<IEventLogger>();
            var randomNumberGeneratorMock = new Mock<IRandomNumberGenerator>();
            var sut = new Undead(eventLoggerMock.Object, randomNumberGeneratorMock.Object);

            sut.SetupHero("TestHero");
            sut.Effects.Add(new GetRegularDamage(5, eventLoggerMock.Object));
            sut.Effects.Add(new SkipMove(1, eventLoggerMock.Object));

            // Act
            sut.RefreshAfterFight();

            // Assert
            Assert.True(sut.Effects.Count == 0);
        }
    }
}
