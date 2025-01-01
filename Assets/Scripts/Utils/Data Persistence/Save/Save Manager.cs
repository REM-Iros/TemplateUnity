using System.Runtime.CompilerServices;
using UnityEngine;

/// <summary>
/// Save manager singleton will directly handle saving and loading data, pulling from files and
/// storing the data in the data manager. The save manager is not to be interfaced with by
/// other scripts and simply handles the saving and loading of data, the data manager will
/// be the one that every script will reference when they need save data. 
/// 
/// There will also be saving and loading through encryption, which is really not necessary, but it sounded like
/// a fun challenge to learn on the side so here we are.
/// 
/// Basic idea of save load system from here: https://www.youtube.com/watch?v=aUi9aijvpgs
/// 
/// REM-i
/// </summary>
public class SaveManager : EagerSingleton<SaveManager>
{
    // Calls base method, which sets it as a save manager, then checks for Save Data and loads it up.
    protected override void Awake()
    {
        base.Awake();


    }
}
