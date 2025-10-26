using UnityEngine;
using UnityEngine.InputSystem;

public class TestingScript : MonoBehaviour
{
    [SerializeField]
    private InputActionReference _actionRef;

    // Update is called once per frame
    void Update()
    {
        if (_actionRef.action.IsPressed())
        {
            Debug.Log(_actionRef.action.GetBindingDisplayString());
        }
    }
}
