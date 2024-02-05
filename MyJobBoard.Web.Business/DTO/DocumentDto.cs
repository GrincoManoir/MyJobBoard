using MyJobBoard.Domain.Entities;
using MyJobBoard.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyJobBoard.Web.Business.DTO
{
    public record DocumentDto
    {
        public DocumentDto(Document d)
        {
            this.Id = d.Id;
            this.Name = d.Name;
            this.UploadedDate = d.UploadedDate;
            this.UserId = d.UserId;
            this.Type = d.Type;
        }

        public Guid Id { get; set; }
        public string UserId { get; set; }
        public string Name { get; set; }
        
        public EDocumentType Type { get; set; }
        public DateTime UploadedDate { get; set; }
    }
}
