using Npgsql;
using CoursSupDeVinci.Utils;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
namespace CoursSupDeVinci;

public interface IServiceCSV
{
    Classe ReadCSV(string path);
}

public class ServiceCSV :  IServiceCSV
{
    public Classe ReadCSV(string path)
    {

        // code qui lit le CSV et met tout dans la classe

        List<Person> persons = new List<Person>();

        var lignes = File.ReadAllLines(path);

        for (int i = 1; i < lignes.Length; i++)
        {
            String line = lignes[i];
            Person person = new Person();

            person.Lastname = line.Split(',')[1];
            person.Firstname = line.Split(',')[2];
            person.Birthdate = DateTimeUtils.ConvertToDateTime(line.Split(',')[3]);
            person.Taille = Int32.Parse(line.Split(',')[5]);

            List<String> details = line.Split(',')[4].Split(';').ToList();

            person.AdressDetails = new Detail(details[0], int.Parse(details[1]), details[2]);

            persons.Add(person);
        }

        Classe maClasse = new Classe();
        maClasse.Niveau = "B2";
        maClasse.Name = "B2 C#";
        maClasse.Ecole = "SupDeVinci";
        maClasse.Eleves = persons.ToList();
            
        return maClasse;
    }
}