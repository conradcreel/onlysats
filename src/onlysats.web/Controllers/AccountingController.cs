using Microsoft.AspNetCore.Mvc;
using onlysats.domain.Services;

namespace onlysats.web.Controllers;

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
}