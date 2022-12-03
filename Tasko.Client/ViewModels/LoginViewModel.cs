using System.ComponentModel.DataAnnotations;

namespace Tasko.Client.ViewModels
{
    public class LoginViewModel
    {
        [Display(Name = "Логин")]
        [Required(ErrorMessage = "Логин обязателен для заполнения")]
        public string Username { get; set; }

        [Display(Name = "Пароль")]
        [Required(ErrorMessage = "Пароль обязателен для заполнения")]
        public string Password { get; set; }

        public bool LoginFailureHidden { get; set; } = true;

        public bool ValidateLogin(out string jwtToken)
        {
            if (Username.Equals("Test") && Password.Equals("Test"))
            {
                jwtToken = "eyJhbGciOiJodHRwOi8vd3d3LnczLm9yZy8yMDAxLzA0L3htbGRzaWctbW9yZSNobWFjLXNoYTI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1laWRlbnRpZmllciI6IjMxNjZjMzVmLTkyMzEtNDgzYS1iNzhjLWQyOWUyZDU3MThjOCIsImh0dHA6Ly9zY2hlbWFzLnhtbHNvYXAub3JnL3dzLzIwMDUvMDUvaWRlbnRpdHkvY2xhaW1zL25hbWUiOiJzdHJpbmciLCJodHRwOi8vc2NoZW1hcy5taWNyb3NvZnQuY29tL3dzLzIwMDgvMDYvaWRlbnRpdHkvY2xhaW1zL3JvbGUiOiJVc2VyIiwiZXhwIjoxNjcwMzcyNzEyLCJpc3MiOiJzb21lQG1haWxhZGRyZXNzLmNvbSIsImF1ZCI6InNvbWVAbWFpbGFkZHJlc3MuY29tIn0.Ub2Btk6M8bWE-edaMAeODmhZqeoS9nyNDW6N85q6USE";
                return true;
            }

            jwtToken = null;
            LoginFailureHidden = false;
            return false;
        }
    }
}
