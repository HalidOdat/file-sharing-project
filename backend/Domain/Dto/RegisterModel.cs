using System.ComponentModel.DataAnnotations;

namespace Domain.Dto
{
    public class RegisterModel
    {
        [EmailAddress(ErrorMessage = "Invalid email address")]
        [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; }
        
        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }
    }
}

/*
var result = _db.Schedules.Where(x => x.AppointmentDateEnd <= objDateTime).ToList();            
if (result.Any())
{
    _db.Schedules.RemoveRange(result);
    _db.SaveChanges();
}

*/