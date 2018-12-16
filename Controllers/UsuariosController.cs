using System.Collections.Generic;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using vidibr_api.Models;
using vidibr_api.Repositorio;

namespace vidibr_api.Controllers
{
    [Route("api/[Controller]")]
    public class UsuariosController : Controller
    {
        private readonly IUsuarioRepository _usuarioRepositorio;

        public UsuariosController(IUsuarioRepository _usuarioRepo )
        {
            _usuarioRepositorio = _usuarioRepo;
        }

        [EnableCors("CorsPolicy")]
        [HttpGet]
        public IEnumerable<Usuario>GetAll()
        {
            return _usuarioRepositorio.GetAll();
        }

        [EnableCors("CorsPolicy")]
        [HttpGet("{id}", Name="GetUsuario")]
        public IActionResult GetById(long id)
        {
            var usuario = _usuarioRepositorio.Find(id);
            if(usuario == null)
            {
                return NotFound();
            }
            return new ObjectResult(usuario);
        }

        // [Route("api/usuarios/create")]
        [EnableCors("CorsPolicy")]
        [HttpPost]
        public IActionResult Create([FromBody] Usuario usuario)
        {
            if(usuario == null)
            {
                return BadRequest();
            }
            _usuarioRepositorio.add(usuario);
            return CreatedAtRoute("GetUsuario", new {id=usuario.UsuarioId}, usuario);
        }

        [EnableCors("CorsPolicy")]
        [HttpPut("{id}")]
        public IActionResult Update(long id, [FromBody] Usuario usuario)
        {
            if(usuario == null || usuario.UsuarioId != id)
            {
                return BadRequest();
            } 

            var _usuario =_usuarioRepositorio.Find(id);

            if(usuario==null)
            {
                return NotFound();
            }
             
            _usuario.Email = usuario.Email;
            _usuario.Nome = usuario.Nome;

            _usuarioRepositorio.Update(_usuario);
            return new NoContentResult();
        }

        [EnableCors("CorsPolicy")]
        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            var usuario = _usuarioRepositorio.Find(id);

            if(usuario == null)
            {
                return NotFound();
            }
            _usuarioRepositorio.Remove(id);
            return new NoContentResult();
        }
    }
}