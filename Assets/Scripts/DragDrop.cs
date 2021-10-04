using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragDrop : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    public Canvas canvas;
    public bool colocado;
    public Generator generador;
    private RectTransform rectTransform;
    private CanvasGroup canvasGrupo;
    private MainLvl3 main;
    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGrupo = GetComponent<CanvasGroup>();
        string nombre = gameObject.tag + "Gen";
        GameObject gen = GameObject.Find(nombre);
        GameObject camara = GameObject.Find("Main Camera");
        main = camara.GetComponent<MainLvl3>();
        generador = gen.GetComponent<Generator>();
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (!colocado)
        {
            //rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
            rectTransform.position = eventData.position;   
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (!colocado)
        {
            canvasGrupo.alpha = .8f;
            canvasGrupo.blocksRaycasts = false;   
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGrupo.alpha = 1f;
        canvasGrupo.blocksRaycasts = true;
        if (!colocado)
        {
            main.PlaySound(1);
            generador.cont -= 1;
            Destroy(eventData.pointerDrag);
        }
        else
        {
            main.PlaySound(5);
        }
    }
}
