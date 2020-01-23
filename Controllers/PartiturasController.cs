using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Partituras.Data.IRepositorio;
using Partituras.Data.Repositorio;
using Partituras.Models;

namespace Partituras.Controllers
{
    public class PartiturasController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private IPartiturasRepo _svc; 
        public PartiturasController(ILogger<HomeController> logger, IPartiturasRepo svc)
        {
            _logger = logger;
            _svc = svc;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Create(){
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create([Bind("Id,Title,ReleaseDate,Genre,Time,Price")] Partitura partitura)
        {
            try
            {
                await _svc.create(partitura);
                return RedirectToAction(nameof(Index));
            }
            catch (ApplicationException aEx)
            {
                ViewBag.Error = aEx.Message;
                return View();
            }

        }

        public async Task<IActionResult> GetAll (string searchString){
            var data = await _svc.getAll(searchString);
            return Ok(data);

        }

        public async Task<IActionResult> GetById(int Id){
            var data = await _svc.getById(Id);
            return Ok(data);
        }
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movie = await _svc.getById(id.Value);
            return View(movie);
        }
       [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,ReleaseDate,Genre,Time,Price")] Partitura partitura)
        {
            if (id != partitura.Id)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
                return View(partitura);

            await _svc.update(id, partitura);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id){
            await _svc.delete(id);
            return RedirectToAction(nameof(Index));
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        
    }
}
