using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(CompararRespuesta());
    }

    // Update is called once per frame
    void Update()
    {

    }

    //Grilla, 0 = vacio
    //DEFINIR LOS COLORES SEGUN VALOR
    sbyte[,] valoresGrilla = new sbyte[7, 7]
    {
        {0, 0, 0, 0, 0, 0, 0},
        {0, 0, 0, 0, 0, 0, 0},
        {0, 0, 0, 0, 0, 0, 0},
        {0, 0, 0, 0, 0, 0, 0},
        {0, 0, 0, 0, 0, 0, 0},
        {0, 0, 0, 0, 0, 0, 0},
        {0, 0, 0, 0, 0, 0, 0}
    };



    /// <summary>
    /// Leer los valores de cada cubo en la grilla
    /// </summary>
    void SetearValoresGrilla()
    {

    }



    /// <summary>
    /// Compara los valores de la grilla con los de la respuesta correcta
    /// </summary>
    bool CompararRespuesta()
    {
        int iteraciones = 0;
        for (int i = 0; i < 7; i++)
        {
            //string m = "";
            for (int j = 0; j < 7; j++)
            {
                iteraciones++;
                if (valoresGrilla[i, j] != ejemploRespuesta[i, j])
                {
                    Debug.Log(iteraciones);
                    return false;
                }
            }
            //Debug.Log(m);
        }
        Debug.Log(iteraciones);
        return true;
    }




    //EJEMPLO RESPUESTA CORRECTA
    sbyte[,] ejemploRespuesta = new sbyte[7, 7]
   {
        {0, 0, 0, 0, 0, 0, 0},
        {0, 0, 0, 0, 0, 0, 0},
        {0, 0, 0, 0, 0, 0, 0},
        {0, 0, 0, 0, 0, 0, 0},
        {0, 0, 0, 0, 0, 0, 0},
        {0, 0, 0, 0, 0, 0, 0},
        {0, 0, 0, 0, 0, 0, 0}
   };







    //Icializar
    //sbyte[,][,] arreglo = new sbyte[2,1][,]
    //{
    //
    //};


}


class SituacionesNivel3
{
    public sbyte[,] cuboDado = new sbyte[7, 7];
    public sbyte[,] respuestaCorrecta = new sbyte[7, 7];
}
