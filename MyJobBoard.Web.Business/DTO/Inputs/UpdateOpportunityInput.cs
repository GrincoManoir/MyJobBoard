using Microsoft.AspNetCore.Mvc;
using MyJobBoard.Application.Features.Companies.Commands;
using MyJobBoard.Application.Features.Opportunities.Commands;
using MyJobBoard.Domain.Entities;
using MyJobBoard.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MyJobBoard.Web.Business.DTO.Inputs
{
    public class UpdateOpportunityInput
    {
        public string RoleTitle { get; set; }
        public IndicativeSalaryRangeInput? IndicativeSalaryRange { get; set; }
        public string? Location { get; set; }
        public RemoteCondition? RemoteCondition { get; set; }
        public string? Industry { get; set; }
        public Guid? CompanyId { get; set; }
        public OpportunityState State { get; set; }


        public class IndicativeSalaryRangeInput
        {
            public int MinSalary { get; set; }
            public int MaxSalary { get; set; }
            public SalaryPeriodicity Periodicity { get; set; }
        }
    }

    public static class UpdateOpportunityInputExtensions
    {
        public static UpdateOpportunityCommand toBusinessCommand(this UpdateOpportunityInput input, Guid id)
        {
            return new UpdateOpportunityCommand
            {
                Id = id,
                RoleTitle = input.RoleTitle,
                IndicativeSalaryRange = input.IndicativeSalaryRange == null ? null : new() {MaxSalary = input.IndicativeSalaryRange.MaxSalary, MinSalary = input.IndicativeSalaryRange.MaxSalary, Periodicity = input.IndicativeSalaryRange.Periodicity},
                Location = input.Location,
                RemoteCondition = input.RemoteCondition,
                Industry = input.Industry,
                CompanyId = input.CompanyId,
                State = input.State
            };
        }
    }
}
