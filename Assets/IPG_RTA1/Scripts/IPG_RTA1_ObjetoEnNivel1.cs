using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IPG_RTA1_ObjetoEnNivel1 : MonoBehaviour
{

    public Sprite sprite1;
    public Sprite sprite2;

    public GameObject objeto1;
    public GameObject objeto2;
    public GameObject Glow;

    public string nombre = "nombre con pronombre"; 
    public string alternativa1 = "Ingrese"; //arrriba,izquierda,lejos
    public string alternativa2 = "Ingrese"; //abajo,derecha,cerca

    public bool change = true;

    // Start is called before the first frame update
    void Start()
    {
        if(change)
        {
            int numero = Random.RandomRange(0, 3);
            //Debug.Log(numero);

            switch (numero)
            {
                case 1:
                    objeto1.GetComponent<Image>().sprite = sprite1;
                    objeto2.GetComponent<Image>().sprite = sprite2;
                    break;

                case 2:
                    objeto1.GetComponent<Image>().sprite = sprite2;
                    objeto2.GetComponent<Image>().sprite = sprite1;
                    break;

                default:
                    objeto1.GetComponent<Image>().sprite = sprite1;
                    objeto2.GetComponent<Image>().sprite = sprite2;
                    break;
            }
        }
    }

    public void ActivateButtons(bool activate)
    {
        objeto1.GetComponent<Button>().enabled = activate;
        objeto2.GetComponent<Button>().enabled = activate;
        Glow.SetActive(activate);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
