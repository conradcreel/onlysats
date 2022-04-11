using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using onlysats.domain.Services;
using onlysats.domain.Services.Request.Accounting;
using onlysats.web.Models.Webhooks;

namespace onlysats.web.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class AccountingController : ControllerBase
    {
        private readonly ILogger<AccountingController> _Logger;
        private readonly IAccountingService _AccountingService;

        public AccountingController(ILogger<AccountingController> logger, IAccountingService accountingService)
        {
            _Logger = logger;
            _AccountingService = accountingService;
        }

        [HttpPost("btc_webhook")]
        public async Task<IActionResult> BtcPayServerWebhook([FromBody] BtcPayServerWebhookModel webhook)
        {
            switch (webhook.Type)
            {
                case "InvoiceSettled":
                    await _AccountingService.HandleInvoiceSettled(new InvoiceSettledRequest
                    {
                        BtcPayServerAccountId = webhook.StoreId,
                        InvoiceId = webhook.InvoiceId,
                        ManuallyMarked = webhook.ManuallyMarked,
                        OverPaid = webhook.OverPaid
                    }).ConfigureAwait(continueOnCapturedContext: false);
                    break;

                default: break;
            }

            return Ok();
        }
    }
}