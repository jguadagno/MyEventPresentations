using System;
using System.Collections.Generic;
using System.Linq;
using Moq;
using MyEventPresentations.Domain.Interfaces;
using MyEventPresentations.Domain.Models;
using Xunit;

namespace MyEventPresentations.BusinessLayer.Tests
{
    public class PresentationManagerTests
    {

        [Fact]
        public void SavePresentation_WithNullPresentation_ShouldThrowException()
        {
            // Arrange
            var presentationRepositoryMock = new Mock<IPresentationRepository>();
            presentationRepositoryMock.Setup(presentationRepository =>
                    presentationRepository.SavePresentation(It.IsAny<Presentation>()));
            var presentationManager = new PresentationManager(presentationRepositoryMock.Object);
            
            // Act
            var ex =
                Assert.Throws<ArgumentNullException>(() => presentationManager.SavePresentation(null));

            // Assert
            Assert.Equal("presentation", ex.ParamName);
            Assert.Equal("The presentation can not be null (Parameter 'presentation')", ex.Message);
        }
        
        [Fact]
        public void SavePresentation_WithPresentationIdLessThanOne_ShouldThrowException()
        {
            // Arrange
            var presentationRepositoryMock = new Mock<IPresentationRepository>();
            presentationRepositoryMock.Setup(presentationRepository =>
                    presentationRepository.SavePresentation(It.IsAny<Presentation>()));
            var presentationManager = new PresentationManager(presentationRepositoryMock.Object);
            var presentation = new Presentation
                {PresentationId = 0, Abstract = "Abstract", Title = "Title", PowerpointUri = "PowerpointUri"};
            
            // Act
            var ex =
                Assert.Throws<ArgumentOutOfRangeException>(() => presentationManager.SavePresentation(presentation));
            
            // Assert
            Assert.Equal("presentation", ex.ParamName);
            Assert.Equal("The presentation id can not be less than 1 (Parameter 'presentation')", ex.Message);
        }
        
        [Fact]
        public void SavePresentation_WithNullTitle_ShouldThrowException()
        {
            // Arrange
            var presentationRepositoryMock = new Mock<IPresentationRepository>();
            presentationRepositoryMock.Setup(presentationRepository =>
                    presentationRepository.SavePresentation(It.IsAny<Presentation>()));
            var presentationManager = new PresentationManager(presentationRepositoryMock.Object);
            var presentation = new Presentation
                {PresentationId = 1, Abstract = "Abstract", Title = null, PowerpointUri = "PowerpointUri"};
            
            // Act
            var ex =
                Assert.Throws<ArgumentNullException>(() => presentationManager.SavePresentation(presentation));
            
            // Assert
            Assert.Equal("Title", ex.ParamName);
            Assert.Equal("The Title of the presentation can not be null (Parameter 'Title')", ex.Message);
        }
        
        [Fact]
        public void SavePresentation_WithNullAbstract_ShouldThrowException()
        {
            // Arrange
            var presentationRepositoryMock = new Mock<IPresentationRepository>();
            presentationRepositoryMock.Setup(presentationRepository =>
                    presentationRepository.SavePresentation(It.IsAny<Presentation>()));
            var presentationManager = new PresentationManager(presentationRepositoryMock.Object);
            var presentation = new Presentation
                {PresentationId = 1, Abstract = null, Title = "Title", PowerpointUri = "PowerpointUri"};
            
            // Act
            var ex =
                Assert.Throws<ArgumentNullException>(() => presentationManager.SavePresentation(presentation));
            
            // Assert
            Assert.Equal("Abstract", ex.ParamName);
            Assert.Equal("The Abstract of the presentation can not be null (Parameter 'Abstract')", ex.Message);
        }
                
        [Fact]
        public void SavePresentation_WithValidPresentation_ShouldReturnPresentation()
        {
            // Arrange
            var presentationRepositoryMock = new Mock<IPresentationRepository>();
            presentationRepositoryMock.Setup(presentationRepository =>
                    presentationRepository.SavePresentation(It.IsAny<Presentation>()))
                .Returns((Presentation presentationInput) => presentationInput);
            var presentationManager = new PresentationManager(presentationRepositoryMock.Object);
            var presentation = new Presentation
                {PresentationId = 1, Abstract = "Abstract", Title = "Title", PowerpointUri = "PowerpointUri"};
            
            // Act
            var savedPresentation =  presentationManager.SavePresentation(presentation);
            
            // Assert
            Assert.NotNull(savedPresentation);
            Assert.Equal(presentation.PresentationId, savedPresentation.PresentationId);
            Assert.Equal(presentation.Abstract, savedPresentation.Abstract);
            Assert.Equal(presentation.Title, savedPresentation.Title);
            Assert.Equal(presentation.PowerpointUri, savedPresentation.PowerpointUri);
        }
        
        [Fact]
        public void GetPresentation_ShouldReturnPresentation()
        {
            // Arrange
            var presentationRepositoryMock = new Mock<IPresentationRepository>();
            presentationRepositoryMock.Setup(presentationRepository => presentationRepository.GetPresentation(It.IsAny<int>()))
                .Returns((int id) => new Presentation
                    {PresentationId = id, Abstract = "Abstract", Title = "Title", PowerpointUri = "PowerpointUri"});

            var presentationManager = new PresentationManager(presentationRepositoryMock.Object);
            var presentationId = 15; // any Random Number will do
            
            // Act
            var presentation = presentationManager.GetPresentation(presentationId);

            // Assert
            Assert.NotNull(presentation);
            Assert.Equal(presentationId, presentation.PresentationId);
            Assert.Equal("Abstract", presentation.Abstract);
            Assert.Equal("Title", presentation.Title);
            Assert.Equal("PowerpointUri", presentation.PowerpointUri);
        }
        
        [Fact]
        public void GetPresentations_ShouldReturnListOfPresentations()
        {
            // Arrange
            var presentationRepositoryMock = new Mock<IPresentationRepository>();
            presentationRepositoryMock.Setup(presentationRepository => presentationRepository.GetPresentations())
                .Returns(new List<Presentation>
                {
                    new Presentation
                        {PresentationId = 1, Abstract = "Abstract", Title = "Title", PowerpointUri = "PowerpointUri"},
                    new Presentation
                        {PresentationId = 2, Abstract = "Abstract", Title = "Title", PowerpointUri = "PowerpointUri"},

                });

            var presentationManager = new PresentationManager(presentationRepositoryMock.Object);
            
            // Act
            var presentations = presentationManager.GetPresentations();

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
        public void DeletePresentation_ShouldReturnTrue()
        {
            // Arrange
            var presentationRepositoryMock = new Mock<IPresentationRepository>();
            presentationRepositoryMock
                .Setup(presentationRepository => presentationRepository.DeletePresentation(It.IsAny<int>()))
                .Returns(true);
            var presentationManager = new PresentationManager(presentationRepositoryMock.Object);
            
            // Act
            var deletedPresentation = presentationManager.DeletePresentation(1); // Any number
            
            // Assert
            Assert.True(deletedPresentation);
        }

        [Fact]
        public void GetScheduledPresentation_ShouldReturnAScheduledPresentations()
        {
            // Arrange
            var presentationRepositoryMock = new Mock<IPresentationRepository>();
            presentationRepositoryMock
                .Setup(presentationRepository => presentationRepository.GetScheduledPresentation(It.IsAny<int>()))
                .Returns(new ScheduledPresentation
                {
                    ScheduledPresentationId = 1, AttendeeCount = 1, Presentation = new Presentation()
                });
            var presentationManager = new PresentationManager(presentationRepositoryMock.Object);
            
            // Act
            var scheduledPresentation = presentationManager.GetScheduledPresentation(1);
            
            // Assert
            Assert.NotNull(scheduledPresentation);
            Assert.NotNull(scheduledPresentation.Presentation);
            Assert.Equal(1, scheduledPresentation.ScheduledPresentationId);
            Assert.Equal(1, scheduledPresentation.AttendeeCount);
        }
        
        [Fact]
        public void GetScheduledPresentation_ShouldReturnAListScheduledPresentations()
        {
            // Arrange
            var presentationRepositoryMock = new Mock<IPresentationRepository>();
            presentationRepositoryMock
                .Setup(presentationRepository =>
                    presentationRepository.GetScheduledPresentationsForPresentation(It.IsAny<int>()))
                .Returns((int presentationIdInput) => new List<ScheduledPresentation>
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
            var presentationManager = new PresentationManager(presentationRepositoryMock.Object);
            var presentationId = 15; // Can be any int
            
            // Act
            var scheduledPresentations = presentationManager.GetScheduledPresentationsForPresentation(presentationId);
            
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
        public void SaveScheduledPresentation_WithNullScheduledPresentation_ShouldThrowException()
        {
            // Arrange
            var presentationRepositoryMock = new Mock<IPresentationRepository>();
            presentationRepositoryMock
                .Setup(presentationRepository => presentationRepository.SaveScheduledPresentation(null));
            var presentationManager = new PresentationManager(presentationRepositoryMock.Object);
            
            // Act
            var ex = Assert.Throws<ArgumentNullException>(() => presentationManager.SaveScheduledPresentation(null));

            // Assert
            Assert.Equal("scheduledPresentation", ex.ParamName);
            Assert.Equal("The scheduled presentation can not be null (Parameter 'scheduledPresentation')", ex.Message);
        }
        
        [Fact]
        public void SaveScheduledPresentation_WithNullPresentation_ShouldThrowException()
        {
            // Arrange
            var presentationRepositoryMock = new Mock<IPresentationRepository>();
            presentationRepositoryMock
                .Setup(presentationRepository => presentationRepository.SaveScheduledPresentation(It.IsAny<ScheduledPresentation>()));
            var presentationManager = new PresentationManager(presentationRepositoryMock.Object);
            
            var scheduledPresentation = new ScheduledPresentation
            {
                Presentation = null
            };
            
            // Act
            var ex = Assert.Throws<ArgumentNullException>(() => presentationManager.SaveScheduledPresentation(scheduledPresentation));

            // Assert
            Assert.Equal("Presentation", ex.ParamName);
            Assert.Equal("The presentation can not be null (Parameter 'Presentation')", ex.Message);
        }
        
        [Fact]
        public void SaveScheduledPresentation_WithStartTimeGreaterThanEndTime_ShouldThrowException()
        {
            // Arrange
            var presentationRepositoryMock = new Mock<IPresentationRepository>();
            presentationRepositoryMock
                .Setup(presentationRepository => presentationRepository.SaveScheduledPresentation(It.IsAny<ScheduledPresentation>()));
            var presentationManager = new PresentationManager(presentationRepositoryMock.Object);
            var startTime = DateTime.Now;
            
            var scheduledPresentation = new ScheduledPresentation
            {
                Presentation = new Presentation(),
                StartTime = startTime,
                EndTime = startTime.AddMinutes(-1)
            };
            
            // Act
            var ex = Assert.Throws<ArgumentOutOfRangeException>(() => presentationManager.SaveScheduledPresentation(scheduledPresentation));

            // Assert
            Assert.Equal("StartTime", ex.ParamName);
            Assert.Equal(startTime, ex.ActualValue);
            Assert.StartsWith("The start time of the presentation can not be greater then the end time (Parameter 'StartTime')", ex.Message);
        }

        [Fact]
        public void SaveScheduledPresentation_WithValidScheduledPresentation_ShouldReturnScheduledPresentation()
        {
            // Arrange
            var presentationRepositoryMock = new Mock<IPresentationRepository>();
            presentationRepositoryMock
                .Setup(presentationRepository => presentationRepository.SaveScheduledPresentation(It.IsAny<ScheduledPresentation>()))
                .Returns<ScheduledPresentation>((scheduledPresentationInput) => scheduledPresentationInput);
            var presentationManager = new PresentationManager(presentationRepositoryMock.Object);
            
            var startTime = DateTime.Now;
            var scheduledPresentation = new ScheduledPresentation
            {
                Presentation = new Presentation(),
                StartTime = startTime,
                EndTime = startTime.AddMinutes(1)
            };
            
            // Act
            var savedScheduledPresentation = presentationManager.SaveScheduledPresentation(scheduledPresentation);
            
            // Assert
            Assert.NotNull(savedScheduledPresentation);
            Assert.NotNull(savedScheduledPresentation.Presentation);
            Assert.Equal(scheduledPresentation.StartTime, savedScheduledPresentation.StartTime);
            Assert.Equal(scheduledPresentation.EndTime, savedScheduledPresentation.EndTime);
        }
    }
}