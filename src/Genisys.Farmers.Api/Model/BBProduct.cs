namespace Genisys.Farmers.Api.Model;

public record BBProduct
{
    public required string Title { get; init; }
    public required string Body { get; set; }
    public required Rating Rating { get; init; }
    public required string Quantity { get; init; }
    public decimal Price { get; set; }
    public required string Img { get; init; }
    public required string Category { get; init; }
}


public record Rating
{
    public decimal Rate { get; init; }
    public int Count { get; init; }
}
