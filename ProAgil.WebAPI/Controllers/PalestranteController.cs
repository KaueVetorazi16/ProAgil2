using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProAgil.Domain;
using ProAgil.Repository;

namespace ProAgil.WebAPI.Controllers
{

    [Route("api/[Controller]")]
    [ApiController]
    public class PalestranteController : ControllerBase
    {

        private readonly IProAgilRepository _repo;
        public PalestranteController(IProAgilRepository repo)
        {
            this._repo = repo;
        }
        
           [HttpGet("{EventoId}")]
        public async Task<IActionResult> Get(int PalestranteId)
        {
            try {
                 var results = await _repo.GetPalestranteAsyncByID(PalestranteId, true);
           return Ok (results);
            } catch(System.Exception){
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco de dados falhou!");
            }
           
        }

        
        [HttpPost]
        public async Task<IActionResult> Post(Palestrante model)
        {
            try {

                 _repo.Add(model);

                 if(await _repo.SaveChangesAsync()){

                     return Created($"/api/palestrante/{model.Id}", model);

                 }

            } catch(System.Exception){
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco de dados falhou!");
            }

            return BadRequest();
           
        }

        [HttpPut]
        public async Task<IActionResult> Put(int PalestranteId, Palestrante model)
        {
            try {

                var palestrante = await _repo.GetPalestranteAsyncByID(PalestranteId, false);
                if (palestrante == null) return NotFound();

                 _repo.Update(model);

                 if(await _repo.SaveChangesAsync()){

                     return Created($"/api/palestrante/{model.Id}", model);

                 }

            } catch(System.Exception){
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco de dados falhou!");
            }

            return BadRequest();
           
        }
        
        [HttpDelete]
        public async Task<IActionResult> Delete(int PalestranteId)
        {
            try {

                var palestrante = await _repo.GetPalestranteAsyncByID(PalestranteId, false);
                if (palestrante == null) return NotFound();
                
                 _repo.Delete(palestrante);

                 if(await _repo.SaveChangesAsync()){

                     return Ok();

                 }

            } catch(System.Exception){
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco de dados falhou!");
            }

            return BadRequest();
           
        }
    }
}