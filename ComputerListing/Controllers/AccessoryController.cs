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
    public class AccessoryController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<AccessoryController> _logger;
        private readonly IMapper _mapper;

        public AccessoryController(IUnitOfWork unitOfWork, ILogger<AccessoryController> logger, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
        }


        [ProducesResponseType(statusCode: StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(statusCode: StatusCodes.Status200OK)]
        [HttpGet]
        public async Task<IActionResult> GetAccessories()
        {
            try
            {
                var accessories = await _unitOfWork.Accessories.GetAll();
                var results = _mapper.Map<IList<AccessoryDTO>>(accessories);
                return Ok(results);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Somthing went worng in the {nameof(GetAccessories)}");
                return StatusCode(500, "Internal Server Error, Please Try Again Later");
            }
        }



        [ProducesResponseType(statusCode: StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(statusCode: StatusCodes.Status204NoContent)]
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetAccessory(int id)
        {
            try
            {
                var accessory = await _unitOfWork.Accessories.Get(q => q.Id == id , new List<string> { "Computer" });
                var result = _mapper.Map<AccessoryDTO>(accessory);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Somthing went worng in the {nameof(GetAccessory)}");
                return StatusCode(500, "Internal Server Error, Please Try Again Later");
            }
        }

    }
}
