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
        [ProducesResponseType(statusCode: StatusCodes.Status400BadRequest)]
        [ProducesResponseType(statusCode: StatusCodes.Status500InternalServerError)]
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



        [HttpGet("{id:int}", Name = "GetComputer")]
        [ProducesResponseType(statusCode: StatusCodes.Status204NoContent)]
        [ProducesResponseType(statusCode: StatusCodes.Status400BadRequest)]
        [ProducesResponseType(statusCode: StatusCodes.Status500InternalServerError)]     
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


        [Authorize(Roles = "Administrator")]
        [ProducesResponseType(statusCode: StatusCodes.Status201Created)]
        [ProducesResponseType(statusCode: StatusCodes.Status400BadRequest)]
        [ProducesResponseType(statusCode: StatusCodes.Status500InternalServerError)]
        [HttpPost]
        public async Task<IActionResult> CreateComputer([FromBody] CreateComputerDTO computerDTO)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogInformation($"Invalid Post Attempt in {nameof(CreateComputer)}");
                return BadRequest(ModelState);
            }

            try
            {
                var computer = _mapper.Map<Computer>(computerDTO);
                await _unitOfWork.Computers.Insert(computer);
                await _unitOfWork.Save();

                return CreatedAtRoute("GetComputer", new { id = computer.Id }, computer);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Somthing went wrong in the {nameof(CreateComputer)}");
                return StatusCode(500, "Internal Server Error, Please try Again Later!");
            }

        }



        [Authorize(Roles = "Administrator")]
        [ProducesResponseType(statusCode: StatusCodes.Status204NoContent)]
        [ProducesResponseType(statusCode: StatusCodes.Status400BadRequest)]
        [ProducesResponseType(statusCode: StatusCodes.Status500InternalServerError)]
        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateComputer(int id, [FromBody] UpdateComputerDTO computerDTO)
        {
            if (!ModelState.IsValid || id < 1)
            {
                _logger.LogInformation($"Invalid UPDATE Attempt in {nameof(UpdateComputer)}");
                return BadRequest(ModelState);
            }

            try
            {
                var computer = await _unitOfWork.Computers.Get(q => q.Id == id);
                if (computer == null)
                {
                    _logger.LogInformation($"Invalid UPDATE Attempt in {nameof(UpdateComputer)}");
                    return BadRequest("The Provided ComputerId is Invalid");
                }

                _mapper.Map(computerDTO, computer);
                _unitOfWork.Computers.Update(computer);
                await _unitOfWork.Save();

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Somthing went wrong in the {nameof(UpdateComputer)}");
                return StatusCode(500, "Internal Server Error, Please try Again Later!");
            }

        }



        [Authorize(Roles = "Administrator")]
        [ProducesResponseType(statusCode: StatusCodes.Status204NoContent)]
        [ProducesResponseType(statusCode: StatusCodes.Status400BadRequest)]
        [ProducesResponseType(statusCode: StatusCodes.Status500InternalServerError)]
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteComputer(int id)
        {
            if (id < 1)
            {
                _logger.LogInformation($"Invalid DELETE Attempt in {nameof(DeleteComputer)}");
                return BadRequest();
            }

            try
            {
                var computer = await _unitOfWork.Computers.Get(q => q.Id == id);
                if (computer == null)
                {
                    _logger.LogInformation($"Invalid DELETE Attempt in {nameof(DeleteComputer)}");
                    return BadRequest("The Provided computerId is Invalid");
                }

                await _unitOfWork.Computers.Delete(id);
                await _unitOfWork.Save();

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Somthing went wrong in the {nameof(DeleteComputer)}");
                return StatusCode(500, "Internal Server Error, Please try Again Later!");
            }

        }

    }
}
