namespace DeviceManager.WebApi.Controllers
{
    using DeviceManager.Application.Features.Devices.Commands.DeleteDeviceById;
    using DeviceManager.Application.Features.Devices.Queries.GetAllDevices;
    using DeviceManager.Application.Features.Devices.Queries.GetDeviceById;
    using DeviceManager.Application.Features.Devices.Queries.GetDevicesByBrand;
    using DeviceManager.WebApi.Models.Device;
    using MediatR;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.JsonPatch;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using System.Threading.Tasks;

    [ApiController]
    [Route("[controller]")]
    [Produces("application/json")]
    public class DeviceController : ControllerBase
    {
        private readonly ILogger<DeviceController> _logger;
        private readonly IMediator _mediator;

        public DeviceController(
            ILogger<DeviceController> logger,
            IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        // POST /Device
        [HttpPost(Name = "AddDevice")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Post([FromBody] AddDevice request)
        {
            _logger.LogDebug($"Add Device {request}");

            var response = await _mediator.Send(request.ToCommand());

            return Created($"/device/{response.Data}", null);
        }

        // GET /Device/5
        [HttpGet("{id}", Name ="GetDeviceByIdentifier")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get(long id)
        {
            _logger.LogDebug($"Get Device {id}");

            var response = await _mediator.Send(new GetDeviceByIdQuery() { Id = id });

            return Ok(response.Data);
        }

        // GET: /Device
        [HttpGet(Name = "ListAllDevices")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Get()
        {
            _logger.LogDebug($"List All Devices.");

            var qry = new GetAllDevicesQuery();

            var response = await _mediator.Send(qry);

            return Ok(response.Data);
        }

        // GET: /Device/bybrand
        [HttpGet("bybrand", Name = "ListDevicesByBrand")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Get(string brand)
        {
            _logger.LogDebug($"List Devices by Brand.");

            var qry = new GetDevicesByBrandQuery() { Brand = brand };

            var response = await _mediator.Send(qry);

            return Ok(response.Data);
        }

        // PUT /Device/5
        [HttpPut("{id}", Name = "FullUpdateDevice")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Put(long id, [FromBody] UpdateDevice request)
        {
            _logger.LogDebug($"Full Update Device {id}");

            _ = await _mediator.Send(request.ToCommand(id));

            return NoContent();
        }

        // PATCH /Device/5
        [HttpPatch("{id}", Name = "PartialUpdateDevice")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Patch(long id, [FromBody] JsonPatchDocument<UpdateDevice> request)
        {
            _logger.LogDebug($"Partial Update Device {id}");

            var response = await _mediator.Send(new GetDeviceByIdQuery() { Id = id });

            UpdateDevice updateDevice = new(response.Data.Name, response.Data.Brand);

            request.ApplyTo(updateDevice);

            _ = await _mediator.Send(updateDevice.ToCommand(id));

            return NoContent();
        }


        // DELETE /Device/5
        [HttpDelete("{id}", Name = "DeleteDevice")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(long id)
        {
            _logger.LogDebug($"Delete Device {id}");

            DeleteDeviceByIdCommand cmd = new() { Id = id };

            _ = await _mediator.Send(cmd);
            
            return NoContent();
        }


        //// GET: /Device
        //[HttpGet(Name = "ListAllDevices")]
        //[ProducesResponseType(StatusCodes.Status200OK)]
        //public async Task<IActionResult> Get(int index, int size)
        //{
        //    _logger.LogDebug($"Get Devices. index:{index} size:{size} ");

        //    var qry = new GetAllDevicesQuery()
        //    {
        //        PageNumber = index + 1,
        //        PageSize = size < 1 ? 10 : (size > 1000 ? 1000 : size)
        //    };

        //    var response = await _mediator.Send(qry);

        //    return Ok(response.Data);
        //}

    }
}
