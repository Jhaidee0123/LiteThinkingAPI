namespace LiteThinking.Domain.Entities;

public abstract class BaseEntity
{
    public Guid Id { get; protected set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }
}

