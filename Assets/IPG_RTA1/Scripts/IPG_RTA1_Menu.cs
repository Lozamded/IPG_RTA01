using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class IPG_RTA1_Menu : MonoBehaviour
{
    public GameObject Panel;
    public TextMeshProUGUI textoPanel;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setMessage(string texto)
    {
        Panel.SetActive(true);
        textoPanel.text = texto;
    }

    public void deactivatePanel()
    {
        Panel.SetActive(false);
    }
}
