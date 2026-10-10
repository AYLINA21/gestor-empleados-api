using Microsoft.AspNetCore.Mvc;
using MiWebAPI.Data;
using MiWebAPI.Models;

namespace MiWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductoController : ControllerBase
    {
        private readonly ProductoData _productoData;
        public ProductoController(ProductoData productoData)
        {
            _productoData = productoData;
        }

        [HttpGet]
        public async Task<IActionResult> Lista()
        {
            List<ProductoDetalle> Lista = await _productoData.Lista();
            return StatusCode(StatusCodes.Status200OK, Lista);
        }
        [HttpPost]
        public async Task<IActionResult> Crear([FromBody] ProductoDetalle objeto)
        {
            bool respuesta = await _productoData.Crear(objeto);

            if (respuesta)
                return StatusCode(StatusCodes.Status200OK, new { isSuccess = true });
            else
                return StatusCode(StatusCodes.Status500InternalServerError, new { isSuccess = false });
        }

        [HttpPut]
        public async Task<IActionResult> Editar([FromBody] ProductoDetalle objeto)
        {
            bool respuesta = await _productoData.Editar(objeto);

            if (respuesta)
                return StatusCode(StatusCodes.Status200OK, new { isSuccess = true });
            else
                return StatusCode(StatusCodes.Status500InternalServerError, new { isSuccess = false });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Eliminar(long id)
        {
            bool respuesta = await _productoData.Eliminar(id);

            if (respuesta)
                return StatusCode(StatusCodes.Status200OK, new { isSuccess = true });
            else
                return StatusCode(StatusCodes.Status500InternalServerError, new { isSuccess = false });
        }
    }
}

        

