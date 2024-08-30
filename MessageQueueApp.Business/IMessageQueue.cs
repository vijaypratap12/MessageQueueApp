using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageQueueApp.Business
{
    public interface IMessageQueue
    {
        void Enqueue(Message message);
        Message Dequeue();
    }
}
