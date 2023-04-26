using Application.Common.Specifications;
using Domain.Users;

namespace Application.Users.Common.Specifications;

public class UserSpecification : BaseSpecification<User>
{
    public UserSpecification(Guid id, bool includeChildUsers = false)
    {
        AddCriteria(u => u.Id == id);

        if (includeChildUsers)
        {
            AddInclude(u => u.ChildUsers);
        }
    }

    public UserSpecification(string email)
    {
        AddCriteria(u => u.Email.Equals(email));
    }
}