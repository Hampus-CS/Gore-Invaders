using UnityEngine;

public class Invaders : MonoBehaviour
{
    public Invader[] prefab = new Invader[5];

    private int row = 5;
    private int col = 11;

    private Vector3 initialPosition;
    private Vector3 direction = Vector3.right;

    public Missile missilePrefab;

    public float speed = 1f; // Start speed for invaders.

    private void Awake()
    {
        initialPosition = transform.position;
        CreateInvaderGrid();
    }

    private void Start()
    {
        InvokeRepeating(nameof(MissileAttack), 1, 1); // How often should it launch missiles.
    }

    // Creates the grid itself with all the invaders.
    void CreateInvaderGrid()
    {
        for(int r = 0; r < row; r++)
        {
            float width = 2f * (col - 1);
            float height = 2f * (row - 1);

            // For centring invaders.
            Vector2 centerOffset = new Vector2(-width * 0.5f, -height * 0.5f);
            Vector3 rowPosition = new Vector3(centerOffset.x, (2f * r) + centerOffset.y, 0f);
            
            for (int c = 0; c < col; c++)
            {
                Invader tempInvader = Instantiate(prefab[r], transform);

                Vector3 position = rowPosition;
                position.x += 2f * c;
                tempInvader.transform.localPosition = position;
            }
        }
    }

    // Activates all invaders again and places from original position.
    public void ResetInvaders()
    {
        direction = Vector3.right;
        transform.position = initialPosition;

        foreach(Transform invader in transform)
        {
            invader.gameObject.SetActive(true);
        }
    }

    // Randomly fires a missile.
    void MissileAttack()
    {
        int nrOfInvaders = GetInvaderCount();

        if(nrOfInvaders == 0)
        {
            return;
        }

        foreach(Transform invader in transform)
        {

            if (!invader.gameObject.activeInHierarchy) // If an invader is dead, it should not be able to shoot.
                continue;
            
           
            float rand = UnityEngine.Random.value;
            if (rand < 0.2)
            {
                Instantiate(missilePrefab, invader.position, Quaternion.identity);
                break;
            }
        }
       
    }

    // Check how many invaders are alive.
    public int GetInvaderCount()
    {
        int nr = 0;

        foreach(Transform invader in transform)
        {
            if (invader.gameObject.activeSelf)
                nr++;
        }
        return nr;
    }

    // Moves invaders aside.
    void Update()
    {
        transform.position += speed * Time.deltaTime * direction;

        Vector3 leftEdge = Camera.main.ViewportToWorldPoint(Vector3.zero);
        Vector3 rightEdge = Camera.main.ViewportToWorldPoint(Vector3.right);

        foreach (Transform invader in transform)
        {
            if (!invader.gameObject.activeInHierarchy) // Just look at the invaders living.
                continue;

            if (direction == Vector3.right && invader.position.x >= rightEdge.x - 1f)
            {
                AdvanceRow();
                break;
            }
            else if (direction == Vector3.left && invader.position.x <= leftEdge.x + 1f)
            {
                AdvanceRow();
                break;
            }
        }
    }
    // Changes direction and moves down one step.
    void AdvanceRow()
    {
        direction = new Vector3(-direction.x, 0, 0);
        Vector3 position = transform.position;
        position.y -= 1f;
        transform.position = position;
    }

    public void IncreaseSpeed()
    {
        speed *= 1.25f; // Increases speed by 25% every 100 points.
        Debug.Log($"Invader hastighet ökad till: {speed}");
    }

}
