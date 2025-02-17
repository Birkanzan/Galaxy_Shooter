using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject EnemyPrefab;
    [SerializeField]
    private GameObject EnemyContainer;
    [SerializeField]
    private GameObject[] powerups;

    private bool StopSpawninig = false;
    // Start is called before the first frame update
    
    public void StartSpawning()
    {
        StartCoroutine(SpawnEnemyRoutine());
        StartCoroutine(SpawnPowerupRoutine());
    }

    // Update is called once per frame
    void Update()
    {

    }
    
    IEnumerator SpawnEnemyRoutine()
    {
        yield return new WaitForSeconds(3.0f);
        while (StopSpawninig == false)
        {
            Vector3 PosToSpawn = new Vector3(Random.Range(-8f, 8f), 7, 0);
            GameObject NewEnemy = Instantiate(EnemyPrefab, PosToSpawn, Quaternion.identity);
            NewEnemy.transform.parent = EnemyContainer.transform;
            yield return new WaitForSeconds(3.0f);
        }
    }
    IEnumerator SpawnPowerupRoutine()
    {
        yield return new WaitForSeconds(3.0f);
        while (StopSpawninig == false)
        {
            Vector3 PosToSpawn = new Vector3(Random.Range(-8f, 8f), 7, 0);
            int randomPowerup = Random.Range(0, 3);
            GameObject NewEnemy = Instantiate(powerups[randomPowerup], PosToSpawn, Quaternion.identity);
            yield return new WaitForSeconds(Random.Range(3,5));
        }
    }

    public void OnPlayerDeath() 
    {
        StopSpawninig = true;
    }

}
