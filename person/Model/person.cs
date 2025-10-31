using CoursSupDeVinci;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

#region Avec ORM
public class Person
{
    [Key]
    public Guid id_person { get; set; } =  Guid.NewGuid();
    
    [Required]
    public string FirstName { get; set; }
    
    [Required]
    public string LastName { get; set; }
    
    [Required]
    public DateTime birthdate { get; set; }
    
    [Required]
    //relation n..n vers Detail
    public ICollection<Details> Details { get; set; } = new List<Details>();
    
    [Required]
    public double size {get; set;}
    
    [ForeignKey("Classe")]
    public Guid id_classe { get; set; }
    public Classe Classe { get; set; }

}
#endregion
#region Avant ORM
/*
public class Person
{
    private String firstname;

    private String lastname;
    
    private DateTime birthdate;
    
    private Detail adressDetails;

    private int taille;
    public Detail AdressDetails
    {
        get => adressDetails;
        set => adressDetails = value ?? throw new ArgumentNullException(nameof(value));
    }

    public int Taille
    {
        get => taille;
        set => taille = value;
    }

    public string Firstname
    {
        get => firstname;
        set => firstname = value ?? throw new ArgumentNullException(nameof(value));
    }

    public string Lastname
    {
        get => lastname;
        set => lastname = value ?? throw new ArgumentNullException(nameof(value));
    }

    public DateTime Birthdate
    {
        get => birthdate;
        set => birthdate = value;
    }

    public int getYearsOld()
    {
        DateTime today = DateTime.Today;

        int years = today.Year - birthdate.Year;

        if (today.Month < birthdate.Month || today.Month == birthdate.Month && today.Day < birthdate.Day)
        {
            years--;
        }
        
        return years;
    }
}
*/
#endregion