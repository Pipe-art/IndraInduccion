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
    public class RepresentantesController : ControllerBase
    {
        private readonly IStoreProcedureHelper<BasePropertieResponseDTO> _storeProcedureHelper;
        private readonly AppDbContext context;

        public RepresentantesController(AppDbContext context, IStoreProcedureHelper<BasePropertieResponseDTO> storeProcedureHelper)
        {
            this.context = context;
            this._storeProcedureHelper = storeProcedureHelper;
        }

        // GET: api/<ValuesController1>
        [HttpGet]
        public IEnumerable<Representante> Get()
        {
            return context.Representante.ToList();
        }

        // GET api/<ValuesController1>/5
        [HttpGet("{id}")]
        public Representante Get(string id)
        {
            var Representante = context.Representante.FirstOrDefault(r=>r.rep_codigo==id);
            return Representante;
        }

        [HttpPost("ObtenerDatosRepresentante")]
        public IActionResult ObtenerDatosArtista([FromBody] DatosRepresentanteDTORequest resquest)
        {

            if (Request == null)
            {
                return BadRequest();
            }
            Parametros lstParametros = new Parametros();
            try
            {

                lstParametros.AddSqlParam("rut", System.Data.SqlDbType.VarChar, resquest.Rut);
                var retorno = _storeProcedureHelper.ExecuteReader<DatosRepresentanteDTOResponse>(Resource.Resource.ConsultaDatosRepresentante, lstParametros.ListaSqlParam);

                if (retorno.Count > 0)
                {
                    return Ok(retorno[0]);
                }
                else
                {
                    return Ok(new DatosRepresentanteDTOResponse { RutRepresentante = "", NombreRepresentante = "", Ciudad="",Bithday=DateTime.MinValue  });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);

            }
        }
    }
}
