﻿using AutoMapper;
using CarWashAggregator.CarWashes.Domain.Interfaces;
using CarWashAggregator.CarWashes.Domain.Models;
using CarWashAggregator.Common.Domain.Contracts;
using CarWashAggregator.Common.Domain.DTO.CarWash.Querys.Request;
using CarWashAggregator.Common.Domain.DTO.CarWash.Querys.Response;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CarWashAggregator.CarWashes.BL.QueryHandlers
{
    public class GetCarWashQueryHandler : IQueryHandler<RequestGetCarWashQuery, ResponseGetCarWashQuery>
    {
        private readonly ICarWashService _carWashService;

        public GetCarWashQueryHandler(ICarWashService carWashService)
        {
            _carWashService = carWashService;
        }
        public async Task<ResponseGetCarWashQuery> Handle(RequestGetCarWashQuery request)
        {
            var mapper = new Mapper(new MapperConfiguration(cfg => cfg.CreateMap<CarWash, ResponseGetCarWashQuery>()));

            CarWash carWash = await _carWashService.GetCarWashAsync(request.Id);
            ResponseGetCarWashQuery carWashQuery = mapper.Map<ResponseGetCarWashQuery>(carWash);

            return await Task.FromResult(carWashQuery);
        }
    }
}
