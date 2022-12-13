namespace ServicesAPI.ViewModels.Service;

public class CreateServiceViewModel
{
    public string ServiceName { get; set; } = null!;
    public Guid CategoryId { get; set; }
    public int Price { get; set; }
}