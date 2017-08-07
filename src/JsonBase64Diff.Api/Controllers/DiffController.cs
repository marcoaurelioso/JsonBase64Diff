using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using JsonBase64Diff.Application.Service.Interfaces;
using JsonBase64Diff.Application.Service.Dto;
using System.Net.Http;
using System.Net;
using Newtonsoft.Json.Linq;
using JsonBase64Diff.Api.Models;

namespace JsonBase64Diff.Api.Controllers
{
    [Produces("application/json")]
    [Route("v1/[controller]")]
    public class DiffController : Controller
    {
        private readonly IJsonBase64DiffService _service;

        public DiffController(IJsonBase64DiffService jsonBase64DiffService)
        {
            _service = jsonBase64DiffService;
        }

        // GET: api/values
        [HttpGet]
        public string Get()
        {
            return "The Diff api is ready";
        }

        /// <summary>
		/// Handles the request to process data.
		/// </summary>
		/// <param name="id">Id to identify the request</param>
		/// <param name="data">The base64 encoded data in Json format</param>
		/// <returns>Data was stored</returns>
        /// <response code="201">When the data was stored</response>
        /// <response code="400">If the item is null or any error</response>
		[HttpPost]
        [Route("{id}/left")]
        public async Task<IActionResult> Left(string id, [FromBody]DiffDataRequest jsonData)
        {
            if (string.IsNullOrEmpty(id) || string.IsNullOrEmpty(jsonData.Data))
                return StatusCode(400, new DiffDataResponse() { Message = "Required data not found" });

            JsonBase64ItemDto entity = new JsonBase64ItemDto() { Id = id, Position = Domain.Enums.EJsonBase64Position.Left, Data = jsonData.Data };

            try
            {
                bool success = await _service.Save(entity);

                if (success)
                    return StatusCode(201, new DiffDataResponse() { Message = "OK" });
                else
                    return StatusCode(400, new DiffDataResponse() { Message = "Error when input data" });
            }
            catch (Exception)
            {
                //write log error
                return StatusCode(400, new DiffDataResponse() { Message = "Error when input data" });
            }
            
        }

        /// <summary>
		/// Handles the request to process data.
		/// </summary>
		/// <param name="id">Id to identify the request</param>
		/// <param name="data">The base64 encoded data in Json format</param>
		/// <returns>Data was stored</returns>
        /// <response code="201">When the data was stored</response>
        /// <response code="400">If the item is null or any error</response>
		[HttpPost]
        [Route("{id}/right")]
        public async Task<IActionResult> Right(string id, [FromBody]DiffDataRequest jsonData)
        {
            if (string.IsNullOrEmpty(id) || string.IsNullOrEmpty(jsonData.Data))
                return StatusCode(200);

            JsonBase64ItemDto entity = new JsonBase64ItemDto() { Id = id, Position = Domain.Enums.EJsonBase64Position.Right, Data = jsonData.Data };

            try
            {
                bool success = await _service.Save(entity);

                if (success)
                    return StatusCode(201, new DiffDataResponse() { Message = "OK" });
                else
                    return StatusCode(400, new DiffDataResponse() { Message = "Error when input data" });
            }
            catch (Exception)
            {
                //write log error
                return StatusCode(400, "Error when input data");
            }

        }

        /// <summary>
		/// Request the comparison between two data sent as left and right (route) using same id. 
		/// </summary>
		/// <param name="id">Id of the request.</param>
		/// <returns><see cref="JsonDiffDto"/>Information about the comparison of the data.</returns>
		[HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> Diff(string id)
        {
            if (string.IsNullOrEmpty(id))
                return StatusCode(400);

            try
            {
                JsonDiffDto diffResult = await _service.GetComparison(id);
                return StatusCode(200, diffResult);
            }
            catch (Exception)
            {
                return StatusCode(400, "Error when input data");
            }
        }

    }
 
}
