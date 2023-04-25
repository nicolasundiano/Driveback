using Domain.Common;
using Domain.Common.Validators;

namespace Domain.Admins;

public class Admin : Entity<Guid>, IUser, IAggregateRoot
{
    public string Email { get; private set; }

    private Admin(Guid id, string email) : base(id)
    {
        Email = email;
        
        Validate();
    }

    public static Admin Create(string email)
    {
        return new Admin(Guid.NewGuid(), email);
    }

    private void Validate()
    {
        ValidationHelper.ValidateEmail(Email);
    }
}