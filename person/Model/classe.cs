namespace CoursSupDeVinci;

public class Classe
{
    
    private string name;

    private string ecole;

    private string niveau;
    private List<Person> eleves = new();

   public string Name
   {
       get => name;
       set => name = value ?? throw new ArgumentNullException(nameof(value));
   }
   public string Ecole
   {
       get => ecole;
       set => ecole = value ?? throw new ArgumentNullException(nameof(value));
   }

   public string Niveau
   {
       get => niveau;
       set => niveau = value ?? throw new ArgumentNullException(nameof(value));
   }

   public List<Person> Eleves
   {
       get => eleves;
       set => eleves = value ?? throw new ArgumentNullException(nameof(value));
   }

   // public Classe(string name, string ecole, string niveau)
   // {
   //     this.name = name;
   //     this.ecole = ecole;
   //     this.niveau = niveau;
   // }
}