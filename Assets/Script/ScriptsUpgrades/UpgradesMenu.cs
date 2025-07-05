using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradesMenu : MonoBehaviour
{
    GameManager gameManager;

    [Header("Botão do Upgrade")]
    public Button botaoUpgrade;

    [Header("Textos UI")]
    public TMP_Text upgrade_preco;
    public TMP_Text upgrade_desc;
    public TMP_Text upgrade_Lv;
    public TMP_Text upgrade_Qtd_Queijo;

    [Header("Componentes")]
    public int preco_Inicial; //preco da compra
    public int novo_Preco; //preco das novas compras
    public float multiplicador_Preco; //multiplicador do preço inicial
    public int queijos_Por_Upgrade; // queijos ganhos por upgrade
    public bool desbloqueado;

    public int level_Upgrade = 0; // level do upgrade


    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        // Começa com a função de compra
        botaoUpgrade.onClick.AddListener(ComprarUpgrade);
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
    }

    public int CalcularPreço()
    {
        int preco = Mathf.RoundToInt(preco_Inicial * Mathf.Pow(multiplicador_Preco, level_Upgrade));
        novo_Preco = preco;
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

    public void ComprarUpgrade()
    {
        
        if (gameManager.cont_Queijos >= preco_Inicial && desbloqueado == false)
        {
            gameManager.cont_Queijos -= preco_Inicial; // subtração do preço inicial do up
            desbloqueado = true;
            Debug.Log("Desbloqueado");
            if (botaoUpgrade.tag == "Up_Botao_Click")
            {
                UpLvUPClick();
                level_Upgrade = 1;
            }
            if (botaoUpgrade.tag == "Up_Botao_Segundos")
            {
                UpLvUPSegundos();
                level_Upgrade = 1;

            }

            botaoUpgrade.onClick.RemoveListener(ComprarUpgrade); // remove essa função do botão
           
        }
        else
        {
            Debug.Log("Queijos insulficiente para desbloquear upgrade");
        }
        
    }

    public void UparUpgrade()
    {
        
    }

    public void UpLvUPClick()
    {
        int preco = CalcularPreço();

        if (gameManager.cont_Queijos >= novo_Preco && desbloqueado == true) //verifica se pode comprar
        {
            gameManager.cont_Queijos -= preco; //subtrai o custo do up
            gameManager.queijos_Por_Click += queijos_Por_Upgrade; //adiciona o upgrade de click
            level_Upgrade++; //adiciona +1 level ao upgrade
            Debug.Log("Upgrade de click upado");
        }
        else
        {
            Debug.Log("Queijo insuficiente para upgrade de click");
        }
    }
    public void UpLvUPSegundos()
    {
        int preco = CalcularPreço();

        if (gameManager.cont_Queijos >= novo_Preco && desbloqueado == true) //verifica se pode comprar
        {
            gameManager.cont_Queijos -= preco; //subtrai o custo do up
            gameManager.queijos_Por_Segundo += queijos_Por_Upgrade; //adiciona o upgrade de segundos
            level_Upgrade++; //adiciona +1 level ao upgrade
            Debug.Log("upgrade de segundos upado");
        }
        else
        {
            Debug.Log("Queijo insuficiente para upgrade de segundos");
        }
    }
}
