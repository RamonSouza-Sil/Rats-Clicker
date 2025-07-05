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

    [Header("Timer")]
    public float tempo_Passado;
    public float intervalo_geracao;

    [Header("Textos UI")]
    public TMP_Text upgrade_preco;
    public TMP_Text upgrade_desc;
    public TMP_Text upgrade_Lv;
    public TMP_Text upgrade_Qtd_Queijo;
    public static double total_Queijos_Segundo;

    [Header("Componentes")]
    public int preco_Inicial; //preco da compra
    public int novo_Preco; //preco das novas compras
    public float multiplicador_Preco; //multiplicador do preço inicial
    public int queijos_Por_Upgrade; // queijos ganhos por upgrade
    public int queijos_Level_Total;
    public bool desbloqueado;

    public int level_Upgrade = 0; // level do upgrade


    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        // Começa com a função de compra
        botaoUpgrade.onClick.AddListener(ComprarUpgrade);
        UpdateUI();
    }

    private void Update()
    {
        

        if (!desbloqueado || queijos_Por_Upgrade <= 0) return;
        if (botaoUpgrade.tag != "Up_Botao_Segundos") return;
        tempo_Passado += Time.deltaTime;

        // upgrade de queijos por segundos
        if(tempo_Passado >= intervalo_geracao)
        {
            int ganho = queijos_Por_Upgrade * level_Upgrade;
            gameManager.queijos_Por_Segundo = ganho;
            UpdateUI();
            gameManager.cont_Queijos += ganho;
            Debug.Log("Gerando " + ganho + " queijos a cada " + intervalo_geracao + " segundos.");
            tempo_Passado = 0;
        }

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
            
            Debug.Log("Desbloqueado | fun ComprarUp");
            if (botaoUpgrade.tag == "Up_Botao_Click")
            {
                level_Upgrade = 1;
                UpdateUI();
            }
            if (botaoUpgrade.tag == "Up_Botao_Segundos")
            {
                level_Upgrade = 1;
                AtualizarTotalQueijosSegundos();
                UpdateUI();
                
            }

            botaoUpgrade.onClick.RemoveListener(ComprarUpgrade);

            if (botaoUpgrade.tag == "Up_Botao_Click")
            {
                botaoUpgrade.onClick.AddListener(UpLvUPClick);
            }
            else if (botaoUpgrade.tag == "Up_Botao_Segundos")
            {
                botaoUpgrade.onClick.AddListener(UpLvUPSegundos);
            }


        }
        else
        {
            Debug.Log("Queijos insulficiente para desbloquear upgrade");
        }
        
    }

    public void UpLvUPClick()
    {
        int preco = CalcularPreço();

        if (gameManager.cont_Queijos >= novo_Preco) //verifica se pode comprar
        {
            gameManager.cont_Queijos -= preco; //subtrai o custo do up
            gameManager.queijos_Por_Click += queijos_Por_Upgrade; //adiciona o upgrade de click
            
            level_Upgrade++; //adiciona +1 level ao upgrade
            Debug.Log("Upgrade de click upado");
            UpdateUI();
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
            
            level_Upgrade++; //adiciona +1 level ao upgrade
            Debug.Log("upgrade de segundos upado");
            AtualizarTotalQueijosSegundos();
            UpdateUI();
        }
        else
        {
            Debug.Log("Queijo insuficiente para upgrade de segundos");
        }
    }

    public static void AtualizarTotalQueijosSegundos()
    {
        total_Queijos_Segundo = 0;

        
        UpgradesMenu[] todosUpgrades = FindObjectsOfType<UpgradesMenu>();

        foreach (var up in todosUpgrades)
        {
            if (up.desbloqueado && up.botaoUpgrade.tag == "Up_Botao_Segundos")
            {
                // Ganha X a cada Y segundos = (X / Y) por segundo
                double ganhoPorSegundo = (up.queijos_Por_Upgrade * up.level_Upgrade) / up.intervalo_geracao;
                total_Queijos_Segundo += ganhoPorSegundo;
            }
        }
    }
}
