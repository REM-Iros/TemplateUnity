using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Basic input manager created as a singleton that persists through scenes.
/// This may not be the best approach currently, but considering I'm not at the
/// point where I want to introduce multiplayer, it should be fine, but changes will
/// be made if issues arise.
/// 
/// REM-i
/// </summary>
public class InputManager : EagerSingleton<InputManager>
{
    // 
    private PlayerInput playerInput;
}
