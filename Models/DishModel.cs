using System.ComponentModel.DataAnnotations;

namespace ChefsAndDishes.Models;

public class Dish
{
    [Key]
    public int DishId { get; set; }
    [Required]
    public string Name { get; set; }
    [Required]
    [Range(1, 5)]
    public int  Tastiness { get; set; }
    [Required]
    [Range(0, 5000)]
    public int Calories { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; } = DateTime.Now;
    //Llave foranea
    public int ChefId { get; set; }
    //Propiedad de navegacion
    public Chef? Creator { get; set; }
}