namespace HR_PLATFORM.DTOs.Auth
{
    public class AddNewLoginDto
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public int codeEmployee {  get; set; }
        public string Role { get; set; }
    }
}
