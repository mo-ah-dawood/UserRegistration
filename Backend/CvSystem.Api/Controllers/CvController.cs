using Microsoft.AspNetCore.Mvc;
using CvSystem.Api.Response;
using CvSystem.Core.Common;
using CvSystem.Core.Services;
using CvSystem.Api.Requests;
using CvSystem.Core.Services.Params;

namespace CvSystem.Api.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class CvController(ICVService service) : ControllerBase
{

    [HttpGet]
    public async Task<IActionResult> LoadCvs([FromQuery] CvPagingParams parameters)
    {
        int total = 0;
        var data = await service.LoadCVs(parameters, (count) => total = count);
        return Ok(GenericResult.Success(data.ProjectToDto(), total));
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetCV(int id)
    {
        var data = await service.Get(id);
        return Ok(GenericResult.Success(data.ToDto()));
    }

    [HttpPost]
    public async Task<IActionResult> Add(CreateCVRequest request)
    {
        var data = await service.Create(request.ToEntity());
        return Ok(GenericResult.Success(data.ToDto()));
    }

    [HttpPost]
    public async Task<IActionResult> Update(UpdateCVRequest request)
    {
        var data = await service.Create(request.ToEntity());
        return Ok(GenericResult.Success(data.ToDto()));
    }

    [HttpPost("{cvId}")]
    public async Task<IActionResult> UpdatePersonalInformation(int cvId, PersonalInformationRequest request)
    {
        var cv = await service.Get(cvId);
        if (cv.PersonalInformation == null)
            cv.PersonalInformation = request.ToEntity();
        else
            request.MapTo(cv.PersonalInformation);

        await service.Update(cv);
        return Ok(GenericResult.Success(cv.ToDto()));
    }

    [HttpPost("{cvId}")]
    public async Task<IActionResult> UpdateExperienceInformation(int cvId, List<ExperienceInformationRequest> request)
    {
        var cv = await service.Get(cvId);
        await service.AddExperiences(cv, request.ToEntity());
        return Ok(GenericResult.Success(cv.Experiences.ToDto()));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var entity = await service.Get(id);
        await service.Delete(entity);
        return Ok(GenericResult.Success("Cv deleted successfully"));
    }

}


