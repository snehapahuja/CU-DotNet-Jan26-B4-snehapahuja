using AutoMapper;
using LoanManagementWebAPI.DTOs;
using LoanManagementWebAPI.Models;
namespace LoanManagementWebAPI.Mappings
{
    public class MappingPorfile : Profile
    {
        public MappingPorfile()
        {
            //client to database
            CreateMap<CreateLoanDto, Loan>();
            CreateMap<UpdateLoanDto, Loan>();

            //database to client
            CreateMap<Loan, GetLoanDto>();

            //if want both directions(read/update) mapping .ReverseMap() can be used
        }
    }
}
