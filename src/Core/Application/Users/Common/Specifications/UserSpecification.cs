using Application.Common.Specifications;
using Domain.Users;
using Domain.Users.ValueObjects;

namespace Application.Users.Common.Specifications;

public class UserSpecification : BaseSpecification<User>
{
    public UserSpecification(UserId id)
    {
        AddCriteria(u => u.Id == id);
    }

    public UserSpecification(string email)
    {
        AddCriteria(u => u.Email.Equals(email));
    }
}