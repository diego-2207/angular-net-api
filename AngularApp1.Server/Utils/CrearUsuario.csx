#r "nuget: BCrypt.Net-Next, 4.0.3"
using BCrypt.Net;

Console.Write("Introduce el nombre de usuario: ");
string user = Console.ReadLine();
Console.Write("Introduce la contraseña: ");
string pass = Console.ReadLine();

string hash = BCrypt.Net.BCrypt.HashPassword(pass);

Console.WriteLine("\n--- COPIA ESTO A TU SQL ---");
Console.WriteLine($"Usuario: {user}");
Console.WriteLine($"Hash: {hash}");
Console.WriteLine(BCrypt.Net.BCrypt.Verify(pass, hash));