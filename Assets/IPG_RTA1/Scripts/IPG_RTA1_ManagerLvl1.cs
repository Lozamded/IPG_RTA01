using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class IPG_RTA1_ManagerLvl1 : MonoBehaviour
{
    //Intentos
    public string correcta;
    public string enviada;
    public int numCorrecta;
    public int juego = 0;
    public int jugadas = 4;
    public int buenas;
    public int malas;
    public List<int> casos = new List<int>();

    [SerializeField] GameObject imagenCelebracionConfetti;
    [SerializeField] GameObject imagenIntentaloDeNuevo;
    [SerializeField] GameObject imagenGanaste;

    public Sprite spriteEstrellaNeutra;
    public Sprite spriteEstrellaConseguida;
    public Sprite spriteEstrellaNoConseguida;
    public Image[] estrellasGFX;

    //Escenario
    public enum escenarios { dormitorio, paisaje };
    public escenarios escenario;
    public Sprite spriteDormitorio;
    public Sprite spritePaisaje;
    public GameObject background;
    public GameObject Texto;
    public GameObject Objeto;
    public GameObject Fade;

    //Objetos
    public List<GameObject> ObjetosDormitorio = new List<GameObject>();
    public List<GameObject> ObjetosPaisaje = new List<GameObject>();


    // Start is called before the first frame update
    void Start()
    {
        int numero = Random.RandomRange(1,3);
        Debug.Log(numero);

        int iteraciones = 0;
        do
        {
            int caso = Random.Range(0, 11);
            //Debug.Log("Numero " + numero);
            bool repetido = false;
            foreach (int num in casos)
            {
                if (num == caso)
                {
                    //Debug.Log("Repetido");
                    repetido = true;
                }
            }
            if (repetido == false)
            {
                casos.Add(caso);
                iteraciones++;
                //Debug.Log("Se añade el " + numero);
            }
        }
        while (iteraciones < jugadas);
        hacerJuego();

    }

    public void SetearJuego(int indiceObjeto)
    {

        Debug.Log("Caso " + indiceObjeto);
        switch (indiceObjeto)
        {
            case 0:
                Objeto = ObjetosDormitorio[0];
                break;

            case 1:
                Objeto = ObjetosDormitorio[1];
                break;

            case 2:
                Objeto = ObjetosDormitorio[2];
                break;

            case 3:
                Objeto = ObjetosDormitorio[3];
                break;

            case 4:
                Objeto = ObjetosDormitorio[4];
                break;

            case 5:
                Objeto = ObjetosDormitorio[5];
                break;

            case 6:
                Objeto = ObjetosPaisaje[0];
                Objeto.GetComponent<IPG_RTA1_ObjetoEnNivel1>().alternativa1 = "izquierda";
                Objeto.GetComponent<IPG_RTA1_ObjetoEnNivel1>().alternativa2 = "derecha";
                break;

            case 7:
                Objeto = ObjetosPaisaje[1];
                break;

            case 8:
                Objeto = ObjetosPaisaje[2];
                Objeto.GetComponent<IPG_RTA1_ObjetoEnNivel1>().alternativa1 = "izquierda";
                Objeto.GetComponent<IPG_RTA1_ObjetoEnNivel1>().alternativa2 = "derecha";
                break;

            case 9:
                Objeto = ObjetosPaisaje[2];
                Objeto.GetComponent<IPG_RTA1_ObjetoEnNivel1>().alternativa1 = "lejos";
                Objeto.GetComponent<IPG_RTA1_ObjetoEnNivel1>().alternativa2 = "cerca";
                break;

            case 10:
                Objeto = ObjetosPaisaje[0];
                Objeto.GetComponent<IPG_RTA1_ObjetoEnNivel1>().alternativa1 = "lejos";
                Objeto.GetComponent<IPG_RTA1_ObjetoEnNivel1>().alternativa2 = "cerca";
                break;
        }

        if (indiceObjeto < 6)
        {
            setEscenario(escenarios.dormitorio);
        } else
        {
            setEscenario(escenarios.paisaje);
        }

        numCorrecta = Random.Range(1, 3);
        Debug.Log("Correcta " + numCorrecta);

        Objeto.GetComponent<IPG_RTA1_ObjetoEnNivel1>().ActivateButtons(true);
        

        switch (numCorrecta)
        {
            case 1:
                correcta = Objeto.GetComponent<IPG_RTA1_ObjetoEnNivel1>().alternativa1;
            break;

            case 2:
                correcta = Objeto.GetComponent<IPG_RTA1_ObjetoEnNivel1>().alternativa2;
            break;
        }
        

        string complemento = "";
        string nombre = Objeto.GetComponent<IPG_RTA1_ObjetoEnNivel1>().nombre;

        if (correcta == "izquierda" || correcta == "derecha")
        {
            complemento = " a la ";
        }

        Texto.GetComponent<TextMeshProUGUI>().text = "¿Que " + nombre + " está " + complemento + correcta + "?";
    }

    public void GetEnviada (int recibida)
    {

        Debug.Log("Respuesta " + enviada);
        Texto.GetComponent<TextMeshProUGUI>().text = "";
        Objeto.GetComponent<IPG_RTA1_ObjetoEnNivel1>().ActivateButtons(false);
        respuesta(recibida);
    }


    public void hacerJuego()
    {
        if(juego < jugadas)
        {
            Fade.GetComponent<FadeController>().BeginFade(1);
            SetearJuego(casos[juego]);
        }
        else
        {
            Debug.Log("Juego terminado");
            if(buenas > malas)
            {
                imagenGanaste.SetActive(true);
            }
            
        }

    }

    public void respuesta(int answer)
    {
        
        if (answer == numCorrecta)
        {
            Debug.Log("Correcto");
            EvaluarEstrella(true);
            buenas++;
            StartCoroutine(MostrarResultado(true));
        }
        else
        {
            Debug.Log("Incorrecto");
            EvaluarEstrella(false);
            malas++;
            StartCoroutine(MostrarResultado(false));
        }
    }

    void ApagarResultado()
    {
        imagenCelebracionConfetti.SetActive(false);
        imagenIntentaloDeNuevo.SetActive(false);
        juego++;
        hacerJuego();
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

    void deactivateButtonsList(List<GameObject> List)
    {
        foreach (GameObject objeto in List)
        {
            objeto.GetComponent<IPG_RTA1_ObjetoEnNivel1>().ActivateButtons(false);
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

    public void setEscenario(escenarios escenario)
    {
        switch (escenario)
        {
            case escenarios.dormitorio:
                background.GetComponent<Image>().sprite = spriteDormitorio;
                activateObjects(ObjetosPaisaje, false);
                activateObjects(ObjetosDormitorio, true);
                deactivateButtonsList(ObjetosDormitorio);
                break;


            case escenarios.paisaje:
                background.GetComponent<Image>().sprite = spritePaisaje;
                activateObjects(ObjetosDormitorio, false);
                activateObjects(ObjetosPaisaje, true);
                deactivateButtonsList(ObjetosPaisaje);
                break;

            default:
                Objeto = ObjetosDormitorio[0];
                break;
        }
    }


    public void activateObjects(List<GameObject> List, bool activate)
    {
        foreach (GameObject objeto in List)
        {
            objeto.SetActive(activate);
        }
    }
}
