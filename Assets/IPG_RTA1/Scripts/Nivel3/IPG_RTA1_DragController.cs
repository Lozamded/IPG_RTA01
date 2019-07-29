using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IPG_RTA1_DragController : MonoBehaviour
{
    public bool draggable = true;
    public bool specialSprite = false;

    public string nombre;
    public string respuesta;
    public GameObject manager;
    public int posicionador;
    public GameObject sprite;
    //public GameObject UltimoColisionador;

    public Vector3 escalaCompleta = new Vector3(1, 1, 1);
    public Vector3 escalaReducida = new Vector3(0.7f, 0.7f, 1);


    enum ColorCubo { rojo, azul, amarillo, rosado, verde, naranjo}
    [SerializeField] ColorCubo color = new ColorCubo();

    public Vector3 posInicial = new Vector3(0, 0, 0);

    bool dentroDeGrilla = false;
    Transform ultimoCuboTocado = null;

    public int nivel = 2;


    private void Awake()
    {
        posInicial = gameObject.transform.position;
    }


    public bool isDragging = false;


    public bool sizeMini = true;

    bool isSnapped = false;
    Transform snapeedTo = null;

    public bool desactivar = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "GridSquare" && isDragging && other.GetComponent<IPG_RTA1_Posicionador>().fijo == false && other.GetComponent<IPG_RTA1_Posicionador>().ocupado == false)
        {
            Debug.Log(other.name);
            other.GetComponent<IPG_RTA1_Posicionador>().ocupado = true;
            other.GetComponent<IPG_RTA1_Posicionador>().objeto = this.name;
            respuesta = other.name;

            switch (nivel)
            {
                case 2:
                    manager.GetComponent<IPG_RTA1_ManagerLvl2>().enviadaPosicion = other.name;
                    manager.GetComponent<IPG_RTA1_ManagerLvl2>().enviadaObjeto = other.GetComponent<IPG_RTA1_Posicionador>().nombreObjeto;
                    manager.GetComponent<IPG_RTA1_ManagerLvl2>().enviadaArrastrable = this.name;
                break;

                case 3:
                    posicionador = int.Parse(other.name);
                    manager.GetComponent<IPG_RTA1_ManagerLvl3>().enviada[posicionador] = this.name;
                break;
            }
            transform.position = other.transform.position;
            isSnapped = true;
            snapeedTo = other.transform;
            //SnappedSetting(true);
            ultimoCuboTocado = other.transform;
        }

        if (other.tag == "Grilla")
        {
            dentroDeGrilla = true;
        }

    }

    public void setFijo( Transform Position)
    {
        transform.position = Position.position;
        transform.localScale = escalaCompleta;
        if(specialSprite)sprite.transform.localScale = escalaCompleta;
        isSnapped = true;
        snapeedTo = Position;
        dentroDeGrilla = true;
        draggable = false;
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("Dejo el posicionador " + other.name);
        if(other.GetComponent<IPG_RTA1_Posicionador>())
        {
            Debug.Log(other.GetComponent<IPG_RTA1_Posicionador>().objeto + " VS " + this.name);
            if (other.GetComponent<IPG_RTA1_Posicionador>().objeto == this.name && other.tag == "GridSquare")
            {
                Debug.Log("Desactivar ");
                other.GetComponent<IPG_RTA1_Posicionador>().ocupado = false;

                if (nivel == 3)
                {
                    posicionador = int.Parse(other.name);
                    if (other.GetComponent<IPG_RTA1_Posicionador>().fijo == false)
                    {
                        Debug.Log("Dejo el posicionador " + posicionador);
                        manager.GetComponent<IPG_RTA1_ManagerLvl3>().enviada[posicionador] = "";
                    }
                }

                if (nivel == 2)
                {
                    manager.GetComponent<IPG_RTA1_ManagerLvl2>().enviadaArrastrable = "";
                }
            }

            if (other.tag == "Grilla")
            {
                dentroDeGrilla = false;
                ultimoCuboTocado = null;
            }


        }    
    }

    void OnMouseDrag()
    {
        if(draggable)
        {
            isDragging = true;
            SnappedSetting(false);

            if (sizeMini)
            {
                transform.localScale = escalaCompleta;
                if (specialSprite) sprite.transform.localScale = escalaCompleta;
                BoxCollider colliderBox = GetComponent<BoxCollider>();
                colliderBox.size = escalaReducida;
                sizeMini = false;
            }
            if (isSnapped == false)
            {
                float distance_to_screen = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;
                Vector3 pos_move = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, distance_to_screen));
                transform.position = new Vector3(pos_move.x, pos_move.y, transform.position.z);
            }

            else if (isSnapped == true)
            {
                float distance_to_screen = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;
                Vector3 pos_move = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, distance_to_screen));
                if (Vector3.Distance(pos_move, snapeedTo.position) > 0.5f)
                {
                    isSnapped = false;
                    snapeedTo = null;
                }
            }
        }
    }

    private void OnMouseUp()
    {
        if(draggable)
        {
            isDragging = false;
            if (isSnapped == false)
            {
                StartCoroutine(MoverCuboAResetPosition());
                ResetCubo();
            }
            else
            {
                SnappedSetting(true);
            }
            //Debug.Log("yas");
        }
    }


    void SnappedSetting(bool snappedToGrid)
    {
        if (snappedToGrid)
        {
            BoxCollider colliderBox = GetComponent<BoxCollider>();
            colliderBox.size = escalaCompleta;
            transform.localScale = escalaCompleta;
            if (specialSprite) sprite.transform.localScale = escalaCompleta;
            Debug.Log("Ay conchales");
        }
        else
        {
            sizeMini = true;
        }

    }

    public void ResetCubo()
    {
        Debug.Log("Resetear");
        transform.localScale = escalaReducida;
        if (specialSprite) sprite.transform.localScale = escalaReducida;
        BoxCollider colliderBox = GetComponent<BoxCollider>();
        colliderBox.size = escalaCompleta;
        sizeMini = true;
        if(nivel == 2)
        {
            manager.GetComponent<IPG_RTA1_ManagerLvl2>().enviadaObjeto = "";
            manager.GetComponent<IPG_RTA1_ManagerLvl2>().enviadaPosicion = "";
            if (desactivar)
            {
                Debug.Log("Llegue");
                desactivar = false;
                this.gameObject.SetActive(false);
                //manager.GetComponent<IPG_RTA1_ManagerLvl2>().DoGame();
            
            }
        }

    }

    public void volver()
    {
        //Debug.Log("Volver");
        isDragging = false;
        isSnapped = false;
        StartCoroutine(MoverCuboAResetPosition());
        ResetCubo();
    }


    IEnumerator MoverCuboAResetPosition()
    {
        while(transform.position != posInicial)
        {
            transform.position = Vector3.MoveTowards(transform.position, posInicial, 0.5f);
            yield return new WaitForEndOfFrame();
        }


    }

    /// <summary>
    /// Hace spawn de un nuevo cubo este color si no hay un cubo en
    /// la posición inicial
    /// </summary>
    void SpawnNewCube()
    {

    }

}
