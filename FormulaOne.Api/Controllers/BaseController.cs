using AutoMapper;
using FormulaOne.DataService.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FormulaOne.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BaseController(IUnitOfWork unitOfWork, IMapper mapper) : ControllerBase
{
    protected readonly IMapper _mapper = mapper;
    protected readonly IUnitOfWork _unitOfWork = unitOfWork;
}