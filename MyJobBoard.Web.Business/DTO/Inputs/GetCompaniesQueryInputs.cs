using MyJobBoard.Application;
using MyJobBoard.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc;
using MyJobBoard.API.Web.Business.SwaggerSchemaFilters;
using System.Runtime.CompilerServices;
using MyJobBoard.Domain.Entities;

namespace MyJobBoard.Web.Business.DTO.Inputs
{
    public class GetCompaniesQueryInputs : FilteredQueryInput
    {

    }
}
