namespace App.Models
{
    public class BusinessCardWidget : Widget
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }

        public BusinessCardWidget(string firstName, string lastName, string email)
        {
            this.FirstName = firstName;
            this.LastName = lastName;
            this.Email = email;
        }

        public override void Display()
        {
            Console.WriteLine($"--- Business Card ---");
            Console.WriteLine($"Name: {FirstName} {LastName}");
            Console.WriteLine($"Email: {Email}");
            Console.WriteLine("---------------------");
        }
    }
}
