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
    public class AchievementsController : BaseController
    {
        public AchievementsController(IUnitOfWork unitOfWork, Mapper mapper) : base(unitOfWork, mapper)
        {
        }

        [HttpGet]
        [Route("{driverId:guid}")]

        public async Task<IActionResult> GetDriverAchievement(Guid driverId)
        {
            var driverAchievement = await _unitOfWork.Achievements.GetDriverAchievementAsync(driverId);

            if (driverAchievement == null)
            {
                return NotFound("Achievement Not Found");

            }
            return Ok(_mapper.Map<DriverAchievementResponse>(driverAchievement));
        }

        [HttpPost("")]
        public async Task<IActionResult> AddAchievement([FromBody] CreateDriverAchievementRequest achievementRequest)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = _mapper.Map<Achievement>(achievementRequest);

            await _unitOfWork.Achievements.Add(result);
            await _unitOfWork.CompleteAsync();

            return CreatedAtAction(nameof(GetDriverAchievement), new { driverId = result.DriverId }, result);
        }
    }
}
