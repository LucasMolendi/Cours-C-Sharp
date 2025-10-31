using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoursSupDeVinci;

#region ORM

public class Details
{
    [Key] 
    public Guid id_details { get; set; } = Guid.NewGuid();
    
    [Required]
    public string street { get; set; }
    
    [Required]
    public int zipCode { get; set; }
    
    [Required]
    public string city { get; set; }

}

#endregion

#region avant ORM
/*public class Detail
{
    private String street;

    private int zipCode;
    
    private String city;

    public Detail(string street, int zipCode, string city)
    {
        this.street = street;
        this.zipCode = zipCode;
        this.city = city;
    }
    
    public string Street
    {
        get => street;
        set => street = value ?? throw new ArgumentNullException(nameof(value));
    }

    public int ZipCode
    {
        get => zipCode;
        set => zipCode = value;
    }

    public string City
    {
        get => city;
        set => city = value ?? throw new ArgumentNullException(nameof(value));
    }
}*/
#endregion