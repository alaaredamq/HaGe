using System;

namespace HaGe.Application.Models {
    public class RegisterViewModel {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        
        public Guid? RoleId { get; set; }
        // public string? Name => $"{FirstName} {LastName}";

        public RegisterViewModel(){}
    }
}
