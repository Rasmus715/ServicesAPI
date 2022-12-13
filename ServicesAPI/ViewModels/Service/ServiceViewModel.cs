namespace ServicesAPI.ViewModels.Service;

public class ServiceViewModel
{
    public Guid Id { get; set; }
    public string ServiceName { get; set; } = null!;
    public decimal Price { get; set; }
    public bool IsActive { get; set; }
}