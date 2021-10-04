using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;

public class Generator : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public GameObject dragDropObj;
    public Canvas canvas;
    public int cont;
    private RectTransform rectTransform;
    private CanvasGroup canvasGrupo;
    private MainLvl3 main;

    private void Awake()
    {
        GameObject camara = GameObject.Find("Main Camera");
        main = camara.GetComponent<MainLvl3>();
        rectTransform = GetComponent<RectTransform>();
    }
    
    public void OnDrag(PointerEventData eventData)
    {
    }
    
    public void OnBeginDrag(PointerEventData eventData)
    {
        if (cont < 2)
        {
            GameObject newDragDrop = Instantiate(dragDropObj);
            canvasGrupo = newDragDrop.GetComponent<CanvasGroup>();
            Transform trans = newDragDrop.transform;
         
            main.PlaySound(0);
            newDragDrop.GetComponent<DragDrop>().canvas = canvas;
            trans.SetParent(canvas.transform);
            trans.localScale = new Vector3(1, 1, 1);
            newDragDrop.GetComponent<RectTransform>().anchoredPosition = rectTransform.anchoredPosition;
            canvasGrupo.alpha = .8f;
            canvasGrupo.blocksRaycasts = false;
            eventData.pointerDrag = newDragDrop;
            cont += 1;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (cont < 2)
        {
            canvasGrupo.alpha = 1f;
            canvasGrupo.blocksRaycasts = true;
        }
    }
}
