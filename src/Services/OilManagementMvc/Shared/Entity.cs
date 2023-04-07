namespace OilManagementMvc.Shared;

public class Entity
{
    public Guid Id { get; set; }

    private DateTime createdAt;

    public DateTime GetCreatedAt()
    {
        return createdAt;
    }

    public void SetCreatedAt(DateTime value)
    {
        createdAt = DateTime.UtcNow;
    }

    public DateTime UpdatedAt { get; set; }
}
