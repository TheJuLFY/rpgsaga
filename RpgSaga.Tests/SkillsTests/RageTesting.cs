namespace RPGSagaUnitTests.SkillsTests
{
    using System.Collections.Generic;
    using System.Linq;
    using Moq;
    using RpgSaga.Core.Entities;
    using RpgSaga.Core.Interfaces;
    using RpgSaga.Core.Skills;
    using Xunit;

    public class RageTesting
    {
        [Theory]
        [InlineData(100, 10, 1.5, 85)]
        [InlineData(150, 10, 2, 130)]
        [InlineData(30, 1, 5, 25)]
        public void Minus_EnemyHp(int currentHp, int damage, decimal damageCoefficient, int expectedHp)
        {
            // Arrange
            var eventLoggerMock = new Mock<IEventLogger>();
            var randomNumberGeneratorMock = new Mock<IRandomNumberGenerator>();
            var sut = new RageSkill(eventLoggerMock.Object);
            sut.DamageCoefficient = damageCoefficient;

            Hero hero1 = new Barbarian(eventLoggerMock.Object, randomNumberGeneratorMock.Object);
            hero1.SetupHero("TestHero1");
            hero1.Power = damage;
            hero1.Skills = new List<ISkill> { sut };

            Hero hero2 = new Witcher(eventLoggerMock.Object, randomNumberGeneratorMock.Object);
            hero2.Hp = currentHp;

            // Act
            hero1.Skills.First().UseSkill(hero1, hero2);

            // Assert
            Assert.Equal(expectedHp, hero2.Hp);
        }

        [Fact]
        public void Check_Not_Changes_SkillCanBeUsed()
        {
            // Arrange
            var eventLoggerMock = new Mock<IEventLogger>();
            var randomNumberGeneratorMock = new Mock<IRandomNumberGenerator>();
            var sut = new RageSkill(eventLoggerMock.Object);

            Hero hero1 = new Barbarian(eventLoggerMock.Object, randomNumberGeneratorMock.Object);
            hero1.SetupHero("TestHero1");
            hero1.Skills = new List<ISkill> { sut };
            bool before = hero1.Skills.First().SkillCanBeUsed;

            Hero hero2 = new Witcher(eventLoggerMock.Object, randomNumberGeneratorMock.Object);

            // Act
            hero1.Skills.First().UseSkill(hero1, hero2);

            // Assert
            bool after = hero1.Skills.First().SkillCanBeUsed;
            Assert.Equal(after, before);
        }
    }
}