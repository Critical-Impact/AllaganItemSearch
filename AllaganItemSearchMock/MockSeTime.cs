using AllaganLib.Shared.Time;

namespace AllaganItemSearchMock;

public class MockSeTime : ISeTime
{
    public TimeStamp ServerTime { get; } = TimeStamp.UtcNow;
    public TimeStamp EorzeaTime => this.ServerTime.ConvertToEorzea();
    public long EorzeaTotalMinute { get; }
    public long EorzeaTotalHour { get; }
    public short EorzeaMinuteOfDay { get; }
    public byte EorzeaHourOfDay { get; }
    public byte EorzeaMinuteOfHour { get; }
    public event Action? Updated;
    public event Action? HourChanged;
    public event Action? WeatherChanged;
    public void Dispose()
    {

    }
}