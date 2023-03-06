using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChase : EnemyBehavior
{
    private void OnDisable()
    {
        enemy.scatter.Enable(); 
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Node node = collision.GetComponent<Node>();

        if (node != null && enabled && !enemy.weak.enabled)
        {
            Vector2 direction = Vector2.zero;
            float min = float.MaxValue;

            foreach (Vector2 available in node.available)
            {
                Vector3 newPosition = transform.position + new Vector3(available.x, available.y, 0f);
                float distance = (enemy.player.position - newPosition).sqrMagnitude;
                if (distance < min)
                {
                    direction = available;
                    min = distance;
                }
            }

            enemy.movement.SetDirection(direction);
        }
    }
}
