using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageQueueApp.Business
{
    public class Consumer
    {
        private readonly IMessageQueue _messageQueue;
        private readonly ILogger<Consumer> _logger;
        private int _successCount;
        private int _errorCount;

        public Consumer(IMessageQueue messageQueue, ILogger<Consumer> logger)
        {
            _messageQueue = messageQueue;
            _logger = logger;
        }

        public Message Consume()
        {
            var message = _messageQueue.Dequeue(); // Dequeue the next message from the queue
            if (message != null) 
            {
                try
                {
                    if (message.Content.Contains("error"))
                    {
                        throw new System.Exception("Processing error");
                    }

                    _successCount++;
                    _logger.LogInformation($"Successfully processed message: {message.Content}");
                }
                catch (System.Exception ex) 
                {
                    _errorCount++; 
                    _logger.LogError(ex, $"Error while processing the message: {message.Content}");
                }
            }
            return message;
        }

        public int GetSuccessCount() => _successCount;
        public int GetErrorCount() => _errorCount;
    }
}
