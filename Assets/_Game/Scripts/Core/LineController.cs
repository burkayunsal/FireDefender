using System.Collections;
using System.Collections.Generic;
using SBF.Extentions.Vector;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class LineController : MonoBehaviour
{
    [SerializeField] private LineRenderer lr;
    [SerializeField] private Material matLine;

    private float offset = 0f;

    [SerializeField] private float offsetSpeed;
    // Update is called once per frame
    void Update()
    {
        Repos();
        MaterialOffset();
    }

    void Repos()
    {
        lr.SetPosition(0,PlayerController.I.rb.position.WithY(.2f));
    }

    void MaterialOffset()
    {
        offset -= Time.deltaTime * offsetSpeed;
        matLine.mainTextureOffset = new Vector2(offset, 0f);
    }
}
