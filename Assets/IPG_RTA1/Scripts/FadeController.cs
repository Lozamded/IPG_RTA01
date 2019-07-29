using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeController : MonoBehaviour
{
    public Texture2D fadeOutTexture; //Textura para el fade, puede ser una imagen en negro o una imagen de pantalla de carga
    public float fadeSpeed = 0.8f; //Velocidad a la que ocurre el fade
    public bool FadeAtStart = true;

    private int drawDepth = -1000; //Profundidad del fade;
    private float alpha = 1.0f; //Visibilidad del fade entre 0 y 1
    private int fadeDir = -1; //Direccion hacia donde se realiza el fade -1 

    void Awake()
    {
        if (FadeAtStart == true)
        {
            BeginFade(-1);
        }
    }

    void OnGUI()
    {
        alpha += fadeDir * fadeSpeed * Time.deltaTime;
        alpha = Mathf.Clamp01(alpha);//Forzar que alpha sea un valor entre 0 y 1

        GUI.color = new Color(GUI.color.r, GUI.color.g, GUI.color.b, alpha); //Seteo del valor del alpha
        GUI.depth = drawDepth;
        GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), fadeOutTexture);
    }

    public float BeginFade(int direction)
    {
        fadeDir = direction;
        if (direction == 1)
        {
            StartCoroutine(stopFade(0.65f));
        }
        return (fadeSpeed);

    }

    public IEnumerator stopFade(float seconds)
    {
        Debug.Log("Terminemos el fade");
        yield return new WaitForSeconds(seconds);
        BeginFade(-1);
    }
}
