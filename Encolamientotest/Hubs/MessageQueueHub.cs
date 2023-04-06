using Encolamientotest.Entities;
using Encolamientotest.Process;
using Microsoft.AspNetCore.SignalR;

namespace Encolamientotest.Hubs
{
    public class MessageQueueHub : Hub
    {

        private IMessageQueue _messageQueue { get;  set; }
        public MessageQueueHub(IMessageQueue messageQueue)
        {
            _messageQueue = messageQueue;
        }

        public void Enqueue(Retiro retiro)
        {
            _messageQueue.Enqueue(retiro);
        }
    }
}
