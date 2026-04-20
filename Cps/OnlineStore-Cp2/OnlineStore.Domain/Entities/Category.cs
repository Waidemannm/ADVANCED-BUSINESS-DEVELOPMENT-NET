using OnlineSore.Domain.Commom;

namespace OnlineSore.Domain.Entities;

public class Category : BaseEntity
{
    public string Name { get; private set; }
    
    public string Description { get;  private set; }
    
    public Category()
    {
        
    }
    
    public Category(string name, string description)
    { 
        UpdateName(name);
        UpdateDescription(description);
    }
    
    public void UpdateName(string newName)
    {
        if(!string.IsNullOrWhiteSpace(newName) && newName.Length <= 300)
        {
            Name = newName;       
        }else{
            throw new Exception("Nome inválido.");
        }
    }
    
    public void UpdateDescription(string newDescription)
    {
        if(!string.IsNullOrWhiteSpace(newDescription) && newDescription.Length <= 150)
        {
            Description = newDescription;      
        }else{
            throw new Exception("Descrição inválida.");
        }
       
    }
}