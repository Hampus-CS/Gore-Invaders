using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class timedDestrutcion : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
    
    public float targetTime = 5.0f;

    // Update is called once per frame
    void FixedUpdate()
    {
        // Is a timer that when ready removes the object it is on.
        targetTime -= Time.deltaTime;

        if (targetTime <= 0.0f)
        {
            TimerEnded();
        }

    }

    void TimerEnded()
    {
        Destroy(gameObject);
    }
}

