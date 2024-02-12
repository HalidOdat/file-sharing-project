using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace Domain.Identity
{
    public class ApplicationUser : IdentityUser
    {
        public virtual ICollection<FileModel> FileModels { get; set; }
    }
}