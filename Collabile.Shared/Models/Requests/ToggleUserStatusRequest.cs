namespace Collabile.Shared.Models.Requests
{
    public class ToggleUserStatusRequest
    {
        public bool ActivateUser { get; set; }
        public string UserId { get; set; }
    }
}
