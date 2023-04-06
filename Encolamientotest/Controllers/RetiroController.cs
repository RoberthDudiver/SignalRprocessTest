using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Encolamientotest.Entities;
using Encolamientotest.Hubs;
using Encolamientotest.Process;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;

namespace Encolamientotest
{
    [Route("api/[controller]")]
    [ApiController]
    public class RetiroController : ControllerBase
    {
        private readonly ILogger<RetiroController> _logger;
        private readonly IMessageQueue _retiroQueue;
        private readonly IHubContext<MessageQueueHub> _hubContext;

        public RetiroController(ILogger<RetiroController> logger, IMessageQueue retiroQueue, IHubContext<MessageQueueHub> hubContext)
        {
            _logger = logger;
            _retiroQueue = retiroQueue;
            _hubContext = hubContext;
        }

        [HttpPost]
        public async Task<IActionResult> AddRetiro(RetiroDTO retiro)
        {
            try
            {
                // Add the retiro to the queue
                _retiroQueue.Enqueue(retiro);

                // Send status update using SignalR
              ///  await _hubContext.Clients.All.SendAsync("RetiroAdded", retiro);

                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error adding Retiro {Retiro}", retiro);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
