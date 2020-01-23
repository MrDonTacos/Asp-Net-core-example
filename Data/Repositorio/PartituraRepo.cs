using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Partituras.Data.IRepositorio;
using Partituras.Models;
using System.Linq;


namespace Partituras.Data.Repositorio{

    public class PartituraRepo : IPartiturasRepo
    {
        private readonly MvcPartituraContext _context;

        public PartituraRepo(MvcPartituraContext context)
        {
            _context = context;
        }

        private async Task<bool> Exist (Partitura partitura)
        {

           return await _context.Partitura.AnyAsync(e => e.Title
            == partitura.Title && e.Id != partitura.Id);
        }

        public async Task create(Partitura partitura)
        {
            var part = new Partitura(){
                Id = partitura.Id,
                Title = partitura.Title,
                ReleaseDate = partitura.ReleaseDate,
                Genre = partitura.Genre,
                Time = partitura.Time,
                Price = partitura.Price
                
            };
            try{
              if (await Exist(partitura))
                  throw new Exception("La partitura ya existe en el catalogo");
                await _context.AddAsync(part);
                await _context.SaveChangesAsync();


            }
            catch(Exception aEx){
                throw new ApplicationException ("Imposible crear partitura por: ", aEx);
            }
            
        }

        public async Task delete(int id)
        {
            try{
                var part = await _context.Partitura.FindAsync(id);
                if(part!=null)
                throw new ApplicationException("No existe la partitura en el catalogo");
                 _context.Remove(part);
                 await _context.SaveChangesAsync();
            }catch(Exception aEx){
                throw new ApplicationException("No se pudo borrar por: ",aEx);
            }

        }

        public async Task<IList<Partitura>> getAll(string searchString)
        {
            
            IList<Partitura> resultSet = new List<Partitura>();
            var parti = from m in _context.Partitura
                         select m;

            if (!string.IsNullOrEmpty(searchString))
            {
                parti = parti.Where(s => s.Title.Contains(searchString));
            }

           
            var data = await parti.ToListAsync();
            resultSet = data.Select(s => new Partitura
            {
                Id = s.Id,
                Title = s.Title,
                ReleaseDate = s.ReleaseDate,
                Genre = s.Genre,
                Time = s.Time,
                Price = s.Price
            }).ToList();

            return resultSet;
        }

        public async Task<Partitura> getById(int Id)
        {
            var partitura=new Partitura();
            try{
            var _par = await _context.Partitura.FirstOrDefaultAsync(w=>w.Id == Id);
            if(_par!=null)
            partitura = new Partitura(){
                 Id = _par.Id,
                Title = _par.Title,
                ReleaseDate = _par.ReleaseDate,
                Genre = _par.Genre,
                Time = _par.Time,
                Price = _par.Price
            };
            }catch(Exception aEx){
                throw new ApplicationException("No se encontro el ID", aEx);
            }
            return partitura;
        }

        public async Task update(int id, Partitura partitura)
        {
            var _par = await _context.Partitura.FirstOrDefaultAsync(w=>w.Id==id);

            if(_par==null)
            throw new ApplicationException("No se pudo hacer la consulta a la db");

            if (await Exist(partitura))
            throw new ApplicationException("La pelicula ya existe en la bd");

            _par.Id = partitura.Id;
            _par.Title = partitura.Title;
            _par.ReleaseDate = partitura.ReleaseDate;
            _par.Genre = partitura.Genre;
            _par.Time = partitura.Time;
            _par.Price = partitura.Price;
            
            _context.Update(_par);
            await _context.SaveChangesAsync();
        }
    }
}