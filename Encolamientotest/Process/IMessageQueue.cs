using Encolamientotest.Entities;

namespace Encolamientotest.Process
{
    public interface IMessageQueue
    {
        void Enqueue(RetiroDTO retiro);
        Task StopAsync(CancellationToken cancellationToken);
    }
}