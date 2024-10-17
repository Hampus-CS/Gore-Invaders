using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(BoxCollider2D))]
public class Bunker : MonoBehaviour
{
    
    int nrOfHits = 0;
    SpriteRenderer spRend;
    public Sprite[] bunkerLifeSprites = new Sprite[4];

    private void Awake()
    {
        spRend = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
       
        if (other.gameObject.layer == LayerMask.NameToLayer("Missile") || other.gameObject.layer == LayerMask.NameToLayer("Invader"))
        {

            //�ndrar f�rgen beroende p� antal tr�ffar.
            nrOfHits++;
            //Color oldColor = spRend.color;

            //Color newColor = new Color(oldColor.r +(nrOfHits*0.1f), oldColor.g + (nrOfHits * 0.1f), oldColor.b + (nrOfHits * 0.1f));
            
            //spRend.color = newColor;
            if(nrOfHits == 0)
            {
                spRend.sprite = bunkerLifeSprites[0];
            }
            else if (nrOfHits == 1)
            {
                spRend.sprite = bunkerLifeSprites[1];
            }
            else if (nrOfHits == 2)
            {
                spRend.sprite = bunkerLifeSprites[2];
            }
            else if (nrOfHits == 3)
            {
                spRend.sprite = bunkerLifeSprites[3];
            }
            else if (nrOfHits == 4)
            {
                gameObject.SetActive(false);
            }


        }
    }

    public void ResetBunker()
    {
        gameObject.SetActive(true);
    }
}
