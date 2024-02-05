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

namespace MyJobBoard.Web.Business.DTO.Inputs
{
    public class GetDocumentsQueryInputs : FilteredQueryInput
    {
        [EnumDataType(typeof(EDocumentType))]
        public EDocumentType? Type { get; set; }
        public DateTime? StartDate { get; set; }

    }
}
