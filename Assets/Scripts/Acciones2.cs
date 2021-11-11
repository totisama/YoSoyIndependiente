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
    private float rayDist = 1.3f;
    private int puntosBarrer;
    private int puntosTrapear;
    private int puntosBasura;
    private string txt = "Agarrar";
    private bool holding;
    private bool buttonPressedGeneral;
    private bool buttonPressedAccion;
    private bool chocaDeposito;
    private bool barrerTerminado;
    private bool trapearTerminado;
    private bool basurasTerminadas;
    private bool trapeadorMojado;
    private bool termino;
    private RaycastHit2D check;
    private GameObject player;
    private Text buttonAccionText;
    private Polvo polvoScript;
    private Trapeador trapeadorScript;
    private string herramientaActual;
    public AudioSource[] arraySonidos;
    private AudioSource MyAudioSource;
    public Canvas canvasPausa;

    private void Awake()
    {
        canvasPausa.enabled = false;
        Time.timeScale = 1f;
        buttonGeneral.interactable = true;
        moveIzquierda.interactable = true;
        moveDerecha.interactable = true;
    }
    private void Start()
    {
        MyAudioSource = GetComponent<AudioSource>();
        player = GameObject.FindGameObjectWithTag("Player");
        buttonAccion.interactable = false;
        buttonAccionText = buttonAccion.GetComponentInChildren<Text>();
        canvasFinalMenuUi.SetActive(false);
        misionBasurasText.text = "(0/3)";
        misionPolvoText.text = "(0/2)";
        misionManchaText.text = "(0/2)";
    }

    void Update()
    {
        if (!barrerTerminado || !trapearTerminado || !basurasTerminadas)
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
                            if (basurasTerminadas && !barrerTerminado && gameObj.GetComponent<Escoba>())
                            {
                                buttonGeneral.interactable = false;
                                gameObj.transform.parent = holder;
                                gameObj.transform.position = holder.position;
                                herramientaActual = "Escoba";
                                arraySonidos[2].Play();
                                check.collider.gameObject.GetComponent<Rigidbody2D>().isKinematic = true;
                                holding = true;
                                buttonPressedGeneral = false;
                                txt = "Soltar";
                            } else if (basurasTerminadas && barrerTerminado && !trapearTerminado && gameObj.GetComponent<Trapeador>())
                            {
                                buttonGeneral.interactable = false;
                                arraySonidos[2].Play();
                                check.collider.gameObject.GetComponent<Rigidbody2D>().isKinematic = true;
                                holding = true;
                                buttonPressedGeneral = false;
                                txt = "Soltar";
                                gameObj.transform.parent = holder;
                                gameObj.transform.position = holder.position;
                                herramientaActual = "Trapeador";
                            }
                            else if (!gameObj.GetComponent<Escoba>() && !gameObj.GetComponent<Trapeador>())
                            {
                                gameObj.transform.parent = holder;
                                gameObj.transform.position = holder.position;
                                arraySonidos[2].Play();
                                check.collider.gameObject.GetComponent<Rigidbody2D>().isKinematic = true;
                                holding = true;
                                buttonPressedGeneral = false;
                                txt = "Soltar";   
                            }
                            else
                            {
                                buttonPressedGeneral = false;
                            }
                        }
                    } else if (holding)
                    {
                        GameObject obj = holder.GetChild(0).gameObject;
                        if (obj.CompareTag("HerramientaLimpieza"))
                        {
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
                                        puntosBarrer += 1;
                                        misionPolvoText.text = "(" + puntosBarrer + "/2)";
                                        if (puntosBarrer == 2)
                                        {
                                            polvoTogg.isOn = true;
                                            Image fondo = polvoTogg.GetComponentInChildren<Image>();
                                            fondo.color = Color.green;
                                            MyAudioSource.Stop();
                                            arraySonidos[4].Play();
                                            barrerTerminado = true;
                                            buttonGeneral.interactable = true;
                                        }
                                        else
                                        {
                                            arraySonidos[0].Play();
                                        }
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
                                            puntosTrapear += 1;
                                            misionManchaText.text = "(" + puntosTrapear + "/2)";
                                            trapeadorScript = holder.GetComponentInChildren<Trapeador>();
                                            trapeadorScript.CambioSprite(0);
                                            if (puntosTrapear == 2)
                                            {
                                                manchasTogg.isOn = true;
                                                Image fondo = manchasTogg.GetComponentInChildren<Image>();
                                                fondo.color = Color.green;
                                                MyAudioSource.Stop();
                                                arraySonidos[4].Play();
                                                trapearTerminado = true;
                                                buttonGeneral.interactable = true;
                                            }
                                            else
                                            {
                                                arraySonidos[10].Play();
                                            }
                                        }   
                                    }
                                }
                            } else if(check.collider.CompareTag("Cubeta") && herramientaActual == "Trapeador")
                            {
                                if (!trapeadorMojado)
                                {
                                    buttonAccion.interactable = true;
                                    stringButton = "Mojar";
                                    if (buttonPressedAccion)
                                    {
                                        arraySonidos[1].Play();
                                        trapeadorMojado = true;
                                        buttonPressedAccion = false;
                                        trapeadorScript = holder.GetComponentInChildren<Trapeador>();
                                        trapeadorScript.CambioSprite(1);
                                        buttonAccion.interactable = false;
                                    }
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
                            buttonGeneral.interactable = true;
                            Destroy(obj);
                        }
                        buttonAccionText.text = stringButton;
                    }
                }
            }

            // Condicion para soltar el objeto que se tenga en la mano
            if (holding)
            {
                ObjetoMovible obj = player.GetComponentInChildren<ObjetoMovible>();
                if (obj.CompareTag("HerramientaLimpieza"))
                {
                    if (buttonPressedGeneral)
                    {
                        if (player.GetComponentInChildren<Escoba>())
                        {
                            if (basurasTerminadas && barrerTerminado)
                            {
                                Vector3 vector = grabDetect.position;
                                vector.y = -1.45f;
                                obj.transform.position = vector;
                                herramientaActual = "";
                                arraySonidos[9].Play();
                                obj.transform.parent = null;
                                obj.GetComponent<Rigidbody2D>().isKinematic = false;
                                buttonPressedGeneral = false;
                                holding = false;
                                txt = "Agarrar";
                            }   
                        }
                    }
                }
                else
                {
                    buttonGeneral.interactable = false;
                }
            }
            buttonTxtGeneral.text = txt;
        }
        else if (!termino && barrerTerminado && trapearTerminado && basurasTerminadas)
        {
            canvasFinalMenuUi.SetActive(true);
            Time.timeScale = 0f;
            buttonGeneral.interactable = false;
            moveIzquierda.interactable = false;
            moveDerecha.interactable = false;
            MyAudioSource.Stop();
            arraySonidos[8].Play();
            termino = true;
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
        misionBasurasText.text = "("+ puntosBasura + "/3)";
        if (puntosBasura >= 3)
        {
            basurasTerminadas = true;
            basurasTogg.isOn = basurasTerminadas;
            Image fondo = basurasTogg.GetComponentInChildren<Image>();
            fondo.color = Color.green;
            MyAudioSource.Stop();
            arraySonidos[4].Play();
        }
        else
        {
            arraySonidos[3].Play();
        }
    }
}
