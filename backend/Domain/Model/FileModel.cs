﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.Identity;

namespace Domain
{
    public class FileModel : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string ContentType { get; set; }
        public long Size { get; set; } 
        public byte[] Content { get; set; }

        [Required]
        public virtual ApplicationUser Creator { get; init; }
    }
}
