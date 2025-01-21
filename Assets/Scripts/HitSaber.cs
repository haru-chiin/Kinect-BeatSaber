using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitSaber : MonoBehaviour
{
    private Vector3 previousPos;
    public LayerMask layer;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update() 
    {

        Debug.DrawRay(transform.position, transform.forward * 1.5f, Color.red);

        RaycastHit hit;
        if(Physics.Raycast(transform.position, transform.forward, out hit,1.5f,layer))
        {
            Debug.Log("Hais");
            if(Vector3.Angle(transform.position - previousPos, hit.transform.up) > 130)
            {
                if(hit.transform.gameObject != null)
                {
                    Debug.Log("Hais2");
                    hit.transform.gameObject.GetComponent<BeatMovement>().Break();
                }
                
                
            }
        }
        previousPos = transform.position;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + transform.forward * 1.5f);
    }
}
