using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class RaycastLaserController : MonoBehaviour
{
    public LineRenderer lineRenderer;  // Laser visual
    public Transform rayOrigin;  // GameObject tempat laser ditembakkan
    public float rayDistance = 20f;  // Panjang maksimum laser
    public LayerMask raycastLayer;  // Layer untuk mendeteksi UI

    void Start()
    {
        // Set up LineRenderer
        lineRenderer.positionCount = 2;
        lineRenderer.startWidth = 0.03f;
        lineRenderer.endWidth = 0.01f;
        lineRenderer.startColor = Color.red;
        lineRenderer.endColor = Color.yellow;
    }

    void Update()
    {
        DrawRaycastLaser();
    }

    void DrawRaycastLaser()
    {
        // Set titik awal LineRenderer dari GameObject (rayOrigin)
        Vector3 start = rayOrigin.position;
        Vector3 direction = rayOrigin.forward;

        lineRenderer.SetPosition(0, start);  // Titik awal laser

        // Buat raycast ke arah depan dari GameObject (tangan)
        Ray ray = new Ray(start, direction);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, rayDistance, raycastLayer))
        {
            // Jika terkena objek, gambar laser sampai titik yang terkena
            lineRenderer.SetPosition(1, hit.point);

            // Cek apakah yang terkena adalah UI
            if (IsPointerOverUI(hit))
            {
                Debug.Log("Mengenai UI: " + hit.collider.gameObject.name);
            }
        }
        else
        {
            // Jika tidak terkena apapun, gambar laser lurus sepanjang rayDistance
            lineRenderer.SetPosition(1, start + direction * rayDistance);
        }
    }

    // Cek apakah raycast mengenai UI
    private bool IsPointerOverUI(RaycastHit hit)
    {
        PointerEventData pointerData = new PointerEventData(EventSystem.current)
        {
            position = Camera.main.WorldToScreenPoint(hit.point)
        };

        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(pointerData, results);

        return results.Count > 0;
    }
}
