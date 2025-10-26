/// <summary>
/// Timed Velocity Request class works to provide the managers with a request
/// that will apply for a duration of time, and then go away.
/// 
/// REM-i
/// </summary>
public class TimedVelocityRequest
{
    public VelocityRequest request;
    public float duration;

    // Constructor
    public TimedVelocityRequest(VelocityRequest request, float duration)
    {
        this.request = request;
        this.duration = duration;
    }
}
