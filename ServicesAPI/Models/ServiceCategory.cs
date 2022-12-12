namespace ServicesAPI.Models;

public class ServiceCategory
{
    public Guid Id { get; set; }
    public string CategoryName { get; set; } = null!;
    public int TimeSlotSize { get; set; }
}