using ServicesAPI.ViewModels.Service;

namespace ServicesAPI.ViewModels.ServiceCategories;

public class ServiceCategoryWithServices
{
    public Guid Id { get; set; }
    public string CategoryName { get; set; } = null!;
    public int TimeSlotSize { get; set; }
    public IEnumerable<ServiceViewModel>? Services { get; set; }
}