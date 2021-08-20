using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cama : MonoBehaviour
{
    public Sprite[] sprites;
    private int contador;
    public SpriteRenderer spriteRenderer;

    private void Start()
    {
        spriteRenderer.sprite = sprites[0];
    }

    public bool CambioSprite()
    {
        bool terminada = false;
        if (contador < 3)
        {
            contador += 1;
            spriteRenderer.sprite = sprites[contador];
            if (contador >= 3)
            {
                terminada = true;
            }
        }
        else if (contador >= 3 )
        {
            terminada = true;
        }

        return terminada;
    }
}
