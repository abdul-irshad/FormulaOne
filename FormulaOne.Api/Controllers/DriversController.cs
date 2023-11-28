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
        [HttpGet]

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
    }
}
