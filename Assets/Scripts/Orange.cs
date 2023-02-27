
using UnityEngine;

public class Orange : Cherry
{
    public float duration = 8f;

    protected override void Eat()
    {
        FindObjectOfType<GameManager>().OrangeEaten(this);
    }
}
