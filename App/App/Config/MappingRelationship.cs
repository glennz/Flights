namespace App.Config
{
    using App.Dto;
    using AutoMapper;
    using FlightManager.Models;

    public class MappingRelationship
    {
        /// <summary>
        /// create mapping relationship between model class and entity class
        /// </summary>
        public static void Create(ConfigurationStore cfg)
        {
            cfg.CreateMap<Flight, FlightDto>();
            cfg.CreateMap<FlightDto, Flight>();

            cfg.CreateMap<Gate, GateDto>()
                .ForMember(dto => dto.Id, from => from.MapFrom(g => g.Id))
                .ForMember(dto => dto.FlightDtos, from => from.MapFrom(g => g.Flights))
                .ForMember(dto => dto.GateName, o => o.Ignore());

            cfg.CreateMap<GateDto, Gate>()
                .ForMember(g => g.Id, from => from.MapFrom(dto => dto.Id))
                .ForMember(g => g.Flights, from => from.MapFrom(dto => dto.FlightDtos));
        }
    }
}
