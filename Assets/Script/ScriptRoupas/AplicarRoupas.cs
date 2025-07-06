using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AplicarRoupas : MonoBehaviour
{
    [Header("Rato a ser trocado")]
    public GameObject ratoBase;

    
    

    public void TrocarRoupas()
    {
        Image imgSkinNova = gameObject.GetComponent<Image>();
        Sprite novaImagem = imgSkinNova.sprite;
        Image imgSkinBase = ratoBase.GetComponent<Image>();

        if (imgSkinBase != null) 
        { 
            imgSkinBase.sprite = novaImagem;
        }
        else
        {
            Debug.LogWarning("Componente Image não encontrado em " + ratoBase.name);
        }
        
    }
}
