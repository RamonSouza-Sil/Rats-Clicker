using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TrocarBackground : MonoBehaviour
{
    [Header("Imagem que exibe o background atual")]
    public Image backgroundAtual;

    [Header("Lista de sprites de fundo disponíveis")]
    public List<Sprite> opcoesDeBackground;

    public void TrocarFundoPorIndice(int indice)
    {
        if (indice >= 0 && indice < opcoesDeBackground.Count)
        {
            backgroundAtual.sprite = opcoesDeBackground[indice];
            Debug.Log("Fundo alterado para: " + opcoesDeBackground[indice].name);
        }
        else
        {
            Debug.LogWarning("Índice de fundo inválido: " + indice);
        }
    }
}

