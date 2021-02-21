using System;
using System.Collections.Generic;
using System.IO;
using System.IO.MemoryMappedFiles;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Mime;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.Logging;

namespace MasevaGiveOutPoint.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class GiveOutController : ControllerBase
	{
		private readonly ILogger<GiveOutController> _logger;

		public GiveOutController(ILogger<GiveOutController> logger)
		{
			_logger = logger;
		}

		[HttpGet]
		[Route("{item}.jpg")]
		[Consumes(MediaTypeNames.Application.Octet)]
		[ProducesResponseType(StatusCodes.Status201Created)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		public async Task<ActionResult> Get(string item = "")
		{
			int attempts = 0;
			
			try
			{				
				do
				{
					try
					{
						using (var mmf = MemoryMappedFile.OpenExisting(System.IO.Path.GetFileNameWithoutExtension(item)))
						{
							var res = new FileStreamResult(mmf.CreateViewStream(), "image/jpeg");
							res.EnableRangeProcessing = true;
							return res;							
						}
					}
					catch
					{
						attempts++;
						_logger.LogInformation("attempts " + attempts);
						await Task.Delay(200);
					}
				}
				while (attempts < 3);
				return NotFound();
			}
			catch
			{
				return NotFound();
			}
		}

	}
}
