using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbirTelaRoupas : MonoBehaviour
{
    [Header("Menu de Roupas")]
    public GameObject menuRoupas;
    public GameObject menuConfig;
    public void ClickBotao() //ativa o menu de troca de roupas
    {
        if(menuRoupas.activeInHierarchy == false)
        {
            menuRoupas.SetActive(true);
        }
    }

    public void FecharMenu()
    {
        if(menuRoupas.activeInHierarchy == true)
        {
            menuRoupas.SetActive(false);
        }
    }

    public void FecharMenuConfig()
    {
        if (menuConfig.activeInHierarchy == true)
        {
            menuConfig.SetActive(false);
        }
    }

    public void AbrirMenuConfig()
    {
        if (menuConfig.activeInHierarchy == false)
        {
            menuConfig.SetActive(true);
        }
    }
}
