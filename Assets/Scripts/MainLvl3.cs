using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainLvl3 : MonoBehaviour
{
    public GameObject restantes1;
    public GameObject restantes2;
    public ItemSlot individual1;
    public ItemSlot individual2;
    public ItemSlot plato1;
    public ItemSlot plato2;
    public ItemSlot vaso1;
    public ItemSlot vaso2;
    public ItemSlot cuchara1;
    public ItemSlot cuchara2;
    public ItemSlot tenedor1;
    public ItemSlot tenedor2;
    public ItemSlot cuchillo1;
    public ItemSlot cuchillo2;
    public Toggle individualTogg;
    public Text individualTxt;
    public Toggle platoTogg;
    public Text platoTxt;
    public Toggle vasoTogg;
    public Text vasoTxt;
    public Toggle cuchilloTogg;
    public Text cuchillolTxt;
    public Toggle tenedorTogg;
    public Text tenedorTxt;
    public Toggle cucharaTogg;
    public Text cucharaTxt;
    public AudioSource[] arraySonidos;
    public GameObject canvasFinalMenuUi;
    public Canvas canvasPausa;
    private bool individual;
    private bool plato;
    private bool vaso;
    private bool cuchillo;
    private bool tenedor;
    private bool cuchara;
    private bool termino;
    private bool entro;
    private int contIndividual;
    private int contPlato;
    private int contVaso;
    private int contCuchillo;
    private int contTenedor;
    private int contCuchara;

    private void Awake()
    {
        canvasPausa.enabled = false;
        Time.timeScale = 1f;
        restantes1.SetActive(false);
        restantes2.SetActive(false);
        canvasFinalMenuUi.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (!entro)
        {
            if (individual1.rest)
            {
                restantes1.SetActive(true);
            }

            if (individual2.rest)
            {
                restantes2.SetActive(true);
            }

            if (individual1.colocado || individual2.colocado)
            {
                if (individual1.colocado)
                {
                    contIndividual += 1;
                    individual1.colocado = false;
                }

                if (individual2.colocado)
                {
                    contIndividual += 1;
                    individual2.colocado = false;
                }

                individualTxt.text = "Coloca los 2 individuales en su lugar (" + contIndividual + "/2)";
                if (contIndividual == 2)
                {
                    Image fondo = individualTogg.GetComponentInChildren<Image>();
                    fondo.color = Color.green;
                    individualTogg.isOn = true;
                    termino = SumaPuntos();
                    PlaySound(2);
                }
            }

            if (vaso1.colocado || vaso2.colocado)
            {
                if (vaso1.colocado)
                {
                    contVaso += 1;
                    vaso1.colocado = false;
                }

                if (vaso2.colocado)
                {
                    contVaso += 1;
                    vaso2.colocado = false;
                }

                vasoTxt.text = "Coloca los 2 vasos en su lugar (" + contVaso + "/2)";
                if (contVaso == 2)
                {
                    Image fondo = vasoTogg.GetComponentInChildren<Image>();
                    fondo.color = Color.green;
                    vasoTogg.isOn = true;
                    termino = SumaPuntos();
                    PlaySound(2);
                }
            }

            if (cuchara1.colocado || cuchara2.colocado)
            {
                if (cuchara1.colocado)
                {
                    contCuchara += 1;
                    cuchara1.colocado = false;
                }

                if (cuchara2.colocado)
                {
                    contCuchara += 1;
                    cuchara2.colocado = false;
                }

                cucharaTxt.text = "Coloca las 2 cucharas en su lugar (" + contCuchara + "/2)";
                if (contCuchara == 2)
                {
                    Image fondo = cucharaTogg.GetComponentInChildren<Image>();
                    fondo.color = Color.green;
                    cucharaTogg.isOn = true;
                    termino = SumaPuntos();
                    PlaySound(2);
                }
            }

            if (tenedor1.colocado || tenedor2.colocado)
            {
                if (tenedor1.colocado)
                {
                    contTenedor += 1;
                    tenedor1.colocado = false;
                }

                if (tenedor2.colocado)
                {
                    contTenedor += 1;
                    tenedor2.colocado = false;
                }

                tenedorTxt.text = "Coloca los 2 tenedores en su lugar (" + contTenedor + "/2)";
                if (contTenedor == 2)
                {
                    Image fondo = tenedorTogg.GetComponentInChildren<Image>();
                    fondo.color = Color.green;
                    tenedorTogg.isOn = true;
                    termino = SumaPuntos();
                    PlaySound(2);
                }
            }

            if (cuchillo1.colocado || cuchillo2.colocado)
            {
                if (cuchillo1.colocado)
                {
                    contCuchillo += 1;
                    cuchillo1.colocado = false;
                }

                if (cuchillo2.colocado)
                {
                    contCuchillo += 1;
                    cuchillo2.colocado = false;
                }

                cuchillolTxt.text = "Coloca los 2 cuchillos en su lugar (" + contCuchillo + "/2)";
                if (contCuchillo == 2)
                {
                    Image fondo = cuchilloTogg.GetComponentInChildren<Image>();
                    fondo.color = Color.green;
                    cuchilloTogg.isOn = true;
                    termino = SumaPuntos();
                    PlaySound(2);
                }
            }

            if (plato1.colocado || plato2.colocado)
            {
                if (plato1.colocado)
                {
                    contPlato += 1;
                    plato1.colocado = false;
                }

                if (plato2.colocado)
                {
                    contPlato += 1;
                    plato2.colocado = false;
                }

                platoTxt.text = "Coloca los 2 platos en su lugar (" + contPlato + "/2)";
                if (contPlato == 2)
                {
                    Image fondo = platoTogg.GetComponentInChildren<Image>();
                    fondo.color = Color.green;
                    platoTogg.isOn = true;
                    termino = SumaPuntos();
                    PlaySound(2);
                }
            }

            if (termino && !entro)
            {
                arraySonidos[4].Play();
                canvasFinalMenuUi.SetActive(true);
                Time.timeScale = 0f;
                entro = true;
            }
        }
    }

    private bool SumaPuntos()
    {
        if (contIndividual + contPlato + contVaso + contCuchillo + contTenedor + contCuchara == 12)
        {
            return true;
        }
        
        return false;
    }

    public void PlaySound(int sonido)
    {
        arraySonidos[sonido].Play();
    }
}
