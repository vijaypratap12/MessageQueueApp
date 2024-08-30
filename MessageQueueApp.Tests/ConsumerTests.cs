using MessageQueueApp.Business;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities;
using Moq;
using Xunit;
using Message = MessageQueueApp.Business.Message;

namespace MessageQueueApp.Tests
{
    public class ConsumerTests
    {
        [Fact]
        public void Consumer_SuccessfulMessageProcessing()
        {
            // Arrange
            var mockQueue = new Mock<IMessageQueue>();
            mockQueue.Setup(q => q.Dequeue()).Returns(new Message { Content = "Test Message" });

            var mockLogger = new Mock<ILogger<Consumer>>();

            var consumer = new Consumer(mockQueue.Object, mockLogger.Object);

            // Act
            consumer.Consume();

            // Assert
            Assert.Equal(1, consumer.GetSuccessCount());
            Assert.Equal(0, consumer.GetErrorCount());
        }

        [Fact]
        public void Consumer_ErrorMessageProcessing()
        {
            // Arrange
            var mockQueue = new Mock<IMessageQueue>();
            mockQueue.Setup(q => q.Dequeue()).Returns(new Message { Content = "error message" });

            var mockLogger = new Mock<ILogger<Consumer>>();

            var consumer = new Consumer(mockQueue.Object, mockLogger.Object);

            // Act
            consumer.Consume();

            // Assert
            Assert.Equal(0, consumer.GetSuccessCount());
            Assert.Equal(1, consumer.GetErrorCount());
        }

        [Fact]
        public void Consumer_NoMessageInQueue()
        {
            // Arrange
            var mockQueue = new Mock<IMessageQueue>();
            mockQueue.Setup(q => q.Dequeue()).Returns((Message)null);

            var mockLogger = new Mock<ILogger<Consumer>>();

            var consumer = new Consumer(mockQueue.Object, mockLogger.Object);

            // Act
            consumer.Consume();

            // Assert
            Assert.Equal(0, consumer.GetSuccessCount());
            Assert.Equal(0, consumer.GetErrorCount());
        }
    }
}
