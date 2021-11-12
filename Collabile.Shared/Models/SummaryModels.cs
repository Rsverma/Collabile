namespace Collabile.Shared.Models
{

    public record AuthenticatedUser(string Username, string Token);
    public record TaskSummary(int Id, string Title);
    public record ReleaseSummary(int Id, string Title);
}