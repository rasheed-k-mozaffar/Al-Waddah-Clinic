using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AlWaddahClinic.Server.Data;
using AlWaddahClinic.Server.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AlWaddahClinic.Server.Controllers
{
    [Route("api/[controller]")]
    [Authorize(Roles = "Admin")]
    public class AppointmentsController : BaseController
    {
        private readonly IAppointmentsRepository _appointmentsRepository;
        private readonly ILogger<AppointmentsController> _logger;
        public AppointmentsController
            (
                ClinicDbContext context,
                IAppointmentsRepository appointmentsRepository,
                ILogger<AppointmentsController> logger
            ) : base(context)
        {
            _appointmentsRepository = appointmentsRepository;
            _logger = logger;
        }


        //GET
        [HttpGet]
        public async Task<IActionResult> GetAppointments()
        {
            try
            {
                var appointments = await _appointmentsRepository.GetAppointmentsAsync();

                var appointmentsAsDtos = appointments.Select(a => a.ToAppointmentDto()).ToList();

                return Ok(new ApiResponse<IEnumerable<AppointmentDto>>
                {
                    Message = "Appointments retrieved successfully",
                    Value = appointmentsAsDtos,
                    IsSuccess = true
                });
            }
            catch(NotFoundException ex)
            {
                return BadRequest(new ApiResponse
                {
                    Message = ex.Message,
                    IsSuccess = false
                });
            }
        }

        //GET
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAppointmentById(int id)
        {
            try
            {
                var appointment = await _appointmentsRepository.GetAppointmentByIdAsync(id);

                var appointmentAsDto = appointment.ToAppointmentDto();

                return Ok(new ApiResponse<AppointmentDto>
                {
                    Message = "Appointment retrieved successfully",
                    Value = appointmentAsDto,
                    IsSuccess = true
                });

            }
            catch(NotFoundException ex)
            {
                return BadRequest(new ApiResponse
                {
                    Message = ex.Message,
                    IsSuccess = false
                });
            }

        }

        //POST
        [HttpPost("{patientId}")]
        public async Task<IActionResult> CreateAppointment(int patientId,[FromBody] AppointmentCreateDto model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var appointmentToCreate = model.ToAppointmentCreate();

                    AssignAdminstrativeProperties(appointmentToCreate);

                    await _appointmentsRepository.AddAppointmentAsync(patientId, appointmentToCreate);


                    return Ok(new ApiResponse
                    {
                        Message = "Appointment added successfully",
                        IsSuccess = true
                    });
                }
                catch(NotFoundException ex)
                {
                    return BadRequest(new ApiResponse
                    {
                        Message = ex.Message,
                        IsSuccess = false
                    });
                }

            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        //PUT
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAppointment(int id, [FromBody] AppointmentUpdateDto model)
        {
            if(ModelState.IsValid)
            {
                try
                {
                    var appointmentToUpdate = await _appointmentsRepository.GetAppointmentByIdAsync(id);

                    appointmentToUpdate.StartAt = model.StartAt;
                    appointmentToUpdate.FinishAt = model.FinishAt;
                    appointmentToUpdate.ModifiedByUserId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)!.Value;
                    appointmentToUpdate.ModifiedOn = DateTime.Now;


                    await _context.SaveChangesAsync();

                    return NoContent();
                }
                catch(NotFoundException ex)
                {
                    return BadRequest(new ApiResponse
                    {
                        Message = ex.Message,
                        IsSuccess = false
                    });
                }
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        //DELETE
        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveAppointment(int id)
        {
            try
            {
                await _appointmentsRepository.RemoveAppointmentAsync(id);

                return Ok(new ApiResponse
                {
                    Message = "Appointment was removed successfully",
                    IsSuccess = true
                });
            }
            catch(NotFoundException ex)
            {
                return BadRequest(new ApiResponse
                {
                    Message = ex.Message,
                    IsSuccess = false
                });
            }
        }
    }
}

