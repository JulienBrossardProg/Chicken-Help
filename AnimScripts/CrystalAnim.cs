using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrystalAnim : MonoBehaviour
{
    public Vector3 trans;
    public Vector3 rot;
    public GameObject checkpointEffect;
    public int angle;

    private void Start()
    {
        StartCoroutine("SaveAnimBegin");
        Instantiate(checkpointEffect, gameObject.transform.parent.transform.position, Quaternion.Euler(-90, 0, 0));
        Instantiate(checkpointEffect, gameObject.transform.parent.transform.position, Quaternion.Euler(-90, 0, 0));
        Instantiate(checkpointEffect, gameObject.transform.parent.transform.position, Quaternion.Euler(-90, 0, 0));
    }
    void Update()
    {
        transform.Rotate(rot * Time.deltaTime);
        transform.Translate(trans * Time.deltaTime);
    }

    IEnumerator SaveAnimBegin()
    {
        //GameObject go = Instantiate(checkpointEffect, gameObject.transform.parent.position, Quaternion.identity);
        trans = new Vector3(0, 0, -6);
        rot = new Vector3(0, 0, 100);
        yield return new WaitForSeconds(2);
        trans = new Vector3(0, 0, 0);
        rot = new Vector3(0, 0, 100);
        yield return new WaitForSeconds(1);
        trans = new Vector3(0, 0, 6);
        rot = new Vector3(0, 0, 100);
        yield return new WaitForSeconds(2);
        //Destroy(go);
        trans = new Vector3(0, 0, 0);
        rot = new Vector3(0, 5, 50);
    }

}
