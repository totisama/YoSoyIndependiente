using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemSlot : MonoBehaviour, IDropHandler
{
    private RectTransform rectTransform;
    public bool rest;
    public bool colocado;
    public bool puesto;

    // Start is called before the first frame update
    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (!puesto && !eventData.pointerDrag.GetComponent<DragDrop>().colocado)
        {
            if (eventData.pointerDrag.CompareTag(gameObject.tag))
            {
                eventData.pointerDrag.GetComponent<RectTransform>().position = rectTransform.position;
                eventData.pointerDrag.isStatic = true;
                eventData.pointerDrag.transform.SetParent(gameObject.transform);
                eventData.pointerDrag.transform.localEulerAngles = new Vector3(0, 0, gameObject.transform.rotation.z);
                eventData.pointerDrag.GetComponent<DragDrop>().colocado = true;
                colocado = true;
                puesto = true;
                if (eventData.pointerDrag.CompareTag("Mantel"))
                {
                    rest = true;
                }
            }
        }
    }
}
