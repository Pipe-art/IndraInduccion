using Discografica.Context;
using Discografica.Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Discografica.DTO.DTORequest;
using Discografica.DTO.DTOResponse;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Discografica.Class;
using Discografica.DAL;
using Discografica.DTO.Base;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Discografica.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ArtistasController : ControllerBase
    {

        private readonly IStoreProcedureHelper<BasePropertieResponseDTO> _storeProcedureHelper;


        private readonly AppDbContext context;
        

        public ArtistasController(AppDbContext context, IStoreProcedureHelper<BasePropertieResponseDTO> storeProcedureHelper)
        {
            this.context = context;
            this._storeProcedureHelper=storeProcedureHelper;
        }

        // GET: api/<ArtistasController1>
        [HttpGet]
        public IEnumerable<Artista> Get()
        {
            return context.Artista.ToList();
        } 

        // GET api/<ArtistasController1>/5
        [HttpGet("{id}")]
        public Artista Get(string id)
        {
            var Artista = context.Artista.FirstOrDefault(a=>a.art_codigo==id);
            return Artista;
        }

        [HttpPost("ObtenerDatosArtista")]
        public IActionResult ObtenerDatosArtista([FromBody]DatosArtistaDTORequest resquest)
        {

            if (Request == null)
            {
                return BadRequest();
            }
            Parametros lstParametros = new Parametros();
            try
            {
                
                lstParametros.AddSqlParam("rut", System.Data.SqlDbType.VarChar, resquest.Rut);
                var retorno = _storeProcedureHelper.ExecuteReader<DatosArtistaDTOResponse>(Resource.Resource.ConsultaDatosArtista, lstParametros.ListaSqlParam);

                if (retorno.Count>0)
                {
                    return Ok(retorno[0]);
                }
                else
                {
                    return Ok(new DatosArtistaDTOResponse { NombreArtista = "", NombreDisco = "", Precio = 0 });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
                
            }
        }
        [HttpPost("CancionesArtista")]
        public IActionResult CancionesArtista ([FromBody]CancionesArtistaDTORequest request)
        {

            if (Request == null)
            {
                return BadRequest();
            }
            Parametros lstParametros = new Parametros();
            try
            {

                lstParametros.AddSqlParam("aka", System.Data.SqlDbType.VarChar, request.Aka);
                var retorno = _storeProcedureHelper.ExecuteReader<CancionesArtistaDTOResponse>(Resource.Resource.CancionesArtista, lstParametros.ListaSqlParam);

                if (retorno.Count > 0)
                {
                    return Ok(retorno[0]);
                }
                else
                {
                    return Ok(new CancionesArtistaDTOResponse { CodigoDisco = "", DiscoNombre = "", CancionNombre = "", Alias ="" });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);

            }

        }

        [HttpPost("DobleArtista")]
        public IActionResult DobleArtista([FromBody] DobleArtistaDTORequest request)
        {

            if (Request == null)
            {
                return BadRequest();
            }
            Parametros lstParametros = new Parametros();
            try
            {

                lstParametros.AddSqlParam("Rut", System.Data.SqlDbType.VarChar, request.Rut);

                var retorno = _storeProcedureHelper.ExecuteReader<DobleArtistaDTOResponse>(Resource.Resource.DatosDobles, lstParametros.ListaSqlParam);

                if (retorno.Count > 0)
                {
                    return Ok(retorno[0]);
                }
                else
                {
                    return Ok(new DobleArtistaDTOResponse { AliasArtista = "", NombreArtista = "", NombreDisco = "", Precio =0 });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);

            }

        }
    }
}
