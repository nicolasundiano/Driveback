using Application.Common.Specifications;
using Domain.Users;

namespace Application.Users.Common.Specifications;

public class UsersSpecification : PaginationSpecification<User>
{
    public UsersSpecification(
        string? search,
        string? sort,
        int? pageIndex,
        int? pageSize) : base(pageIndex, pageSize)
    {
        if (!string.IsNullOrEmpty(search))
        {
            AddCriteria(u => u.Email.Contains(search) || 
                             u.FirstName.Contains(search) ||
                             u.LastName.Contains(search));
        }
		
        switch (sort)
        {
            case "emailAsc":
                AddOrderBy(c => c.Email);
                break;
            case "emailDesc":
                AddOrderByDescending(c => c.Email);
                break;
            
            case "firstNameAsc":
                AddOrderBy(c => c.FirstName);
                break;
            case "firstNameDesc":
                AddOrderByDescending(c => c.FirstName);
                break;
            
            case "lastNameAsc":
                AddOrderBy(c => c.LastName);
                break;
            case "lastNameDesc":
                AddOrderByDescending(c => c.LastName);
                break;

            default:
                AddOrderBy(c => c.Email);
                break;
        }
    }
}