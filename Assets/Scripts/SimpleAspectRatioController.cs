using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleAspectRatioController : MonoBehaviour
{
    public float targetAspect = 1.0f; // 1:1 aspect ratio.

    void Start()
    {
        // Calculates the screen aspect (width/height) of the current screen and calculates the height scale needed to match the aspect we want.
        float screenAspect = (float)Screen.width / Screen.height;
        float scaleHeight = screenAspect / targetAspect;

        Camera camera = GetComponent<Camera>();
        
        if (scaleHeight < 1.0f) // If the height scale is less than 1, it means that the screen is wider than the target aspect.
        {
            // Adjusts the camera viewport to add black borders vertically.
            camera.rect = new Rect(0, (1.0f - scaleHeight) / 2.0f, 1.0f, scaleHeight);
        }
        else // Otherwise, if the screen is narrower, the width scale is calculated.
        {
            // Adjusts the camera viewport to add black borders horizontally.
            float scaleWidth = 1.0f / scaleHeight;
            camera.rect = new Rect((1.0f - scaleWidth) / 2.0f, 0, scaleWidth, 1.0f);
        }
    }
}