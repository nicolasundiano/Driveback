using Domain.Common;
using Domain.Common.Validators;

namespace Domain.Users.Entities;

public class ChildUser : Entity<Guid>
{
    public string Property1 { get; private set; }
    public int Property2 { get; private set; }

    private ChildUser(Guid id, string property1, int property2)
    {
        Property1 = property1;
        Property2 = property2;

        Validate();
    }

    public static ChildUser Create(string property1, int property2)
    {
        return new ChildUser(Guid.NewGuid(), property1, property2);
    }

    private void Validate()
    {
        ValidationHelper.ValidateString(Property1, nameof(Property1), 100);
        ValidationHelper.ValidateOutOfRange(Property2, nameof(Property2), 1, 500);
    }
}