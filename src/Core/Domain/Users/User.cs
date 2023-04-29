using Domain.Common;
using Domain.Common.Validators;
using Domain.Users.Entities;

namespace Domain.Users;

public class User : Entity<Guid>, IUser, IAggregateRoot
{
    private readonly List<ChildUser> _childUsers = new();
    public string Email { get; private set; }
    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public string Phone { get; private set; }
    public IReadOnlyList<ChildUser> ChildUsers => _childUsers.AsReadOnly();

    private User(Guid id, string email, string firstName, string lastName, string phone)
        : base(id)
    {
        Email = email;
        FirstName = firstName;
        LastName = lastName;
        Phone = phone;
        
        Validate();
    }

    public static User Create(string email, string firstName, string lastName, string phone)
    {
        return new User(Guid.NewGuid(), email, firstName, lastName, phone);
    }
    
    public void UpdateDetails(string? firstName, string? lastName, string? phone)
    {
        if (!string.IsNullOrEmpty(firstName))
        {
            FirstName = firstName;
        }
        if (!string.IsNullOrEmpty(lastName))
        {
            LastName = lastName;
        }
        if (!string.IsNullOrEmpty(phone))
        {
            Phone = phone;
        }
        
        Validate();
    }

    public void AddChildUser(ChildUser childUser)
    {
        _childUsers.Add(childUser);
        
        Validate();
    }

    public ChildUser? GetChildUser(Guid childUserId)
    {
        return ChildUsers.SingleOrDefault(cu => cu.Id == childUserId);
    }

    public void UpdateChildUser(Guid childUserId, string? property1, int? property2)
    {
        var childUser = GetChildUser(childUserId);

        childUser?.Update(property1, property2);
        
        Validate();
    }

    public bool ChildUserProperty2Exists(Guid childUserId, int property2)
    {
        return ChildUsers.Any(cu => cu.Property2 == property2 && cu.Id != childUserId);
    }

    private void Validate()
    {
        Throw.ValidateEmail(Email, nameof(Email));
        Throw.ValidateString(FirstName, nameof(FirstName), 100);
        Throw.ValidateString(LastName, nameof(LastName), 100);
        Throw.ValidateString(Phone, nameof(Phone), 100);
        Throw.ValidateUniqueListProperty(
            ChildUsers,
            nameof(ChildUsers),
            cu => cu.Property2,
            nameof(ChildUser.Property2));
    }

#pragma warning disable CS8618
    private User()
    {
    }
#pragma warning restore CS8618
}