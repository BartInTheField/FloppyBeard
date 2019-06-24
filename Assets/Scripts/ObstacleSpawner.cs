using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] obstaclePatterns = null;
    [SerializeField] private float timeBetweenSpawn = 2f;

    private InputManager inputManager;

    private void Awake()
    {
        // InputManager is always in the scene
        inputManager = FindObjectOfType<InputManager>();
    }

    private void Start()
    {
        inputManager.OnPlayerReady += StartSpawning;
    }

    private void StartSpawning()
    {
        StartCoroutine(AwaitSpawn(timeBetweenSpawn));
    }

    private IEnumerator AwaitSpawn(float waitTime)
    {
        int random = Random.Range(0, obstaclePatterns.Length);
        Instantiate(obstaclePatterns[random], transform.position, Quaternion.identity);

        yield return new WaitForSeconds(waitTime);

        StartCoroutine(AwaitSpawn(waitTime));
    }
}
