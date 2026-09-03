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

    [Tooltip("This is the index for the rig. This is assigned by the rig registry and passed to the input registry.")]
    private int _index;

    #endregion

    #region Methods

    /// <summary>
    /// On start, the rig registers itself to the service locator's rig registry and registers the input router to the input router registry. 
    /// It also sets itself to be persistant across scenes.
    /// </summary>
    public void GenerateRig(int index)
    {
        // Set as persistant across scenes.
        DontDestroyOnLoad(_rig);

        // Set the rig's index.
        _index = index;

        // Register the input router to the input router registry.
        ServiceLocator.Get<PlayerInputRegistry>().Register(_index, _router);
    }

    /// <summary>
    /// On call via the rig registry, removes the rig from the registries and destroys the rig.
    /// </summary>
    public void RemoveRig()
    {
        // Remove the input router from the input registry
        ServiceLocator.Get<PlayerInputRegistry>().Unregister(_index);

        // Destroy this rig.
        Destroy(_rig);
    }

    #endregion
}
