using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScatter : EnemyBehavior
{
    private void OnDisable()
    {
        enemy.chase.Enable();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Node node = collision.GetComponent<Node>();
        
        if(node != null && enabled && !enemy.weak.enabled)
        {
            
            int index = Random.Range(0, node.available.Count);
            if (node.available.Count > 1 && node.available[index] == -enemy.movement.direction)
            {
                index++;

                if (index >= node.available.Count)
                {
                    index = 0;
                }
            }
            
            enemy.movement.SetDirection(node.available[index]);
        }
    }
}
