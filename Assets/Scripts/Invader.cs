using UnityEngine;


[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(SpriteRenderer))]

public class Invader : MonoBehaviour
{
    public Sprite[] animationSprites = new Sprite[4];
    
    // Maybe make time between animations random??
    public float animationTime;

    public int invaderType;

    SpriteRenderer spRend;
    int animationFrame;
    public GameObject Blod;
    private void Awake()
    {
        spRend = GetComponent<SpriteRenderer>();
        spRend.sprite = animationSprites[0];

    }

    void Start()
    {
        // Calls AnimateSprite with a certain time interval.
        InvokeRepeating( nameof(AnimateSprite) , animationTime, animationTime);
    }

    // Switches between different sprites to create an animation.
    private void AnimateSprite()
    {
        animationFrame++;
        if(animationFrame >= animationSprites.Length)
        {
            animationFrame = 0;
        }
        spRend.sprite = animationSprites[animationFrame];
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Laser"))
        {
            // Spawns a particle system that causes blood to be released.
            Instantiate(Blod, new Vector3(transform.position.x, transform.position.y, transform.position.z - 2), Quaternion.identity);


            GameManager.Instance.OnInvaderKilled(this);
            
        }
        else if(collision.gameObject.layer == LayerMask.NameToLayer("Boundary")) // Reached the bottom edge.
        {
            GameManager.Instance.OnBoundaryReached();
        }
    }

}
