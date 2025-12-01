/************************************************************
* COPYRIGHT:  2025
* PROJECT: AdventureGame
* FILE NAME: EnemySpawner.cs
* DESCRIPTION: Control script for the enemy spawner.
*                   
* REVISION HISTORY:
* Date [YYYY/MM/DD] | Author | Comments
* ------------------------------------------------------------
* 2000/01/01 | Your Name | Created class
* 2025/11/07 | Noah Zimmerman | Created class
* 2025/11/17 | Leyton McKinney | Do not instantiate new enemies when game state is GameState.GamePaused
* 2025/12/01 | Noah Zimmerman | Changed the spawner to spawn using navmesh
 * 
************************************************************/

using System.Collections;
using UnityEngine;
using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine.AI;
using Random = UnityEngine.Random;


public class EnemySpawner : MonoBehaviour
{
    [SerializeField] [Tooltip("Time between spawns")]
    private int SpawnTime = 30;
    
    [SerializeField] [Tooltip("Amount of spawns, need to be multiple of 4")]
    private int SpawnAmount = 8;
    
    public List<GameObject> enemies = new List<GameObject>();
 
    private List<GameObject> _directions = new List<GameObject>();
    
    [SerializeField] [Tooltip("Player object")]
    private GameObject player;

    private GameManager gameManager;
    
    [SerializeField] [Tooltip("Radius of Spawns")]
    private int radius;
    
    [Tooltip("Method of choosing what enemies will spawn")]
    public SpawnMethod spawnMethod;

    private int enemieType = 0;
    public enum SpawnMethod
    {
        RoundRobin,Random
    }
    
    // Start is called once before the first Update
    private void Start()
    {
       
       /* if (!GameObject.FindGameObjectWithTag("GameManager").TryGetComponent(out gameManager))
        {
            Debug.LogError($"{name} could not find GameManager component.");
        }*/
        StartCoroutine(WaveSpawner());
    } //end Start()

    private void LateUpdate()
    {
        if (!player.IsDestroyed())
        {
            transform.position = player.transform.position;
        }
    }

    /// <summary>
    /// The purpose of this is to wait a certain amount of time before spawning a wave.
    /// </summary>
    IEnumerator WaveSpawner()
    {
        while (true)
        {
            yield return new WaitForSeconds(SpawnTime);
           /* if (gameManager.CurrentState != GameState.GamePaused)*/
           for (int i = 0; i < SpawnAmount; i++)
           {
               if (spawnMethod == SpawnMethod.RoundRobin)
                   SpawnRoundRobin();
               else SpawnRandom();
           }

           if (enemieType < enemies.Count-1)
           {
               enemieType++;
           }
           else
           {
               enemieType = 0;
           }
        }
    }//end WaveSpawner()
    
    
    /// <summary>
    /// The purpose of this is to spawn an enemy at a random spot within the provided nav mesh, with the enemy types being set each time.
    /// </summary>
    private void SpawnRoundRobin()
    {
        NavMeshTriangulation triangulation = NavMesh.CalculateTriangulation();
        int VertexIndex = Random.Range(0, triangulation.vertices.Length);
        NavMeshHit hit;
        if (NavMesh.SamplePosition(triangulation.vertices[VertexIndex], out hit, radius, -1))
        {
            Instantiate(enemies[enemieType], hit.position, Quaternion.identity);
        }
    }//end SpawnRoundRobin()
 
    /// <summary>
    /// The purpose of this is to spawn an enemy at a random spot within the provided game object.
    /// </summary>
    private void SpawnRandom()
    {
        NavMeshTriangulation triangulation = NavMesh.CalculateTriangulation();
        int VertexIndex = Random.Range(0, triangulation.vertices.Length);
        NavMeshHit hit;
        if (NavMesh.SamplePosition(triangulation.vertices[VertexIndex], out hit, radius, -1))
        {
            Instantiate(enemies[Random.Range(0,enemies.Count)], hit.position, Quaternion.identity);
        }
    }//end SpawnRandom()
 
}//end EnemySpawner
