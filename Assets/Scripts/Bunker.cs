using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(BoxCollider2D))]
public class Bunker : MonoBehaviour
{
    
    int nrOfHits = 0;
    SpriteRenderer spRend;
    public Sprite[] bunkerLifeSprites = new Sprite[4];
    public GameObject BunkerBlock;
    public GameObject BunkerBreak;

    private void Awake()
    {
        spRend = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
       
        if (other.gameObject.layer == LayerMask.NameToLayer("Missile") || other.gameObject.layer == LayerMask.NameToLayer("Invader"))
        {
            // change of sprite depending on the number of hits on the bunker.
            
            nrOfHits++;

            if(nrOfHits == 0)
            {
                spRend.sprite = bunkerLifeSprites[0];
            }
            else if (nrOfHits == 1)
            {
                Instantiate(BunkerBlock, transform.position, Quaternion.identity);
                spRend.sprite = bunkerLifeSprites[1];
            }
            else if (nrOfHits == 2)
            {
                Instantiate(BunkerBlock, transform.position, Quaternion.identity);
                spRend.sprite = bunkerLifeSprites[2];
            }
            else if (nrOfHits == 3)
            {
                Instantiate(BunkerBlock, transform.position, Quaternion.identity);
                spRend.sprite = bunkerLifeSprites[3];
            }
            else if (nrOfHits == 4)
            {
                Instantiate(BunkerBreak, transform.position, Quaternion.identity);
                gameObject.SetActive(false);
            }


        }
    }

    public void ResetBunker()
    {
        nrOfHits = 0; // Reset the hit counter
        spRend.sprite = bunkerLifeSprites[0]; // Reset to the initial sprite
        gameObject.SetActive(true); // Reactivate the bunker
    }
}
