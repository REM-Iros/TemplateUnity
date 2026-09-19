using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This is a basic placeholder script for starting an RPG Battle. This should eventually be replaced with either a state machine or a more robust startup script, but for now it's good for testing.
/// 
/// REM-i
/// TODO: Find a long term solution for this eventually.
/// </summary>
public class RPGBattleStartup : MonoBehaviour
{
    public List<RPGActorStats> playerActorStats;
    public List<RPGActorStats> enemyActorStats;

    public Transform playerActorLocation;
    public Transform enemyActorLocation;

    public GameObject actorPrefab;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        foreach (RPGActorStats actorStats in playerActorStats)
        {
            GameObject actor = Instantiate(actorPrefab, playerActorLocation);
            RPGActor rpgActor = actor.GetComponent<RPGActor>();
            rpgActor.InitializeActor(actorStats);
        }

        foreach (RPGActorStats actorStats in enemyActorStats)
        {
            GameObject actor = Instantiate(actorPrefab, enemyActorLocation);
            RPGActor rpgActor = actor.GetComponent<RPGActor>();
            rpgActor.InitializeActor(actorStats);
        }
    }
}
