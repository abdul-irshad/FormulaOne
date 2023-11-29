using AutoMapper;
using FormulaOne.DataService.Repositories.Interfaces;
using FormulaOne.Entities.DbSet;
using FormulaOne.Entities.Dtos.Requests;
using FormulaOne.Entities.Dtos.Responses;
using Microsoft.AspNetCore.Mvc;

namespace FormulaOne.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DriversController : BaseController
    {

        public DriversController(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {

        }

        [HttpGet("GetAllDrivers")]
        public async Task<IActionResult> GetAllDrivers()
        {
            var driver = await _unitOfWork.Drivers.All();

            return Ok(_mapper.Map<IEnumerable<GetDriverResponse>>(driver));
        }

        [HttpGet("GetDriverById")]
        [Route("{driverId:Guid}")]
        public async Task<IActionResult> GetDriverById(Guid driverId)
        {
            var driver = await _unitOfWork.Drivers.GetById(driverId);

            if (driver == null)
                return NotFound();

            return Ok(_mapper.Map<GetDriverResponse>(driver));
        }

        [HttpPost("")]

        public async Task<IActionResult> AddDriver([FromBody] CreateDriverRequest createDriverRequest)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = _mapper.Map<Driver>(createDriverRequest);

            await _unitOfWork.Drivers.Add(result);
            await _unitOfWork.CompleteAsync();

            return CreatedAtAction(nameof(GetDriverById), new { driverId = result.Id }, result);
        }

        [HttpPut("")]
        public async Task<IActionResult> UpdateDriver([FromBody] UpdateDriverRequest updateDriverRequest)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = _mapper.Map<Driver>(updateDriverRequest);

            await _unitOfWork.Drivers.Update(result);
            await _unitOfWork.CompleteAsync();

            return NoContent();
        }
    }
}
