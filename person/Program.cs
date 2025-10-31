using CoursSupDeVinci;
using CoursSupDeVinci.Utils;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Npgsql;

// Charger la configuration manuellement
var configuration = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile(@"C:\Users\Lucas\OneDrive - SUP DE VINCI\Documents\B2\C#\Cours-C-Sharp\person\appsettings.json", optional: false, reloadOnChange: true)
    .Build();

var host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        // NpgsqlConnection singleton avec ouverture automatique
        services.AddSingleton(provider =>
        {
            var conn = new NpgsqlConnection(
                configuration.GetConnectionString("DefaultConnection")); 
            conn.Open(); // ouverture unique
            return conn;
        });

        // On enregistre notre service applicatif
        services.AddTransient<CoursSupDeVinci.DbConnection>();

        
        // on enregistre le service ServiceCSV
        services.AddTransient<IServiceCSV, ServiceCSV>();
    })
    .Build();

using var scope = host.Services.CreateScope();
DbConnection DbConnection = scope.ServiceProvider.GetRequiredService<DbConnection>();

// Récupération du service CSV
IServiceCSV ServiceCSV = scope.ServiceProvider.GetRequiredService<IServiceCSV>();



String path = @"C:\Users\Lucas\OneDrive - SUP DE VINCI\Documents\B2\C#\Cours-C-Sharp\person\Data\Classe_SDV_B2.csv";


Classe maClasse = ServiceCSV.ReadCSV(path);


var db = new DbConnection(new NpgsqlConnection("Host=localhost;Port=5432;Database=postgres;Username=postgres;Password=root"));
await db.init(maClasse);

#region version avant db
/*
String path = @"C:\Users\Lucas\OneDrive - SUP DE VINCI\Documents\B2\C#\Cours-C-Sharp\person\Data\Classe_SDV_B2.csv";

Dictionary<int,Person> persons = new Dictionary<int, Person>(); 

var lignes = File.ReadAllLines(path);

for (int i = 1; i < lignes.Length; i++)
{
    String line = lignes[i];
    Person person = new Person
    {
        Lastname = line.Split(',')[1],
        Firstname = line.Split(',')[2],
        Birthdate = ConvertToDateTime(line.Split(',')[3]),
        Taille = int.Parse(line.Split(',')[5])
    };
    List<String> details = line.Split(',')[4].Split(';').ToList();
    
    person.AdressDetails = new Detail(details[0], Int32.Parse(details[1]), details[2]);
    
    persons.Add(int.Parse(line.Split(',')[0]), person);
}

*/
#endregion


#region renseigne à la main

// Person person1 = new Person();
// Person person2 = new Person();
//
// Console.WriteLine("Quelle est le nom de P1 ?");
// person1.Lastname = Console.ReadLine();
// Console.WriteLine("Quelle est le nom de P2 ?");
// person2.Lastname = Console.ReadLine();
//
// Console.WriteLine("Quelle est le prénom de P1 ?");
// person1.Firstname = Console.ReadLine();
// Console.WriteLine("Quelle est le prénom de P2 ?");
// person2.Firstname = Console.ReadLine();
//
// Console.WriteLine("Quelle est votre Date de Naissance de P1 (au format JJ/MM/YYYY) ?");
// String birthDate1 = Console.ReadLine();
// Console.WriteLine("Quelle est votre Date de Naissance P2 (au format JJ/MM/YYYY) ?");
// String birthDate2 = Console.ReadLine();
//
// Console.WriteLine("Quelle est l'adresse de P1 (au format rue,codePostal,Ville) ?");
// String address1 = Console.ReadLine();
// Console.WriteLine("Quelle est l'adresse de P2 (au format rue,codePostal,Ville) ?");
// String address2 = Console.ReadLine();
//
// List<String> listAdress = address1.Split(",").ToList();
// List<String> listAdress2 = address2.Split(",").ToList();
//
// person1.AdressDetails = new Detail(listAdress[0], Int32.Parse(listAdress[1]), listAdress[2]);
// person2.AdressDetails = new Detail(listAdress2[0], Int32.Parse(listAdress2[1]), listAdress2[2]);

#endregion

#region classe
/*
Console.Write("Nom de la classe : ");
string className = Console.ReadLine();

Console.Write("Nom de l'école : ");
string schoolName = Console.ReadLine();

Console.Write("Niveau de la classe : ");
string level = Console.ReadLine();

// Création de l'objet Classe
Classe maClasse = new Classe(className, schoolName, level);

Person p = new Person();

Console.Write("Prénom : ");
p.Firstname = Console.ReadLine();

Console.Write("Nom : ");
p.Lastname = Console.ReadLine();

Console.Write("Date de naissance (JJ/MM/AAAA) : ");
if (DateTime.TryParse(Console.ReadLine(), out DateTime birth))
    p.Birthdate = birth;
else
    Console.WriteLine("️ Date invalide, elle sera laissée vide.");

Console.Write("Adresse (rue,codePostal,ville) : ");
string[] parts = Console.ReadLine().Split(',');
if (parts.Length == 3)
{
    p.AdressDetails = new Detail(parts[0], int.Parse(parts[1]), parts[2]);
}
else
{
    Console.WriteLine(" Adresse invalide, champs ignorés.");
}


maClasse.Eleves.Add(p);

// Affichage du résultat final
Console.WriteLine($"\n=== Classe {maClasse.Name} ({maClasse.Niveau}) - {maClasse.Ecole} ===");
foreach (var eleve in maClasse.Eleves)
{
    Console.WriteLine($"{eleve.Firstname} {eleve.Lastname}, {eleve.getYearsOld()} ans");
}
*/
#endregion

#region taille moyenne
/*
    double tailleMoyenne = persons.Average(person => person.Value.Taille);
    double tailleMoyenneMetre = Math.Floor(tailleMoyenne) / 100;

    Dictionary<int, Person> tallerPersons = persons.Where(person => person.Value.Taille > tailleMoyenne)
        .ToDictionary(person => person.Key, person => person.Value);

    Console.WriteLine($"Il y a {tallerPersons.Count.ToString()} personnes qui sont plus grandes que la moyenne " +
                    $"de la classe qui est de {tailleMoyenneMetre} mètre");
*/
#endregion
/*
DateTime ConvertToDateTime(String date)
{
    if (DateTime.TryParse(date, out DateTime birthdate))
    {
        return  birthdate;
    }
    else
    {
        Console.WriteLine($"La date de P1 est mal renseignée");
        return DateTime.Now;
    }
}
int moyenneTailles = (int)persons.Values.Average(p => p.Taille);

//création de la liste "grands" qui est une liste Person
List<Person> grands = new List<Person>();
//Pour chaque person
foreach (Person person in persons.Values)
{   
    //vérification de la taille
    if (person.Taille > moyenneTailles)
    {   
        //vérification de l'adresse
        if (person.AdressDetails.City == "Nantes")
        {
            //ajout de la person à la liste
            grands.Add(person);
        }
    }
}
//mettre les persons du plus grand au plus petit par ordre de taille
var grandstri = grands.OrderByDescending(p => p.Taille).ToList();

//phrase de conclusion
Console.WriteLine("Les plus grands nantais de la classes sont : ");
foreach (Person grand in grandstri)
{
    Console.WriteLine($"{grand.Firstname} qui fait {(float)grand.Taille / 100}m");
        
}
*/
#region Foreach personne classique
/*
foreach (KeyValuePair<int, Person> person in persons)
{
    
    Console.WriteLine($"Bonjour {person.Value.Firstname} {person.Value.Lastname},");
    Console.WriteLine($"tu as {person.Value.getYearsOld().ToString()} ans, tu fais {person.Value.Taille} cm de haut et tu habites au {person.Value.AdressDetails.Street}" +
                      $" {person.Value.AdressDetails.ZipCode.ToString()} {person.Value.AdressDetails.City}.");

}
*/
#endregion

#region comparateur d'age

// if (person1.getYearsOld() > person2.getYearsOld())
// {
//     Console.WriteLine($"{person1.Firstname} {person1.Lastname} est plus agé(e) que {person2.Firstname} {person2.Lastname} de {(person1.getYearsOld() - person2.getYearsOld()).ToString()} an(s)");
// }
// else if (person1.getYearsOld() < person2.getYearsOld())
// {
//     Console.WriteLine($"{person2.Firstname} {person2.Lastname} est plus agé(e) que {person1.Firstname} {person1.Lastname} de {(person2.getYearsOld() - person1.getYearsOld()).ToString()} an(s)");
// }
// else
// {
//     Console.WriteLine($"{person1.Firstname} {person1.Lastname} a le même age que {person2.Firstname} {person2.Lastname}");
// }
#endregion