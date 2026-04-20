using OnlineSore.Domain.Commom;
using OnlineSore.Domain.Enum;

namespace OnlineSore.Domain.Entities;

public class Costumer : BaseEntity
{

    //1:N
    public Guid IdPayment { get; private set; }

    //1:1
    public Guid IdAddress { get; private set; }

    public string Name { get; private set; }

    public DateOnly SetBirthDate { get; set; }
    
    public GenderEnum? Gender { get; private set; }
    
    public string Email { get; private set; }

    public Costumer()
    {
        
    }
    public Costumer(Guid idPayment,  Guid idAddress, string name, DateOnly birthDate, GenderEnum? gender, string email)
    {
        IdPayment = idPayment;
        IdAddress = idAddress;
        UpdateName(name);
        UpdateEmail(email);
        Gender = gender;
        UpdateBirthDate(birthDate);
    }
    
    
    public void UpdateName(string newName)
    {
        if (!string.IsNullOrWhiteSpace(newName) && newName.Length <= 300)
        {
            Name = newName;   
        }
        else
        {
            throw new Exception("Nome inválido.");
        }
    }
    
    public void UpdateBirthDate(DateOnly newDate)
    {
        var age = CalculateAge(newDate);
        
        if (age < 13)
            throw new Exception("Cliente deve ter pelo menos 13 anos.");

        SetBirthDate = newDate;
    }
    
    public void UpdateEmail(string newEmail)
    {
        if (!string.IsNullOrWhiteSpace(newEmail) && newEmail.Length <= 100)
        {
            Email = newEmail;   
        }
        else
        {
            throw new Exception("Email inválido.");
        }
    }
    
    private static int CalculateAge(DateOnly date)
    {
        var today = DateOnly.FromDateTime(DateTime.Today);
        var age = today.Year - date.Year;
        if (date > today.AddYears(-age)) age--;
        return age;
    }
    
}