﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.Xml;
using System.Threading.Tasks;
using CarWashAggregator.ApiGateway.Business.Interfaces;
using CarWashAggregator.ApiGateway.Deamon.Helpers;
using CarWashAggregator.ApiGateway.Domain.Models;
using CarWashAggregator.ApiGateway.Domain.Models.HttpRequestModels;
using CarWashAggregator.ApiGateway.Domain.Models.HttpResultModels;
using Microsoft.AspNetCore.Cors;
using Microsoft.Extensions.Logging;

namespace CarWashAggregator.ApiGateway.Deamon.Controllers
{
    [ApiController]
    [Route("/car-wash")]
    [EnableCors]
    public class CarWashesController : Controller
    {
        private readonly ILogger<CarWashesController> _logger;
        private readonly ICarWashService _carWashService;

        public CarWashesController(ILogger<CarWashesController> logger, ICarWashService carWashService)
        {
            _logger = logger;
            _carWashService = carWashService;
        }

        [Route("/[action]")]
        [HttpGet]
        public async Task<IActionResult> Search([FromQuery] CarWashSearch query)
        {
            try
            {
                var washes = await _carWashService.SearchAsync(query);
                var result = new ListWashesResult {CarWashes = washes};
                return washes is null ? Ok("no carWashes found") : Ok(result);
            }
            catch
            {
                _logger.LogError("error in executing search");
                throw;
            }

        }

        [Route("/[action]")]
        [HttpGet]
        public async Task<IActionResult> GetById([FromRoute] CarWashGet request)
        {
            try
            {
                if (!Guid.TryParse(request.Id, out var id))
                    return Problem("cant parse guid");
                var result = await _carWashService.GetById(id);
                return Ok(result);
            }
            catch
            {
                _logger.LogError("error in executing");
                throw;
            }
        }
     
        [Route("/[action]")]
        [HttpGet]
        public async Task<IActionResult> GetByUserId([FromRoute] CarWashGet request)
        {
            try
            {
                if (!Guid.TryParse(request.Id, out var id))
                    return Problem("cant parse guid");

                var washes = await _carWashService.GetByUserId(id);

                var result = new ListWashesResult { CarWashes = washes };
                return Ok(result);
            }
            catch
            {
                _logger.LogError("error in executing");
                throw;
            }
        }

        [Route("/[action]")]
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] CarWashAdd request)
        {
            try
            {
                if (await _carWashService.AddCarWash(request))
                {
                    return Ok();
                }
                return Problem("cannot create car wash");
            }
            catch
            {
                _logger.LogError("error in executing");
                throw;
            }
        }
    }
}
