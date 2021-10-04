using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trapeador : MonoBehaviour
{
    public Sprite[] sprites;
    public SpriteRenderer spriteRenderer;
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer.sprite = sprites[0];
    }

    public void CambioSprite(int etapa)
    {
        spriteRenderer.sprite = sprites[etapa];
    }
}
