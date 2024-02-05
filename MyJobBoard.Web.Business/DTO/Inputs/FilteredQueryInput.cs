using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MyJobBoard.Web.Business.DTO.Inputs
{
    /// <summary>
    /// Classe de base pour les QueryInputs destinée à implémenter les filtres
    /// </summary>
    public class FilteredQueryInput
    {
        public string? Range { get; set; }
        public string? Sort { get; set; }
        public bool? Desc { get; set; }
    }
}
