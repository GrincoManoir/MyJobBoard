﻿using MyJobBoard.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyJobBoard.Web.Business.DTO
{
    public class InterlocutorDto
    {
        public Guid Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? LinkedinProfile { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public List<InterlocutorOpportunityDto> Opportunities { get; set; }

        public InterlocutorDto(Interlocutor interlocutor)
        {
            Id = interlocutor.Id;
            FirstName = interlocutor.FirstName;
            LastName = interlocutor.LastName;
            LinkedinProfile = interlocutor.LinkedinProfile;
            Email = interlocutor.Email;
            Phone = interlocutor.Phone;
            Opportunities = interlocutor.Opportunities?.Select(o => new InterlocutorOpportunityDto(o)).ToList() ?? new List<InterlocutorOpportunityDto>();
        }
    }
}
