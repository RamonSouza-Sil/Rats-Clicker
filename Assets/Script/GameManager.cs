using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public int cont_Queijos = 0;

    public GameObject moeda;
    public Transform referencia;
    public TextMeshProUGUI text_Contador;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ClickContador()
    {
        cont_Queijos++;
        text_Contador.text = cont_Queijos.ToString();
        
    }

    
}
