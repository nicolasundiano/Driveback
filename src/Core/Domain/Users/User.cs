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

    private void Validate()
    {
        ValidationHelper.ValidateEmail(Email, nameof(Email));
        ValidationHelper.ValidateString(FirstName, nameof(FirstName), 100);
        ValidationHelper.ValidateString(LastName, nameof(LastName), 100);
        ValidationHelper.ValidateString(Phone, nameof(Phone), 100);
    }

#pragma warning disable CS8618
    private User()
    {
    }
#pragma warning restore CS8618
}