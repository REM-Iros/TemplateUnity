using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// This script is called at the start of the game, it initializes all of the
/// managers we are going to need in the game. If you need to add more managers,
/// add them in the init managers script (only works for singletons).
/// 
/// REM-i
/// </summary>
public class Bootstrap : MonoBehaviour
{
    [SerializeField] private string nextScene;

    /// <summary>
    /// On awake, set to not destroy on load, and load async while
    /// instantiating managers.
    /// </summary>
    private void Awake()
    {
        //Set this object to not destroy
        DontDestroyOnLoad(gameObject);

        //Initialize the managers
        InitManagers();

        //Transition to the next scene
        SceneManager.LoadSceneAsync(nextScene);
    }

    /// <summary>
    /// Make calls to the managers, so that they are loaded properly
    /// </summary>
    private void InitManagers()
    {
        /* NEXT: Start here, need to figure out way to call our singletons
        if (AudioManager.Instance != null)
        {
            
        }
        */
    }
}
