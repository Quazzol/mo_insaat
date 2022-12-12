namespace Backend.Misc;

public class DefaultDateTimeProvider : IDateTimeProvider
{
    public DefaultDateTimeProvider(Func<DateTime> currentTimeGetter)
    {
        TimeDiff = DateTime.Now - currentTimeGetter();
    }

    private TimeSpan TimeDiff { get; }

    public DateTime UtcNow => DateTime.UtcNow - TimeDiff;

    public DateTime Now => DateTime.Now - TimeDiff;

    public DateTime Today => Now.Date;
}