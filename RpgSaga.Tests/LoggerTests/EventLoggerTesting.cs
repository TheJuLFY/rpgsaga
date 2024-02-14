namespace RPGSagaUnitTests.LoggerTests
{
    using System.Collections.Generic;
    using Moq;
    using RpgSaga.Core;
    using RpgSaga.Core.Effects;
    using RpgSaga.Core.Entities;
    using RpgSaga.Core.Interfaces;
    using RpgSaga.Core.Skills;
    using Xunit;

    public class EventLoggerTesting
    {
        [Fact]
        public void Check_LogRoundAnnouncement()
        {
            // Arrange
            var eventLoggerMock = new Mock<IEventLogger>();
            var randomNumberGeneratorMock = new Mock<IRandomNumberGenerator>();

            var heroes = new List<Hero>
            {
                new Undead(eventLoggerMock.Object, randomNumberGeneratorMock.Object),
                new Undead(eventLoggerMock.Object, randomNumberGeneratorMock.Object),
            };

            var loggerMock = new Mock<ILogger>();
            var sut = new EventLogger(loggerMock.Object);

            // Act
            sut.LogRoundAnnouncement(heroes);

            // Assert
            loggerMock.Verify(
                    x => x.LogMessage("+++++++++++++++++++++++++++++++++++++++++++++++++" +
                                     "\nNEW ROUND: There are 2 heroes!" +
                                     "\n+++++++++++++++++++++++++++++++++++++++++++++++++"), Times.Once);
        }

        [Fact]
        public void Check_LogDuelAnnouncement()
        {
            // Arrange
            var eventLoggerMock = new Mock<IEventLogger>();
            var randomNumberGeneratorMock = new Mock<IRandomNumberGenerator>();

            var hero1 = new Undead(eventLoggerMock.Object, randomNumberGeneratorMock.Object);
            hero1.SetupHero("TestHero1");

            var hero2 = new Undead(eventLoggerMock.Object, randomNumberGeneratorMock.Object);
            hero2.SetupHero("TestHero2");

            var loggerMock = new Mock<ILogger>();
            var sut = new EventLogger(loggerMock.Object);

            // Act
            sut.LogDuelAnnouncement(hero1, hero2);

            // Assert
            loggerMock.Verify(
                    x => x.LogMessage("-------------------------------------------------" +
                                     "\nNEW DUEL: Undead TestHero1 vs Undead TestHero2!" +
                                     "\n-------------------------------------------------"), Times.Once);
        }

        [Fact]
        public void Check_LogHit()
        {
            // Arrange
            var eventLoggerMock = new Mock<IEventLogger>();
            var randomNumberGeneratorMock = new Mock<IRandomNumberGenerator>();

            var hero1 = new Undead(eventLoggerMock.Object, randomNumberGeneratorMock.Object);
            hero1.SetupHero("TestHero1");
            hero1.Power = 20;

            var hero2 = new Undead(eventLoggerMock.Object, randomNumberGeneratorMock.Object);
            hero2.SetupHero("TestHero2");
            hero2.Hp = 80;

            var loggerMock = new Mock<ILogger>();
            var sut = new EventLogger(loggerMock.Object);

            // Act
            sut.LogHit(hero1, hero2);

            // Assert
            loggerMock.Verify(x => x.LogMessage("\"U.TestHero1\" hits \"U.TestHero2\" on 20 HP, \"U.TestHero2\"'s HP left 80."), Times.Once);
        }

        [Fact]
        public void Check_LogSkill()
        {
            // Arrange
            var eventLoggerMock = new Mock<IEventLogger>();
            var randomNumberGeneratorMock = new Mock<IRandomNumberGenerator>();

            var hero1 = new Undead(eventLoggerMock.Object, randomNumberGeneratorMock.Object);
            hero1.SetupHero("TestHero1");

            var hero2 = new Undead(eventLoggerMock.Object, randomNumberGeneratorMock.Object);
            hero2.SetupHero("TestHero2");
            hero2.Hp = 80;

            var skill = new RageSkill(eventLoggerMock.Object);
            string skillInfo = $"Hero's damage increased by 1.3 times!";

            var loggerMock = new Mock<ILogger>();
            var sut = new EventLogger(loggerMock.Object);

            // Act
            sut.LogSkill(hero1, hero2, skill, skillInfo);

            // Assert
            loggerMock.Verify(
                x => x.LogMessage("*SKILL* \"U.TestHero1\" uses RageSkill on \"U.TestHero2\", he has 80 HP left!" +
                                  "\n*SKILL* Hero's damage increased by 1.3 times!"), Times.Once);
        }

        [Fact]
        public void Check_LogEffect()
        {
            // Arrange
            var eventLoggerMock = new Mock<IEventLogger>();
            var randomNumberGeneratorMock = new Mock<IRandomNumberGenerator>();

            var hero = new Undead(eventLoggerMock.Object, randomNumberGeneratorMock.Object);
            hero.SetupHero("TestHero");

            var effect = new SkipMove(1, eventLoggerMock.Object);
            string effectInfo = $"Hero skip move!";

            var loggerMock = new Mock<ILogger>();
            var sut = new EventLogger(loggerMock.Object);

            // Act
            sut.LogEffect(hero, effect, effectInfo);

            // Assert
            loggerMock.Verify(
                x => x.LogMessage("*EFFECT* \"U.TestHero\" is under effect SkipMove!" +
                                  "\n*EFFECT* Hero skip move!"), Times.Once);
        }

        [Fact]
        public void Check_LogWinner()
        {
            // Arrange
            var eventLoggerMock = new Mock<IEventLogger>();
            var randomNumberGeneratorMock = new Mock<IRandomNumberGenerator>();

            var hero = new Undead(eventLoggerMock.Object, randomNumberGeneratorMock.Object);
            hero.SetupHero("TestHero");

            var loggerMock = new Mock<ILogger>();
            var sut = new EventLogger(loggerMock.Object);

            // Act
            sut.LogWinner(hero);

            // Assert
            loggerMock.Verify(x => x.LogMessage("----\"UNDEAD TESTHERO\" IS A WINNER!"), Times.Once);
        }

        [Fact]
        public void Check_LogDraw()
        {
            // Arrange
            var eventLoggerMock = new Mock<IEventLogger>();
            var randomNumberGeneratorMock = new Mock<IRandomNumberGenerator>();

            var hero1 = new Undead(eventLoggerMock.Object, randomNumberGeneratorMock.Object);
            hero1.SetupHero("TestHero1");
            int hero1Luck = 5;

            var hero2 = new Undead(eventLoggerMock.Object, randomNumberGeneratorMock.Object);
            hero2.SetupHero("TestHero2");
            int hero2Luck = 2;

            var loggerMock = new Mock<ILogger>();
            var sut = new EventLogger(loggerMock.Object);

            // Act
            sut.LogDraw(hero1, hero1Luck, hero2, hero2Luck);

            // Assert
            loggerMock.Verify(
                x => x.LogMessage("Nobody is a winner, both \"U.TestHero1\" and \"U.TestHero2\" will roll the dice." +
                                  "\n\"U.TestHero1\" got 5 points vs \"U.TestHero2\" 2 points." +
                                  "\n\"U.TestHero1\" goes further."), Times.Once);
        }
    }
}
