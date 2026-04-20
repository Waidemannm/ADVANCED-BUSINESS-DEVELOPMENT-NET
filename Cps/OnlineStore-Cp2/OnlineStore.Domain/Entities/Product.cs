using OnlineSore.Domain.Commom;

namespace OnlineSore.Domain.Entities;

public class Product: BaseEntity
{
    public Guid IdCategory { get; private set; }
    
    public string Name { get; private set; }
    
    public string Description { get; private set; }
    
    public decimal Price { get; private set; }
    
    public int Stock { get; private set; }

    public Product()
    {
        
    }

    public Product(Guid idCategory, string name, string description, decimal price, int stock)
    {
        IdCategory = idCategory;
        UpdateName(name);
        UpdateDescription(description);
        UpdatePrice(price);
        UpdateStock(stock);
    }
    
    public void UpdateName(string newName)
    {
        if (string.IsNullOrWhiteSpace(newName) && newName.Length <= 300)
        {
            Name = newName;
        }
        else
        {
            throw new Exception("Nome inválido.");
        }
    }
    
    public void UpdatePrice(decimal newPrice)
    {
        if (newPrice > 0 && newPrice < 999999999999.99m)
        {
            Price = newPrice; 
        }else{
            throw new ArgumentException("O Preço está inválido.");
            
        }
    }  
    public void UpdateStock(int newStock)
    {
        if (newStock >= 0 && newStock < 100000){
            Stock = newStock;
        }else{
            throw new ArgumentException("Quantidade inválida de estoque.");
            
        }
    }  
    
    public void UpdateDescription(string newDescription)
    {
        if (string.IsNullOrWhiteSpace(newDescription) && newDescription.Length <= 500)
        {
            Description = newDescription;
        }
        else
        {
            throw new Exception("Descrição inválida.");
        }
    }


}