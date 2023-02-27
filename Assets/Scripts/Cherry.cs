using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cherry : MonoBehaviour
{
    public int points = 20;

    protected virtual void Eat()
    {
        FindObjectOfType<GameManager>().CherryEaten(this);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            Eat();
        }
    }
}
