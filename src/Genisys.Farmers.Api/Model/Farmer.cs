// Farmer model
namespace Genisys.Farmers.Api.Model;
public class Farmer
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string FarmName { get; set; }
    public string Location { get; set; }
    public string Contact { get; set; }
    public List<Product> Products { get; set; } = new List<Product>(); // One-to-many relationship
}