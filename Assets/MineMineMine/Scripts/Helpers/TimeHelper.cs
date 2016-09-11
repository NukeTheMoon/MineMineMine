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

    public static bool WithinDoubleTapTimeWindow(float firstTapTimeMs, float timeWindowMs)
    {
        float currentTimeMs = SecondsToMilliseconds(Time.time);
        return currentTimeMs <= firstTapTimeMs + timeWindowMs;
    }
}
