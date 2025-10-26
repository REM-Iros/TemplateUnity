/// <summary>
/// This interface will be used for any unique jump behavior we want to use.
/// 
/// REM-i
/// </summary>
public interface IJumpInterface
{
    // This is how we determine if we meet the conditions to jump
    bool CanJump();

    // This is the actual jump that gets implemented
    VelocityRequest Jump();

    // We want to check for forces to apply afterwards
    bool IsPersistantForce();

    // If there is a persistant force, we need to continue applying it
    TimedVelocityRequest PersistantJump();

    // If we want a jump to override other movement for a time, we need this
    bool HasOverride();

    // Override duration
    float GetOverrideDuration();
}
