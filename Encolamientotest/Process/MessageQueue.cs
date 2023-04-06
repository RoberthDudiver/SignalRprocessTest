using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Encolamientotest.Entities;
using Encolamientotest.Hubs;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
namespace Encolamientotest.Process
{
    public class MessageQueue : BackgroundService, IMessageQueue
    {
        private readonly Queue<RetiroDTO> _queue = new Queue<RetiroDTO>();
        private readonly ILogger<MessageQueue> _logger;
        private readonly IHubContext<MessageQueueHub> _hubContext;
        private readonly IHostedService _hostedService;
        private readonly Timer _timer;

        public MessageQueue(ILogger<MessageQueue> logger, IHubContext<MessageQueueHub> hubContext, IHostedService hostedService)
        {
            _logger = logger;
            _hubContext = hubContext;
            _hostedService = hostedService;

            // Crea un temporizador que verifica la cola cada 5 segundos
            _timer = new Timer(CheckQueue, null, TimeSpan.Zero, TimeSpan.FromSeconds(5));
        }

        public void Enqueue(RetiroDTO retiro)
        {
            lock (_queue)
            {
                _queue.Enqueue(retiro.ToConvertObjects<Retiro>()) ;
            }
        }

        private void CheckQueue(object state)
        {
            // Verifica si hay elementos en la cola
            if (_queue.Count > 0)
            {
                Retiro retiro;
                lock (_queue)
                {
                    retiro = _queue.Dequeue().ToConvertObjects<Retiro>();
                }

                try
                {
                   
                    Console.WriteLine("Retiro procesado");
                    // Llama a la API externa para procesar el elemento
                    // ProcessRetiro(retiro).Wait();
                   
                    retiro.Id = Guid.NewGuid().ToString();
                    retiro.Estado = "Procesado";
                    // Envía una actualización de estado usando SignalR
                    _hubContext.Clients.All.SendAsync("RetiroProcessed", retiro, "Processed").Wait();
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error procesando Retiro {Retiro}", retiro);
                    _hubContext.Clients.All.SendAsync("RetiroProcessed", retiro, "Error").Wait();
                }
            }
        }

        private async Task ProcessRetiro(Retiro retiro)
        {
            // TODO: Llama a la API externa para calcular la distancia
            int distance = await CalculateDistanceAsync(retiro.SucursalDestino.Direccion, retiro.Direccion);

            var result = new DistanceResult
            {
                Distance = distance,
                RetiroCode = retiro.Retiros[0]
            };
            retiro.Id = new Guid().ToString();
            retiro.Estado = "Procesado";
            // Envía el resultado de la distancia usando SignalR
            await _hubContext.Clients.All.SendAsync("DistanceResult", result);
        }

        private Task<int> CalculateDistanceAsync(string direccion1, string direccion2)
        {
            return Task.FromResult(900);
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            // No es necesario llamar StartProcessing aquí porque el temporizador ya está manejando la cola en segundo plano
            await Task.CompletedTask;
        }

        public override async Task StopAsync(CancellationToken cancellationToken)
        {
            // Detiene el temporizador cuando se detiene el servicio
            _timer.Dispose();
            await base.StopAsync(cancellationToken);
        }
    }
}
