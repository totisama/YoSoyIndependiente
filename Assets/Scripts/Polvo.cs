using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Polvo : MonoBehaviour
{
    public Sprite[] sprites;
    private int contador;
    public GameObject obj;
    public SpriteRenderer spriteRenderer;
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer.sprite = sprites[0];
    }

    public bool CambioSprite(string herramienta)
    {
        bool termino = false;
        if (herramienta == "Escoba" && contador == 0)
        {
            contador += 1;
            spriteRenderer.sprite = sprites[contador];
        }
        else if (herramienta == "Trapeador" && contador == 1)
        {
            termino = true;
            Destroy(obj);
        }

        return termino;
    }

    public int GetEtapa()
    {
        return contador;
    }
}
