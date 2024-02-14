namespace RPGSagaUnitTests.EntitiesTests
{
    using System;
    using Moq;
    using RpgSaga.Core.Data;
    using RpgSaga.Core.Entities;
    using RpgSaga.Core.Interfaces;
    using Xunit;

    public class HeroGeneratorTesting
    {
        [Fact]
        public void Generating_Hero_Is_Notnull()
        {
            // Arrange
            var serviceProviderMock = new Mock<IServiceProvider>();
            var nameGeneratorMock = new Mock<INameGenerator>();
            var randomNumberGeneratorMock = new Mock<IRandomNumberGenerator>();
            var eventLoggerMock = new Mock<IEventLogger>();

            var sut = new HeroGenerator(serviceProviderMock.Object, nameGeneratorMock.Object, randomNumberGeneratorMock.Object, eventLoggerMock.Object);

            serviceProviderMock
                .Setup(x => x.GetService(typeof(Undead)))
                .Returns(new Undead(eventLoggerMock.Object, randomNumberGeneratorMock.Object));

            nameGeneratorMock
                .Setup(x => x.GetRandomName())
                .Returns("TestHero");

            // Act
            var hero = sut.Generate();

            // Assert
            Assert.NotNull(hero);
        }

        [Fact]
        public void Generating_Hero_From_Json_Is_Notnull()
        {
            // Arrange
            var serviceProviderMock = new Mock<IServiceProvider>();
            var nameGeneratorMock = new Mock<INameGenerator>();
            var randomNumberGeneratorMock = new Mock<IRandomNumberGenerator>();
            var eventLoggerMock = new Mock<IEventLogger>();

            var sut = new HeroGenerator(serviceProviderMock.Object, nameGeneratorMock.Object, randomNumberGeneratorMock.Object, eventLoggerMock.Object);

            serviceProviderMock
                .Setup(x => x.GetService(typeof(Undead)))
                .Returns(new Undead(eventLoggerMock.Object, randomNumberGeneratorMock.Object));

            // Act
            var hero = sut.Generate(new HeroDto() { Type = "Undead", Name = "TestHero", Hp = 100, Power = 20 });

            // Assert
            Assert.NotNull(hero);
        }
    }
}
