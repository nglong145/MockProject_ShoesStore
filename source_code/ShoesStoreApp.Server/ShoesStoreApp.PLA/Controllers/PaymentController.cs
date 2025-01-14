using Microsoft.AspNetCore.Mvc;
using ShoesStoreApp.BLL.Services.CartService;
using ShoesStoreApp.BLL.ViewModels;
using ShoesStoreApp.DAL.Models;

namespace ShoesStoreApp.PLA.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PaymentController : ControllerBase
{
    private readonly IPaymentService _paymentService;

    public PaymentController(IPaymentService paymentService)
    {
        _paymentService = paymentService;
    }
    
    [HttpGet("get-all-payment")]
        public async Task<IActionResult> GetAll()
        {
            var payments = await _paymentService.GetAllAsync();
            var paymentVM = new List<PaymentVM>();
            foreach (var payment in payments)
            {
                paymentVM.Add(new PaymentVM
                {
                    PaymentId = payment.PaymentId,
                    Description = payment.Description,
                    PaymentMethod = payment.PaymentMethod,
                });
            }
            return Ok(paymentVM);
        }

        [HttpGet("get-payment-by-id/{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var payment = await _paymentService.GetByIdAsync(id);
            if(payment != null)
            {
                var paymentVM = new PaymentVM()
                {
                    PaymentId = payment.PaymentId,
                    Description = payment.Description,
                    PaymentMethod = payment.PaymentMethod,
                };
                return Ok(paymentVM);
            }
            return BadRequest("The payment does not exist!");
        }

        [HttpPost("add-new-payment")]
        public async Task<IActionResult> Add([FromBody] AddPaymentVM addPaymentVm)
        {
            var payment = new Payment()
            {
                Description = addPaymentVm.Description,
                PaymentMethod = addPaymentVm.PaymentMethod,
            };
            await _paymentService.AddAsync(payment);
            return Ok(payment);
        }

        [HttpPut("update-payment/{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] AddPaymentVM addPaymentVm)
        {
            var payment = await _paymentService.GetByIdAsync(id);
            if(payment != null)
            {
               payment.Description = addPaymentVm.Description;
               payment.PaymentMethod = addPaymentVm.PaymentMethod;

                await _paymentService.UpdateAsync(payment);
                return Ok(payment);
            }
            return BadRequest("The payment does not exist!");
        }

        [HttpDelete("delete-payment/{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var paymeny = await _paymentService.GetByIdAsync(id);
            if(paymeny != null)
            {
                await _paymentService.DeleteAsync(paymeny);
                return Ok(paymeny);
            }
            return BadRequest("Delete Faild!");
        }
}