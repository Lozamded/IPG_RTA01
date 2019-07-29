using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class IPG_RTA1_ManagerLvl2 : MonoBehaviour
{
    //Intentos
    public string correcta;
    public string correctaObjeto;
    public string correctaArrastrable;
    public string enviadaPosicion;
    public string enviadaObjeto;
    public string enviadaArrastrable;
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
    public enum escenarios { lago, verduleria };
    public escenarios escenario;
    public Sprite spriteLago;
    public Sprite spriteVerduleria;
    public GameObject background;
    public GameObject Texto;
    public GameObject Objeto;
    public GameObject btnResponder;
    public GameObject Flechas;
    public GameObject Fade;

    //Objetos
    public List<GameObject> UbicablesLago = new List<GameObject>();
    public List<GameObject> UbicablesVerduleria = new List<GameObject>();
    public List<GameObject> ObjetosLago = new List<GameObject>();
    public List<GameObject> ObjetosVerduleria = new List<GameObject>();
    public List<GameObject> ListaElegida = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        int numero = Random.RandomRange(1, 3);
        //Debug.Log(numero);

        int iteraciones = 0;
        do
        {
            int caso = Random.Range(0, 18);
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
    
        hacerJuego();
    }

    public void setEscenario(escenarios seleccion)
    {
        
        switch (seleccion)
        {
            case escenarios.lago:
                background.GetComponent<Image>().sprite = spriteLago;
                activateObjects(ObjetosVerduleria, false);
                activateObjects(UbicablesVerduleria, false);
                activateObjects(ObjetosLago, true);
                activateObjects(UbicablesLago, true);
                ListaElegida = UbicablesLago;
            break;

            case escenarios.verduleria:
                background.GetComponent<Image>().sprite = spriteVerduleria;
                activateObjects(ObjetosLago, false);
                activateObjects(UbicablesLago, false);
                activateObjects(ObjetosVerduleria, true);
                activateObjects(UbicablesVerduleria, true);
                ListaElegida = UbicablesVerduleria;
                break;
        }
    }

    public void SetearJuego(int indiceCaso)
    {
        switch (indiceCaso)
        {
            case 0:
                correctaObjeto = "l pintor";
                correctaArrastrable = "canasto";
                correcta = "izquierda";
                setEscenario(escenarios.lago);
                Objeto = UbicablesLago[0];
            break;

            case 1:
                correctaObjeto = "l pintor";
                correctaArrastrable = "canasto";
                correcta = "derecha";
                setEscenario(escenarios.lago);
                Objeto = UbicablesLago[0];
            break;

            case 2:
                correctaObjeto = "l pintor";
                correcta = "adelante";
                correctaArrastrable = "canasto";
                setEscenario(escenarios.lago);
                Objeto = UbicablesLago[0];
            break;

            case 3:
                correctaObjeto = "l pintor";
                correcta = "atras";
                correctaArrastrable = "canasto";
                setEscenario(escenarios.lago);
                Objeto = UbicablesLago[0];
            break;

            case 4:
                correctaObjeto = " la nube";
                correcta = "arriba";
                correctaArrastrable = "avion";
                setEscenario(escenarios.lago);
                Objeto = UbicablesLago[1];
            break;

            case 5:
                correctaObjeto = " la nube";
                correcta = "abajo";
                correctaArrastrable = "avion";
                setEscenario(escenarios.lago);
                Objeto = UbicablesLago[1];
            break;

            case 6:
                correctaObjeto = "l pescador";
                correcta = "adelante";
                correctaArrastrable = "botella";
                setEscenario(escenarios.lago);
                Objeto = UbicablesLago[2];
            break;

            case 7:
                correctaObjeto = "l pescador";
                correctaArrastrable = "botella";
                correcta = "atras";
                setEscenario(escenarios.lago);
                Objeto = UbicablesLago[2];
            break;

            case 8:
                correctaObjeto = "l pescador";
                correcta = "izquierda";
                correctaArrastrable = "botella";
                setEscenario(escenarios.lago);
                Objeto = UbicablesLago[2];
            break;

            case 9:
                correctaObjeto = "l pescador";
                correcta = "derecha";
                correctaArrastrable = "botella";
                setEscenario(escenarios.lago);
                Objeto = UbicablesLago[2];
                break;

            case 10:
                correctaObjeto = " las manzanas";
                correcta = "izquierda";
                correctaArrastrable = "sandia";
                setEscenario(escenarios.verduleria);
                Objeto = UbicablesVerduleria[0];
                break;

            case 11:
                correctaObjeto = " las manzanas";
                correcta = "derecha";
                correctaArrastrable = "sandia";
                setEscenario(escenarios.verduleria);
                Objeto = UbicablesVerduleria[0];
                break;

            case 12:
                correctaObjeto = " las manzanas";
                correcta = "arriba";
                correctaArrastrable = "sandia";
                setEscenario(escenarios.verduleria);
                Objeto = UbicablesVerduleria[0];
                break;

            case 13:
                correctaObjeto = " las manzanas";
                correcta = "abajo";
                correctaArrastrable = "sandia";
                setEscenario(escenarios.verduleria);
                Objeto = UbicablesVerduleria[0];
                break;

            case 14:
                correctaObjeto = "l vendedor";
                correcta = "adelante";
                correctaArrastrable = "frutillas";
                setEscenario(escenarios.verduleria);
                Objeto = UbicablesVerduleria[1];
                break;

            case 15:
                correctaObjeto = "l vendedor";
                correcta = "atras";
                correctaArrastrable = "frutillas";
                setEscenario(escenarios.verduleria);
                Objeto = UbicablesVerduleria[1];
                break;

            case 16:
                correctaObjeto = "l carro";
                correcta = "encima";
                correctaArrastrable = "gata";
                setEscenario(escenarios.verduleria);
                Objeto = UbicablesVerduleria[2];
                break;

            case 17:
                correctaObjeto = "l carro";
                correcta = "debajo";
                correctaArrastrable = "gata";
                setEscenario(escenarios.verduleria);
                Objeto = UbicablesVerduleria[2];
                break;

        }

        string nombreObjeto = Objeto.GetComponent<IPG_RTA1_DragController>().nombre;
        Objeto.SetActive(true);
        Objeto.GetComponent<IPG_RTA1_DragController>().volver();
        Objeto.GetComponent<IPG_RTA1_DragController>().draggable = true;
        if (Flechas != null)
        {
            Flechas.GetComponent<IPG_RTA1_FlechasController>().Activate(false);
        }
        enviadaObjeto = "";
        enviadaPosicion = "";
        enviadaArrastrable = "";
        intento = 0;
        string complemento = " ";

        if (correcta == "izquierda" || correcta == "derecha")
        {
            complemento = " a la ";
        }
        
       Texto.GetComponent<TextMeshProUGUI>().text = "Ubica " +nombreObjeto + complemento + correcta + " de" + correctaObjeto;
    }

    public void GetEnviada()
    {
        if(enviadaPosicion != "" || enviadaPosicion != "")
        {
            //Debug.Log("Respuesta " + enviadaPosicion + " de" + enviadaObjeto);
            respuesta();
        }
        Debug.Log("No has seleccionado nada");
    }


    public void hacerJuego()
    {
        if (juego < jugadas)
        {
            Fade.GetComponent<FadeController>().BeginFade(1);
            SetearJuego(casos[juego]);
            btnResponder.SetActive(true);
        }
        else
        {
            Debug.Log("Juego terminado");
            btnResponder.SetActive(false);
        }

    }

    public void respuesta()
    {
        btnResponder.SetActive(false);
        Debug.Log(enviadaPosicion + " VS " + correcta);
        if (enviadaPosicion == correcta) Debug.Log("Se cumple");else Debug.Log("NO se cumple");
        Debug.Log(enviadaObjeto + " VS " + correctaObjeto);
        if (enviadaObjeto == correctaObjeto) Debug.Log("Se cumple"); else Debug.Log("NO se cumple");
        Debug.Log(enviadaArrastrable + " VS " + correctaArrastrable);
        if (enviadaArrastrable == correctaArrastrable) Debug.Log("Se cumple"); else Debug.Log("NO se cumple");

    
        if (enviadaPosicion == correcta && enviadaObjeto == correctaObjeto  && enviadaArrastrable == correctaArrastrable)
        {
            Debug.Log("Correcto");
            EvaluarEstrella(true);
            buenas++;
            Objeto.GetComponent<IPG_RTA1_DragController>().draggable = false;
            StartCoroutine(MostrarResultado(true));
            Texto.GetComponent<TextMeshProUGUI>().text = "";
        }
        else
        {
            Debug.Log("Incorrecto");
            if (intento > 0)
            {
                Texto.GetComponent<TextMeshProUGUI>().text = "";
                EvaluarEstrella(false);
                malas++;
                StartCoroutine(MostrarResultado(false));
                Objeto.GetComponent<IPG_RTA1_DragController>().draggable = false;
                Objeto.GetComponent<IPG_RTA1_DragController>().desactivar = true;
            }
            else
            {
                Debug.Log("Gastar Intento");
                intento++;
                Flechas = GameObject.Find("Flechas de" + correctaObjeto);
                //Debug.Log("Flechas de" + correctaObjeto);
                Debug.Log(Flechas);
                if (Flechas != null)
                {
                    //Debug.Log("Encontradas");
                    Flechas.GetComponent<IPG_RTA1_FlechasController>().Activate(true);
                }
            }
            volverObjetos(ListaElegida);
            //Objeto.GetComponent<IPG_RTA1_DragController>().volver();
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
        hacerJuego();
    }

    void EvaluarEstrella(bool respuestaCorrecta)
    {
        Debug.Log("Evaluar estrella");
        int posicion = juego;
        Debug.Log("situacion: " + posicion);
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

    public void activateObjects (List<GameObject> List,bool activate)
    {
        foreach (GameObject objeto in List)
        {
            objeto.SetActive(activate);
        }
    }

    public void volverObjetos(List<GameObject> List)
    {
        foreach (GameObject objeto in List)
        {
            objeto.GetComponent<IPG_RTA1_DragController>().volver();
        }
    }

    /*
    void deactivateButtonsList(List<GameObject> List)
    {
        foreach (GameObject objeto in List)
        {
            objeto.GetComponent<IPG_RTA1_ObjetoEnNivel1>().ActivateButtons(false);
        }
    }
    */
}
