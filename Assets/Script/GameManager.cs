using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    UpgradesMenu upgradesMenu;

    [Header("Componentes")]
    public int queijos_Por_Segundo;
    public int queijos_Por_Click = 1;
    public int cont_Queijos;
    public int click_contador;



    [Header("Referencias")]
    [SerializeField] GameObject moeda;
    [SerializeField] Transform referencia;

    [Header("Textos UI")]
    public TextMeshProUGUI text_Contador;
    public TextMeshProUGUI text_Queijos_Por_Click;
    public TextMeshProUGUI text_Queijos_Por_Segundo;

    // Start is called before the first frame update
    void Start()
    {
        cont_Queijos = PlayerPrefs.GetInt("quantidadeQueijos", 0);
        queijos_Por_Segundo = PlayerPrefs.GetInt("queijosSegundos", 0);
        queijos_Por_Click = PlayerPrefs.GetInt("queijosClick", 1);
        click_contador = PlayerPrefs.GetInt("contadorClick", 0);

        UpdateUI();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateUI();
    }

    

    public void SaveGame()
    {
        PlayerPrefs.SetInt("quantidadeQueijos", cont_Queijos);
        PlayerPrefs.SetInt("queijosSegundos", queijos_Por_Segundo);
        PlayerPrefs.SetInt("queijosClick", queijos_Por_Click);
        PlayerPrefs.SetInt("contadorClick", click_contador);
        PlayerPrefs.Save();
    }

    public void ClickContador()
    {
        click_contador++;
        cont_Queijos += queijos_Por_Click;
        SaveGame();
        

    }

    void UpdateUI()
    {
        text_Contador.text = FormatNumber(cont_Queijos);
        text_Queijos_Por_Click.text = FormatNumber(queijos_Por_Click) + "Queijos p/c";
        text_Queijos_Por_Segundo.text = FormatNumber(UpgradesMenu.total_Queijos_Segundo) + " Queijos p/s";

    }

    public static string FormatNumber(double number)
    {
        if (number < 1_000_000)
            return number.ToString("0");

        string[] suffixes = {
        "M", "B", "T", "Qa", "Qi", "Sx", "Sp", "Oc", "No",
        "Dc", "Ud", "Dd", "Td", "Qad", "Qid", "Sxd", "Spd", "Ocd", "Nod", "Vg"
    };

        int i = 0;
        number /= 1_000_000; // começa a contagem em milhões

        while (number >= 1000 && i < suffixes.Length - 1)
        {
            number /= 1000;
            i++;
        }

        return number.ToString("0.##") + suffixes[i];
    }


}