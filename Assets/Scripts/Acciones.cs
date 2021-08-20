using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class Acciones : MonoBehaviour
{
    public Transform grabDetect;
    public Transform holder;
    public Text buttonTxtGeneral;
    public Text misionCamisasText;
    public Text misionCamaText;
    public Button buttonAccion;
    public Button buttonGeneral;
    public Button moveIzquierda;
    public Button moveDerecha;
    public GameObject canvasFinalMenuUi;
    private float rayDist = 1;
    private int puntosCama;
    private int puntosCamisas;
    private string txt = "Agarrar";
    private bool holding;
    private bool buttonPressedGeneral;
    private bool buttonPressedAccion;
    private bool chocaDeposito;
    private bool camaTerminada;
    private bool camisasTerminadas;
    public Toggle camaTogg;
    public Toggle camisasTogg;
    private RaycastHit2D check;
    private GameObject player;
    private GameObject cama;
    private Text buttonAccionText;
    private Cama camaScript;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        buttonAccion.interactable = false;
        buttonAccionText = buttonAccion.GetComponentInChildren<Text>();
        cama = GameObject.FindGameObjectWithTag("Cama");
        camaScript = cama.GetComponent<Cama>();
        canvasFinalMenuUi.SetActive(false);
        misionCamisasText.text = "Mete las camisas en la ropa sucia. (0/3)";
        misionCamaText.text = "Has la cama (0/3)";
    }

    void Update()
    {
        if (!camaTerminada || !camisasTerminadas)
        {
            check = Physics2D.Raycast(grabDetect.position, Vector2.right * transform.localScale, rayDist);

            if (check.collider != null)
            {
                if (check.collider.CompareTag("ObjetoMovible"))
                {
                    if (!holding)
                    {
                        if (buttonPressedGeneral)
                        {
                            GameObject gameObj = check.collider.gameObject;
                            gameObj.transform.parent = holder;
                            gameObj.transform.position = holder.position;
                            check.collider.gameObject.GetComponent<Rigidbody2D>().isKinematic = true;
                            holding = true;
                            buttonPressedGeneral = false;
                            txt = "Soltar";
                        }
                    }
                }
                else if (check.collider.CompareTag("Cama"))
                {
                    if (!camaTerminada)
                    {
                        if (!holding)
                        {
                            buttonAccion.interactable = true;
                            string stringButton = "Hacer cama";
                            if (buttonPressedAccion)
                            {
                                buttonPressedAccion = false;
                                camaTerminada = camaScript.CambioSprite();
                                puntosCama += 1;
                                camaTogg.isOn = camaTerminada;
                                misionCamaText.text = "Has la cama (" + puntosCama + "/3)";
                            }

                            buttonAccionText.text = stringButton;
                        }
                    }
                    else
                    {
                        buttonAccion.interactable = false;
                        buttonPressedAccion = false;
                        buttonAccionText.text = "Acción";
                    }
                }
            }
            else
            {
                buttonPressedAccion = false;
                buttonAccionText.text = "Acción";
            }

            if (chocaDeposito)
            {
                if (holding)
                {
                    buttonAccion.interactable = true;
                    string stringButton = "Depositar";
                    if (buttonPressedAccion)
                    {
                        SumarPuntosCamisa();
                        stringButton = "Acción";
                        txt = "Agarrar";
                        buttonAccion.interactable = false;
                        holding = false;
                        buttonPressedAccion = false;
                        GameObject obj = holder.GetChild(0).gameObject;
                        Destroy(obj);
                    }

                    buttonAccionText.text = stringButton;
                }
            }

            if (holding && buttonPressedGeneral)
            {
                ObjetoMovible obj = player.GetComponentInChildren<ObjetoMovible>();
                obj.transform.position = grabDetect.position;
                obj.transform.parent = null;
                obj.GetComponent<Rigidbody2D>().isKinematic = false;
                buttonPressedGeneral = false;
                holding = false;
                txt = "Agarrar";
            }
            buttonTxtGeneral.text = txt;
        }
        else
        {
            canvasFinalMenuUi.SetActive(true);
            Time.timeScale = 0f;
            buttonGeneral.interactable = false;
            moveIzquierda.interactable = false;
            moveDerecha.interactable = false;
        }
    }

    public void PresionarBotonGeneral()
    {
        if (check.collider != null && !holding || holding && !check.collider.CompareTag("Cama"))
        {
            buttonPressedGeneral = true;
        }
    }
    
    public void PresionarBotonAccion()
    {
        if (holding && chocaDeposito)
        {
            buttonPressedAccion = true;
        }
        else if (check.collider.CompareTag("Cama") && !camaTerminada)
        {
            buttonPressedAccion = true;
        }
    }
    
    void OnCollisionEnter2D(Collision2D coll)
    {
        
        if (coll.gameObject.CompareTag("Deposito"))
        {
            chocaDeposito = true;
        }
    }
    
    void OnCollisionExit2D(Collision2D coll)
    {
        if (coll.gameObject.CompareTag("Deposito"))
        {
            chocaDeposito = false;
            buttonAccion.interactable = false;
        }
        buttonAccionText.text = "Acción";
    }

    void SumarPuntosCamisa()
    {
        puntosCamisas += 1;
        misionCamisasText.text = "Mete las camisas en la ropa sucia. ("+ puntosCamisas + "/3)";
        if (puntosCamisas >= 3)
        {
            camisasTerminadas = true;
            camisasTogg.isOn = camisasTerminadas;
        }
    }
}
