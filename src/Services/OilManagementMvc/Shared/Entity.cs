namespace OilManagementMvc.Shared;

public class Entity
{
    public Guid Id { get; set; }

    private DateTime createdAt;

    public DateTime GetCreatedAt()
    {
        return createdAt;
    }

    public void SetCreatedAt()
    {
        createdAt = DateTime.UtcNow;
    }

    private DateTime updatedAt;

    public DateTime GetUpdatedAt()
    {
        return updatedAt;
    }

    public void SetUpdatedAt()
    {
        updatedAt = DateTime.UtcNow;
    }
}
