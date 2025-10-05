using AutoMapper;
using El_Aref.DAL.Model;
using El_Aref.PL.DTO;

namespace El_Aref.PL.Mappling
{
    public class EmployeeProfile :Profile
    {
        public EmployeeProfile()
        {
            CreateMap<CreateEmployeeTDO,Employee>().ReverseMap();
            //CreateMap<CreateEmployeeTDO,Employee>().ReverseMap().
            //    ForMember(d=>d.Name,o=>o.MapFrom(o=>o.EmpName));

        }
    }
}
