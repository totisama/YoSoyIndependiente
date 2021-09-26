using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class Acciones2 : MonoBehaviour
{
    public Transform grabDetect;
    public Transform holder;
    public Text buttonTxtGeneral;
    public Text misionBasurasText;
    public Toggle basurasTogg;
    public Text misionPolvoText;
    public Toggle polvoTogg;
    public Text misionManchaText;
    public Toggle manchasTogg;
    public Button buttonAccion;
    public Button buttonGeneral;
    public Button moveIzquierda;
    public Button moveDerecha;
    public GameObject canvasFinalMenuUi;
    private float rayDist = 1;
    private int puntosCama;
    private int puntosBasura;
    private string txt = "Agarrar";
    private bool holding;
    private bool buttonPressedGeneral;
    private bool buttonPressedAccion;
    private bool chocaDeposito;
    private bool polvosTerminados;
    private bool basurasTerminadas;
    private bool trapeadorMojado;
    private RaycastHit2D check;
    private GameObject player;
    private Text buttonAccionText;
    private Polvo polvoScript;
    private string herramientaActual;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        buttonAccion.interactable = false;
        buttonAccionText = buttonAccion.GetComponentInChildren<Text>();
        canvasFinalMenuUi.SetActive(false);
        misionBasurasText.text = "Mete la basura en el basurero. (0/3)";
        misionPolvoText.text = "Barre los 2 polvos (0/2)";
        misionManchaText.text = "Moja el trapeador y trapea las 2 manchas (0/2)";
    }

    void Update()
    {
        if (!polvosTerminados || !basurasTerminadas)
        {
            check = Physics2D.Raycast(grabDetect.position, Vector2.right * transform.localScale, rayDist);
            //Debug.Log(check.collider.tag);

            if (check.collider != null)
            {
                if (check.collider.CompareTag("ObjetoMovible") || check.collider.CompareTag("HerramientaLimpieza") || check.collider.CompareTag("Polvo") || check.collider.CompareTag("Cubeta"))
                {
                    if (!holding)
                    {
                        // Agarrar objectos
                        if (buttonPressedGeneral)
                        {
                            GameObject gameObj = check.collider.gameObject;
                            gameObj.transform.parent = holder;
                            gameObj.transform.position = holder.position;
                            if (gameObj.GetComponent<Escoba>())
                            {
                                herramientaActual = "Escoba";
                            } else if (gameObj.GetComponent<Trapeador>())
                            {
                                 herramientaActual = "Trapeador";
                            }
                            check.collider.gameObject.GetComponent<Rigidbody2D>().isKinematic = true;
                            holding = true;
                            buttonPressedGeneral = false;
                            txt = "Soltar";
                        }
                    } else if (holding)
                    {
                        GameObject obj = holder.GetChild(0).gameObject;
                        if (obj.CompareTag("HerramientaLimpieza"))
                        {
                            SpriteRenderer sprite = obj.GetComponentInChildren<SpriteRenderer>();
                            sprite.sortingOrder = 5;
                            string stringButton = "Acción";
                            if (check.collider.CompareTag("Polvo"))
                            {
                                polvoScript = check.collider.GetComponent<Polvo>();
                                int etapa = polvoScript.GetEtapa();

                                if (etapa == 0 && herramientaActual == "Escoba")
                                {
                                    buttonAccion.interactable = true;
                                    stringButton = "Barrer";
                                    if (buttonPressedAccion)
                                    {
                                        stringButton = "Acción";
                                        polvoScript.CambioSprite(herramientaActual);
                                        buttonAccion.interactable = false;
                                        buttonPressedAccion = false;
                                    }
                                } else if (etapa == 1 && herramientaActual == "Trapeador")
                                {
                                    if (trapeadorMojado)
                                    {
                                        buttonAccion.interactable = true;
                                        stringButton = "Trapear";
                                        if (buttonPressedAccion)
                                        {
                                            stringButton = "Acción";
                                            polvoScript.CambioSprite(herramientaActual);
                                            buttonAccion.interactable = false;
                                            buttonPressedAccion = false;
                                            trapeadorMojado = false;
                                        }   
                                    }
                                }
                            } else if(check.collider.CompareTag("Cubeta"))
                            {
                                buttonAccion.interactable = true;
                                stringButton = "Mojar";
                                if (buttonPressedAccion)
                                {
                                    MojarTrapeador();
                                    buttonPressedAccion = false;
                                    //Llamar funcion de trapeador para cambiar sprite
                                }
                            }
                            else
                            {
                                buttonAccion.interactable = false;
                                stringButton = "Acción";
                            }
                            buttonAccionText.text = stringButton;
                        }
                    }
                }
                /*
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
                */
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
                    GameObject obj = holder.GetChild(0).gameObject;
                    if (obj.CompareTag("ObjetoMovible"))
                    {
                        buttonAccion.interactable = true;
                        string stringButton = "Tirar";
                        if (buttonPressedAccion)
                        {
                            SumarPuntosBasura();
                            stringButton = "Acción";
                            txt = "Agarrar";
                            buttonAccion.interactable = false;
                            holding = false;
                            buttonPressedAccion = false;
                            Destroy(obj);
                        }

                        buttonAccionText.text = stringButton;
                    }
                }
            }

            // Condicion para soltar el objeto que se tenga en la mano
            if (holding && buttonPressedGeneral)
            {
                ObjetoMovible obj = player.GetComponentInChildren<ObjetoMovible>();
                if (obj.CompareTag("HerramientaLimpieza"))
                {
                    Vector3 vector = grabDetect.position;
                    vector.y = -1.40f;
                    obj.transform.position = vector;
                    herramientaActual = "";
                }
                else
                {
                    obj.transform.position = grabDetect.position;    
                }
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
        if (check.collider != null)
        {
            if (check.collider.CompareTag("ObjetoMovible") || check.collider.CompareTag("HerramientaLimpieza") && !holding)
            {
                buttonPressedGeneral = true;
            }
            else if(holding)
            {
                buttonPressedGeneral = true;
            }
        } else if (holding)
        {
            buttonPressedGeneral = true;
        }
    }
    
    public void PresionarBotonAccion()
    {
        if (holding)
        {
            ObjetoMovible obj = player.GetComponentInChildren<ObjetoMovible>();
            if (chocaDeposito || check.collider.CompareTag("Polvo") && obj.CompareTag("HerramientaLimpieza") || check.collider.CompareTag("Cubeta") && herramientaActual == "Trapeador")
            {
                buttonPressedAccion = true;
            }
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

    void SumarPuntosBasura()
    {
        puntosBasura += 1;
        misionBasurasText.text = "Mete la basura en el basurero. ("+ puntosBasura + "/3)";
        if (puntosBasura >= 3)
        {
            basurasTerminadas = true;
            basurasTogg.isOn = basurasTerminadas;
        }
    }

    void MojarTrapeador()
    {
        trapeadorMojado = true;
    }
}
