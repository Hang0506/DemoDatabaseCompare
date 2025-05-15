using AutoMapper;
using DemoDatabaseCompare.Students;

namespace DemoDatabaseCompare;

public class DemoDatabaseCompareApplicationAutoMapperProfile : Profile
{
    public DemoDatabaseCompareApplicationAutoMapperProfile()
    {
        /* You can configure your AutoMapper mapping configuration here.
         * Alternatively, you can split your mapping configurations
         * into multiple profile classes for a better organization. */
        CreateMap<Student, StudentDto>();
        CreateMap<StudentDto, Student>();
    }
}
