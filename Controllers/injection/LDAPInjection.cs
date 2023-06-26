namespace WebApi.Controllers;

using Microsoft.AspNetCore.Mvc;
using WebApi.Services;

/* using System.DirectoryServices;


[ApiController]
[Route("ldap")]


public class DesLDAP : ControllerBase
{
	private string ldapPath = "LDAP://your-ldap-url";

	[HttpGet("/{name}")]
	public IActionResult GetLDAP(string userName)
	{

		string email = string.Empty;
		string ldapQuery = $"(sAMAccountName={userName})";
		
		using (var entry = new DirectoryEntry(ldapPath))
		{
			using (var searcher = new DirectorySearcher(entry))
			{
				searcher.Filter = ldapQuery;
				
				SearchResult result = searcher.FindOne();
				if (result != null)
				{
					email = (string)result.Properties["mail"][0];
				}
			}
		}

		return Ok(email);
	}



	[HttpGet("parametrized/{id}")]
	public IActionResult GetParLDAP(string command, string argument)
	{

		return Ok();

	}
} */