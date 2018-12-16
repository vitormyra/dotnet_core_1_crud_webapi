using System.Collections.Generic;
using vidibr_api.Models;

namespace vidibr_api.Repositorio
{
    public interface IUsuarioRepository
    {
         void add(Usuario user);
         IEnumerable<Usuario>GetAll();
         Usuario Find(long id);
         void Remove(long id);
         void Update(Usuario user);
    }
}