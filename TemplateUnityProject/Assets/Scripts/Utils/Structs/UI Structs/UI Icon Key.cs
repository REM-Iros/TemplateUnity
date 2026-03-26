using System;

/// <summary>
/// Created a struct key for the UI icon database to use as a key for the dictionary. This is used to store the device and control path for each icon in the database.
/// 
/// REM-i
/// </summary>
public struct UIIconKey
{
    public string ControlScheme;
    public string ControlPath;

    // Constructor for the struct, takes in the device and control path and assigns them to the struct fields.
    public UIIconKey(string controlScheme, string controlPath)
    {
        ControlScheme = controlScheme;
        ControlPath = controlPath;
    }

    public bool Equals(UIIconKey other)
    {
        return ControlScheme == other.ControlScheme && ControlPath == other.ControlPath;
    }

    public override bool Equals(object obj)
    {
        return obj is UIIconKey other && Equals(other);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(ControlScheme, ControlPath);
    }
}
