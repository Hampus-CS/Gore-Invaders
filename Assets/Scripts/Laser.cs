using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
public class Laser : Projectile
{
    public GameObject LaserhitSound;
    private void Awake()
    {
        direction = Vector3.up;
    }

    void Update()
    {
        transform.position += speed * Time.deltaTime * direction;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        CheckCollision(collision);
    }

    void CheckCollision(Collider2D collision)
    {
        Bunker bunker = collision.gameObject.GetComponent<Bunker>();

        if(bunker == null) // If it's not a bunker we hit, the shot should disappear.
        {
            // Spawns an empty object that makes the laser sound.
            Instantiate(LaserhitSound, transform.position, Quaternion.identity);

            // Sends a signal that the camera is shaking
            GameObject.Find("Main Camera").GetComponent<screan_shake_code>().shake = 2f;


            Destroy(gameObject);
        }
    }
}
