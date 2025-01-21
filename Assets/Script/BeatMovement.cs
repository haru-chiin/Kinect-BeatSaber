using UnityEngine;

public class BeatMovement : MonoBehaviour
{
    public float speed = 4f;
    private Vector3 movementDirection = Vector3.forward;
    public string beatType;

    private Vector3 startSwipePos;
    private Vector3 endSwipePos;
    private bool isSwiping = false;

    public float amplitude = 0.5f;
    public float frequency = 2f;
    public float bounceDuration = 1f;

    private ParticleSystem effect;
    private float bounceTimer = 0f;

    private Vector3 startPosition;

    void Start()
    {
        Destroy(gameObject,10);
        startPosition = transform.position;
        Debug.Log("Rotate : " + transform.rotation.eulerAngles.z);
        effect = GetComponent<ParticleSystem>();
    }
    void Update()
    {
        // if (bounceTimer < bounceDuration)
        // {
        //     // Gerakan naik-turun menggunakan sinusoidal
        //     float bounceOffset = Mathf.Sin(Time.time * frequency) * amplitude;
        //     transform.position = startPosition + new Vector3(0, bounceOffset, 0);

        //     bounceTimer += Time.deltaTime;
        // }
        // else
        // {
        //     // Lanjutkan gerakan lurus setelah durasi naik-turun selesai
        //     transform.Translate(movementDirection * speed * Time.deltaTime);
        // }
        
        transform.Translate(movementDirection * speed * Time.deltaTime);
    }

    public void SetRotation(string direction)
    {
        switch (direction.ToLowerInvariant())
        {
            case "up":
                transform.rotation = Quaternion.Euler(0, 0, 0);
                break;
            case "down":
                transform.rotation = Quaternion.Euler(0, 0, 180);
                break;
            case "left":
                transform.rotation = Quaternion.Euler(0, 0, -90);
                break;
            case "right":
                transform.rotation = Quaternion.Euler(0, 0, 90);
                break;
        }
    }

    public void Break()
    {
        effect.Play();
        GetComponent<AudioSource>().Play();
        GetComponent<MeshRenderer>().enabled = false;
        GetComponent<Collider>().enabled = false;
        PointHandler.instance.point += 10;
        Destroy(gameObject, .5f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("NotScored"))
        {
            Debug.Log("Must be not scored");
            GetComponent<Collider>().enabled = false;
        }

        if (other.CompareTag("LeftHand") || other.CompareTag("RightHand"))
        {
            startSwipePos = other.transform.position;
            isSwiping = true;
            //DetectSwipe(other);
        }
    }

    // private void OnTriggerExit(Collider other)
    // {
    //     if (isSwiping && (other.CompareTag("LeftHand") || other.CompareTag("RightHand")))
    //     {
    //         endSwipePos = other.transform.position;
    //         isSwiping = false;
    //         DetectSwipe(other);
    //     }
    // }

    // void DetectSwipe(Collider handCollider)
    // {
        
    //     Vector3 swipeVector = endSwipePos - startSwipePos;
    //     string swipeDirection = "";

    //     if (Mathf.Abs(swipeVector.x) > Mathf.Abs(swipeVector.y))
    //     {
    //         // Horizontal swipe
    //         swipeDirection = swipeVector.x > 0 ? "right" : "left";
    //     }
    //     else
    //     {
    //         // Vertical swipe
    //         swipeDirection = swipeVector.y > 0 ? "up" : "down";
    //     }

    //     Debug.Log("Swipe : " + swipeDirection);
    //     bool validSwipe = true;

    //     // Cek rotasi box dan arah swipe
    //     if ((transform.rotation.eulerAngles.z == 0 && swipeDirection == "up") ||
    //         (transform.rotation.eulerAngles.z == 180 && swipeDirection == "down") ||
    //         (transform.rotation.eulerAngles.z == 90 && swipeDirection == "right") ||
    //         (transform.rotation.eulerAngles.z == 270 && swipeDirection == "left"))
    //     {
    //         validSwipe = true;
    //     }

    //     if (validSwipe)
    //     {

    //         if ((handCollider.CompareTag("LeftHand") && beatType == "blue") ||
    //                 (handCollider.CompareTag("RightHand") && beatType == "red"))
    //         {
    //             Debug.Log("Correct Swipe! Box destroyed.");
    //             effect.Play();
    //             GetComponent<AudioSource>().Play();
    //             GetComponent<MeshRenderer>().enabled = false;
    //             GetComponent<Collider>().enabled = false;
    //             PointHandler.instance.point += 10;
    //             Destroy(gameObject, .5f);
    //         }
    //         else
    //         {
    //             Debug.Log("Wrong hand!");
    //         }
    //     }
    //     else
    //     {
    //         Debug.Log("Wrong swipe direction!");
    //     }
    // }
}
