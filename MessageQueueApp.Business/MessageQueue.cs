using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageQueueApp.Business
{
    public class MessageQueue : IMessageQueue
    {
        private readonly ConcurrentQueue<Message> _queue = new ConcurrentQueue<Message>();

        public void Enqueue(Message message)
        {
            _queue.Enqueue(message);
        }

        public Message Dequeue()
        {
            _queue.TryDequeue(out var message);
            return message;
        }
    }
}
