using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class Acciones : MonoBehaviour
{
    public Transform grabDetect;
    public Transform holder;
    public Text buttonTxt;
    private float rayDist = 1;
    private bool holding;
    private string txt = "Agarrar"; 
    private bool buttonPressed;
    private RaycastHit2D check;

    void Update()
    {
        check = Physics2D.Raycast(grabDetect.position, Vector2.right * transform.localScale, rayDist);

        if (check.collider != null && check.collider.CompareTag("basura") && !holding)
        {
            if (buttonPressed) {
                check.collider.gameObject.transform.parent = holder;
                check.collider.gameObject.transform.position = holder.position;
                check.collider.gameObject.GetComponent<Rigidbody2D>().isKinematic = true;
                holding = true;
                buttonPressed = false;
                txt = "Soltar";
            }
        }

        if (holding && buttonPressed)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            Basura obj = player.GetComponentInChildren<Basura>();
            obj.transform.position = grabDetect.position;
            obj.GetComponent<Rigidbody2D>().isKinematic = false;
            buttonPressed = false;
            holding = false;
            txt = "Agarrar";
        }

        buttonTxt.text = txt;
    }

    public void PresionarBoton() {
        if ((check.collider != null && !holding) || holding)
        {
            buttonPressed = true;
        }
    }
}
