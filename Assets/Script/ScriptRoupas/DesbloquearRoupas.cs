using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
public class DesbloquearRoupas : MonoBehaviour
{
    [Header("Referência ao GameManager")]
    public GameManager gameManager;

    [Header("Lista de botões de skins")]
    public List<SkinBotao> botoesDeSkins;

    void Update()
    {
        VerificarDesbloqueios();
    }

    void VerificarDesbloqueios()
    {
        foreach (var skinBotao in botoesDeSkins)
        {
            
            if (!TemFilhosAtivos(skinBotao.botao)) continue;

            // Verifica se o jogador tem queijos suficientes
            if (gameManager.cont_Queijos >= skinBotao.precoEmQueijos)
            {
                DesbloquearBotao(skinBotao.botao);
            }
        }
    }

    
    void DesbloquearBotao(Button botao)
    {
        foreach (Transform filho in botao.transform)
        {
            filho.gameObject.SetActive(false);
        }

        botao.interactable = true;
        Debug.Log("Skin desbloqueada: " + botao.name);
    }

    
    bool TemFilhosAtivos(Button botao)
    {
        foreach (Transform filho in botao.transform)
        {
            if (filho.gameObject.activeSelf)
                return true;
        }
        return false;
    }

}

[System.Serializable]
public class SkinBotao
{
    public Button botao;           // O botão da skin
    public int precoEmQueijos;     // Quantidade necessária de queijos para desbloquear
}

