using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree : MonoBehaviour
{
    public Transform treeRoot;
    public void Place(Point p)
    {
        transform.rotation = Quaternion.FromToRotation(transform.up, p.normal) * transform.rotation;

        //Vector3 positionbRandomizeFactor = Random.insideUnitSphere * 15;
        //positionbRandomizeFactor.y = 0;

        transform.position += transform.forward * Random.Range(-7, 7) + transform.right * Random.Range(-7, 7);
        //transform.eulerAngles = new Vector3(Random.Range(-4, 4), Random.Range(-4, 4), Random.Range(-4, 4));
    }
}
