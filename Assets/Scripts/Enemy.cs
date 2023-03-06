
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Movement movement { get; private set; }
    public EnemyHome home { get; private set; }
    public EnemyChase chase { get; private set; }
    public EnemyScatter scatter { get; private set; }
    public EnemyWeak weak { get; private set; }
    
    public EnemyBehavior initialBehavior;
    
    public Transform player;
    
    public int points = 200;

    private void Awake()
    {
        movement = GetComponent<Movement>();
        home = GetComponent<EnemyHome>();
        chase = GetComponent<EnemyChase>();
        scatter = GetComponent<EnemyScatter>();
        weak = GetComponent<EnemyWeak>();

    }

    private void Start()
    {
        ResetState();
    }

    public void ResetState()
    {
        gameObject.SetActive(true);
        movement.ResetState();

        weak.Disable();
        chase.Disable();
        scatter.Enable();

        if(home!= initialBehavior)
        {
            home.Disable();
        }

        if(initialBehavior!= null)
        {
            initialBehavior.Enable();
        }
    }

    public void SetPosition(Vector3 position)
    {
        // Keep the z-position the same since it determines draw depth
        position.z = transform.position.z;
        transform.position = position;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            if (weak.enabled)
            {
                FindObjectOfType<GameManager>().EnemyKilled(this);
            }
            else
            {
                FindObjectOfType<GameManager>().PlayerKilled();
            }
        }
    }

}
