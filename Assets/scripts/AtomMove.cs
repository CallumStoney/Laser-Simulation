using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtomMove : MonoBehaviour
{
    private Vector3 origin;
    private Vector3 currentDirection;
    private float speed = 1.5f;          // How fast it moves
    private float range = 2.5f;          // Max ± distance from origin
    private float changeDirectionTime = 1.5f; // How often to change direction
    private float directionTimer = 0f;

    void Start()
    {
        origin = transform.position;
        PickNewDirection();
    }

    void Update()
    {
        directionTimer += Time.deltaTime;

        // Change direction every few seconds
        if (directionTimer >= changeDirectionTime)
        {
            PickNewDirection();
            directionTimer = 0f;
        }

        // Move in current direction
        Vector3 movement = currentDirection * speed * Time.deltaTime;
        Vector3 newPosition = transform.position + movement;

        // Clamp to within cube
        Vector3 offset = newPosition - origin;
        offset = Vector3.Max(Vector3.Min(offset, Vector3.one * range), Vector3.one * -range);
        transform.position = origin + offset;
    }

    private void PickNewDirection()
    {
        currentDirection = new Vector3(
            Random.Range(-1f, 1f),
            Random.Range(-1f, 1f),
            Random.Range(-1f, 1f)
        ).normalized;
    }
}