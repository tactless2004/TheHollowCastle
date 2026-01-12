/************************************************************
* COPYRIGHT:  2026
* PROJECT: TheHollowCastle
* FILE NAME: EnemySpawner.cs
* DESCRIPTION: Samples random positions on the NavMesh and Spawns enemies there.
*                   
* REVISION HISTORY:
* Date [YYYY/MM/DD] | Author | Comments
* ------------------------------------------------------------
* 2026/01/11 | Leyton McKinney | Init
*
************************************************************/

using System.Collections.Generic; // For List
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;


public class EnemySpawner : MonoBehaviour
{
    // Nested, because we don't need it anywhere else
    [System.Serializable]
    public class EnemySpawnOption
    {
        public GameObject prefab;

        [Range(0f, 1f)]
        public float probability;
    }

    [Header("Enemies")]
    [SerializeField]
    private List<EnemySpawnOption> enemies;

    // Used to ensure probabilities always sum up to 1.0
    private void OnValidate()
    {
        float sum = 0f;
        foreach (EnemySpawnOption e in enemies)
            sum += e.probability;

        // if sum is leq 0 (somehow), we don't want to divide anything by it DIV BY ZERO BAD >:(
        if (sum <= 0f) return;

        // By normalizing each probability to the sum, we ensure that
        // the sum is always 1.0 (within the constraints of floating point precision)
        for (int i = 0; i < enemies.Count; i++)
            enemies[i].probability /= sum;
    }

    [Header("Spawning")]
    [SerializeField]
    private GameObject player;

    [SerializeField, Tooltip("Max distance away from the player that enemies will spawn")]
    private float playerSpawnRadius;

    [SerializeField, Tooltip("Delay between attempted spawns")]
    private float spawnDelay;

    [SerializeField, Tooltip("Maximum Number of Total Spawns")]
    private int maximumSpawns;

    private float spawnTimer; // Internal Spawn Timer, shouldn't be modified externally
    private int spawns; // Internal Spawn counter, shouldn't be modified externally
    private HashSet<GameObject> enemyInstances;

    private void Start()
    {
        ReacquirePlayer();
        spawnTimer = spawnDelay;
        enemyInstances = new HashSet<GameObject>();
    }

    private void Update()
    {
        // Make a list of enemies that have been spawned and subsequently destroyed
        var enemiesToRemove = enemyInstances.Where(enemy => enemy.IsDestroyed());

        // Remove destroyed enemies from the instances HashSet
        // Programming Tip: Don't remove or add elements to an iterable while iterating over it
        // unless that data structure specifically allows for it.
        // This frequently is either an illegal or undefined operation.
        foreach (var enemy in enemiesToRemove)
        {
            enemyInstances.Remove(enemy);
            spawns -= 1;
        }

        // Wait time between spawns
        if (spawnTimer > 0f)
        {
            spawnTimer -= Time.deltaTime;
            return;
        }
        
        // Ensure that number of spawns doesn't exceed maximum spawns
        if (spawns >= maximumSpawns) return;

        GameObject Spawn = RollEnemy();
        Vector3 pos = GetNavMeshPosition();

        if (Spawn == null)
        {
            Debug.LogWarning("Spawner could not roll a suitable enemy");
            return;
        }

        if (pos == Vector3.zero)
        {
            Debug.LogWarning("Spawner could not find a suitable position to place spawned enemy");
            return;
        }

        GameObject spawnedInstance = Instantiate(
            Spawn,
            pos,
            Quaternion.identity
        );
        enemyInstances.Add(spawnedInstance);
        spawnTimer = spawnDelay;
        spawns += 1;

    }
    private GameObject RollEnemy()
    {
        float roll = Random.value;
        float cumulative = 0f;

        foreach (var enemy in enemies)
        {
            cumulative += enemy.probability;

            if (roll <= cumulative)
                return enemy.prefab;
        }

        return null;
    }

    private void ReacquirePlayer()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private Vector3 GetNavMeshPosition()
    {
        for (int i = 0; i < 5; i++)
        {
            Vector3 randomPoint = player.transform.position + Random.insideUnitSphere * playerSpawnRadius;

            if (NavMesh.SamplePosition(randomPoint, out NavMeshHit hit, playerSpawnRadius, NavMesh.AllAreas))
                return hit.position;
        }

        return Vector3.zero;
    }

}
