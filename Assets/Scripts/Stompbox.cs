using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stompbox : MonoBehaviour
{
    public GameObject deathEffect;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Enemy")
        {
            Debug.Log("Hit Enemy");

            other.transform.parent.gameObject.SetActive(false);

            Instantiate(deathEffect, transform.position, transform.rotation);

            PlayerController.instance.Bounce();

            AudioManager.instance.PlaySFX(3);
        }
    }
}
