using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject enemySpawnPrefab;
    [SerializeField]
    private GameObject[] powerupSpawnsPrefabs;
    private GameManager _gameManager;

    // Start is called before the first frame update
    void Start()
    {
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        StartCoroutine(SpawnEnemyRoutine());
        StartCoroutine(SpawnPowerupsRoutine());
    }

    public void StarSpawnRoutines()
    {
        StartCoroutine(SpawnEnemyRoutine());
        StartCoroutine(SpawnPowerupsRoutine());
    }

    private IEnumerator SpawnEnemyRoutine()
    {
        while (_gameManager.gameOver == false)
        {
            Instantiate(enemySpawnPrefab, new Vector3(Random.Range(-8.0f, 8.0f), 8.0f, 0), Quaternion.identity);
            yield return new WaitForSeconds(3.0f);
        }
    }

    private IEnumerator SpawnPowerupsRoutine()
    {
        while (_gameManager.gameOver == false)
        {
            int powerupsRandom = Random.Range(0, 3);
            Instantiate(powerupSpawnsPrefabs[powerupsRandom], new Vector3(Random.Range(-8.0f, 8.0f), 8.0f, 0), Quaternion.identity);
            yield return new WaitForSeconds(4.0f);
        }
    }
}
