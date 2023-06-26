namespace WebApi.Controllers;

using Microsoft.AspNetCore.Mvc;
using WebApi.Services;
using WebApi.Models;


[ApiController]
[Route("command")]


public class DesCommands : ControllerBase
{

	[HttpGet("{command}/{argument}")]
	public IActionResult GetCommand(string command, string argument)
	{
		// Ejecución de procesos sin validación de parámetros
		var process = new System.Diagnostics.Process();
		var startInfo = new System.Diagnostics.ProcessStartInfo();
		startInfo.FileName = command;
		startInfo.Arguments = argument;
		startInfo.UseShellExecute = false;
		startInfo.RedirectStandardOutput = true;
		process.StartInfo = startInfo;

		process.Start();

		string output = "";
		while (!process.StandardOutput.EndOfStream)
		{
			string line = process.StandardOutput.ReadLine();
			output += line + "\n";
		}

		process.WaitForExit();	

		return Ok(output);

	}

	[HttpGet("parametrized/{command}/{argument}")]
	public IActionResult GetParCommand(string command, string argument)
	{
		// Ejecución de procesos con validación de parámetros
		var process = new System.Diagnostics.Process();
		var startInfo = new System.Diagnostics.ProcessStartInfo();
		startInfo.FileName = command;
		startInfo.Arguments = argument;
		startInfo.UseShellExecute = false;
		startInfo.RedirectStandardOutput = true;
		process.StartInfo = startInfo;

		process.Start();

		string output = "";
		while (!process.StandardOutput.EndOfStream)
		{
			string line = process.StandardOutput.ReadLine();
			output += line + "\n";
		}

		process.WaitForExit();	

		return Ok(output);

	}
}