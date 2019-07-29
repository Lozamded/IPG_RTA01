using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerificarNivel : MonoBehaviour
{
    public static VerificarNivel verNivel = null;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        Singleton();
    }

    void Singleton()
    {
        if(verNivel == null)
        {
            verNivel = this;
        }
        else if (verNivel != this)
        {
            Destroy(this);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
