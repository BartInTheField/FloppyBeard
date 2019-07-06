    
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepeatingBackground : MonoBehaviour
{
    [SerializeField] private float speed = 2f;

    [SerializeField] private float endX = -9f;
    [SerializeField] private float startX = 9f;

    private void FixedUpdate()
    {
        transform.Translate(Vector2.left * speed * Time.deltaTime);

        if (transform.position.x <= endX)
        {
            Vector2 position = new Vector2(startX, transform.position.y);
            transform.position = position;
        }
    }
}