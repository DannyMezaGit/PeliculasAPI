using PeliculasAPI.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PeliculasAPI.repositorios
{
    public class RepositorioEnMemoria
    {
        private List<Genero> _genres;

        public RepositorioEnMemoria()
        {
            _genres = new List<Genero>()
            {
                new Genero() { Id = 1, Nombre = "Comedia" },
                new Genero() { Id = 2, Nombre = "Acción" }
            };
        }

        public List<Genero> ObtenerTodosLosGeneros()
        {
            return _genres;
        }
    }
}
