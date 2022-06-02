using AuthControl.Models;
using AuthControl.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AuthControl.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HotmartNotificationController : ControllerBase
    {
        private readonly HotmartService _service;
        public HotmartNotificationController(HotmartService service)
        {
            _service = service;
        }


        /// <returns></returns>
        [HttpPost("NotifyApprovedCompleted")]
        public async Task<IActionResult> NotifyApprovedCompleted([FromBody]OrderEvent order)
        {
            try
            {

                if (order.Data == null)
                {
                    return BadRequest("Erro ao tentar receber notificação do hotmart");
                }

                if (order.Data.Subscription.Status == "ACTIVE")
                {
                    await _service.ProcessOrderAprovedCompletedEvent(order);

                }

                return Ok();

            }
            catch (Exception e)
            {
                return StatusCode(500);
            }

        }


        /// <returns></returns>
        [HttpPost("NotifyCanceled")]
        public async Task<IActionResult> NotifyCanceled([FromBody] OrderEvent order)
        {
            try
            {


                    await _service.ProcessOrderCanceledEstornedEvent(order);


                
                return Ok();

            }
            catch (Exception e)
            {
                return StatusCode(500);
            }
        }


        [HttpPost("NotifyChangedPlan")]
        public async Task<IActionResult> NotifyChangedPlan([FromBody] RootChangePlan changePlan)
        {
            try
            {


                await _service.ProcessOrderChangedPlanEvent(changePlan);


                return Ok();
            }
            catch (Exception e)
            {
                return StatusCode(500);
            }
        }
    }
}
