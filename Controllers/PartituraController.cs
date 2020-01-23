using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Partituras.Data.IRepositorio;
using Partituras.Models;


namespace Partituras.Controllers
{
    [Route("api/partitura")]
    [ApiController]
    public class PartituraController : ControllerBase
    {
        private readonly ILogger<HomeController> _logger;
        private IPartiturasRepo _svc;

        public PartituraController(ILogger<HomeController> logger, IPartiturasRepo svc)
        {
            _logger = logger;
            _svc = svc;
        }
        [HttpPost]                
         public async Task<IActionResult> create (Partitura partitura){
             try{
            await _svc.create(partitura);
            return Ok();
             }catch(ApplicationException aEx){
                 return BadRequest(aEx.Message);
             }
         }
        
        [HttpGet]
        [Route("items")]
        public async Task<IActionResult> GetPartituras(string searchString)
        {
            var data = await _svc.getAll(searchString);
            return Ok(data);
        }
        [HttpPut]
        public async Task <IActionResult> Edit (int id, Partitura partitura){
            if(id!=partitura.Id)
            return NotFound();
            await _svc.update(id, partitura);
            return Ok();
        }
        [HttpDelete]
        public async Task <IActionResult> delete (int id){
            try{
                await _svc.delete(id);
                return Ok();
            }catch(ApplicationException aEx){
                return BadRequest(aEx.Message);
            }
        }
        [HttpGet]
        [Route("Id")]
        public async Task <IActionResult> getById(int Id){
            try{
            var data = await _svc.getById(Id);
             return Ok(data);
            }catch(ApplicationException aEx){
                return BadRequest(aEx.Message);
            }
        }
    }
}
