using UnityEngine;

/// <summary>
/// This is an interface wrapper that will enable serialization of interfaces in Unity.
/// 
/// REM-i
/// </summary>
[System.Serializable]
public class InterfaceWrapper<T> where T : class
{
    // This gives a field that Unity can serialize.
    [SerializeField] 
    private Object _object;

    // This will be how we access the interface
    // Used like this: _interface.Value.MethodName();
    public T Value => _object as T;

    /// <summary>
    /// This provides us with a simple setter in code
    /// </summary>
    /// <param name="obj"></param>
    public void Set(Object obj)
    {
        // We want to ensure the object we are assigning is actually the proper type
        if (obj is T)
        {
            _object = obj;
        }
        else
        {
            Debug.LogError($"Object of type {obj.GetType()} cannot be set to InterfaceWrapper of type {typeof(T)}.");
        }
    }
}
