namespace RPGSagaUnitTests.EntitiesTests
{
    using Moq;
    using RpgSaga.Core;
    using RpgSaga.Core.Interfaces;
    using Xunit;

    public class NameGeneratorTesting
    {
        [Fact]
        public void Generated_Name_Is_NotNull()
        {
            // Arrange
            var randomNumberGeneratorMock = new Mock<IRandomNumberGenerator>();
            var sut = new NameGenerator(randomNumberGeneratorMock.Object);

            randomNumberGeneratorMock
                .SetupSequence(x => x.CreateRandomNumber(1, 10))
                .Returns(1)
                .Returns(2)
                .Returns(6);

            // Act
            string name = sut.GetRandomName();

            // Assert
            Assert.NotNull(name);
        }
    }
}
