public abstract class Widget
{
    public required string Name { get; init; }  // C# 11 ile gelen `required` özelliği
    public abstract void Display();  // Display metodunu her alt sınıf özelleştirir.
}

public class BusinessCardWidget : Widget
{
    public string FullName { get; private set; }  // Dışarıdan değiştirilmesin
    public string Email { get; private set; }

    public BusinessCardWidget(string fullName, string email)
    {
        if (string.IsNullOrWhiteSpace(fullName)) throw new ArgumentException("Full name cannot be empty.");
        if (string.IsNullOrWhiteSpace(email)) throw new ArgumentException("Email cannot be empty.");
        
        Name = "Business Card";
        FullName = fullName;
        Email = email;
    }

    public override void Display()
    {
        Console.WriteLine($"[Business Card] Name: {FullName}, Email: {Email}");
    }
}

public class TextWidget : Widget
{
    public string TextContent { get; private set; }

    public TextWidget(string initialContent)
    {
        Name = "Text Field";
        TextContent = initialContent ?? string.Empty; // Eğer boş verilirse boş string ata
    }

    public void AddText(string text)
    {
        if (!string.IsNullOrWhiteSpace(text))
        {
            TextContent += text;
        }
        else
        {
            Console.WriteLine("Cannot add empty text!");
        }
    }

    public override void Display()
    {
        Console.WriteLine($"[Text Field] Content: {TextContent}");
    }
}
