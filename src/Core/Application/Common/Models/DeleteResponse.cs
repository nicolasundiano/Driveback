namespace Application.Common.Models;

public class DeleteResponse
{
    public string Message { get; }

    public DeleteResponse(string id)
    {
        Message = $"Entity deleted. Id: {id}";
    }
}