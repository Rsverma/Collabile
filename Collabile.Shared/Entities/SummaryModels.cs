namespace Collabile.Shared.Entities;

public record UserCred(string Username, string Password, string UserRole);
public record TaskSummary(int Id, string Title);
public record ReleaseSummary(int Id, string Title);
