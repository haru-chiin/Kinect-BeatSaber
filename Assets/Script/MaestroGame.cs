using UnityEngine;

public class MaestroGame : MonoBehaviour
{
    public KinectManager kinectManager; // Drag & drop KinectManager script to this field in Inspector

    private uint playerId;

    public float handRaiseThreshold = 0.2f; // Threshold to determine hand raise (relative to shoulder height)
    public float handSwingThreshold = 0.1f; // Threshold to detect horizontal swing
    public float handCircleThreshold = 0.3f; // Threshold for detecting circular motion
    public float handOpenThreshold = 0.5f; // Threshold for detecting open wide gesture

    public GameObject leftHandPrefab; // Red box prefab
    public GameObject bothHandsPrefab; // Green box prefab
    public GameObject rightHandPrefab; // Blue box prefab

    private GameObject currentPrefabInstance;
    public Vector3 spawnPosition; // Position to spawn prefab

    private Vector3 previousLeftHandPos;
    private Vector3 previousRightHandPos;

    private string currentAction = "";

    void Start()
    {
        if (kinectManager == null)
        {
            kinectManager = KinectManager.Instance;
        }
    }

    void Update()
    {
        if (kinectManager && kinectManager.IsUserDetected())
        {
            playerId = kinectManager.GetPlayer1ID();

            if (playerId != 0)
            {
                // Get joint positions
                Vector3 handLeftPos = kinectManager.GetJointPosition(playerId, (int)KinectWrapper.NuiSkeletonPositionIndex.HandLeft);
                Vector3 handRightPos = kinectManager.GetJointPosition(playerId, (int)KinectWrapper.NuiSkeletonPositionIndex.HandRight);
                Vector3 shoulderCenterPos = kinectManager.GetJointPosition(playerId, (int)KinectWrapper.NuiSkeletonPositionIndex.ShoulderCenter);

                // Check if hands are raised above shoulder level
                bool isLeftHandRaised = handLeftPos.y > shoulderCenterPos.y + handRaiseThreshold;
                bool isRightHandRaised = handRightPos.y > shoulderCenterPos.y + handRaiseThreshold;

                string newAction = "";

                if (isLeftHandRaised && isRightHandRaised)
                {
                    newAction = "Both Hands Raised";
                }
                else if (isLeftHandRaised)
                {
                    newAction = "Left Hand Raised";
                }
                else if (isRightHandRaised)
                {
                    newAction = "Right Hand Raised";
                }
                else
                {
                    newAction = "Hands Lowered";
                }

                // Only update if action changes
                if (newAction != currentAction)
                {
                    currentAction = newAction;
                    HandleAction(currentAction);
                }

                // Update previous positions
                previousLeftHandPos = handLeftPos;
                previousRightHandPos = handRightPos;
            }
        }
    }

    private void HandleAction(string action)
    {
        // Destroy current prefab instance
        if (currentPrefabInstance != null)
        {
            Destroy(currentPrefabInstance);
        }

        // Spawn corresponding prefab
        if (action == "Left Hand Raised" || action == "Hands Lowered")
        {
            currentPrefabInstance = Instantiate(leftHandPrefab, spawnPosition, Quaternion.identity);
        }
        else if (action == "Both Hands Raised")
        {
            currentPrefabInstance = Instantiate(bothHandsPrefab, spawnPosition, Quaternion.identity);
        }
        else if (action == "Right Hand Raised")
        {
            currentPrefabInstance = Instantiate(rightHandPrefab, spawnPosition, Quaternion.identity);
        }

        Debug.Log("Action: " + action);
    }
}
