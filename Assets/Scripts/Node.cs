using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    public List<Vector2> available { get; private set; }
    public LayerMask obstacleLayer;
    private void Start()
    {
        available = new List<Vector2>();
        checkDirection(Vector2.up);
        checkDirection(Vector2.down);
        checkDirection(Vector2.right);
        checkDirection(Vector2.left);
    }

    private void checkDirection(Vector2 direction)
    {
        RaycastHit2D hit = Physics2D.BoxCast(transform.position, Vector2.one * 0.5f, 0f, direction, 0.5f, obstacleLayer);
        if (hit.collider == null)
        {
            available.Add(direction);
        }
    }
}
