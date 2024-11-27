public class User
{
    public string Name { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public List<Space> Spaces { get; set; } = new List<Space>();  // Kullanıcının sahip olduğu space'leri tutan liste

    public User(string name, string email, string password)
    {
        Name = name;
        Email = email;
        Password = password;
    }
}
