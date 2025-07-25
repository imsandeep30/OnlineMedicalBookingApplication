using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineMedicineBookingApplication.Application.Interfaces;
using OnlineMedicineBookingApplication.Application.Models.MedicineDTOS;

namespace OnlineMedicineBookingApplication.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MedicineController : ControllerBase
    {
        public readonly IMedicineService _medicineService;
        public MedicineController(IMedicineService medicineService)
        {
            _medicineService = medicineService;
        }

        [HttpPost("add-medicine")]
        public async Task<IActionResult> AddMedicineAsync([FromBody] AddMedicineDTO medicineDto)
        {
            var result = await _medicineService.AddMedicine(medicineDto);
            if (result == null)
            {
                return BadRequest("Invalid medicine data.");
            }

            return Ok(medicineDto);
        }

        [HttpPut("update-medicine/{id}")]
        public async Task<IActionResult> UpdateMedicineAsync(int id, [FromBody] MedicineDTO medicineDto)
        {
            if (id != medicineDto.MedicineId)
            {
                return BadRequest("Medicine ID mismatch.");
            }
            var updatedMedicine = await _medicineService.UpdateMedicine(medicineDto);
            if (updatedMedicine == null)
            {
                return NotFound("Medicine not found.");
            }
            return Ok(updatedMedicine);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetMedicineByIdAsync(int id)
        {
            var medicine = await _medicineService.GetMedicineByIdAsync(id);
            if (medicine == null)
                return NotFound();
            return Ok(medicine);
        }
        [HttpGet("all-medicines")]
        public async Task<IActionResult> GetAllMedicinesAsync()
        {
            var medicines = await _medicineService.GetAllMedicinesAsync();
            return Ok(medicines);
        }
        [HttpPost("filter-medicines")]
        public async Task<IActionResult> FilterMedicinesAsync([FromBody] MedicineFilterDTO filter)
        {
            if (filter == null)
            {
                return BadRequest("Invalid filter data.");
            }
            var filteredMedicines = await _medicineService.FilterMedicinesAsync(filter);
            return Ok(filteredMedicines);
        }
        [HttpDelete("delete-medicine/{name}")]
        public async Task<IActionResult> DeleteMedicineAsync(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return BadRequest("Invalid medicine name.");
            }
            try
            {
                await _medicineService.DeleteMedicine(name);
                return Ok("Medicine deleted successfully.");
            }
            catch (KeyNotFoundException)
            {
                return NotFound("Medicine not found.");
            }
        }
    }
}
