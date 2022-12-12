namespace ServicesAPI.Models;

public class Service
{
    public Guid Id { get; set; }
    public Guid? CategoryId { get; set; }
    public string ServiceName { get; set; } = null!;
    public decimal Price { get; set; }
    public Guid? SpecializationId { get; set; }
    public bool IsActive { get; set; }
}