using UnityEngine;

/// <summary>
/// This router goes between the input controller and the battle controller, activating specific events based on action context.
/// 
/// REM-i
/// </summary>
public class RPGBattleInputRouter : MonoBehaviour
{
    #region Vars

    [Tooltip("This is the player input controller that will be read from.")]
    private PlayerInputController _controller;

    [Tooltip("This is the current actor that is taking their turn in battle.")]
    private RPGActor _currentActor;

    private RPGBattleInputContext _context = RPGBattleInputContext.None;

    #endregion

    #region Methods

    /// <summary>
    /// Binds the player input controller to the router.
    /// </summary>
    /// <param name="controller"></param>
    public void Bind(PlayerInputController controller)
    {
        _controller = controller;

        
    }

    //TODO: Start creating events that other components will subscribe to

    #endregion
}
