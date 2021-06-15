using Famous.Quote.Quiz.Domain.AggregatesModel.UserAggregate;
using System.ComponentModel.DataAnnotations;

namespace Famous.Quote.Quiz.Application.Services.DataModels
{
    public class UserDto
    {
        public int? Id { get; set; }
        public UserStatus Status { get; set; }
        public UserRole Role { get; set; }

        [Display(Name = "User Name")]
        public string UserName { get; set; }
        public string Password { get; set; }

        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        public string LastName { get; set; }
    }
}
