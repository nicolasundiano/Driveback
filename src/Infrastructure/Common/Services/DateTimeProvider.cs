using Application.Common.Interfaces.Services;

namespace Infrastructure.Common.Services;

public class DateTimeProvider : IDateTimeProvider
{
    public DateTime UtcNow => DateTime.UtcNow;
}