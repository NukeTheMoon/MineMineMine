using UnityEngine;
using System.Collections;

public class TimeHelper : MonoBehaviour
{

    public static float SecondsToMilliseconds(float seconds)
    {
        return seconds * 1000;
    }

    public static float MillisecondsToSeconds(int milliseconds)
    {
        return (float)milliseconds / 1000.0f;
    }

    public static float MillisecondsToSeconds(float milliseconds)
    {
        return milliseconds / 1000.0f;
    }

    public static bool WithinTimeWindow(float startTimeMs, float timeWindowMs)
    {
        float currentTimeMs = SecondsToMilliseconds(Time.time);
        return currentTimeMs <= startTimeMs + timeWindowMs;
    }
}
