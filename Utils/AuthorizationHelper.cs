namespace Flare.AccountService.Utils;

public static class AuthorizationHelper
{
    public static bool IsOwner(Guid principalUserId, Guid targetUserId)
    {
        return principalUserId == targetUserId;
    }
}
