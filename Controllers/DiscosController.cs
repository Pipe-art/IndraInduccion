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
    public class DiscosController : ControllerBase
    {

        private readonly IStoreProcedureHelper<BasePropertieResponseDTO> _storeProcedureHelper;


        private readonly AppDbContext context;


        public DiscosController(AppDbContext context, IStoreProcedureHelper<BasePropertieResponseDTO> storeProcedureHelper)
        {
            this.context = context;
            this._storeProcedureHelper = storeProcedureHelper;
        }

        // GET: api/<DiscoController>
        [HttpGet]
        public IEnumerable<Disco> Get()
        {
            return context.Disco.ToList();
        }

        // GET api/<DiscoController>/5
        [HttpGet("{id}")]
        public Disco Get(string id)
        {
            var Disco = context.Disco.FirstOrDefault(d => d.dis_codigo == id);
            return Disco;
        }


        [HttpPost("CancionesDisco")]
        public IActionResult CancionesArtista([FromBody] CancionesDiscoDTORequest request)
        {

            if (Request == null)
            {
                return BadRequest();
            }
            Parametros lstParametros = new Parametros();
            try
            {

                lstParametros.AddSqlParam("NombreDisco", System.Data.SqlDbType.VarChar, request.NombreDisco);
                var retorno = _storeProcedureHelper.ExecuteReader<CancionesDiscoDTOResponse>(Resource.Resource.CancionesDisco, lstParametros.ListaSqlParam);

                if (retorno.Count > 0)
                {
                    return Ok(retorno[0]);
                }
                else
                {
                    return Ok(new CancionesDiscoDTOResponse { CodigoDisco = "", DiscoNombre = "", CancionNombre = "", Alias = "" });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);

            }

        }


        [HttpPost("NumeroCanciones")]
        public IActionResult NumneroCanciones([FromBody] NumeroCancionesDTORequest request)
        {

            if (Request == null)
            {
                return BadRequest();
            }
            Parametros lstParametros = new Parametros();
            try
            {

                lstParametros.AddSqlParam("codigoDisco", System.Data.SqlDbType.VarChar, request.codigoDisco);
                var retorno = _storeProcedureHelper.ExecuteReader<NumeroCancionesDTOResponse>(Resource.Resource.NumeroCanciones, lstParametros.ListaSqlParam);

                if (retorno.Count > 0)
                {
                    return Ok(retorno[0]);
                }
                else
                {
                    return Ok(new NumeroCancionesDTOResponse { AliasArtista = "", DiscoNombre = "", NumeroCanciones = ""});
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);

            }

        }
    }
}
