using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crosshair : MonoBehaviour
{
    [SerializeField] private LayerMask targetMask;

    [SerializeField] private SpriteRenderer dot;
    [SerializeField] private Color dotHightlightColour;
    private Color originalDotColour;

    private void Start()
    {
        originalDotColour = dot.color;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.forward * -40 * Time.deltaTime);
    }

    public void DetectTarget(Ray ray)
    {
        if(Physics.Raycast(ray, 100, targetMask))
        {
            dot.color = dotHightlightColour;
        }
        else
        {
            dot.color = originalDotColour;
        }
    }
}
