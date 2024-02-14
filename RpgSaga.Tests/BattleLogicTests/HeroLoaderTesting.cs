namespace RPGSagaUnitTests.BattleLogicTests
{
    using System.Collections.Generic;
    using Moq;
    using RpgSaga.Core.BattleLogic;
    using RpgSaga.Core.Data;
    using RpgSaga.Core.Entities;
    using RpgSaga.Core.Interfaces;
    using Xunit;

    public class HeroLoaderTesting
    {
        [Fact]
        public void There_Is_Number_Prompt()
        {
            // Arrange
            var heroGeneratorMock = new Mock<IHeroGenerator>();
            var heroDeserializationMock = new Mock<IHeroJsonReader>();
            var heroSerializationMock = new Mock<IHeroJsonWriter>();
            var processArgumentsReaderMock = new Mock<IProcessArgumentsReader>();
            var loggerMock = new Mock<ILogger>();
            var userInputReaderMock = new Mock<IUserInputReader>();

            var sut = new HeroLoader(
                        heroGeneratorMock.Object,
                        heroDeserializationMock.Object,
                        heroSerializationMock.Object,
                        processArgumentsReaderMock.Object,
                        loggerMock.Object,
                        userInputReaderMock.Object);

            userInputReaderMock
                .Setup(x => x.ReadLine())
                .Returns("exit");

            // Act
            sut.LoadHeroes();

            // Assert
            loggerMock.Verify(x => x.LogMessage("Enter the number of heroes to the power of 2"), Times.Once);
        }

        [Fact]
        public void Number_Is_Not_Power_Of_2_Considered_Invalid()
        {
            // Arrange
            var heroGeneratorMock = new Mock<IHeroGenerator>();
            var heroDeserializationMock = new Mock<IHeroJsonReader>();
            var heroSerializationMock = new Mock<IHeroJsonWriter>();
            var processArgumentsReaderMock = new Mock<IProcessArgumentsReader>();
            var loggerMock = new Mock<ILogger>();
            var userInputReaderMock = new Mock<IUserInputReader>();

            var sut = new HeroLoader(
                        heroGeneratorMock.Object,
                        heroDeserializationMock.Object,
                        heroSerializationMock.Object,
                        processArgumentsReaderMock.Object,
                        loggerMock.Object,
                        userInputReaderMock.Object);

            userInputReaderMock
                .SetupSequence(x => x.ReadLine())
                .Returns("3")
                .Returns("exit");

            // Act
            sut.LoadHeroes();

            // Assert
            loggerMock.Verify(x => x.LogError("You entered the number not to the power of 2, please, try again"), Times.Once);
        }

        [Fact]
        public void Only_Numbers_Accepted()
        {
            // Arrange
            var heroGeneratorMock = new Mock<IHeroGenerator>();
            var heroDeserializationMock = new Mock<IHeroJsonReader>();
            var heroSerializationMock = new Mock<IHeroJsonWriter>();
            var processArgumentsReaderMock = new Mock<IProcessArgumentsReader>();
            var loggerMock = new Mock<ILogger>();
            var userInputReaderMock = new Mock<IUserInputReader>();

            var sut = new HeroLoader(
                        heroGeneratorMock.Object,
                        heroDeserializationMock.Object,
                        heroSerializationMock.Object,
                        processArgumentsReaderMock.Object,
                        loggerMock.Object,
                        userInputReaderMock.Object);

            userInputReaderMock
                .SetupSequence(x => x.ReadLine())
                .Returns("abc")
                .Returns("exit");

            // Act
            sut.LoadHeroes();

            // Assert
            loggerMock.Verify(x => x.LogError("You entered not the number, please, try again"), Times.Once);
        }

        [Fact]
        public void Number_Is_Power_Of_2_Considered_Invalid()
        {
            // Arrange
            var heroGeneratorMock = new Mock<IHeroGenerator>();
            var heroDeserializationMock = new Mock<IHeroJsonReader>();
            var heroSerializationMock = new Mock<IHeroJsonWriter>();
            var processArgumentsReaderMock = new Mock<IProcessArgumentsReader>();
            var loggerMock = new Mock<ILogger>();
            var userInputReaderMock = new Mock<IUserInputReader>();

            var sut = new HeroLoader(
                        heroGeneratorMock.Object,
                        heroDeserializationMock.Object,
                        heroSerializationMock.Object,
                        processArgumentsReaderMock.Object,
                        loggerMock.Object,
                        userInputReaderMock.Object);

            userInputReaderMock
                .SetupSequence(x => x.ReadLine())
                .Returns("16")
                .Returns("exit");

            var randomNumberGeneratorMock = new Mock<IRandomNumberGenerator>();
            var eventLoggerMock = new Mock<IEventLogger>();
            var roundMock = new Mock<IRound>();

            heroGeneratorMock
                .Setup(x => x.Generate())
                .Returns(new Barbarian(eventLoggerMock.Object, randomNumberGeneratorMock.Object));

            roundMock
                .Setup(x => x.StartRound(It.IsAny<List<Hero>>()))
                .Returns(new List<Hero> { new Barbarian(eventLoggerMock.Object, randomNumberGeneratorMock.Object) });

            // Act
            sut.LoadHeroes();

            // Assert
            loggerMock.Verify(x => x.LogMessage("Let's the Game begin!"), Times.Once);
        }

        [Fact]
        public void Generate_From_Json_Is_NotNull()
        {
            // Arrange
            var heroGeneratorMock = new Mock<IHeroGenerator>();
            var heroDeserializationMock = new Mock<IHeroJsonReader>();
            var heroSerializationMock = new Mock<IHeroJsonWriter>();
            var processArgumentsReaderMock = new Mock<IProcessArgumentsReader>();
            var loggerMock = new Mock<ILogger>();
            var userInputReaderMock = new Mock<IUserInputReader>();

            var sut = new HeroLoader(
                        heroGeneratorMock.Object,
                        heroDeserializationMock.Object,
                        heroSerializationMock.Object,
                        processArgumentsReaderMock.Object,
                        loggerMock.Object,
                        userInputReaderMock.Object);

            processArgumentsReaderMock
                .Setup(x => x.GetProcessArguments())
                .Returns(new string[] { "-i", "LoadPack1.json" });

            heroDeserializationMock
                .Setup(x => x.DeserializeHeroFromJson("LoadPack1.json"))
                .Returns(new List<HeroDto> { new HeroDto() });

            var eventLoggerMock = new Mock<IEventLogger>();
            var randomNumberGeneratorMock = new Mock<IRandomNumberGenerator>();

            heroGeneratorMock
                .Setup(x => x.Generate(new HeroDto()))
                .Returns(new Undead(eventLoggerMock.Object, randomNumberGeneratorMock.Object));

            // Act
            var heroes = sut.LoadHeroes();

            // Assert
            Assert.NotNull(heroes);
        }

        [Fact]
        public void Generate_Using_K_Key_Is_NotNull()
        {
            // Arrange
            var heroGeneratorMock = new Mock<IHeroGenerator>();
            var heroDeserializationMock = new Mock<IHeroJsonReader>();
            var heroSerializationMock = new Mock<IHeroJsonWriter>();
            var processArgumentsReaderMock = new Mock<IProcessArgumentsReader>();
            var loggerMock = new Mock<ILogger>();
            var userInputReaderMock = new Mock<IUserInputReader>();

            var sut = new HeroLoader(
                        heroGeneratorMock.Object,
                        heroDeserializationMock.Object,
                        heroSerializationMock.Object,
                        processArgumentsReaderMock.Object,
                        loggerMock.Object,
                        userInputReaderMock.Object);

            processArgumentsReaderMock
                .Setup(x => x.GetProcessArguments())
                .Returns(new string[] { "-k", "2" });

            var eventLoggerMock = new Mock<IEventLogger>();
            var randomNumberGeneratorMock = new Mock<IRandomNumberGenerator>();

            heroGeneratorMock
                .Setup(x => x.Generate())
                .Returns(new Undead(eventLoggerMock.Object, randomNumberGeneratorMock.Object));

            // Act
            var heroes = sut.LoadHeroes();

            // Assert
            Assert.NotNull(heroes);
        }

        [Fact]
        public void Heroes_Saved_To_Json()
        {
            // Arrange
            var heroGeneratorMock = new Mock<IHeroGenerator>();
            var heroDeserializationMock = new Mock<IHeroJsonReader>();
            var heroSerializationMock = new Mock<IHeroJsonWriter>();
            var processArgumentsReaderMock = new Mock<IProcessArgumentsReader>();
            var loggerMock = new Mock<ILogger>();
            var userInputReaderMock = new Mock<IUserInputReader>();

            var sut = new HeroLoader(
                        heroGeneratorMock.Object,
                        heroDeserializationMock.Object,
                        heroSerializationMock.Object,
                        processArgumentsReaderMock.Object,
                        loggerMock.Object,
                        userInputReaderMock.Object);

            processArgumentsReaderMock
                .Setup(x => x.GetProcessArguments())
                .Returns(new string[] { "-k", "2", "-s", "SavePack1.json" });

            var eventLoggerMock = new Mock<IEventLogger>();
            var randomNumberGeneratorMock = new Mock<IRandomNumberGenerator>();

            heroGeneratorMock
                .Setup(x => x.Generate())
                .Returns(new Undead(eventLoggerMock.Object, randomNumberGeneratorMock.Object));

            heroSerializationMock
                .Setup(x => x.SerializeHeroToJson(new List<HeroDto> { new HeroDto() }, "SavePack1.json"));

            // Act
            var heroes = sut.LoadHeroes();

            // Assert
            loggerMock.Verify(x => x.LogMessage("Heroes successfully saved to set file"), Times.Once);
        }
    }
}
