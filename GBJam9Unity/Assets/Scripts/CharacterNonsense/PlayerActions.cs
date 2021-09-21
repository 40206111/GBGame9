using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActions : MonoBehaviour
{
    PlayerComponents Pcs;

    private void Awake()
    {
        Pcs = GetComponent<PlayerComponents>();
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void AButtonPushed()
    {
        //if (GameManager.Instance.GameState == GameManager.eGameState.Paused)
        //{
        //    return;
        //}

        Vector2Int facing = Pcs.Mover.GetFacingDirection;
        LayerMask mask = LayerMask.GetMask("Interactable");
        RaycastHit2D raycast = Physics2D.Raycast(((Vector2)transform.position) + new Vector2(0.0f,0.25f), facing, 1.0f, mask);

        if(raycast.collider != null)
        {
            Debug.Log(raycast.collider.name);
        }

    }
}
