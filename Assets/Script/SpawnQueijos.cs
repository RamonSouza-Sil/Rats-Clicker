using System.Collections;
using UnityEngine;

public class SpawnQueijos : MonoBehaviour
{
    public GameObject queijoPrefab;
    [Range(0, 100)] public float chanceDeSpawn = 80f; // Ex: 80% de chance de spawnar
    public Transform spawnPoint;
    public float forceMin = 2f;
    public float forceMax = 5f;
    public float timeDrestroy = 2f;

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
    }

    void SpawnarQueijo()
    {
        GameObject instancia = Instantiate(queijoPrefab, spawnPoint.position, Quaternion.identity);

        Rigidbody2D rb = instancia.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            float force = Random.Range(forceMin, forceMax);
            rb.AddForce(new Vector2(Random.Range(-1f, 1f), 1f) * force, ForceMode2D.Impulse);
        }
        Destroy(instancia, timeDrestroy);
    }
}
