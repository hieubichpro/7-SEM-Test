namespace lab_03.BL.Models
{
    public class User
    {
        public int Id {  get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string Role {  get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public bool IsEmailConfirmed { get; set; }
        public User(string login, string password, string role, string name, int id = 1)
        {
            this.Id = id;
            this.Login = login;
            this.Password = password;
            this.Role = role;
            this.Name = name;
        }
        public User(string login, string password, string role, string name, string email, bool emailConfirmed, int id = 1)
        {
            this.Id = id;
            this.Login = login;
            this.Password = password;
            this.Role = role;
            this.Name = name;
            this.Email = email;
            this.IsEmailConfirmed = emailConfirmed;
        }
        public bool checkPassword(string password)
        {
            return this.Password == password;
        }
    }
}
