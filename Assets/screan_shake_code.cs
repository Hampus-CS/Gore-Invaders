using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class screan_shake_code : MonoBehaviour
{
    // Start is called before the first frame update
    public float shake = 0f;
    
    void Start()
    {
        //shake = 5;
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position = new Vector3(Random.Range(-shake, shake), Random.Range(-shake, shake), -10);
        if (shake > 0f) shake -= Time.deltaTime * 10f;
        shake = Mathf.Clamp(shake, 0, 100);
    }
}
