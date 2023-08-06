using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AlWaddahClinic.Server.Data;
using AlWaddahClinic.Server.Extensions;
using AlWaddahClinic.Server.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AlWaddahClinic.Server.Controllers
{
    [Authorize(Roles = "Admin")]
    public class PaymentsController : BaseController
    {
        private readonly IPaymentsRepository _paymentsRepository;
        private readonly ILogger<PaymentsController> _logger;

        public PaymentsController
        (
            ClinicDbContext context,
            IPaymentsRepository paymentsRepository,
            ILogger<PaymentsController> logger
        ) : base(context)
        {
            _paymentsRepository = paymentsRepository;
            _logger = logger;
        }

        [HttpGet("{healthRecordId}")]
        public async Task<IActionResult> GetAllAsync(Guid healthRecordId)
        {
            try
            {
                var payments = await _paymentsRepository.GetPaymentsForHealthRecordAsync(healthRecordId: healthRecordId);
                var paymentsAsDtos = payments.Select(p => p.ToPaymentDto());


                return Ok(new ApiResponse<IEnumerable<PaymentDto>>
                {
                    Message = "Payments retrieved successfully",
                    Value = paymentsAsDtos,
                    IsSuccess = true
                });
            }
            catch (NotFoundException ex)
            {
                return BadRequest(new ApiErrorResponse
                {
                    Message = ex.Message
                });
            }
        }

        [HttpGet("/payment-details/{id}")]
        public async Task<IActionResult> GetByIdAsync(Guid id)
        {
            try
            {
                var payment = await _paymentsRepository.GetPaymentByIdAsync(id);
                var paymentAsDtos = payment.ToPaymentDto();


                return Ok(new ApiResponse<PaymentDto>
                {
                    Message = "Payments retrieved successfully",
                    Value = paymentAsDtos,
                    IsSuccess = true
                });
            }
            catch (NotFoundException ex)
            {
                return BadRequest(new ApiErrorResponse
                {
                    Message = ex.Message
                });
            }
        }

        [HttpPost("{recordId}")]
        public async Task<IActionResult> AddPaymentAsync(Guid recordId, PaymentCreateDto model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var payment = model.ToPaymentCreate();
                    payment.PaidOn = DateTime.Now;
                    AssignAdminstrativeProperties<Payment>(payment);
                    await _paymentsRepository.CreatePaymentAsync(healthRecordId: recordId, payment);

                    return Ok(new ApiResponse
                    {
                        Message = "Payment added successfully",
                        IsSuccess = true
                    });
                }
                catch (NotFoundException ex)
                {
                    return BadRequest(new ApiErrorResponse
                    {
                        Message = ex.Message
                    });
                }
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePaymentAsync(Guid id)
        {
            try
            {
                await _paymentsRepository.RemovePaymentAsync(paymentId: id);

                return Ok(new ApiResponse
                {
                    Message = "Payment deleted successfully",
                    IsSuccess = true
                });

            }
            catch(NotFoundException ex)
            {
                return BadRequest(new ApiErrorResponse
                {
                    Message = ex.Message
                });
            }
        }
    }
}

