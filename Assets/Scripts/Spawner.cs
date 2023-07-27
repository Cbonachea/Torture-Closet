using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject objectToSpawn;
    private Transform spawnLocation;
    private bool roomToSpawn = true;
    internal bool canSpawnObject = true;
    private int spawnDelay = 1;
    private SpriteRenderer spawnerSprite;


    private void Start()
    {
        spawnLocation = GetComponent<Transform>();
        StartCoroutine(SpawnObjectTimer());
        spawnerSprite = GetComponentInChildren<SpriteRenderer>();
        spawnerSprite.enabled = false;
        GameEvents.current.onDie += StopSpawn;
    }

    internal void SpawnObject()
    {   if(roomToSpawn)
        Instantiate(objectToSpawn, spawnLocation);
    }

    private IEnumerator SpawnObjectTimer()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnDelay);
            if(canSpawnObject){SpawnObject();}
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        roomToSpawn = false;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        roomToSpawn = true;
    }

    private void StopSpawn()
    {
        canSpawnObject = false;
    }

    private void OnDestroy()
    {
        GameEvents.current.onDie -= StopSpawn;
    }
}

