namespace RPGSagaUnitTests.SkillsTests
{
    using System.Collections.Generic;
    using System.Linq;
    using Moq;
    using RpgSaga.Core.Effects;
    using RpgSaga.Core.Entities;
    using RpgSaga.Core.Interfaces;
    using RpgSaga.Core.Skills;
    using Xunit;

    public class BleedingTesting
    {
        [Theory]
        [InlineData(100, 5, 95)]
        [InlineData(150, 10, 140)]
        [InlineData(30, 20, 10)]
        public void Minus_EnemyHp(int currentHp, int damage, int expectedHp)
        {
            // Arrange
            var eventLoggerMock = new Mock<IEventLogger>();
            var randomNumberGeneratorMock = new Mock<IRandomNumberGenerator>();
            var sut = new BleedingSkill(eventLoggerMock.Object);
            sut.DamageOneTime = damage;

            Hero hero1 = new Witcher(eventLoggerMock.Object, randomNumberGeneratorMock.Object);
            hero1.SetupHero("TestHero1");
            hero1.Skills = new List<ISkill> { sut };

            Hero hero2 = new Witcher(eventLoggerMock.Object, randomNumberGeneratorMock.Object);
            hero2.Hp = currentHp;

            // Act
            hero1.Skills.First().UseSkill(hero1, hero2);

            // Assert
            Assert.Equal(expectedHp, hero2.Hp);
        }

        [Fact]
        public void Add_Effect_GetRegularDamage_To_Enemy()
        {
            // Arrange
            var eventLoggerMock = new Mock<IEventLogger>();
            var randomNumberGeneratorMock = new Mock<IRandomNumberGenerator>();
            var sut = new BleedingSkill(eventLoggerMock.Object);

            Hero hero1 = new Witcher(eventLoggerMock.Object, randomNumberGeneratorMock.Object);
            hero1.SetupHero("TestHero1");
            hero1.Skills = new List<ISkill> { sut };

            Hero hero2 = new Witcher(eventLoggerMock.Object, randomNumberGeneratorMock.Object);

            // Act
            hero1.Skills.First().UseSkill(hero1, hero2);

            // Assert
            Assert.True(hero2.Effects.First() is GetRegularDamage);
        }

        [Fact]
        public void Check_Changes_SkillCanBeUsed()
        {
            // Arrange
            var eventLoggerMock = new Mock<IEventLogger>();
            var randomNumberGeneratorMock = new Mock<IRandomNumberGenerator>();
            var sut = new BleedingSkill(eventLoggerMock.Object);

            Hero hero1 = new Witcher(eventLoggerMock.Object, randomNumberGeneratorMock.Object);
            hero1.SetupHero("TestHero1");
            hero1.Skills = new List<ISkill> { sut };
            bool before = hero1.Skills.First().SkillCanBeUsed;

            Hero hero2 = new Witcher(eventLoggerMock.Object, randomNumberGeneratorMock.Object);

            // Act
            hero1.Skills.First().UseSkill(hero1, hero2);

            // Assert
            bool after = hero1.Skills.First().SkillCanBeUsed;
            Assert.NotEqual(after, before);
        }
    }
}