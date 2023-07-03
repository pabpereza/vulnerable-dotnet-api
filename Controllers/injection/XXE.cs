namespace WebApi.Controllers;

using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Xml;


[ApiController]
[Route("xxe")]

public class Xxe : ControllerBase
{
    // XXE UNSAFE - NO DTD
	// It´s unsafe because it´s not disabling DTDs. 
    [HttpGet("{id}")]
    public IActionResult UnsafeXXE(string id)
    {
		string xxePayload = "<!DOCTYPE doc [<!ENTITY win SYSTEM 'file:///C:/Users/testdata2.txt'>]>"
                     + "<doc>&win;</doc>";
		string xml = "<?xml version='1.0' ?>" + xxePayload;

		XmlDocument xmlDoc = new XmlDocument();
		
		// UNSAFE - It´s not disabling DTDs
		xmlDoc.XmlResolver = new XmlUrlResolver();
	
		xmlDoc.LoadXml(xml);
		Console.WriteLine(xmlDoc.InnerText);
		Console.ReadLine();

		return Ok();
	}


    // XXE SAFE - NO DTD
	// The XmlResolver property is set to null, which disables DTDs. Now it´s safe.
	// In .NET framework 4.5.2 and later, the XmlResolver property defaults to null.
    [HttpGet("safe/{id}")]
    public IActionResult SafeXXE(string id)
    {
		string xxePayload = "<!DOCTYPE doc [<!ENTITY win SYSTEM 'file:///C:/Users/testdata2.txt'>]>"
                     + "<doc>&win;</doc>";
		string xml = "<?xml version='1.0' ?>" + xxePayload;

		XmlDocument xmlDoc = new XmlDocument();
		// Setting this to NULL disables DTDs - Its NOT null by default.
		xmlDoc.XmlResolver = null;
		xmlDoc.LoadXml(xml);
		Console.WriteLine(xmlDoc.InnerText);
		Console.ReadLine();
	
		return Ok();
	}

}

