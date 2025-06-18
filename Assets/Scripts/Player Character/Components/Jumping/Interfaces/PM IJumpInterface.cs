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
    void Jump();
}
