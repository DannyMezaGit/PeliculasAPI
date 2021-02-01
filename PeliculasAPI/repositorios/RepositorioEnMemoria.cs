using PeliculasAPI.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PeliculasAPI.repositorios
{
    public class RepositorioEnMemoria
    {
        private List<Genre> _genres;

        public RepositorioEnMemoria()
        {
            _genres = new List<Genre>()
            {
                new Genre() { Id = 1, Nombre = "Comedia" },
                new Genre() { Id = 2, Nombre = "Acción" }
            };
        }

        public List<Genre> ObtenerTodosLosGeneros()
        {
            return _genres;
        }
    }
}
