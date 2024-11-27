namespace App.Models
{
    public class TextAreaWidget : Widget
    {
        public string TextContent { get; set; }

        public TextAreaWidget(string textContent)
        {
            this.TextContent = textContent;
        }

        public override void Display()
        {
            Console.WriteLine($"--- Text Area ---");
            Console.WriteLine($"Content: {TextContent}");
            Console.WriteLine("-----------------");
        }
    }
}
