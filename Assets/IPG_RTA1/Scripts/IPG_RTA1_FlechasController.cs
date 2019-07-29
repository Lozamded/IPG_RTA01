using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IPG_RTA1_FlechasController : MonoBehaviour
{
    public List<GameObject> Flechas = new List<GameObject>();

    public void Activate(bool active)
    {
        foreach (GameObject flecha in Flechas)
        {
            flecha.SetActive(active);
        }
    }
}
