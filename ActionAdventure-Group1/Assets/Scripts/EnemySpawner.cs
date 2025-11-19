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
*
************************************************************/

using System.Collections;
using UnityEngine;
using System;
using System.Collections.Generic;
using Unity.VisualScripting;
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
    
    // Start is called once before the first Update
    private void Start()
    {
        _directions.Add(transform.Find("NorthSpawn").gameObject);
        _directions.Add(transform.Find("SouthSpawn").gameObject);
        _directions.Add(transform.Find("EastSpawn").gameObject);
        _directions.Add(transform.Find("WestSpawn").gameObject);

        if (!GameObject.FindGameObjectWithTag("GameManager").TryGetComponent(out gameManager))
        {
            Debug.LogError($"{name} could not find GameManager component.");
        }
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
            if (gameManager.CurrentState != GameState.GamePaused)
                DirectionalSpawner();
        }
    }//end WaveSpawner()
    
    /// <summary>
    /// The purpose of this is to make sure each direction gets the same amount of enemies.
    /// </summary>
    private void DirectionalSpawner()
    {
        for (int i = 0; i < SpawnAmount / 4; i++)
        {
            Spawner(_directions[0]);
            Spawner(_directions[1]);
            Spawner(_directions[2]);
            Spawner(_directions[3]);
        }
         
    }//end WaveSpawner()

    /// <summary>
    /// The purpose of this is to spawn an enemy at a random spot within the provided game object.
    /// </summary>
    /// <param name="Spawn"> The gameobject that the range of spawning is acquired from.
    ///</param>
    private void Spawner(GameObject Spawn)
    {
        Collider spawnCollider = Spawn.GetComponent<Collider>();
        Vector3 randomPosition = new Vector3(
            Random.Range(spawnCollider.bounds.min.x, spawnCollider.bounds.max.x),
            2,
            Random.Range(spawnCollider.bounds.min.z, spawnCollider.bounds.max.z)
        );
        int monsterIndex =  Random.Range(0, enemies.Count);
        Instantiate(enemies[monsterIndex], randomPosition, Quaternion.identity);
    }//end Spawner()
 
 
}//end EnemySpawner
