namespace Backend.Authorization;

public static class Policies
{
    public const string OnlyAdmins = nameof(OnlyAdmins);
    public const string AtLeastModerators = nameof(AtLeastModerators);
}