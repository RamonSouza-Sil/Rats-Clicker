using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptsMoedas : MonoBehaviour
{
    private Rigidbody2D rb;
    public int valor_Moeda;
    public GameManager gamemanager;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.AddForce(new Vector2(Random.Range(-4,4), 5f), ForceMode2D.Impulse);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown()
    {

        Destroy(gameObject);
    }
}
