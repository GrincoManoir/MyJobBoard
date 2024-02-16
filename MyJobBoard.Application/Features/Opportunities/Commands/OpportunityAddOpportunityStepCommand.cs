using MediatR;
using MyJobBoard.Application.Interfaces;
using MyJobBoard.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyJobBoard.Application.Features.Opportunities.Commands
{
    public class OpportunityAddOpportunityStepCommand(Guid opportunityId, DateTime? dueDate, string freeNotes) : IRequest<RequestResult<Opportunity>>
    {
        public Guid OpportunityId { get; set; } = opportunityId;
        public DateTime? DueDate { get; set; } = dueDate;
        public string FreeNotes { get; set; } = freeNotes;
    }

    public class OpportunityAddOpportunityStepCommandHandler(IOpportunityService opportunityService, IOpportunityStepService stepService) : IRequestHandler<OpportunityAddOpportunityStepCommand, RequestResult<Opportunity>>
    {
        private readonly IOpportunityStepService _stepService = stepService;
        private readonly IOpportunityService _opportunityService = opportunityService;


        public async Task<RequestResult<Opportunity>> Handle(OpportunityAddOpportunityStepCommand request, CancellationToken cancellationToken)
        {
            Opportunity? opportunity = await _opportunityService.GetOpportunityById(request.OpportunityId);
            if (opportunity == null)
            {
                return RequestResult<Opportunity>.Failure("Opportunity not found", ApplicationResult.NOT_FOUND);
            }

            var opportunityStep = new OpportunityStep
            {
                DueDate = request.DueDate,
                FreeNotes = request.FreeNotes,
                CreationDate = DateTime.Now,
                State = opportunity.State,
                OpportunityId = opportunity.Id
            };
            opportunity.OpportunitySteps.Add(opportunityStep);
            try
            {
                await _stepService.CreateOpportunityStep(opportunityStep);
            }
            catch (Exception ex)
            {
                return RequestResult<Opportunity>.Failure(ex.InnerException?.Message ?? ex.Message, ApplicationResult.BAD_REQUEST);
            }

            return RequestResult<Opportunity>.Success(data :opportunity);
        }
    }
}
