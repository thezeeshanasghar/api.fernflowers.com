using AutoMapper;
using api.fernflowers.com.ModelDTO;
using api.fernflowers.com.Data.Entities;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<Vaccine, VaccineDTO>().ReverseMap();
        CreateMap<AdminSchedule, AdminScheduleDTO>().ReverseMap();
        CreateMap<Dose, DoseDTO>().ReverseMap();
        CreateMap<DoctorSchedule, DoctorScheduleDTO>().ReverseMap();
        CreateMap<Doctor, DoctorDTO>().ReverseMap();
        CreateMap<ClinicTiming, ClinicTimingDTO>().ReverseMap();
        CreateMap<Clinic, ClinicDTO>().ReverseMap();
        CreateMap<Child, ChildDTO>().ReverseMap();
        CreateMap<BrandInventory, BrandInventoryDTO>().ReverseMap();
        CreateMap<Brand, BrandDTO>().ReverseMap();
        CreateMap<BrandAmount, BrandAmountDTO>().ReverseMap();
    }
}
