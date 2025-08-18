using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineMedicineBookingApplication.Application.Interfaces;
using OnlineMedicineBookingApplication.Application.Models.MedicineDTOS;
using Microsoft.AspNetCore.Authorization;

namespace OnlineMedicineBookingApplication.API.Controllers
{
    // Controller to handle medicine-related operations
    [Route("api/[controller]")]
    [ApiController]
    public class MedicineController : ControllerBase
    {
        // Injecting the medicine service
        public readonly IMedicineService _medicineService;

        // Constructor to initialize medicine service
        public MedicineController(IMedicineService medicineService)
        {
            _medicineService = medicineService;
        }

        // Add a new medicine to the system (Admin only)
        [HttpPost("add-medicine")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddMedicineAsync([FromBody] AddMedicineDTO medicineDto)
        {
            var result = await _medicineService.AddMedicine(medicineDto);

            // If adding fails, return bad request
            if (result == null)
            {
                return BadRequest("Invalid medicine data.");
            }

            // Return added medicine data
            return Ok(medicineDto);
        }

        // Update an existing medicine by ID (Admin only)
        [HttpPut("update-medicine/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateMedicineAsync(int id, [FromBody] MedicineDTO medicineDto)
        {
            // Check if ID from URL matches ID in body
            if (id != medicineDto.MedicineId)
            {
                return BadRequest("Medicine ID mismatch.");
            }

            var updatedMedicine = await _medicineService.UpdateMedicine(medicineDto);

            // If medicine not found, return 404
            if (updatedMedicine == null)
            {
                return NotFound("Medicine not found.");
            }

            // Return updated medicine
            return Ok(updatedMedicine);
        }

        // Get a single medicine by its ID (accessible to User and Admin)
        [HttpGet("{id}")]
        [Authorize(Roles = "User,Admin")]
        public async Task<IActionResult> GetMedicineByIdAsync(int id)
        {
            var medicine = await _medicineService.GetMedicineByIdAsync(id);

            // If not found, return 404
            if (medicine == null)
                return NotFound();

            // Return medicine details
            return Ok(medicine);
        }

        // Get all medicines (open to everyone)
        [HttpGet("all-medicines")]
        [AllowAnonymous]
        public async Task<IActionResult> GetAllMedicinesAsync()
        {
            var medicines = await _medicineService.GetAllMedicinesAsync();

            // Return list of all medicines
            return Ok(medicines);
        }

        // Filter medicines based on provided criteria (User and Admin)
        [HttpPost("filter-medicines")]
        [Authorize(Roles = "User,Admin")]
        public async Task<IActionResult> FilterMedicinesAsync([FromBody] MedicineFilterDTO filter)
        {
            // If filter is null, return 400
            if (filter == null)
            {
                return BadRequest("Invalid filter data.");
            }

            var filteredMedicines = await _medicineService.FilterMedicinesAsync(filter);

            // Return filtered results
            return Ok(filteredMedicines);
        }

        // Delete a medicine by name (Admin only)
        [HttpDelete("delete-medicine/{medicineId}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteMedicineAsync(int medicineId)
        {
           

            try
            {
                // Try to delete the medicine
                await _medicineService.DeleteMedicine(medicineId);
                return Ok("Medicine deleted successfully.");
            }
            catch (KeyNotFoundException)
            {
                // If not found, return 404
                return NotFound("Medicine not found.");
            }
        }
    }
}
