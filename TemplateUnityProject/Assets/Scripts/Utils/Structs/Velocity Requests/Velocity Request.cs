using UnityEngine;

/// <summary>
/// This is a struct for the velocity manager to process velocity requests.
/// 
/// REM-i
/// </summary>
public struct VelocityRequest
{
    public Vector2 vector;
    public VelocityPriority priority;
    public string tag;

    // Constructor
    public VelocityRequest(Vector2 velocity, VelocityPriority priority, string sourceTag)
    {
        this.vector = velocity;
        this.priority = priority;
        this.tag = sourceTag;
    }

    // None for errors
    public static readonly VelocityRequest None = new VelocityRequest
    {
        vector = Vector2.zero,
        priority = VelocityPriority.Error,
        tag = "Error"
    };
}
