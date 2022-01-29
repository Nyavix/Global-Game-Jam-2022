using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerRevive : MonoBehaviour
{
    public GameObject playerG;
    private PlayerController playerController;
    private SpriteRenderer spriteRenderer;
    
    // Start is called before the first frame update
    void Start()
    {
        playerG = this.gameObject;
        playerController = playerG.GetComponent<PlayerController>();
        spriteRenderer = playerG.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        //UnityEngine.Debug.Log(playerController.isSoul);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.name == "Revive" && playerController.isSoul)
        {
            Revive();
            Destroy(other.gameObject);
        }
    }

    public void Revive()
    {
        playerController.isSoul = false;
        spriteRenderer.color = Color.white;
    }
}
