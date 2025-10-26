/// <summary>
/// This interface will be for any jump that needs to track when the button is released.
/// 
/// REM-i
/// </summary>
public interface IJumpReleaseListener
{
    // Called when we check if the jump button was released
    void JumpRelease();
}
