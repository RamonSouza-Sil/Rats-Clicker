using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DesbloquearRoupas : MonoBehaviour
{
    [Header("Referência ao GameManager")]
    public GameManager gameManager;

    [Header("Lista de botões de skins")]
    public List<SkinBotao> botoesDeSkins;

    [Header("UI da Skin Desbloqueada")]
    public GameObject painelSkinDesbloqueada;
    public Image imagemSkinDesbloqueada;
    public CanvasGroup canvasGroupSkinDesbloqueada;
    public float tempoExibicao = 2f;
    public float tempoFade = 0.5f;

    void Update()
    {
        VerificarDesbloqueios();
    }

    void VerificarDesbloqueios()
    {
        foreach (var skinBotao in botoesDeSkins)
        {
            if (!TemFilhosAtivos(skinBotao.botao)) continue;

            if (gameManager.cont_Queijos >= skinBotao.precoEmQueijos)
            {
                DesbloquearBotao(skinBotao.botao);
                StartCoroutine(ExibirSkinDesbloqueada(skinBotao.skinSprite));
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



    IEnumerator ExibirSkinDesbloqueada(Sprite sprite)
    {
        imagemSkinDesbloqueada.sprite = sprite;
        painelSkinDesbloqueada.SetActive(true);

        // Fade In
        yield return StartCoroutine(FadeCanvasGroup(canvasGroupSkinDesbloqueada, 0, 1, tempoFade));

        yield return new WaitForSeconds(tempoExibicao);

        // Fade Out
        yield return StartCoroutine(FadeCanvasGroup(canvasGroupSkinDesbloqueada, 1, 0, tempoFade));

        painelSkinDesbloqueada.SetActive(false);
    }

    IEnumerator FadeCanvasGroup(CanvasGroup cg, float inicio, float fim, float duracao)
    {
        float tempo = 0f;
        while (tempo < duracao)
        {
            tempo += Time.deltaTime;
            cg.alpha = Mathf.Lerp(inicio, fim, tempo / duracao);
            yield return null;
        }
        cg.alpha = fim;
    }

}

[System.Serializable]
public class SkinBotao
{
    public Button botao;
    public int precoEmQueijos;
    public Sprite skinSprite; // Adicione o sprite da skin associada
}
