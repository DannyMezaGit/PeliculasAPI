using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PeliculasAPI.Context;
using PeliculasAPI.DTOs;
using PeliculasAPI.Entities;
using PeliculasAPI.Utilidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PeliculasAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenerosController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;

        public GenerosController (ApplicationDbContext dbContext,
                                  IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<GeneroDTO>>> Get([FromQuery] PaginacionDTO paginacion)
        {
            var queryable =  _dbContext.Generos.AsQueryable();
            await HttpContext.InsertarParametrosPaginacionEnCabecera(queryable);

            var generos = await queryable.OrderBy(x => x.Nombre).Paginar(paginacion).ToListAsync();

            return Ok(_mapper.Map<List<GeneroDTO>>(generos));
        }

   
        [HttpGet("{id}")]
        public async Task<ActionResult<GeneroDTO>> Get(int id)
        {
            var genero = await _dbContext.Generos.FirstOrDefaultAsync(x => x.Id == id);
            if (genero == null)
            {
                return NotFound();
            }
            
            return Ok(_mapper.Map<GeneroDTO>(genero));
        }


        [HttpPost]
        public async Task<ActionResult> Post([FromBody] GeneroCreacionDTO modelo)
        {
            var genero = _mapper.Map<Genero>(modelo);
            _dbContext.Add(genero);
            await _dbContext.SaveChangesAsync();
            return NoContent(); // para retornar un 204
        }


        [HttpPut("{id}")]
        public async Task<ActionResult>  Put(int id, [FromBody] GeneroCreacionDTO modelo)
        {
            var genero = await _dbContext.Generos.FirstOrDefaultAsync(x => x.Id == id);
            if (genero == null)
            {
                return NotFound();
            }

            genero = _mapper.Map(modelo, genero);

            await _dbContext.SaveChangesAsync();

            return NoContent();
            
        }


        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var existe = await _dbContext.Generos.AnyAsync(x => x.Id == id);

            if (!existe)
            {
                return NotFound();
            }

            _dbContext.Remove(new Genero() { Id = id });
            await _dbContext.SaveChangesAsync();
            
            return NoContent();
        }
    }
}
