using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    //Crows
    [SerializeField]
    private GameObject crow;
    [SerializeField]
    private float crowSpawnTime;
    private float tempCrowSpawnTime;

    //Locusts
    [SerializeField]
    private GameObject locust;
    [SerializeField]
    private float locustSpawnTime;
    private float tempLocustSpawnTime;

    //Misc
    [SerializeField]
    private Transform spawnEast;
    [SerializeField]
    private Transform spawnWest;
    private float time = 0f;
    private readonly float spawnRange = 4.5f;

    private void Start()
    {
        tempCrowSpawnTime = crowSpawnTime;
        tempLocustSpawnTime = locustSpawnTime;
    }

    private void Update()
    {
        time += Time.deltaTime;
        if (crowSpawnTime != 0 && time >= crowSpawnTime)
        {
            SpawnCrow();
            crowSpawnTime += tempCrowSpawnTime;
        }
        if (locustSpawnTime != 0 && time >= locustSpawnTime)
        {
            SpawnLocust();
            locustSpawnTime += tempLocustSpawnTime;
        }
    }

    public void SetSpawnCrowTime(int crowSpawnTime)
    {
        this.crowSpawnTime = crowSpawnTime;
    }

    public void SetSpawnLocustTime(int locustSpawnTime)
    {
        this.locustSpawnTime = locustSpawnTime;
    }

    private void SpawnCrow()
    {
        int random = Random.Range(0, 2);
        Transform crowSpawnPos = (random == 0) ? spawnEast : spawnWest;
        Vector3 spawnPosition = new Vector3
            (
            crowSpawnPos.position.x,
            Random.Range(crowSpawnPos.position.y - spawnRange, crowSpawnPos.position.y + spawnRange),
            crowSpawnPos.position.z
            );

        Instantiate(crow, spawnPosition, crow.transform.rotation);
    }

    private void SpawnLocust()
    {
        int random = Random.Range(0, 2);
        Transform locustSpawnPos = (random == 0) ? spawnEast : spawnWest;
        Vector3 spawnPosition = new Vector3
         (
         locustSpawnPos.position.x,
         Random.Range(locustSpawnPos.position.y - spawnRange, locustSpawnPos.position.y + spawnRange),
         locustSpawnPos.position.z
         );

        Instantiate(locust, spawnPosition, locust.transform.rotation);
    }
}
