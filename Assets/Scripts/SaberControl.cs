using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaberControl : MonoBehaviour
{
    public Camera mainCamera; // Kamera utama untuk mendeteksi posisi mouse
    public float saberDistance = 5f; // Jarak saber dari kamera

    private void Start() {
        mainCamera = Camera.main;
    }
    void Update()
    {
       
        Vector3 mousePosition = Input.mousePosition;

        
        mousePosition.z = saberDistance; 
        Vector3 worldPosition = mainCamera.ScreenToWorldPoint(mousePosition);

        
        transform.position = worldPosition;
    }
}
