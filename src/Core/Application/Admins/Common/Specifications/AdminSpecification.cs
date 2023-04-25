using Application.Common.Specifications;
using Domain.Admins;

namespace Application.Admins.Common.Specifications;

public class AdminSpecification : BaseSpecification<Admin>
{
    public AdminSpecification(string email)
    {
        AddCriteria(a => a.Email.Equals(email));
    }
}