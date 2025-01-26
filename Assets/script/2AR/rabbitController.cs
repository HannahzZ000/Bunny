using UnityEngine;

public class GyroControl : MonoBehaviour
{
    private bool gyroEnabled;
    private Gyroscope gyro;
    private Quaternion initialGyroRotation; // Stores the initial gyro rotation
    private Quaternion unityRotationFix = new Quaternion(0, 0, 1, 0); // Adjusts for coordinate system differences
    private Quaternion initialRabbitRotation; // Stores the rabbit's initial rotation in the scene

    [Range(0.1f, 10f)] public float smoothSpeed = 5f; // Speed of smooth rotation
    [Range(0.1f, 5f)] public float rotationMultiplier = 1f; // Controls the sensitivity of rotation

    private float currentYRotation; // Tracks the current Y-axis rotation of the rabbit

    void Start()
    {
        // Enable the gyroscope
        gyroEnabled = EnableGyro();

        if (gyroEnabled)
        {
            // Save the initial gyro rotation
            initialGyroRotation = gyro.attitude * unityRotationFix;
            Debug.Log("Gyro enabled and initial rotation calibrated.");
        }

        // Save the rabbit's initial rotation
        initialRabbitRotation = transform.rotation;
        currentYRotation = 0f; // Reset the incremental rotation
    }

    void Update()
    {
        if (gyroEnabled)
        {
            // Get the current gyroscope rotation
            Quaternion currentGyroRotation = gyro.attitude * unityRotationFix;

            // Calculate the relative rotation from the initial gyro rotation
            Quaternion deltaRotation = Quaternion.Inverse(initialGyroRotation) * currentGyroRotation;

            // Extract the Y-axis rotation
            float targetYRotation = deltaRotation.eulerAngles.y;

            // Map the Y-axis rotation to the -180 to 180 range
            if (targetYRotation > 180) targetYRotation -= 360;

            // Scale the rotation using the multiplier
            targetYRotation *= rotationMultiplier;

            // Smoothly interpolate towards the target rotation
            currentYRotation = Mathf.LerpAngle(currentYRotation, targetYRotation, Time.deltaTime * smoothSpeed);

            // Apply the incremental rotation to the rabbit's initial rotation
            transform.rotation = Quaternion.Euler(0, currentYRotation, 0) * initialRabbitRotation;
        }
    }

    private bool EnableGyro()
    {
        // Check if the device supports gyroscope
        if (SystemInfo.supportsGyroscope)
        {
            gyro = Input.gyro;
            gyro.enabled = true;
            Debug.Log("Gyroscope enabled successfully.");
            return true;
        }
        else
        {
            Debug.LogWarning("Device does not support Gyroscope.");
            return false;
        }
    }
}