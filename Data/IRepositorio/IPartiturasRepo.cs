using Partituras.Models;
using Partituras.Data;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Partituras.Data.IRepositorio{

    public interface IPartiturasRepo{

        Task create (Partitura partitura);
        Task update (int id, Partitura partitura);
        Task delete (int id);
        Task <Partitura> getById(int Id);
        Task <IList<Partitura>> getAll (string searchString);

        }


    }
