using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public static Camera ActiveCamera;
    public static Camera MainCamera;

    private Camera ThisCamera;

    public Transform FollowTarget;
    public Vector2 ScreenScrollSize = new Vector2(10, 8);
    public Vector2 CameraOffset = new Vector2(4.5f, 4.0f);
    public bool SnapFollow = true;
    public bool IsMainCamera = false;

    private void Awake()
    {
        ThisCamera = GetComponent<Camera>();
        if (IsMainCamera)
        {
            MainCamera = ThisCamera;
            ActiveCamera = ThisCamera;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(FollowTarget == null)
        {
            return;
        }
        if (SnapFollow)
        {
            Vector3 folPos = FollowTarget.position + new Vector3(0.5f, 0.5f, 0.0f);
            Vector3 newPos = new Vector3(
                Mathf.Floor(folPos.x / ScreenScrollSize.x) * ScreenScrollSize.x + CameraOffset.x,
                Mathf.Floor(folPos.y / ScreenScrollSize.y) * ScreenScrollSize.y + CameraOffset.y,
                transform.position.z
                );
            transform.position = newPos;
        }
        else
        {
            // This looks absolutely terrible, do not use! 
            Vector3 newPos = FollowTarget.position;
            newPos.z = transform.position.z;
            transform.position = newPos;
        }
    }

    private void OnDisable()
    {
        if(ActiveCamera == ThisCamera)
        {
            ActiveCamera = null;
        }
    }
}
