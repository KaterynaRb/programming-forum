namespace ProgrammingForum_ASPNETCore.Models.UserModels
{
    public class UserViewModel
    {
        public string UserName { get; set; }
        public byte[]? Picture { get; set; }
        public string Email { get; set; }
        public DateTime RegisteredDate { get; set; }
        public int Score { get; set; }
        public string? Role { get; set; }
    }
}
