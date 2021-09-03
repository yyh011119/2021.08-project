using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorChange : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private float r, g, b;

    protected void Start()
    {
        spriteRenderer = this.GetComponent<SpriteRenderer>();
    }

    public void RevivalColor()
    {
        r = spriteRenderer.color.r;
        g = spriteRenderer.color.g;
        b = spriteRenderer.color.b;
        Debug.Log("Color changed!");
        spriteRenderer.color = new Color(r-30, g-35, b-40);
    }

}
