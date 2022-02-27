using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    public int nbCoins = 0;
    public GameObject pickupEffect;
    public GameObject mobEffect;
    public GameObject waterEffect;
    public GameObject loot;
    bool canInstantiate = true;
    bool isInvincible = false;
    public GameObject cam1;
    public GameObject cam2;
    public GameObject cam3;
    public AudioClip hitSound;
    public AudioClip pickUpSound;
    public AudioClip plouf;
    AudioSource audioSource;
    public SkinnedMeshRenderer rend;
    public CheckpointMgr chkp;
    private BoxCollider mobParentCollider;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other) 
    {

        if(other.gameObject.tag == "Coin") // si on touche une pièce
        {
            GameObject go = Instantiate(pickupEffect, other.transform.position, Quaternion.identity);
            audioSource.PlayOneShot(pickUpSound);
            Destroy(go, 0.5f);
            PlayerInfos.pi.GetCoin();
            Destroy(other.gameObject);
        }

        if(other.gameObject.name == "End")
        {
            print("Score final = " + PlayerInfos.pi.GetScore());
        }


        if (other.gameObject.tag == "Cam1") // Gestion des caméras
        {
            cam1.SetActive(true);
        }

        if (other.gameObject.tag == "Cam2") // Gestion des caméras
        {
            cam2.SetActive(true);
        }

        if (other.gameObject.tag == "Cam3") // Gestion des caméras
        {
            cam3.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Cam1") // Gestion des caméras
        {
            cam1.SetActive(false);
        }

        if (other.gameObject.tag == "Cam2") // Gestion des caméras
        {
            cam2.SetActive(false);
        }

        if (other.gameObject.tag == "Cam3") // Gestion des caméras
        {
            cam3.SetActive(false);
        }
    }


    //private void OnCollisionEnter(Collision collision) 
    private void OnControllerColliderHit(ControllerColliderHit collision)
    {

        if(collision.gameObject.tag == "Hurt" && !isInvincible) //Si le monstre me touche
        {   //Je suis blessé
            isInvincible = true;
            PlayerInfos.pi.SetHealth(-1);
            iTween.PunchPosition(gameObject, Vector3.back*5, .6f);
            // iTween.PunchScale(gameObject, new Vector3(1,1,1), .6f);
            StartCoroutine("ResetInvincible");
        }

        if (collision.gameObject.tag == "Mob" && canInstantiate) //Si je saute sur le monstre
        {
            canInstantiate = false;
            audioSource.PlayOneShot(hitSound);
            mobParentCollider = collision.gameObject.transform.parent.gameObject.GetComponent<BoxCollider>();
            mobParentCollider.enabled = false;
            iTween.PunchScale(collision.gameObject.transform.parent.gameObject, new Vector3(50, 50, 50), .6f);
            Destroy(collision.gameObject.transform.parent.gameObject.transform.parent.gameObject, 0.5f);
            GameObject go = Instantiate(mobEffect, collision.transform.position, Quaternion.identity);
            Destroy(go, 0.5f);
            Instantiate(loot, collision.transform.position + Vector3.forward, Quaternion.Euler(-90, 0, 0));
            StartCoroutine("ResetInstantiate"); //Appel de la courtine pour remettre canInstantiate à true
        }

        if (collision.gameObject.tag == "Fall")
        {
            StartCoroutine("ResetInvincible");
            gameObject.transform.position = chkp.lastPoint;
            PlayerInfos.pi.SetHealth(-1);
        }

        if (collision.gameObject.tag == "Water")
        {
            GameObject go = Instantiate(waterEffect, gameObject.transform.position, Quaternion.identity);
            audioSource.PlayOneShot(plouf);
            StartCoroutine("ResetInvincible");
            gameObject.transform.position = chkp.lastPoint;
            PlayerInfos.pi.SetHealth(-1);
        }
    } 

    IEnumerator ResetInstantiate() // foncion pour remettre canInstantiate sur true après 8sec
    {
        yield return new WaitForSeconds(0.8f);
        canInstantiate = true;
    }

    IEnumerator ResetInvincible()
    {
        for(int i = 0; i< 10; i++)
        {
            yield return new WaitForSeconds(0.2f);
            rend.enabled = !rend.enabled;
        }
        yield return new WaitForSeconds(.2f);
        rend.enabled = true;
        isInvincible = false;
    }
}

