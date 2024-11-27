public class Space
{
    public string Name { get; set; }
    public User Owner { get; set; }
    public List<User> Members { get; set; } = new List<User>(); // Davet edilen kullanıcılar
    public List<string> Widgets { get; set; } = new List<string>(); // Widget listesi

    // Constructor
public Space(string? name, User? owner) // Nullable türler
{
    if (string.IsNullOrEmpty(name) || owner == null)
    {
        throw new ArgumentNullException("Name or Owner cannot be null.");
    }

    Name = name!;
    Owner = owner!;
    Members.Add(owner); // Sahibi otomatik olarak üye yapıyoruz
}

}
