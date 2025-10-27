class Program
{
    static void Main(string[] args)
    {
        const string prenom = "Lucas";
        const string nom = "Molendi";
        const string rue = "17 rue des Fresches";
        const int code_postal = 44190;
        const string ville = "Saint Hilaire de Clisson";
        DateTime date_naissance = new DateTime(2006,12,12);
        DateTime date = DateTime.Today;
        TimeSpan age = date - date_naissance;

        Console.WriteLine($"Bonjour {prenom} {nom},\n\ntu as {Math.Floor(age.Days/365.4)} et tu habites au {rue} {code_postal} {ville} ");

    }

}