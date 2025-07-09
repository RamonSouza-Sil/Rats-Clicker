using System.Collections;
using TMPro;
using UnityEngine;

public class SpawnQueijos : MonoBehaviour
{
    [SerializeField] private RectTransform spawnAreaTexto;
    public GameObject queijoPrefab;
    GameManager gameManager;
    [Header("Prefabs")]
    public GameObject textoFeedback;
    public Transform spawnPointText;
    public Transform spawnPointQueijos;

    [Header("Modificar chance de Spawn")]
    [Range(0, 100)] public float chanceDeSpawn = 80f; // Ex: 80% de chance de spawnar

    [Header("Componentes")]
    public float forceMin = 2f;
    public float forceMax = 5f;
    public float timeDestroy = 2f;
    public float timeDestroyTexto = 2f;
    public float tempoDesaparecer;

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    void Update()
    {
        
    }

    public void TentarSpawnarQueijo()
    {
        float sorte = Random.Range(0f, 100f);
        if (sorte <= chanceDeSpawn)
        {
            SpawnarQueijo();
        }

        SpawnarTexto();
    }

    void SpawnarQueijo()
    {
        GameObject instancia = Instantiate(queijoPrefab, spawnPointQueijos.position, Quaternion.identity);

        Rigidbody2D rb = instancia.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            float force = Random.Range(forceMin, forceMax);
            rb.AddForce(new Vector2(Random.Range(-1f, 1f), 1f) * force, ForceMode2D.Impulse);
        }
        Destroy(instancia, timeDestroy);
    }

    void SpawnarTexto()
    {
        GameObject obj = Instantiate(textoFeedback, spawnAreaTexto);

        TMP_Text textoTMP = obj.GetComponent<TMP_Text>();

        textoTMP.text = "+" + gameManager.queijos_Por_Click.ToString();

        RectTransform rt = obj.GetComponent<RectTransform>();

        Vector2 posAleatoria = GerarPosicaoAleatoriaDentroDoSpawn();
        rt.anchoredPosition = posAleatoria;

        StartCoroutine(AnimarTexto(rt, textoTMP));
    }
    Vector2 GerarPosicaoAleatoriaDentroDoSpawn()
    {
        Vector2 size = spawnAreaTexto.rect.size;
        float x = Random.Range(-size.x / 2f, size.x / 2f);
        float y = Random.Range(-size.y / 2f, size.y / 2f);
        return new Vector2(x, y);
    }
    IEnumerator AnimarTexto(RectTransform rt, TMP_Text textoTMP)
    {
        Vector2 posInicial = rt.anchoredPosition;
        Vector2 posFinal = posInicial + new Vector2(0,50f);
        float tempo = 0f;

        Color corIncial = textoTMP.color;
        corIncial.a = 1f;
        textoTMP.color = corIncial;

        while (tempo < tempoDesaparecer)
        {
            tempo += Time.deltaTime;
            float t = tempo / tempoDesaparecer;

            rt.anchoredPosition = Vector2.Lerp(posInicial, posFinal, t); // movimenta para cima :3
            Color cor = textoTMP.color;
            cor.a = Mathf.Lerp(1, 0, t);
            textoTMP.color = cor;

            yield return null;
        }

        Destroy(rt.gameObject);
    }
}
