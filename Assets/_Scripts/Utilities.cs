using UnityEngine;

public abstract class Utilities
{
    /// <summary>
    /// Return the current dimension of the orthographic camera
    /// </summary>
    public static Vector2 GetCameraDimensions(Camera camera)
    {
        float height = camera.orthographicSize * 2f;
        float width = height * camera.aspect;
        return new Vector2(width, height);
    }

    public static Vector2 GetCameraDimensions()
    {
        Camera camera = Camera.main;
        float height = camera.orthographicSize * 2f;
        float width = height * camera.aspect;
        return new Vector2(width, height);
    }

    /// <summary>
    /// Convert a value in decibels
    /// </summary>
    public static float ValueToVolume(float value)
    {
        value = Mathf.Clamp(value, 0.0001f, 1);
        return Mathf.Log10(value) * 20;
    }
}
