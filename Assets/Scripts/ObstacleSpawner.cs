using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] obstaclePatterns = null;
    [SerializeField] private float timeBetweenSpawn = 2f;

    private void Start()
    {
        StartCoroutine(StartSpawn(timeBetweenSpawn));
    }

    private IEnumerator StartSpawn(float waitTime)
    {
        int random = Random.Range(0, obstaclePatterns.Length);
        Instantiate(obstaclePatterns[random], transform.position, Quaternion.identity);

        yield return new WaitForSeconds(waitTime);

        StartCoroutine(StartSpawn(waitTime));
    }
}
