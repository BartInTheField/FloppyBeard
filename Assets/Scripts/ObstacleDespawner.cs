using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleDespawner : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Goal"))
        {
            // Destory the pattern
            // Destroys the obstacle and goal with it
            Destroy(other.transform.parent.gameObject);
        }
    }
}
