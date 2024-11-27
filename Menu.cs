using App.Services;
using App.Utils;
using App.Data;


namespace App;
public static class Menu
{
public static void ShowMainMenu(AuthenticationService authService, UserRepository userRepository)
    {
        while (true)
        {
            Console.WriteLine("------ Main Menu ------");
            Console.WriteLine("1. Login");
            Console.WriteLine("2. Register");
            Console.WriteLine("3. Exit");

            string choice = ConsoleHelper.GetStringInput("Please select an option:");

            switch (choice)
            {
                case "1":
                    var loggedInUser = ShowLoginMenu(authService);
                    if (loggedInUser != null)
                    {
                        ShowUserMenu(loggedInUser);
                    }
                    break;

                case "2":
                    RegisterUser(authService, userRepository); // Kullanıcı kaydetme ekranını çağır
                    break;

                case "3":
                    ConsoleHelper.PrintSuccess("Exiting application...");
                    return;

                default:
                    ConsoleHelper.PrintError("Invalid selection, try again.");
                    break;
            }

        }
    }


private static User? ShowLoginMenu(AuthenticationService authService)
{
    ConsoleHelper.PrintSuccess("Welcome to the App!");

    string email = ConsoleHelper.GetStringInput("Enter your email:");
    string password = GetPasswordInput("Enter your password:"); // Şifreyi bu şekilde alıyoruz

    var user = authService.Login(email, password);
    if (user != null)
    {
        ConsoleHelper.PrintSuccess($"Welcome, {user.Name}!");
        return user;
    }

    ConsoleHelper.PrintError("Invalid email or password. Please try again.");
    return null;
}

private static void RegisterUser(AuthenticationService authService, UserRepository userRepository)
{
    Console.Clear();
    Console.WriteLine("------ Register ------");

    string name = ConsoleHelper.GetStringInput("Enter your name:");
    string email = ConsoleHelper.GetStringInput("Enter your email:");
    string password = ConsoleHelper.GetStringInput("Enter your password:");

    var newUser = new User(name, email, password);

    // Kullanıcıları al ve listeye dönüştür
    var usersList = userRepository.GetAllUsers().ToList(); // IEnumerable'i List'e dönüştür

    // Yeni kullanıcıyı listeye ekle
    usersList.Add(newUser);

    // Dosya yolunu mutlak olarak belirtelim
    string filePath = Path.Combine(Directory.GetCurrentDirectory(), "Data", "UserData.json");

    // Güncellenmiş listeyi JSON dosyasına kaydet
    JsonFileHandler.SaveToFile(filePath, usersList);

    ConsoleHelper.PrintSuccess("User registered successfully!");
}



private static string GetPasswordInput(string prompt)
{
    Console.Write(prompt);
    string password = string.Empty;
    while (true)
    {
        var key = Console.ReadKey(true); // true: karakter ekranda gözükmez
        if (key.Key == ConsoleKey.Enter) // Enter tuşuna basıldığında şifre girişi tamamlanır
            break;
        if (key.Key == ConsoleKey.Backspace && password.Length > 0) // Backspace ile silme işlemi
        {
            password = password.Substring(0, password.Length - 1);
            Console.Write("\b \b"); // Ekranda geri silme
        }
        else
        {
            password += key.KeyChar; // Girilen karakteri al
            Console.Write("*"); // Şifreyi gizlemek için yıldız yazdır
        }
    }
    Console.WriteLine(); // Şifreyi bitirdikten sonra yeni satır
    return password;
}

// Space oluşturma işlevi
private static void CreateSpace(User user)
{
    Console.Clear();
    Console.WriteLine("------ Create Space ------");
    string spaceName = ConsoleHelper.GetStringInput("Enter the name of the space:");

    // Burada Space nesnesi yaratıp, kullanıcının sahip olduğu space'lere ekleyebilirsiniz
    // Örneğin, bir List<Space> içinde tutabilirsiniz.
    Space newSpace = new Space(spaceName, user); // Space sınıfınızda istenen alanları tanımlayın
    user.Spaces.Add(newSpace); // Kullanıcının sahip olduğu Space'leri ekliyoruz

    ConsoleHelper.PrintSuccess($"Space '{spaceName}' created successfully!");
    Console.ReadKey();
}

private static void AddWidget(Space space)
{
    Console.Clear();
    Console.WriteLine($"------ Add Widget to {space.Name} ------");
    string widget = ConsoleHelper.GetStringInput("Enter the widget name:");

    space.Widgets.Add(widget);
    ConsoleHelper.PrintSuccess($"Widget '{widget}' added successfully!");
    Console.ReadKey();
}


private static void InviteUser(Space space)
{
    Console.Clear();
    Console.WriteLine($"------ Invite User to {space.Name} ------");
    string email = ConsoleHelper.GetStringInput("Enter the email of the user to invite:");

    // Kullanıcıyı bul ve ekle
    var userRepository = new UserRepository(JsonFileHandler.LoadFromFile<User>("Data/UserData.json"));
    var invitedUser = userRepository.GetByEmail(email);

    if (invitedUser == null)
    {
        ConsoleHelper.PrintError("User not found.");
    }
    else if (space.Members.Contains(invitedUser))
    {
        ConsoleHelper.PrintError("User is already a member of this space.");
    }
    else
    {
        space.Members.Add(invitedUser);
        ConsoleHelper.PrintSuccess($"User {invitedUser.Name} invited successfully!");
    }
    Console.ReadKey();
}


private static void ManageSpaces(User user)
{
    while (true)
    {
        Console.Clear();
        Console.WriteLine("------ Manage Spaces ------");

        if (user.Spaces.Count == 0)
        {
            ConsoleHelper.PrintError("No spaces found. Create a space first.");
            Console.ReadKey();
            return;
        }

        // Tüm Space'leri listele
        for (int i = 0; i < user.Spaces.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {user.Spaces[i].Name}");
        }

        int choice = ConsoleHelper.GetIntInput("Select a space to manage (or 0 to go back):");
        if (choice == 0) return;

        if (choice > 0 && choice <= user.Spaces.Count)
        {
            ManageSingleSpace(user.Spaces[choice - 1]); // Seçilen Space'i yönet
        }
        else
        {
            ConsoleHelper.PrintError("Invalid selection.");
        }
    }
}

private static void ManageSingleSpace(Space space)
{
    while (true)
    {
        Console.Clear();
        Console.WriteLine($"------ Managing Space: {space.Name} ------");
        Console.WriteLine("1. Add Widget");
        Console.WriteLine("2. Invite User");
        Console.WriteLine("3. Back");

        string choice = ConsoleHelper.GetStringInput("Select an option:");

        switch (choice)
        {
            case "1":
                AddWidget(space);
                break;

            case "2":
                InviteUser(space);
                break;

            case "3":
                return;

            default:
                ConsoleHelper.PrintError("Invalid selection, try again.");
                break;
        }
    }
}


// Space listesi gösterme işlevi
private static void ViewSpaceList(User user)
{
    Console.Clear();
    Console.WriteLine("------ Space List ------");

    if (user.Spaces.Count == 0)
    {
        ConsoleHelper.PrintError("No spaces found. Create a space first.");
    }
    else
    {
        foreach (var space in user.Spaces)
        {
            Console.WriteLine($"Space: {space.Name}");
        }
    }

    ConsoleHelper.PrintSuccess("Press any key to return to the menu.");
    Console.ReadKey();
}

private static void ShowUserMenu(User user)
{
    while (true)
    {
        Console.Clear();
        Console.WriteLine("------ User Menu ------");
        Console.WriteLine("1. Create Space");
        Console.WriteLine("2. View Space List");
        Console.WriteLine("3. Log Out");
        Console.WriteLine("4. Exit");

        string choice = ConsoleHelper.GetStringInput("Please select an option:");

        switch (choice)
        {
            case "1":
                CreateSpace(user);
                break;

            case "2":
                ManageSpaces(user); // Space'lerle ilgili yönetim menüsü
                break;

            case "3":
                ConsoleHelper.PrintSuccess("Logging out...");
                return;

            case "4":
                ConsoleHelper.PrintSuccess("Exiting application...");
                Environment.Exit(0);
                break;

            default:
                ConsoleHelper.PrintError("Invalid selection, try again.");
                break;
        }
    }
}



    private static void ViewProfile(User user)
    {
        Console.Clear();
        Console.WriteLine("------ Profile ------");
        Console.WriteLine($"Name: {user.Name}");
        Console.WriteLine($"Email: {user.Email}");
        Console.WriteLine($"Password: {user.Password}");
        ConsoleHelper.PrintSuccess("Press any key to return to the menu.");
        Console.ReadKey();
    }
}
