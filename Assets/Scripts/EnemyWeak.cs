using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWeak : EnemyBehavior
{
    public bool eaten { get; private set; }

    private void Eaten()
    {
        eaten = true;
        enemy.SetPosition(enemy.home.inside.position);
        enemy.home.Enable(duration);
    }

    public void OnEnable()
    {
        
        enemy.movement.speedMultiplier = 0.5f;
        eaten = false;
    }

    private void OnDisable()
    {
        enemy.movement.speedMultiplier = 1f;
        eaten = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Node node = other.GetComponent<Node>();

        if (node != null && enabled)
        {
            //enemy.Animated();
            Vector2 direction = Vector2.zero;
            float maxDistance = float.MinValue;

            // Find the available direction that moves farthest from pacman
            foreach (Vector2 availableDirection in node.available)
            {
                Vector3 newPosition = transform.position + new Vector3(availableDirection.x, availableDirection.y);
                float distance = (enemy.player.position - newPosition).sqrMagnitude;

                if (distance > maxDistance)
                {
                    direction = availableDirection;
                    maxDistance = distance;
                }
            }

            enemy.movement.SetDirection(direction);
            if (direction == Vector2.right)
            {
                enemy.anim.SetBool("isMove", true);
                enemy.flip.x = -1;
                transform.localScale = enemy.flip;
            }
            else if (direction == Vector2.left)
            {
                enemy.anim.SetBool("isMove", true);
                enemy.flip.x = 1;
                transform.localScale = enemy.flip;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            if (enabled)
            {
                Eaten();
            }
        }
    }
}
