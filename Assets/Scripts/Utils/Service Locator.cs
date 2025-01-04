using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Basic service locator. Subscribe managers that need to be accessed at all
/// times to it without needing to actually directly access the manager.
/// 
/// REM-i
/// </summary>
public static class ServiceLocator
{
    #region Vars

    // Create a dictionary to hold all the services
    private static readonly Dictionary<Type, object> services = new Dictionary<Type, object>();

    #endregion

    #region Methods

    /// <summary>
    /// Registers a service of type T to the dictionary
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="service"></param>
    public static void Register<T>(T service)
    {
        // Check if the service is present, if not, add it
        if (!services.ContainsKey(typeof(T)))
        {
            services[typeof(T)] = service;
        }
        else
        {
            Debug.LogWarning($"Service of type {typeof(T)} is already registered.");
        }
    }

    /// <summary>
    /// Returns a service of type T from the dictionary if present
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static T Get<T>()
    {
        // Check if the service is contained in the dictionary, if so, return it
        if (services.ContainsKey(typeof(T)))
        {
            return (T)services[typeof(T)];
        }

        Debug.LogError($"Service of type {typeof(T)} not found.");
        return default;
    }

    #endregion
}
