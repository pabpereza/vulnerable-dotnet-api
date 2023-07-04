namespace WebApi.Controllers;

using AutoMapper;
using Microsoft.AspNetCore.Mvc;

using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Text;

[ApiController]
[Route("hash")]

public class Hash : ControllerBase
{
    // XXE UNSAFE - NO DTD
	// It´s unsafe because it´s not disabling DTDs. 

	[HttpGet("sha512/{id}")]
	public IActionResult GenerarHashSHA512(string id)
	{
		const int DATA_SIZE = 1000000;

		byte[] data = new byte[DATA_SIZE];
		byte[] result;
		using (SHA512 shaM = SHA512.Create())
		{
			result = shaM.ComputeHash(Encoding.UTF8.GetBytes(id));
		}

		return Ok(result);
	}


	[HttpGet("pbkdf2/{id}")]
	public IActionResult GenerarHashPBKDF2(string id)
	{
		byte[] result;
		using (Rfc2898DeriveBytes pbkdf2 = new Rfc2898DeriveBytes(id, 10000))
		{
			result = pbkdf2.GetBytes(500);
		}

		return Ok(result);
	}


	[HttpGet("salt/{id}")]
	public IActionResult GenerarHashSalt(string id)
	{

		byte[] salt_number = RandomNumberGenerator.GetBytes(128 / 8); // divide by 8 to convert bits to bytes
		Console.WriteLine($"Salt: {Convert.ToBase64String(salt_number)}");

		byte[] result;
		using (Rfc2898DeriveBytes pbkdf2 = new Rfc2898DeriveBytes(id, salt_number, 10000))
		{
			result = pbkdf2.GetBytes(500);
		}

		return Ok(result);
	}
}