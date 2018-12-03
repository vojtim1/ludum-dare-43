using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISpawner
{
    void SpawnAnimals(int dayNumer);
}

public class Spawner : MonoBehaviour, ISpawner {

    [SerializeField]
    private float radius = 5;

    [SerializeField]
    private GameObject animalToSpawn;

    [SerializeField]
    private float initAnimalSpawnCount;

    [SerializeField]
    private float numberOfAnimalsToAddEveryDay;

    private List<GameObject> spawnedAnimals;

    bool isRegistered = false;

    private void Awake()
    {
        spawnedAnimals = new List<GameObject>();
        GameController.instance.RegisterAnimalSpawner(this);
        SpawnAnimals(0);
    }

    public void SpawnAnimals(int dayNumber)
    {
        Debug.Log(dayNumber);

        foreach (var animal in spawnedAnimals)
        {
            Destroy(animal.gameObject);
        }

        spawnedAnimals = new List<GameObject>();

        int numberOfAnimalsToSpawn = (int)Mathf.Round(initAnimalSpawnCount + numberOfAnimalsToAddEveryDay * dayNumber) - spawnedAnimals.Count;

        for (int i = 0; i < numberOfAnimalsToSpawn; i++)
        {
            Vector2 spawnPosition = new Vector2(Random.Range(transform.position.x, transform.position.x + radius * 2) - radius, transform.position.y);
            spawnedAnimals.Add(Instantiate(animalToSpawn, spawnPosition, Quaternion.identity));
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + 1, gameObject.transform.position.z), new Vector3(2 * radius, 2));
    }
}
