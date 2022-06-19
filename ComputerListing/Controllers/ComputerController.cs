using AutoMapper;
using ComputerListing.Models;
using ComputerListing.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ComputerListing.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComputerController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<ComputerController> _logger;
        private readonly IMapper _mapper;

        public ComputerController(IUnitOfWork unitOfWork, ILogger<ComputerController> logger, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetComputers()
        {
            try
            {
                var computers = await _unitOfWork.Computers.GetAll();
                var results = _mapper.Map<IList<ComputerDTO>>(computers);
                return Ok(results);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Somthing went wrong in the {nameof(GetComputers)}");
                return StatusCode(500, "Internal Server Error, Please try Again Later!");
            }
        }

        [ProducesResponseType(statusCode: StatusCodes.Status204NoContent)]
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetComputer(int id)
        {
            try
            {
                var computer = await _unitOfWork.Computers.Get(q => q.Id == id, new List<string> { "Accessories" });
                var result = _mapper.Map<ComputerDTO>(computer);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Somthing went wrong in the {nameof(GetComputer)}");
                return StatusCode(500, "Internal Server Error, Please try Again Later!");
            }
        }

    }
}
