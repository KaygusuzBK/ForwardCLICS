using Newtonsoft.Json;

public class SpaceManager
{
    private static string spaceFilePath = "Data/SpaceData.json"; // Space verilerini kaydedeceğimiz dosya

    // Space oluşturulurken JSON dosyasına kaydetme
    public static void SaveSpace(Space space, User user)
    {
    if (space == null || user == null)
    {
        throw new ArgumentNullException("Space or User cannot be null.");
    }
        List<Space> spaces = LoadSpaces();

        spaces.Add(space);

        user.Spaces.Add(space);

        // Space'leri JSON formatında serialize et ve dosyaya yaz
        string json = JsonConvert.SerializeObject(spaces, Formatting.Indented);
        File.WriteAllText(spaceFilePath, json);

        // Kullanıcı verilerini de JSON dosyasına kaydet
        string userJson = JsonConvert.SerializeObject(user, Formatting.Indented);
        File.WriteAllText("Data/UserData.json", userJson);

        Console.WriteLine("Space başarıyla kaydedildi.");
    }

    // JSON dosyasından Space'leri yükleme
    public static List<Space> LoadSpaces()
    {
        if (File.Exists(spaceFilePath))
        {
            // Dosya var, JSON verisini oku ve deserialize et
            string json = File.ReadAllText(spaceFilePath);
            return JsonConvert.DeserializeObject<List<Space>>(json) ?? new List<Space>();
        }
        return new List<Space>();
    }
}
