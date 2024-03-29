﻿using MyJobBoard.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyJobBoard.Domain.Entities
{
    public class Document
    {
        public Guid Id { get; set; }
        public EDocumentType Type { get; set; }
        public string UserId { get; set; }
        public string Name { get; set; }
        public byte[] Content { get; set; }

        public string ContentType { get; set; }
        public DateTime UploadedDate { get; set; }

        [ForeignKey("UserId")]
        public ApplicationUser User { get; set; }
       

    }
}
