using humanResourceManager.Enums;

namespace humanResourceManager.Models.UsersModel
{
    public class RegisterDto
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public int EmployeeID { get; set; }
        public Role Role { get; set; }
	}
}
