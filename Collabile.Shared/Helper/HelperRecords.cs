using Collabile.Shared.Enums;

namespace Collabile.Shared.Helper
{
    public record UserSummary(string Id, string Name, string Email,string UserRole);

    public record ProjectSummary(string Key, string Name, string Owner);

    public record ReleaseSummary(int Id, int Index, string Name);

    public record ItemSummary(string Id, string Title, ItemType ItemType);
}
