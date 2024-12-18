using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
public class Missile : Projectile
{
    public GameObject MissileSound;
    private void Awake()
    {
        direction = Vector3.down;
    }
   
    void Update()
    {
        transform.position += speed * Time.deltaTime * direction;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Instantiate(MissileSound, transform.position, Quaternion.identity);
        Destroy(gameObject); //s� fort den krockar med n�got s� ska den f�rsvinna.
        GameObject.Find("Main Camera").GetComponent<screan_shake_code>().shake = 1.2f;
    }
   
}
