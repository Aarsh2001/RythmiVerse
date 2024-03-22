using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public enum XRHand
{
    Left,
    Right
}
public class Drumstick_vibration : MonoBehaviour
{
    public XRHand handToVibrate = XRHand.Right;
    private void OnCollisionEnter(Collision collision)
    {
        List<string> drumPartTags = new List<string>
        {
            "snare_tom",
            "floor_tom",
            "rack_tom1",
            "rack_tom2",
            "crash",
            "ride",
            "hi_hat"
        };

        if (drumPartTags.Contains(collision.collider.tag))
        {
            Vector3 velocity = collision.relativeVelocity;
            // Calculate haptic feedback intensity based on collision velocity
            float amplitude = CalculateHapticAmplitudeFromVelocity(velocity.magnitude*1000000);
            // Trigger the haptic feedback with calculated amplitude
            TriggerHapticFeedback(amplitude);
            //Debug.Log(velocity.magnitude*1000000);
        }
    }

    private void TriggerHapticFeedback(float amplitude)
    {
        var devices = new List<InputDevice>();
        InputDevices.GetDevicesAtXRNode(handToVibrate == XRHand.Right ? XRNode.RightHand : XRNode.LeftHand, devices);


        foreach (var device in devices)
        {
            HapticCapabilities capabilities;
            if (device.TryGetHapticCapabilities(out capabilities) && capabilities.supportsImpulse)
            {
                uint channel = 0; // Most devices have only one channel
                float duration = 0.1f; // Vibration duration in seconds
                device.SendHapticImpulse(channel, amplitude, duration);
            }
        }
    }

    private float CalculateHapticAmplitudeFromVelocity(float velocityMagnitude)
    {
        // Lower the minimum velocity to start feeling haptic feedback at lower speeds.
        float minVelocity = 0.0f; // Adjusted down for sensitivity
        // Narrow the range of velocity for maximum haptic feedback to make it more responsive.
        float maxVelocity = 5f; // Adjusted down for sensitivity

        // Apply a non-linear scale to make the mapping more sensitive.
        // This can be adjusted based on testing and preference.
        float scaledVelocity = Mathf.Pow(velocityMagnitude, 2); // Example of non-linear scaling

        // Clamp the scaledVelocity between minVelocity and maxVelocity
        float clampedVelocity = Mathf.Clamp(scaledVelocity, minVelocity, maxVelocity);
        // Normalize the clamped velocity to a value between 0 and 1
        float normalizedVelocity = (clampedVelocity - minVelocity) / (maxVelocity - minVelocity);
        // Linearly interpolate the amplitude based on the normalized velocity
        float amplitude = Mathf.Lerp(0.5f, 1f, normalizedVelocity); // Keeping min and max amplitude

        return amplitude;
    }
}
