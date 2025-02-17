using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Logging;

namespace Barbearia.API.Controllers{
    [ApiController]
    [Route("api/[controller]")]
    public class Cliente : ControllerBase {

        [HttpGet]
       public async Task<ActionResult> Get() {
        return Ok();
       }

    }
}