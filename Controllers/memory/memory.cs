namespace WebApi.Controllers;

using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;

[ApiController]
[Route("file")]
public class FileManagementController: ControllerBase
{

    private IMapper _mapper;

    public FileManagementController(IMapper mapper)
    {
        _mapper = mapper;
    }

    [HttpPost("upload")]
    public IActionResult UploadFile()
    {
        try
        {
            var file = Request.Body;
            return Ok(file);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex}");
        }
    }
    
    [HttpPost("upload/saved")]
    public IActionResult UploadFileSaved()
    {
        try
        {
            var filecontent = Request.Body;
            var filename = Request.Headers["Filename"];
            var path = Path.Combine(Directory.GetCurrentDirectory(), "Files", filename);
            using (FileStream fs = System.IO.File.Create(path))
            {
                filecontent.CopyTo(fs);
            }
            return Ok("File saved");
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex}");
        }
    }

}