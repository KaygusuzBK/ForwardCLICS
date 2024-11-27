using App.Data;
using App.Services;
using App.Utils;
using App;


class Program
{
    static string userFilePath = "Data/UserData.json";

    static void Main(string[] args)
    {
        // Kullanıcı verilerini yükle
        var users = JsonFileHandler.LoadFromFile<User>(userFilePath);
        var userRepository = new UserRepository(users);
        var authService = new AuthenticationService(userRepository);

        // Main Menu'yi göster

        Menu.ShowMainMenu(authService, userRepository);
        // Kullanıcıyı kaydet ve verileri JSON'a yaz
        JsonFileHandler.SaveToFile(userFilePath, userRepository.GetAllUsers().ToList());
    }
}
