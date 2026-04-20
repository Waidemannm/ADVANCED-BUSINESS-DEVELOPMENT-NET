using OnlineSore.Domain.Commom;
using OnlineSore.Domain.Enum;
 
 namespace OnlineSore.Domain.Entities;
 
 public class RatingProduct 
 {
     public DateTime CreatedAt { get; private set; } = DateTime.Now;
     
     public Guid IdCostumer { get; private set; }
 
     public Guid IdProduct { get; private set; }
     
     public ScoreEnum? Score { get; private set; }
     
 
     public RatingProduct(){
 
     }
 
     public RatingProduct(Guid idProduct, Guid idCostumer, ScoreEnum? score = ScoreEnum.Regular)
     {
         IdProduct = idProduct;
         IdCostumer =  idCostumer;
         Score = score;
     }
     
     
     public void UpdateScore(ScoreEnum score)
     {
         // Valida se o valor existe dentro do Enum definido
         if (score != ScoreEnum.Regular &&  score != ScoreEnum.VeryGood && score != ScoreEnum.Excellent && score != ScoreEnum.Bad &&  score != ScoreEnum.Good)
             throw new ArgumentOutOfRangeException(nameof(score), "Erro ao avaliar produto");

         Score = score;
     }
 }