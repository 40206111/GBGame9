using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Moves the transform to a pixel perfect position before rendering.
/// Moves the transform back to correct position immediately after rendering.
/// Avoids the physics update, or any other process seeing the move.
/// </summary>
public class PixelPerfectPositioner : MonoBehaviour
{
    [SerializeField]
    int PixelsPerUnit = 16;
    [SerializeField]
    bool Reposition = true;

    Vector3 TruePosition;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Positioner());
    }

    // Update is called once per frame
    void LateUpdate()
    {
        TruePosition = transform.position;

        if (Reposition)
        {
            Vector3Int multiplied = Vector3Int.FloorToInt(TruePosition * PixelsPerUnit);
            transform.position = (Vector3)multiplied / PixelsPerUnit;
        }
    }

    IEnumerator Positioner()
    {
        while (true)
        {
            yield return new WaitForEndOfFrame();
            if (Reposition)
            {
                Debug.Log(transform.position.x + " " + transform.position.y + " " + transform.position.z + " ");
                transform.position = TruePosition;
                Debug.Log(transform.position.x + " " + transform.position.y + " " + transform.position.z + " ");
            }
        }
    }
}
