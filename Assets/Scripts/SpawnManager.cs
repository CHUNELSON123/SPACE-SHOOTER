using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _enemyPrefab;
    [SerializeField]
    private GameObject[] _powerUps;
    [SerializeField]
    private GameObject _enemyContainer;
    private bool _stopSpawning = false;
     
    IEnumerator SpawnEnemy()
    {
        yield return new WaitForSeconds(3f);
        while (_stopSpawning == false)
        {
            Vector3 postToSpawn = new Vector3(Random.Range(-9.2f, 9.2f), 8.0f, 0);
            GameObject newEnemy = Instantiate(_enemyPrefab, postToSpawn, Quaternion.identity);
            newEnemy.transform.parent = _enemyContainer.transform;
            yield return new WaitForSeconds(1f);
        }
    }

    IEnumerator SpawnPowerups()
    {
      
        while (_stopSpawning == false)
        {
            yield return new WaitForSeconds(15f);
            Vector3 postToSpawn = new Vector3(Random.Range(-9.2f, 9.2f), 8.0f, 0);
            int randomPowerUps = Random.Range(0, 3);
            Instantiate(_powerUps[randomPowerUps], postToSpawn, Quaternion.identity);
            yield return new WaitForSeconds(Random.Range(15.0f, 20.0f));
        }
    }
    public void OnPlayerDeath()
    {
        _stopSpawning = true;
        Time.timeScale = 0f;
    }

    public void StartSpawning()
    {
        StartCoroutine(SpawnEnemy());
        StartCoroutine(SpawnPowerups());
    }
}
