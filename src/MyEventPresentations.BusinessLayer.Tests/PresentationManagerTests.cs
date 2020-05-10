using System;
using System.Runtime.CompilerServices;
using Moq;
using MyEventPresentations.Domain.Interfaces;
using MyEventPresentations.Domain.Models;
using Xunit;

namespace MyEventPresentations.BusinessLayer.Tests
{
    public class PresentationManagerTests
    {
        [Fact]
        public void GetPresentation_ShouldReturnObject()
        {
            // Arrange
            var mock = new Mock<IPresentationManager>();
            mock.Setup(presentationManager => presentationManager.GetPresentation(It.IsAny<int>()))
                .Returns(new Presentation
                    {PresentationId = 1, Abstract = "Abstract", Title = "Title", PowerpointUri = "PowerpointUri"});

            // Act
            var presentation = mock.Object.GetPresentation(1);

            // Assert
            Assert.NotNull(presentation);
            Assert.Equal("Abstract", presentation.Abstract);
            Assert.Equal("Title", presentation.Title);
            Assert.Equal("PowerpointUri", presentation.PowerpointUri);
            Assert.Equal(1, presentation.PresentationId);
        }
    }
}