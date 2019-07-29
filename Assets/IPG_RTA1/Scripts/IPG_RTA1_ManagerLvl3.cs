using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class IPG_RTA1_ManagerLvl3 : MonoBehaviour
{
    //Intentos
    public List<string> correcta;
    public List<string> enviada;
    public string enviadaObjeto;
    public int juego = 0;
    public int jugadas = 5;
    public int intento = 0;
    public int buenas;
    public int malas;
    // public int objetosDraggeados;
    public List<int> casos = new List<int>();

    [SerializeField] GameObject imagenCelebracionConfetti;
    [SerializeField] GameObject imagenIntentaloDeNuevo;

    public Sprite spriteEstrellaNeutra;
    public Sprite spriteEstrellaConseguida;
    public Sprite spriteEstrellaNoConseguida;
    public Image[] estrellasGFX;

    //Escenario
    public enum escenarios { estante, verduleria, sendero };
    public escenarios escenario;
    public Sprite fondoEstante;
    public Sprite fondoVerduleria;
    public Sprite fondoSendero;
    public GameObject background;
    public GameObject Texto1;
    public GameObject Texto2;
    public GameObject ObjetoFijo;
    public GameObject btnResponder;
    public GameObject Estante;
    public GameObject EstanteVerduleria;
    public GameObject Fila;
    public GameObject Fade;
    public GameObject PosicionadorFijo;

    //Objetos
    public List<GameObject> ObjetosEstante = new List<GameObject>();
    public List<GameObject> ObjetosVerduleria = new List<GameObject>();
    public List<GameObject> ObjetosSendero = new List<GameObject>();
    public List<GameObject> ObjetosParaOrdenar = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        casos.Add(0);
        SetCasos();
        setearJuego(casos[juego]);

    }

    public void setEscenario(escenarios seleccion)
    {
        Fade.GetComponent<FadeController>().BeginFade(1);
        switch (seleccion)
        {
            case escenarios.estante:
                background.GetComponent<Image>().sprite = fondoEstante;
                Estante.SetActive(true);
                EstanteVerduleria.SetActive(false);
                Fila.SetActive(false);
                ActiveObjects(ObjetosEstante, true);
                ActiveObjects(ObjetosVerduleria, false);
                ActiveObjects(ObjetosSendero, false);
                break;

            case escenarios.verduleria:
                background.GetComponent<Image>().sprite = fondoVerduleria;
                Estante.SetActive(false);
                EstanteVerduleria.SetActive(true);
                Fila.SetActive(false);
                ActiveObjects(ObjetosEstante, false);
                ActiveObjects(ObjetosVerduleria, true);
                ActiveObjects(ObjetosSendero, false);
                break;

            case escenarios.sendero:
                background.GetComponent<Image>().sprite = fondoSendero;
                Estante.SetActive(false);
                EstanteVerduleria.SetActive(false);
                Fila.SetActive(true);
                ActiveObjects(ObjetosEstante, false);
                ActiveObjects(ObjetosVerduleria, false);
                ActiveObjects(ObjetosSendero, true);
                break;
        }
    }

    public void ActiveObjects(List<GameObject> lista, bool estado)
    {
        foreach (GameObject objeto in lista)
        {
            objeto.SetActive(estado);
        }
    }

    public void CambiarEscenario()
    {
        int num = Random.RandomRange(0, 3);
        Debug.Log(num);
        Fade.GetComponent<FadeController>().BeginFade(1);

        List<escenarios> ListaEscenarios = new List<escenarios>();
        ListaEscenarios.Add(escenarios.estante);
        ListaEscenarios.Add(escenarios.verduleria);
        ListaEscenarios.Add(escenarios.sendero);

        setEscenario(ListaEscenarios[num]);
    }

    public void SetCasos()
    {
        int iteraciones = 1;
        do
        {
            int caso = Random.Range(1, 16);
            bool repetido = false;
            foreach (int num in casos)
            {
                if (num == caso)
                {
                    repetido = true;
                }
            }
            if (repetido == false)
            {
                casos.Add(caso);
                iteraciones++;
            }
        }
        while (iteraciones < jugadas);
    }

    public void setearJuego(int indiceCaso)
    {
        btnResponder.SetActive(true);
        List<GameObject> Posicionadores = new List<GameObject>();
        Debug.Log("Juego " + indiceCaso);
        switch (indiceCaso)
        {
            case 0:
                setEscenario(escenarios.estante);
                ObjetoFijo = ObjetosEstante[0];
                Posicionadores = Estante.GetComponent<IPG_RTA1_ListaEstante>().Espacios;
                SetDraggable(ObjetosEstante,true);
                ObjetoFijo.GetComponent<IPG_RTA1_DragController>().setFijo(Posicionadores[2].transform);
                Posicionadores[2].GetComponent<IPG_RTA1_Posicionador>().fijo = true;
                PosicionadorFijo = Posicionadores[2];
                enviada[2] = "robot";
                ObjetosParaOrdenar = ObjetosEstante;
                Texto1.GetComponent<TextMeshProUGUI>().text = "Ubica el barco más arriba que el robot";
                Texto2.GetComponent<TextMeshProUGUI>().text = "Más abajo que el auto.";
                correcta[0] = "auto";
                correcta[1] = "barco";
                correcta[2] = "robot";
                correcta[3] = "";
                correcta[4] = "";
            break;

            case 1:
                setEscenario(escenarios.estante);
                ObjetoFijo = ObjetosEstante[0];
                Posicionadores = Estante.GetComponent<IPG_RTA1_ListaEstante>().Espacios;
                SetDraggable(ObjetosEstante, true);
                ObjetoFijo.GetComponent<IPG_RTA1_DragController>().setFijo(Posicionadores[2].transform);
                enviada[2] = "robot";
                ObjetosParaOrdenar = ObjetosEstante;
                Texto1.GetComponent<TextMeshProUGUI>().text = "Ubica el auto más abajo que el barco";
                Texto2.GetComponent<TextMeshProUGUI>().text = "Más arriba que el robot.";
                correcta[0] = "barco";
                correcta[1] = "auto";
                correcta[2] = "robot";
                correcta[3] = "";
                correcta[4] = "";
            break;

            case 2:
                setEscenario(escenarios.estante);
                ObjetoFijo = ObjetosEstante[0];
                Posicionadores = Estante.GetComponent<IPG_RTA1_ListaEstante>().Espacios;
                SetDraggable(ObjetosEstante, true);
                ObjetoFijo.GetComponent<IPG_RTA1_DragController>().setFijo(Posicionadores[2].transform);
                enviada[2] = "robot";
                ObjetosParaOrdenar = ObjetosEstante;
                Texto1.GetComponent<TextMeshProUGUI>().text = "Ubica el barco más abajo que el robot";
                Texto2.GetComponent<TextMeshProUGUI>().text = "Más abajo que el auto.";
                correcta[0] = "";
                correcta[1] = "";
                correcta[2] = "robot";
                correcta[3] = "barco";
                correcta[4] = "auto";
            break;

            case 3:
                setEscenario(escenarios.estante);
                ObjetoFijo = ObjetosEstante[0];
                Posicionadores = Estante.GetComponent<IPG_RTA1_ListaEstante>().Espacios;
                SetDraggable(ObjetosEstante, true);
                ObjetoFijo.GetComponent<IPG_RTA1_DragController>().setFijo(Posicionadores[2].transform);
                enviada[2] = "robot";
                ObjetosParaOrdenar = ObjetosEstante;
                Texto1.GetComponent<TextMeshProUGUI>().text = "Ubica el auto más arriba que el barco";
                Texto2.GetComponent<TextMeshProUGUI>().text = "Más abajo que el robot.";
                correcta[0] = "";
                correcta[1] = "";
                correcta[2] = "robot";
                correcta[3] = "auto";
                correcta[4] = "barco";
            break;

            case 4:
                setEscenario(escenarios.estante);
                ObjetoFijo = ObjetosEstante[2];
                Posicionadores = Estante.GetComponent<IPG_RTA1_ListaEstante>().Espacios;
                SetDraggable(ObjetosEstante, true);
                ObjetoFijo.GetComponent<IPG_RTA1_DragController>().setFijo(Posicionadores[2].transform);
                enviada[2] = "barco";
                ObjetosParaOrdenar = ObjetosEstante;
                Texto1.GetComponent<TextMeshProUGUI>().text = "Ubica la trompeta más arriba que el barco";
                Texto2.GetComponent<TextMeshProUGUI>().text = "Más abajo que el sombrero.";
                correcta[0] = "sombrero";
                correcta[1] = "trompeta";
                correcta[2] = "barco";
                correcta[3] = "";
                correcta[4] = "";
            break;

            case 5:
                setEscenario(escenarios.estante);
                ObjetoFijo = ObjetosEstante[2];
                Posicionadores = Estante.GetComponent<IPG_RTA1_ListaEstante>().Espacios;
                SetDraggable(ObjetosEstante, true);
                ObjetoFijo.GetComponent<IPG_RTA1_DragController>().setFijo(Posicionadores[2].transform);
                enviada[2] = "barco";
                ObjetosParaOrdenar = ObjetosEstante;
                Texto1.GetComponent<TextMeshProUGUI>().text = "Ubica el sombrero más abajo que la trompeta";
                Texto2.GetComponent<TextMeshProUGUI>().text = "Más arriba que el barco.";
                correcta[0] = "trompeta";
                correcta[1] = "sombrero";
                correcta[2] = "barco";
                correcta[3] = "";
                correcta[4] = "";
            break;

            case 6:
                setEscenario(escenarios.estante);
                ObjetoFijo = ObjetosEstante[2];
                Posicionadores = Estante.GetComponent<IPG_RTA1_ListaEstante>().Espacios;
                SetDraggable(ObjetosEstante, true);
                ObjetoFijo.GetComponent<IPG_RTA1_DragController>().setFijo(Posicionadores[2].transform);
                enviada[2] = "barco";
                ObjetosParaOrdenar = ObjetosEstante;
                Texto1.GetComponent<TextMeshProUGUI>().text = "Ubica la trompeta más abajo que el barco";
                Texto2.GetComponent<TextMeshProUGUI>().text = "Más arriba que el sombrero.";
                correcta[0] = "";
                correcta[1] = "";
                correcta[2] = "barco";
                correcta[3] = "trompeta";
                correcta[4] = "sombrero";
            break;


            case 7:
                setEscenario(escenarios.estante);
                ObjetoFijo = ObjetosEstante[2];
                Posicionadores = Estante.GetComponent<IPG_RTA1_ListaEstante>().Espacios;
                SetDraggable(ObjetosEstante, true);
                ObjetoFijo.GetComponent<IPG_RTA1_DragController>().setFijo(Posicionadores[2].transform);
                enviada[2] = "barco";
                ObjetosParaOrdenar = ObjetosEstante;
                Texto1.GetComponent<TextMeshProUGUI>().text = "Ubica el sombrero más arriba que la trompeta";
                Texto2.GetComponent<TextMeshProUGUI>().text = "Más abajo que el barco.";
                correcta[0] = "";
                correcta[1] = "";
                correcta[2] = "barco";
                correcta[3] = "sombrero";
                correcta[4] = "trompeta";
            break;

            case 8:
                setEscenario(escenarios.verduleria);
                ObjetoFijo = ObjetosVerduleria[0];
                Posicionadores = EstanteVerduleria.GetComponent<IPG_RTA1_ListaEstante>().Espacios;
                SetDraggable(ObjetosVerduleria, true);
                ObjetoFijo.GetComponent<IPG_RTA1_DragController>().setFijo(Posicionadores[2].transform);
                enviada[2] = "limones";
                ObjetosParaOrdenar = ObjetosVerduleria;
                Texto1.GetComponent<TextMeshProUGUI>().text = "Ubica las manzanas a la izquierda de los limones";
                Texto2.GetComponent<TextMeshProUGUI>().text = "A la derecha de las lechugas";
                correcta[0] = "lechugas";
                correcta[1] = "manzanas";
                correcta[2] = "limones";
                correcta[3] = "";
                correcta[4] = "";
            break;

            case 9:
                setEscenario(escenarios.verduleria);
                ObjetoFijo = ObjetosVerduleria[0];
                Posicionadores = EstanteVerduleria.GetComponent<IPG_RTA1_ListaEstante>().Espacios;
                SetDraggable(ObjetosVerduleria, true);
                ObjetoFijo.GetComponent<IPG_RTA1_DragController>().setFijo(Posicionadores[2].transform);
                enviada[2] = "limones";
                ObjetosParaOrdenar = ObjetosVerduleria;
                Texto1.GetComponent<TextMeshProUGUI>().text = "Ubica las lechugas a la derecha de las manzanas";
                Texto2.GetComponent<TextMeshProUGUI>().text = "A la izquierda de los limones";
                correcta[0] = "manzanas";
                correcta[1] = "lechugas";
                correcta[2] = "limones";
                correcta[3] = "";
                correcta[4] = "";
           break;

            case 10:
                setEscenario(escenarios.verduleria);
                ObjetoFijo = ObjetosVerduleria[0];
                Posicionadores = EstanteVerduleria.GetComponent<IPG_RTA1_ListaEstante>().Espacios;
                SetDraggable(ObjetosVerduleria, true);
                ObjetoFijo.GetComponent<IPG_RTA1_DragController>().setFijo(Posicionadores[2].transform);
                enviada[2] = "limones";
                ObjetosParaOrdenar = ObjetosVerduleria;
                Texto1.GetComponent<TextMeshProUGUI>().text = "Ubica las manzanas a la derecha de los limones";
                Texto2.GetComponent<TextMeshProUGUI>().text = "A la izquierda de las lechugas";
                correcta[0] = "";
                correcta[1] = "";
                correcta[2] = "limones";
                correcta[3] = "manzanas";
                correcta[4] = "lechugas";
           break;

            case 11:
                setEscenario(escenarios.verduleria);
                ObjetoFijo = ObjetosVerduleria[0];
                Posicionadores = EstanteVerduleria.GetComponent<IPG_RTA1_ListaEstante>().Espacios;
                SetDraggable(ObjetosVerduleria, true);
                ObjetoFijo.GetComponent<IPG_RTA1_DragController>().setFijo(Posicionadores[2].transform);
                enviada[2] = "limones";
                ObjetosParaOrdenar = ObjetosVerduleria;
                Texto1.GetComponent<TextMeshProUGUI>().text = "Ubica las lechugas a la izquierda de las manzanas";
                Texto2.GetComponent<TextMeshProUGUI>().text = "A la derecha de los limones";
                correcta[0] = "";
                correcta[1] = "";
                correcta[2] = "limones";
                correcta[3] = "lechugas";
                correcta[4] = "manzanas";
            break;

            case 12:
                setEscenario(escenarios.sendero);
                ObjetoFijo = ObjetosSendero[0];
                Posicionadores = Fila.GetComponent<IPG_RTA1_ListaEstante>().Espacios;
                SetDraggable(ObjetosVerduleria, true);
                ObjetoFijo.GetComponent<IPG_RTA1_DragController>().setFijo(Posicionadores[2].transform);
                enviada[2] = "mono";
                ObjetosParaOrdenar = ObjetosSendero;
                Texto1.GetComponent<TextMeshProUGUI>().text = "Ubica al elefante adelante del mono";
                Texto2.GetComponent<TextMeshProUGUI>().text = "Atrás de la jirafa.";
                correcta[0] = "";
                correcta[1] = "";
                correcta[2] = "mono";
                correcta[3] = "elefante";
                correcta[4] = "jirafa";
            break;

            case 13:
                setEscenario(escenarios.sendero);
                ObjetoFijo = ObjetosSendero[0];
                Posicionadores = Fila.GetComponent<IPG_RTA1_ListaEstante>().Espacios;
                SetDraggable(ObjetosVerduleria, true);
                ObjetoFijo.GetComponent<IPG_RTA1_DragController>().setFijo(Posicionadores[2].transform);
                enviada[2] = "mono";
                ObjetosParaOrdenar = ObjetosSendero;
                Texto1.GetComponent<TextMeshProUGUI>().text = "Ubica la jirafa atrás del elefante";
                Texto2.GetComponent<TextMeshProUGUI>().text = "Adelante del mono.";
                correcta[0] = "";
                correcta[1] = "";
                correcta[2] = "mono";
                correcta[3] = "jirafa";
                correcta[4] = "elefante";
           break;

            case 14:
                setEscenario(escenarios.sendero);
                ObjetoFijo = ObjetosSendero[0];
                Posicionadores = Fila.GetComponent<IPG_RTA1_ListaEstante>().Espacios;
                SetDraggable(ObjetosVerduleria, true);
                ObjetoFijo.GetComponent<IPG_RTA1_DragController>().setFijo(Posicionadores[2].transform);
                enviada[2] = "mono";
                ObjetosParaOrdenar = ObjetosSendero;
                Texto1.GetComponent<TextMeshProUGUI>().text = "Ubica al elefante atrás del mono";
                Texto2.GetComponent<TextMeshProUGUI>().text = "Adelante de la jirafa.";
                correcta[0] = "jirafa";
                correcta[1] = "elefante";
                correcta[2] = "mono";
                correcta[3] = "";
                correcta[4] = "";
           break;

            case 15:
                setEscenario(escenarios.sendero);
                ObjetoFijo = ObjetosSendero[0];
                Posicionadores = Fila.GetComponent<IPG_RTA1_ListaEstante>().Espacios;
                SetDraggable(ObjetosVerduleria, true);
                ObjetoFijo.GetComponent<IPG_RTA1_DragController>().setFijo(Posicionadores[2].transform);
                enviada[2] = "mono";
                ObjetosParaOrdenar = ObjetosSendero;
                Texto1.GetComponent<TextMeshProUGUI>().text = "Ubica la jirafa adelante del elefante";
                Texto2.GetComponent<TextMeshProUGUI>().text = "Atrás del mono.";
                correcta[0] = "elefante";
                correcta[1] = "jirafa";
                correcta[2] = "mono";
                correcta[3] = "";
                correcta[4] = "";
           break;
        }

    }

    public void Responder()
    {
        
        if (countInList(enviada) > 2)
        {
            if (juego < jugadas)
            {
                btnResponder.SetActive(false);
                if (compareList(correcta, enviada))
                {
                    Debug.Log("Correcta");
                    Texto1.GetComponent<TextMeshProUGUI>().text = "";
                    Texto2.GetComponent<TextMeshProUGUI>().text = "";
                    EvaluarEstrella(true);
                    buenas++;
                    StartCoroutine(MostrarResultado(true));
                }
                else
                {
                    Debug.Log("Incorrecta");
                    Texto1.GetComponent<TextMeshProUGUI>().text = "";
                    Texto2.GetComponent<TextMeshProUGUI>().text = "";
                    EvaluarEstrella(false);
                    malas++;
                    StartCoroutine(MostrarResultado(false));
                }
                DevolverObjetos(ObjetosParaOrdenar);
            }
            else
            {
                
                Debug.Log("Juego Terminado");
            }
        }else
        {
                Debug.Log("Arrastra mas elementos");
        }


    }

    IEnumerator MostrarResultado(bool respuestaFueCorrecta)
    {
        if (respuestaFueCorrecta)
        {
            imagenCelebracionConfetti.SetActive(true);
            yield return new WaitForSeconds(2.5f);
            ApagarResultado();
        }
        else
        {
            imagenIntentaloDeNuevo.SetActive(true);
            yield return new WaitForSeconds(2.5f);
            ApagarResultado();
        }
    }

    void ApagarResultado()
    {
        imagenCelebracionConfetti.SetActive(false);
        imagenIntentaloDeNuevo.SetActive(false);
        juego++;
        if(juego < jugadas)
        {
            setearJuego(casos[juego]);
        }
    }

    void EvaluarEstrella(bool respuestaCorrecta)
    {
        int posicion = juego;
        Debug.LogWarning("situacion: " + posicion);
        if (estrellasGFX.Length >= posicion)
        {
            if (respuestaCorrecta)
            {
                Debug.Log("IntentandoSetearEstrella");
                estrellasGFX[posicion].overrideSprite = spriteEstrellaConseguida;
            }
            else
            {
                Debug.LogError("Estrella No Conseguida");
                estrellasGFX[posicion].overrideSprite = spriteEstrellaNoConseguida;

                //Color colorEstrella = new Color32(132, 132, 132, 255);
                //estrellasGFX[situacion].color = colorEstrella;
            }
        }
        else
        {
            Debug.LogError("ERROR AL PINTAR LAS ESTRELLAS");
        }
    }

    public bool compareList(List<string>L1, List<string>L2)
    {
        for (int i=0; i<L1.Count; i++)
        {
            if(L1[i] != L2[i])
            {
                Debug.Log(L1[i] + " Vs "+ L2[i]);
                return false;
            }
        }
        return true;
    }

    public void DevolverObjetos (List<GameObject> Lo)
    {
        foreach (GameObject Objeto in Lo)
        {
            Objeto.GetComponent<IPG_RTA1_DragController>().volver();
        }
        PosicionadorFijo.GetComponent<IPG_RTA1_Posicionador>().fijo = false;
    }

    public void SetDraggable(List<GameObject> List, bool activate)
    {
        foreach (GameObject objeto in List)
        {
            objeto.GetComponent<IPG_RTA1_DragController>().draggable = activate;
        }
    }

    public int countInList(List<string> lista)
    {
        int count = 0;
        foreach (string item in lista)
        {
            if(item != "")
            {
                count++;
            }
        }
        return count;
    }

    public void activateObjects(List<GameObject> List, bool activate)
    {
        foreach (GameObject objeto in List)
        {
            objeto.SetActive(activate);
        }
    }
}
