namespace Backend.Datas.Enums;

public enum SignInResultType
{
    Success,
    InvalidUserNameOrPassword,
    Pending,
    Unknown,
    Blocked
}

public enum SignUpResultType
{
    Success,
    ExistingUser,
    IncorrectEmail
}

public enum UserUpdateResult
{
    UserNotExists,
    WeakPassword,
    Success
}