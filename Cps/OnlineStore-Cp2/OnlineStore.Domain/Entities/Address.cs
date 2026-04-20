using OnlineSore.Domain.Commom;

namespace OnlineSore.Domain.Entities;

public class Address : BaseEntity
{

    public string Street { get; private set; }

    public string City { get; private set; }

    public string State { get; private set; }
   
    public string PostalCode { get; private set; }

    public string Number { get; private set; }

    public string Country { get; private set; }

    public Address(){
    }

    public Address(string street, string city, string state, string postalCode, string number, string country)
    {
        UpdateStreet(street);
        UpdateCity(city);
        UpdateState(state);
        UpdatePostalCode(postalCode);
        UpdateNumber(number);
        UpdateCountry(country);
    }

    public void UpdateStreet(string newStreet)
    {
        if (!string.IsNullOrWhiteSpace(newStreet) && newStreet.Length <= 150)
        {
            Street = newStreet;
        }
        else
        {
            throw new Exception("Rua inválida.");
        }
    }

    public void UpdateCity(string newCity)
    {
        if (!string.IsNullOrWhiteSpace(newCity) && newCity.Length <= 70)
        {
            City = newCity;
        }else{
            throw new Exception("Cidade inválida.");
        }
    }
    
    public void UpdateState (String newState)
    {
        if (!string.IsNullOrWhiteSpace(newState) && newState.Length <= 70)
        {
            State = newState;
        }else{
            throw new Exception("Estado inválido.");
        }
    }

    public void UpdatePostalCode(string newPostalCode)
    {
        if (!string.IsNullOrWhiteSpace(newPostalCode) && newPostalCode.Length <= 20)
        {
            PostalCode = newPostalCode;
        }else{
            throw new Exception("Código postal inválido.");
        }
    }
    
    public void UpdateNumber(string newNumber)
    {
        if (!string.IsNullOrWhiteSpace(newNumber) && newNumber.Length <= 20 && newNumber.All(char.IsDigit))
        {
            Number = newNumber;
        }else{
            throw new Exception("Número inválido.");
        }
    }

    public void UpdateCountry(string newCountry)
    {
        if (!string.IsNullOrWhiteSpace(newCountry) && newCountry.Length <= 30)
        {
            Country = newCountry;
        }else{
            throw new Exception("País inválido.");
        }
    }
   
  
}