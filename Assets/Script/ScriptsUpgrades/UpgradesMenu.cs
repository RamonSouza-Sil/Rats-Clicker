using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UpgradesMenu : MonoBehaviour
{
    GameManager gameManager;

    [Header("Componentes")]
    public TMP_Text upgrade_preco;
    public TMP_Text upgrade_desc;
    public TMP_Text upgrade_Lv;
    public TMP_Text upgrade_Qtd_Queijo;

    public int preco_Inicial;
    public float multiplicador_Preco;
    public int queijos_Por_Upgrade = 1;

    public int level_Upgrade = 0;


    private void Start()
    {
        UpdateUI();
    }

    private void FixedUpdate()
    {
        UpdateUI();
    }
    void UpdateUI()
    {
        upgrade_preco.text = CalcularPreço().ToString();
        upgrade_Lv.text = "Lv." + level_Upgrade.ToString();
        upgrade_Qtd_Queijo.text = "x " + queijos_Por_Upgrade.ToString();
        //upgrade_desc.text = level_Upgrade.ToString() + " x " + queijos_Por_Upgrade + "/s";
    }

    int CalcularPreço()
    {
        int preco = Mathf.RoundToInt( preco_Inicial * Mathf.Pow(multiplicador_Preco, level_Upgrade));
        return preco;
    }

    public float CalcularPorSegundo()
    {
        return queijos_Por_Upgrade * level_Upgrade;
    }

    public void CalcularPorClick()
    {
        gameManager.queijos_Por_Click += queijos_Por_Upgrade;
    }
}
