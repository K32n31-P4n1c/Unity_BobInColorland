using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundParallax : MonoBehaviour
{
    [SerializeField] 
    private float parallaxEffectMultiplier;

    private Transform CameraTransform;
    private Vector3 TrackCameraPos;
    private float TextureUnitSizeX;
    
    void Start()
    {
        CameraTransform = Camera.main.transform;
        TrackCameraPos = CameraTransform.position;

        Sprite sprite = GetComponent<SpriteRenderer>().sprite;
        Texture2D texture = sprite.texture;
        TextureUnitSizeX = texture.width / sprite.pixelsPerUnit;

    }

    // Late updatet is called after all Update funcctions have been called. Objects that move inside Update are tracked by the camera after execution.
    void LateUpdate() 
    {
        // How much the camera moved since the previous frame
        Vector3 delta = CameraTransform.position - TrackCameraPos;   

        transform.position = transform.position + delta * parallaxEffectMultiplier;
        TrackCameraPos = CameraTransform.position;
        
    }
}
