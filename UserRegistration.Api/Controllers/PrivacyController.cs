using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using UserRegistration.Api.Response;
using UserRegistration.Core.Common;
using UserRegistration.Core.Enum;
using UserRegistration.Core.Services;

namespace UserRegistration.Api.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class PrivacyController : ControllerBase
{
    private readonly IPrivacyService _service;

    public PrivacyController(IPrivacyService service)
    {
        _service = service;
    }
    [HttpGet]
    public async Task<IActionResult> LoadPrivacy([FromQuery] PagingParameters parameters)
    {
        IRequestCultureFeature? rqf = Request.HttpContext.Features.Get<IRequestCultureFeature>();
        var culture = rqf?.RequestCulture.Culture.TwoLetterISOLanguageName;
        var privacy = await _service.LoadPrivacy(new LoadPrivacyParams()
        {
            Language = culture?.ToLower() == "ar" ? AppLanguage.AR : AppLanguage.EN,
            Skip = parameters.Skip,
            PageSize = parameters.PageSize,
        });
        return Ok(GenericResult.Success(privacy));

    }

}


