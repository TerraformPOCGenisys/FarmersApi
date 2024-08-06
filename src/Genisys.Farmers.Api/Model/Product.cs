namespace Genisys.Farmers.Api.Model;

public class Product
{
    public int Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public string Description { get; set; }
    public int FarmerId { get; set; } // Foreign key
    public Farmer Farmer { get; set; } // Navigation property
}
