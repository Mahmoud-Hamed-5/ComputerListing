using AutoMapper;
using ComputerListing.Data;
using ComputerListing.Models;
using ComputerListing.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
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
        [HttpGet("{id:int}", Name = "GetAccessory")]
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



        [Authorize(Roles = "Administrator")]
        [ProducesResponseType(statusCode: StatusCodes.Status201Created)]
        [ProducesResponseType(statusCode: StatusCodes.Status400BadRequest)]
        [ProducesResponseType(statusCode: StatusCodes.Status500InternalServerError)]
        [HttpPost]
        public async Task<IActionResult> CreateAccessory([FromBody] CreateAccessoryDTO accessoryDTO)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogInformation($"Invalid Post Attempt in {nameof(CreateAccessory)}");
                return BadRequest(ModelState);
            }

            try
            {
                if (await _unitOfWork.Computers.Get(q => q.Id == accessoryDTO.ComputerId) == null)
                {
                    _logger.LogInformation($"Invalid Post Attempt in {nameof(CreateAccessory)}");
                    return BadRequest("The Provided ComputerId is Invalid");
                }

                var accessory = _mapper.Map<Accessory>(accessoryDTO);
                await _unitOfWork.Accessories.Insert(accessory);
                await _unitOfWork.Save();

                return CreatedAtRoute("GetAccessory", new { id = accessory.Id }, accessory);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Somthing went wrong in the {nameof(CreateAccessory)}");
                return StatusCode(500, "Internal Server Error, Please try Again Later!");
            }

        }



        [Authorize(Roles = "Administrator")]
        [ProducesResponseType(statusCode: StatusCodes.Status204NoContent)]
        [ProducesResponseType(statusCode: StatusCodes.Status400BadRequest)]
        [ProducesResponseType(statusCode: StatusCodes.Status500InternalServerError)]
        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateAccessory(int id, [FromBody] UpdateAccessoryDTO accessoryDTO)
        {
            if (!ModelState.IsValid || id < 1)
            {
                _logger.LogInformation($"Invalid UPDATE Attempt in {nameof(UpdateAccessory)}");
                return BadRequest(ModelState);
            }

            try
            {             
                var accessory = await _unitOfWork.Accessories.Get(q => q.Id == id);
                if (accessory == null)
                {
                    _logger.LogInformation($"Invalid UPDATE Attempt in {nameof(UpdateAccessory)}");
                    return BadRequest("The Provided AccessoryId is Invalid");
                }

                var computer = await _unitOfWork.Computers.Get(q => q.Id == accessoryDTO.ComputerId);
                if (computer == null)
                {
                    _logger.LogInformation($"Invalid UPDATE Attempt in {nameof(UpdateAccessory)}");
                    return BadRequest("The Provided ComputerId is Invalid");
                }

                _mapper.Map(accessoryDTO, accessory);
                _unitOfWork.Accessories.Update(accessory);
                await _unitOfWork.Save();

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Somthing went wrong in the {nameof(UpdateAccessory)}");
                return StatusCode(500, "Internal Server Error, Please try Again Later!");
            }

        }


        [Authorize(Roles = "Administrator")]
        [ProducesResponseType(statusCode: StatusCodes.Status204NoContent)]
        [ProducesResponseType(statusCode: StatusCodes.Status400BadRequest)]
        [ProducesResponseType(statusCode: StatusCodes.Status500InternalServerError)]
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteAccessory(int id)
        {
            if (id < 1)
            {
                _logger.LogInformation($"Invalid DELETE Attempt in {nameof(DeleteAccessory)}");
                return BadRequest();
            }

            try
            {
                var accessory = await _unitOfWork.Accessories.Get(q => q.Id == id);
                if (accessory == null)
                {
                    _logger.LogInformation($"Invalid DELETE Attempt in {nameof(DeleteAccessory)}");
                    return BadRequest("The Provided AccessoryId is Invalid");
                }              

                await _unitOfWork.Accessories.Delete(id);
                await _unitOfWork.Save();

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Somthing went wrong in the {nameof(DeleteAccessory)}");
                return StatusCode(500, "Internal Server Error, Please try Again Later!");
            }

        }

    }
}
