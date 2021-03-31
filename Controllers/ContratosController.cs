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
    public class ContratosController : ControllerBase
    {
        private readonly IStoreProcedureHelper<BasePropertieResponseDTO> _storeProcedureHelper;


        private readonly AppDbContext context;


        public ContratosController(AppDbContext context, IStoreProcedureHelper<BasePropertieResponseDTO> storeProcedureHelper)
        {
            this.context = context;
            this._storeProcedureHelper = storeProcedureHelper;
        }

        // GET: api/<ContratosController1>
        [HttpGet]
        public IEnumerable<Contrato> Get()
        {
            return context.Contrato.ToList();
        }

        // GET api/<ContratosController1>/5
        [HttpGet("{id}")]
        public Contrato Get(string id)
        {
            var Contrato = context.Contrato.FirstOrDefault(co => co.con_codigo==id);
            return Contrato;
        }

        [HttpPost("VigenciaContrato")]
        public IActionResult ObtenerDatosArtista([FromBody] VigenciaContratoDTORequest resquest)
        {

            if (Request == null)
            {
                return BadRequest();
            }
            Parametros lstParametros = new Parametros();
            try
            {

                lstParametros.AddSqlParam("vigencia", System.Data.SqlDbType.VarChar, resquest.vigencia);
                var retorno = _storeProcedureHelper.ExecuteReader<VigenciaContratoDTOResponse>(Resource.Resource.VigenciaContrato, lstParametros.ListaSqlParam);

                if (retorno.Count > 0)
                {
                    return Ok(retorno[0]);
                }
                else
                {
                    return Ok(new VigenciaContratoDTOResponse { NumerosContrato = "", Descripcion = "" });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);

            }
        }

    }
}
