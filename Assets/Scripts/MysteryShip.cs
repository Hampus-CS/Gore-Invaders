using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class MysteryShip : MonoBehaviour
{
    float speed = 5f;
    float cycleTime = 5f;
    public int msLives = 3;
    Vector2 leftDestination;
    Vector2 rightDestination;
    int direction = -1;
    bool isVisible;

    
    void Start()
    {
        Vector3 leftEdge = Camera.main.ViewportToWorldPoint(Vector3.zero);
        Vector3 rightEdge = Camera.main.ViewportToWorldPoint(Vector3.right);

        // The position where it will stay off-screen.
        leftDestination = new Vector2(leftEdge.x - 2f, transform.position.y);
        rightDestination = new Vector2(rightEdge.x + 2f, transform.position.y);

        SetInvisible();
    }


    void Update()
    {
        if (!isVisible) // If it's not visible then it can't move.
        {
            return;
        }

        if(direction == 1)
        {
            // Moves right.
            transform.position += speed * Time.deltaTime * Vector3.right;

            if (transform.position.x >= rightDestination.x)
            {
                SetInvisible();
            }
        }
        else
        {
            // Moves left.
            transform.position += speed * Time.deltaTime * Vector3.left;

            if (transform.position.x <= leftDestination.x)
            {
                SetInvisible();
            }
        }
    }


    // Moves it to a place just off scene.
    void SetInvisible()
    {
        isVisible = false;

        if(direction == 1)
        {
            transform.position = rightDestination;
        }
        else
        {
            transform.position = leftDestination;
        }

        Invoke(nameof(SetVisible), cycleTime); // Calls SetVisible after a certain number of seconds.
    }

    void SetVisible()
    {
        direction *= -1; // Change direction.

        isVisible = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Life function for MysteryShip.
        if(collision.gameObject.layer == LayerMask.NameToLayer("Laser"))
        {
            if(msLives == 3)
            {
                msLives--;
            }
            if(msLives == 2)
            {
                msLives--;
            }
            if(msLives == 1)
            {
                SetInvisible();
                GameManager.Instance.OnMysteryShipKilled(this);
            }
            
        }
    }
}
