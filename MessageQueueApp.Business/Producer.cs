using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageQueueApp.Business
{
    public class Producer
    {
        private readonly IMessageQueue _messageQueue;

        public Producer(IMessageQueue messageQueue)
        {
            _messageQueue = messageQueue;
        }

        public void Produce(string content)
        {
            var message = new Message { Content = content };
            _messageQueue.Enqueue(message);
        }
    }
}
