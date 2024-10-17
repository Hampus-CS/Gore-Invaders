using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class timedDestrutcion : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public float targetTime = 60.0f;

    // Update is called once per frame
    void FixedUpdate()
    {
        targetTime -= Time.deltaTime;

        if (targetTime <= 0.0f)
        {
            timerEnded();
        }

    }

    void timerEnded()
    {
        Destroy(gameObject);
    }
}

