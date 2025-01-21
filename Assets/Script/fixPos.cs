using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fixPos : MonoBehaviour
{
    public Transform pos;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = pos.position;   
    }
}
