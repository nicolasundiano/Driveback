using Domain.Common;
using Domain.Common.Validators;

namespace Domain.Users.Entities;

public class ChildUser : Entity<Guid>
{
    public string Property1 { get; private set; }
    public int Property2 { get; private set; }

    private ChildUser(Guid id, string property1, int property2) : base(id)
    {
        Property1 = property1;
        Property2 = property2;

        Validate();
    }

    public static ChildUser Create(string property1, int property2)
    {
        return new ChildUser(Guid.NewGuid(), property1, property2);
    }

    public void Update(string? property1, int? property2)
    {
        if (!string.IsNullOrEmpty(property1))
        {
            Property1 = property1;
        }

        if (property2 is not null)
        {
            Property2 = (int)property2;
        }
        
        Validate();
    }

    private void Validate()
    {
        Throw.ValidateString(Property1, nameof(Property1), 100);
        Throw.ValidateOutOfRange(Property2, nameof(Property2), 1, 500);
    }
    
#pragma warning disable CS8618
    private ChildUser()
    {
    }
#pragma warning restore CS8618
}