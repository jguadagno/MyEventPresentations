using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JosephGuadagno.AzureHelpers.Storage.Interfaces;
using Moq;
using MyEventPresentations.Domain.Interfaces;
using MyEventPresentations.Domain.Models;
using Xunit;

namespace MyEventPresentations.BusinessLayer.Tests
{
    public class PresentationManagerTests
    {
        [Fact]
        public async Task SavePresentation_WithNullPresentation_ShouldThrowException()
        {
            // Arrange
            var queueMock = new Mock<IQueue>();
            var presentationRepositoryMock = new Mock<IPresentationRepository>();
            presentationRepositoryMock.Setup(presentationRepository =>
                presentationRepository.SavePresentationAsync(It.IsAny<Presentation>()));
            var presentationManager = new PresentationManager(presentationRepositoryMock.Object, queueMock.Object);

            // Act
            var ex = await
                Assert.ThrowsAsync<ArgumentNullException>(() => presentationManager.SavePresentationAsync(null));

            // Assert
            Assert.Equal("presentation", ex.ParamName);
            Assert.Equal("The presentation can not be null (Parameter 'presentation')", ex.Message);
        }

        [Fact]
        public async Task SavePresentation_WithPresentationIdLessThanOne_ShouldThrowException()
        {
            // Arrange
            var queueMock = new Mock<IQueue>();
            var presentationRepositoryMock = new Mock<IPresentationRepository>();
            presentationRepositoryMock.Setup(presentationRepository =>
                presentationRepository.SavePresentationAsync(It.IsAny<Presentation>()));
            var presentationManager = new PresentationManager(presentationRepositoryMock.Object, queueMock.Object);
            var presentation = new Presentation
                {PresentationId = 0, Abstract = "Abstract", Title = "Title", PowerpointUri = "PowerpointUri"};

            // Act
            var ex = await
                Assert.ThrowsAsync<ArgumentOutOfRangeException>(() =>
                    presentationManager.SavePresentationAsync(presentation));

            // Assert
            Assert.Equal("presentation", ex.ParamName);
            Assert.Equal("The presentation id can not be less than 1 (Parameter 'presentation')", ex.Message);
        }

        [Fact]
        public async Task SavePresentation_WithNullTitle_ShouldThrowException()
        {
            // Arrange
            var queueMock = new Mock<IQueue>();
            var presentationRepositoryMock = new Mock<IPresentationRepository>();
            presentationRepositoryMock.Setup(presentationRepository =>
                presentationRepository.SavePresentationAsync(It.IsAny<Presentation>()));
            var presentationManager = new PresentationManager(presentationRepositoryMock.Object, queueMock.Object);
            var presentation = new Presentation
                {PresentationId = 1, Abstract = "Abstract", Title = null, PowerpointUri = "PowerpointUri"};

            // Act
            var ex = await
                Assert.ThrowsAsync<ArgumentNullException>(() =>
                    presentationManager.SavePresentationAsync(presentation));

            // Assert
            Assert.Equal("Title", ex.ParamName);
            Assert.Equal("The Title of the presentation can not be null (Parameter 'Title')", ex.Message);
        }

        [Fact]
        public async Task SavePresentation_WithNullAbstract_ShouldThrowException()
        {
            // Arrange
            var queueMock = new Mock<IQueue>();
            var presentationRepositoryMock = new Mock<IPresentationRepository>();
            presentationRepositoryMock.Setup(presentationRepository =>
                presentationRepository.SavePresentationAsync(It.IsAny<Presentation>()));
            var presentationManager = new PresentationManager(presentationRepositoryMock.Object, queueMock.Object);
            var presentation = new Presentation
                {PresentationId = 1, Abstract = null, Title = "Title", PowerpointUri = "PowerpointUri"};

            // Act
            var ex = await
                Assert.ThrowsAsync<ArgumentNullException>(() =>
                    presentationManager.SavePresentationAsync(presentation));

            // Assert
            Assert.Equal("Abstract", ex.ParamName);
            Assert.Equal("The Abstract of the presentation can not be null (Parameter 'Abstract')", ex.Message);
        }

        [Fact]
        public async Task SavePresentation_WithValidPresentation_ShouldReturnPresentation()
        {
            // Arrange
            var queueMock = new Mock<IQueue>();
            var presentationRepositoryMock = new Mock<IPresentationRepository>();
            presentationRepositoryMock.Setup(presentationRepository =>
                    presentationRepository.SavePresentationAsync(It.IsAny<Presentation>()))
                .ReturnsAsync((Presentation presentationInput) => presentationInput);
            var presentationManager = new PresentationManager(presentationRepositoryMock.Object, queueMock.Object);
            var presentation = new Presentation
                {PresentationId = 1, Abstract = "Abstract", Title = "Title", PowerpointUri = "PowerpointUri"};

            // Act
            var savedPresentation = await presentationManager.SavePresentationAsync(presentation);

            // Assert
            Assert.NotNull(savedPresentation);
            Assert.Equal(presentation.PresentationId, savedPresentation.PresentationId);
            Assert.Equal(presentation.Abstract, savedPresentation.Abstract);
            Assert.Equal(presentation.Title, savedPresentation.Title);
            Assert.Equal(presentation.PowerpointUri, savedPresentation.PowerpointUri);
        }

        [Fact]
        public async Task GetPresentation_ShouldReturnPresentation()
        {
            // Arrange
            var queueMock = new Mock<IQueue>();
            var presentationRepositoryMock = new Mock<IPresentationRepository>();
            presentationRepositoryMock.Setup(presentationRepository =>
                    presentationRepository.GetPresentationAsync(It.IsAny<int>()))
                .ReturnsAsync((int id) => new Presentation
                {
                    PresentationId = id,
                    Abstract = "Abstract",
                    Title = "Title",
                    PowerpointUri = "PowerpointUri",
                });

            var presentationManager = new PresentationManager(presentationRepositoryMock.Object, queueMock.Object);
            var presentationId = 15; // any Random Number will do

            // Act
            var presentation = await presentationManager.GetPresentationAsync(presentationId);

            // Assert
            Assert.NotNull(presentation);
            Assert.Equal(presentationId, presentation.PresentationId);
            Assert.Equal("Abstract", presentation.Abstract);
            Assert.Equal("Title", presentation.Title);
            Assert.Equal("PowerpointUri", presentation.PowerpointUri);
        }

        [Fact]
        public async Task GetPresentations_ShouldReturnListOfPresentations()
        {
            // Arrange
            var queueMock = new Mock<IQueue>();
            var presentationRepositoryMock = new Mock<IPresentationRepository>();
            presentationRepositoryMock.Setup(presentationRepository => presentationRepository.GetPresentationsAsync())
                .ReturnsAsync(new List<Presentation>
                {
                    new Presentation
                        {PresentationId = 1, Abstract = "Abstract", Title = "Title", PowerpointUri = "PowerpointUri"},
                    new Presentation
                        {PresentationId = 2, Abstract = "Abstract", Title = "Title", PowerpointUri = "PowerpointUri"},
                });

            var presentationManager = new PresentationManager(presentationRepositoryMock.Object, queueMock.Object);

            // Act
            var presentations = await presentationManager.GetPresentationsAsync();

            // Assert
            Assert.NotNull(presentations);
            var listOfPresentations = presentations.ToList();
            Assert.True(listOfPresentations.Count == 2);

            for (var i = 0; i < listOfPresentations.Count; i++)
            {
                var presentation = listOfPresentations[i];
                Assert.Equal(i + 1, presentation.PresentationId);
                Assert.Equal("Abstract", presentation.Abstract);
                Assert.Equal("Title", presentation.Title);
                Assert.Equal("PowerpointUri", presentation.PowerpointUri);
            }
        }

        [Fact]
        public async Task DeletePresentation_ShouldReturnTrue()
        {
            // Arrange
            var queueMock = new Mock<IQueue>();
            var presentationRepositoryMock = new Mock<IPresentationRepository>();
            presentationRepositoryMock
                .Setup(presentationRepository => presentationRepository.DeletePresentationAsync(It.IsAny<int>()))
                .ReturnsAsync(true);
            var presentationManager = new PresentationManager(presentationRepositoryMock.Object, queueMock.Object);

            // Act
            var deletedPresentation = await presentationManager.DeletePresentationAsync(1); // Any number

            // Assert
            Assert.True(deletedPresentation);
        }

        [Fact]
        public async Task GetScheduledPresentation_ShouldReturnAScheduledPresentations()
        {
            // Arrange
            var queueMock = new Mock<IQueue>();
            var presentationRepositoryMock = new Mock<IPresentationRepository>();
            presentationRepositoryMock
                .Setup(presentationRepository => presentationRepository.GetScheduledPresentationAsync(It.IsAny<int>()))
                .ReturnsAsync(new ScheduledPresentation
                {
                    ScheduledPresentationId = 1, AttendeeCount = 1, Presentation = new Presentation()
                });
            var presentationManager = new PresentationManager(presentationRepositoryMock.Object, queueMock.Object);

            // Act
            var scheduledPresentation = await presentationManager.GetScheduledPresentationAsync(1);

            // Assert
            Assert.NotNull(scheduledPresentation);
            Assert.NotNull(scheduledPresentation.Presentation);
            Assert.Equal(1, scheduledPresentation.ScheduledPresentationId);
            Assert.Equal(1, scheduledPresentation.AttendeeCount);
        }

        [Fact]
        public async Task GetScheduledPresentation_ShouldReturnAListScheduledPresentations()
        {
            // Arrange
            var queueMock = new Mock<IQueue>();
            var presentationRepositoryMock = new Mock<IPresentationRepository>();
            presentationRepositoryMock
                .Setup(presentationRepository =>
                    presentationRepository.GetScheduledPresentationsForPresentationAsync(It.IsAny<int>()))
                .ReturnsAsync((int presentationIdInput) => new List<ScheduledPresentation>
                {
                    new ScheduledPresentation
                    {
                        ScheduledPresentationId = 1, AttendeeCount = 1,
                        Presentation = new Presentation {PresentationId = presentationIdInput}
                    },
                    new ScheduledPresentation
                    {
                        ScheduledPresentationId = 2, AttendeeCount = 1,
                        Presentation = new Presentation {PresentationId = presentationIdInput}
                    }
                });
            var presentationManager = new PresentationManager(presentationRepositoryMock.Object, queueMock.Object);
            var presentationId = 15; // Can be any int

            // Act
            var scheduledPresentations =
                await presentationManager.GetScheduledPresentationsForPresentationAsync(presentationId);

            // Assert
            Assert.NotNull(scheduledPresentations);
            var scheduledPresentationsAsList = scheduledPresentations.ToList();
            Assert.Equal(2, scheduledPresentationsAsList.Count);
            for (var i = 0; i < scheduledPresentationsAsList.Count; i++)
            {
                var scheduledPresentation = scheduledPresentationsAsList[i];
                Assert.NotNull(scheduledPresentation.Presentation);
                Assert.Equal(presentationId, scheduledPresentation.Presentation.PresentationId);
                Assert.Equal(i + 1, scheduledPresentation.ScheduledPresentationId);
                Assert.Equal(1, scheduledPresentation.AttendeeCount);
            }
        }

        [Fact]
        public async Task SaveScheduledPresentation_WithNullScheduledPresentation_ShouldThrowException()
        {
            // Arrange
            var queueMock = new Mock<IQueue>();
            var presentationRepositoryMock = new Mock<IPresentationRepository>();
            presentationRepositoryMock
                .Setup(presentationRepository => presentationRepository.SaveScheduledPresentationAsync(null));
            var presentationManager = new PresentationManager(presentationRepositoryMock.Object, queueMock.Object);

            // Act
            var ex = await Assert.ThrowsAsync<ArgumentNullException>(() =>
                presentationManager.SaveScheduledPresentationAsync(null));

            // Assert
            Assert.Equal("scheduledPresentation", ex.ParamName);
            Assert.Equal("The scheduled presentation can not be null (Parameter 'scheduledPresentation')", ex.Message);
        }

        [Fact]
        public async Task SaveScheduledPresentation_WithNullPresentation_ShouldThrowException()
        {
            // Arrange
            var queueMock = new Mock<IQueue>();
            var presentationRepositoryMock = new Mock<IPresentationRepository>();
            presentationRepositoryMock
                .Setup(presentationRepository =>
                    presentationRepository.SaveScheduledPresentationAsync(It.IsAny<ScheduledPresentation>()));
            var presentationManager = new PresentationManager(presentationRepositoryMock.Object, queueMock.Object);

            var scheduledPresentation = new ScheduledPresentation
            {
                Presentation = null
            };

            // Act
            var ex = await Assert.ThrowsAsync<ArgumentNullException>(() =>
                presentationManager.SaveScheduledPresentationAsync(scheduledPresentation));

            // Assert
            Assert.Equal("Presentation", ex.ParamName);
            Assert.Equal("The presentation can not be null (Parameter 'Presentation')", ex.Message);
        }

        [Fact]
        public async Task SaveScheduledPresentation_WithStartTimeGreaterThanEndTime_ShouldThrowException()
        {
            // Arrange
            var queueMock = new Mock<IQueue>();
            var presentationRepositoryMock = new Mock<IPresentationRepository>();
            presentationRepositoryMock
                .Setup(presentationRepository =>
                    presentationRepository.SaveScheduledPresentationAsync(It.IsAny<ScheduledPresentation>()));
            var presentationManager = new PresentationManager(presentationRepositoryMock.Object, queueMock.Object);
            var startTime = DateTime.Now;

            var scheduledPresentation = new ScheduledPresentation
            {
                Presentation = new Presentation(),
                StartTime = startTime,
                EndTime = startTime.AddMinutes(-1)
            };

            // Act
            var ex = await Assert.ThrowsAsync<ArgumentOutOfRangeException>(() =>
                presentationManager.SaveScheduledPresentationAsync(scheduledPresentation));

            // Assert
            Assert.Equal("StartTime", ex.ParamName);
            Assert.Equal(startTime, ex.ActualValue);
            Assert.StartsWith(
                "The start time of the presentation can not be greater then the end time (Parameter 'StartTime')",
                ex.Message);
        }

        [Fact]
        public async Task SaveScheduledPresentation_WithValidScheduledPresentation_ShouldReturnScheduledPresentation()
        {
            // Arrange
            var queueMock = new Mock<IQueue>();
            var presentationRepositoryMock = new Mock<IPresentationRepository>();
            presentationRepositoryMock
                .Setup(presentationRepository =>
                    presentationRepository.SaveScheduledPresentationAsync(It.IsAny<ScheduledPresentation>()))
                .ReturnsAsync((ScheduledPresentation scheduledPresentationInput) => scheduledPresentationInput);
            var presentationManager = new PresentationManager(presentationRepositoryMock.Object, queueMock.Object);

            var startTime = DateTime.Now;
            var scheduledPresentation = new ScheduledPresentation
            {
                Presentation = new Presentation(),
                StartTime = startTime,
                EndTime = startTime.AddMinutes(1)
            };

            // Act
            var savedScheduledPresentation =
                await presentationManager.SaveScheduledPresentationAsync(scheduledPresentation);

            // Assert
            Assert.NotNull(savedScheduledPresentation);
            Assert.NotNull(savedScheduledPresentation.Presentation);
            Assert.Equal(scheduledPresentation.StartTime, savedScheduledPresentation.StartTime);
            Assert.Equal(scheduledPresentation.EndTime, savedScheduledPresentation.EndTime);
        }
    }
}