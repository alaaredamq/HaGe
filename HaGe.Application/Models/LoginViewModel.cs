namespace HaGe.Application.Models {
    public class LoginViewModel {
        // public Guid Id { get; set; }
        public string? Email { get; set; }
        public string? Username { get; set; }
        public string? Password { get; set; }
        public bool RememberMe { get; set; }
    
        public LoginViewModel() {
        }
    }
}