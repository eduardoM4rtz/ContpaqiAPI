using AutoMapper;
using ContpaqiAPI.Entities;
using ContpaqiAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContpaqiAPI.Profiles
{
    public class EmpleadoProfile : Profile
    {
            public EmpleadoProfile()
            {
                CreateMap<Empleado, EmpleadoSKDto>();
                CreateMap<EmpleadoUPDto, Empleado>();
                CreateMap<Empleado, EmpleadoUPDto>();
                CreateMap<Empleado, EmpleadoINDto>();
                CreateMap<EmpleadoINDto, Empleado>();
            }
    }
}
