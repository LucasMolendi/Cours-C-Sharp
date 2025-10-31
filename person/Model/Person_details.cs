using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace CoursSupDeVinci;

public class Person_details
{
    [Key]
    public Guid Id_PersonDetails { get; set; } =Guid.NewGuid();
    
    [ForeignKey("Person")]
    public Guid id_person { get; set; }
    public Person Person { get; set; }
    
    [ForeignKey("Details")]
    public Guid id_details { get; set; }
    public Details details { get; set; }

    
}