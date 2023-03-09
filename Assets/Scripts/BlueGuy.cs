using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueGuy : MonoBehaviour
{
    public Movement movement { get; private set; }
    private Animator anim;
    private bool animated = false;
    Vector3 flip;
    //private Rigidbody2D rgb2d;

    private void Awake()
    {
        movement = GetComponent<Movement>();
        anim = GetComponent<Animator>();
        flip = transform.localScale;    
    }

    private void Update()
    {
        // Set the new direction based on the current input
        if (Input.GetAxisRaw("Vertical") >0)
        {
            animated = true;
            movement.SetDirection(Vector2.up);
            anim.SetBool("isIdle", !animated);
            anim.SetBool("runVertical", animated);
            anim.SetBool("runHorizontal", !animated);
        }
        else if (Input.GetAxisRaw("Vertical") < 0)
        {
            animated = true;
            movement.SetDirection(Vector2.down);
            anim.SetBool("isIdle",!animated);
            anim.SetBool("runVertical",animated);
            anim.SetBool("runHorizontal", !animated);
        }
        else if (Input.GetAxisRaw("Horizontal") < 0)
        {
            animated = true;
            movement.SetDirection(Vector2.left);
            anim.SetBool("isIdle", !animated);
            anim.SetBool("runHorizontal", animated);
            anim.SetBool("runVertical", !animated);
            flip.x = -1;
            transform.localScale = flip;
        }
        else if (Input.GetAxisRaw("Horizontal") > 0)
        {
            animated = true;
            movement.SetDirection(Vector2.right);
            anim.SetBool("isIdle", !animated);
            anim.SetBool("runHorizontal", animated);
            anim.SetBool("runVertical", !animated);
            flip.x = 1;
            transform.localScale = flip;

        }
        else if(movement.Occupied(movement.direction))
        {
            animated = false;
            anim.SetBool("isIdle", !animated);
            anim.SetBool("runHorizontal", animated);
            anim.SetBool("runVertical", animated);
        }
    }

    private void Start()
    {
        ResetState();
    }
    public void ResetState()
    {
        movement.ResetState(); 
        gameObject.SetActive(true);
    }
}
