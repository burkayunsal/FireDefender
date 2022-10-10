using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateAround : MonoBehaviour
{
    [SerializeField] Vector3 axis;
    [SerializeField] float speed;

    public bool independent = false;
    void Update()
    {
        if (independent)
        {
            transform.Rotate(axis, speed * Time.deltaTime);
        }
        
        float ds = PlayerController.I.dragSpeed;
        if (ds != 0)
        {
            transform.Rotate(axis, speed * Time.deltaTime * ds);
        }
    }
}
