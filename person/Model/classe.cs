namespace CoursSupDeVinci;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#region ORM

public class Classe
{
    [Key]
    public Guid id_classe { get; set; } = Guid.NewGuid();
    
    [Required]
    public string name { get; set; }
    
    [Required]
    public string level { get; set; }
    
    [Required]
    public string school { get; set; }
    
}

#endregion

#region version avant ORM
/*
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
   */
#endregion
   // public Classe(string name, string ecole, string niveau)
   // {
   //     this.name = name;
   //     this.ecole = ecole;
   //     this.niveau = niveau;
   // }
