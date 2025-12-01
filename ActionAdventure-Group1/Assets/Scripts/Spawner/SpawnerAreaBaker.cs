/************************************************************
* COPYRIGHT:  2025
* PROJECT: Hollow Castle
* FILE NAME: SpawnerAreaBaker.cs
* DESCRIPTION: Bakes a new navmesh copy when the player gets a certain distance from the old one.
*                   
* REVISION HISTORY:
* Date [YYYY/MM/DD] | Author | Comments
* ------------------------------------------------------------
* 2025/12/01 | Noah Zimmerman | Created class
*
*
************************************************************/

using System;
using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;
using NavMeshBuilder = UnityEngine.AI.NavMeshBuilder;


public class SpawnerAreaBaker : MonoBehaviour
{
    [SerializeField]
    private NavMeshSurface surface;

    [SerializeField] 
    private float updateRate = .1f;
    [SerializeField]
    private float movementThreshold = 3;
    [SerializeField]
    private Vector3 NavMeshSize = new Vector3(7, 1, 7);

    private Vector3 _anchor;
    private NavMeshData _navMeshData;
    private List<NavMeshBuildSource> Sources = new List<NavMeshBuildSource>();

    private void Start()
    {
        _navMeshData = new NavMeshData();
        NavMesh.AddNavMeshData(_navMeshData);
        BuildNavMesh(false);
        StartCoroutine(CheckPlayerMovement());
    }

    /// <summary>
    /// A coroutine to check if the player has moved a certain amount and if they have then rebuild nav mesh.
    /// </summary>

    private IEnumerator CheckPlayerMovement()
    {
        WaitForSeconds wait = new WaitForSeconds(updateRate);
        while (true)
        {
            if (Vector3.Distance(_anchor, transform.position) > movementThreshold)
            {
                BuildNavMesh(true);
                _anchor = transform.position;
            }

            yield return wait;
        }
        
    }//end CheckPlayerMovement()

    /// <summary>
    /// A method to build or rebuild a navmesh.
    /// </summary>
    /// <param name="Async"> Tells the method weather it should build asynchronously or not.
    ///</param>
    private void BuildNavMesh(bool Async)
    {
        Bounds navMeshBounds = new Bounds(transform.position, NavMeshSize);
        List<NavMeshBuildMarkup> markups = new List<NavMeshBuildMarkup>();
        
        List<NavMeshModifier> modifiers;

        if (surface.collectObjects == CollectObjects.Children)
        {
            NavMeshBuilder.CollectSources(surface.transform, surface.layerMask, surface.useGeometry, surface.defaultArea, markups, Sources);
        }
        else
        {
            NavMeshBuilder.CollectSources(navMeshBounds, surface.layerMask, surface.useGeometry, surface.defaultArea, markups, Sources);
        }
        
        if (Async)
        {
            NavMeshBuilder.UpdateNavMeshDataAsync(_navMeshData, surface.GetBuildSettings(), Sources, new Bounds(transform.position, NavMeshSize));
        }
        else
        {
            NavMeshBuilder.UpdateNavMeshData(_navMeshData, surface.GetBuildSettings(), Sources, new Bounds(transform.position, NavMeshSize));
        }
    }//end BuildNavMesh(bool)
    
    /// <summary>
    /// A method to get navmesh data
    /// </summary>
    public NavMeshData GetNavMeshData()
    {
        return _navMeshData;
    }
 
}//end SpawnerAreaBaker
