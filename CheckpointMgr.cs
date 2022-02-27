using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointMgr : MonoBehaviour
{

    public Vector3 lastPoint;
    void Start()
    {
        lastPoint = transform.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Checkpoint")
        {
            lastPoint = transform.position;
            other.gameObject.GetComponent<CrystalAnim>().enabled = true;
        }
    }

    public void Respawn()
    {
        transform.position = lastPoint;
        PlayerInfos.pi.SetHealth(3);
    }
}
