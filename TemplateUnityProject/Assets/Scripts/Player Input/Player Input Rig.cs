using UnityEngine;

/// <summary>
/// This is the player input rig. It immediately sets itself to be persistant, and is stored in the input rig registry. 
/// A rig is assigned to each player, and a rig has a reference to the input router. The rig is a consumer of the input
/// router, tracking input state for if the player controller disconnects or connects a new one.
/// 
/// REM-i
/// </summary>
public class PlayerInputRig : MonoBehaviour
{
    #region Vars

    [Tooltip("This is the gameobject for the overall rig.")]
    [SerializeField, Header("Game Rig Object")]
    private GameObject _rig;

    [Tooltip("This is the input router reference.")]
    [SerializeField, Header("Input Router")]
    private PlayerInputRouter _router;

    #endregion

    #region Methods

    #endregion
}
