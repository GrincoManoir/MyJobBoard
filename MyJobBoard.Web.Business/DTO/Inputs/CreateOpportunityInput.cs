using MyJobBoard.Domain.Entities;
using MyJobBoard.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyJobBoard.Web.Business.DTO.Inputs
{
    public class CreateOpportunityInput
    {
        public Guid? CompanyId { get; set; }
        public string? Location { get; set; }
        public string RoleTitle { get; set; }
        public RemoteCondition? RemoteCondition { get; set; }
        public string? Industry { get; set; }
        public IndicativeSalaryRangeInPut? IndicativeSalaryRange { get; set; }
        public string? FreeNotes { get; set; }

        public class IndicativeSalaryRangeInPut
        {
            public int MinSalary { get; set; }
            public int MaxSalary { get; set; }
            public SalaryPeriodicity Periodicity { get; set; }
        }
    }
}
