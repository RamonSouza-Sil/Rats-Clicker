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

    [Header("Chaves Unicas")]
    public string upgradeID;

    [Header("Botao do Upgrade")]
    public Button botaoUpgrade;
    public Image iconButton;

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
    public bool desbloqueado;
    public int preco_Inicial; //preco da compra
    public int novo_Preco; //preco das novas compras
    public float multiplicador_Preco; //multiplicador do pre�o inicial
    public int queijos_Por_Upgrade; // queijos ganhos por upgrade
    public int queijos_Level_Total;
    public int valor_Total_Queijos_Upgrades;
    
    

    public int level_Upgrade = 0; // level do upgrade


    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        Image imageBotao = botaoUpgrade.GetComponent<Image>();
        // Comeca com a funcao de compra
        

        // salva
        novo_Preco = PlayerPrefs.GetInt("novoPreco" + upgradeID, novo_Preco);
        queijos_Level_Total = PlayerPrefs.GetInt("queijosLevelTotal" + upgradeID, queijos_Level_Total);
        level_Upgrade = PlayerPrefs.GetInt("levelUpgrade" + upgradeID, level_Upgrade);
        desbloqueado = PlayerPrefs.GetInt("upgradeDesbloqueado" + upgradeID, 0) == 1;

        

        if (desbloqueado)
        {
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
            botaoUpgrade.onClick.AddListener(ComprarUpgrade);
        }
        CalcularPreco();
        UpdateUIUp();
        
        
    }

    private void Update()
    {
        
        if (queijos_Por_Upgrade <= 0)
        {
            Debug.Log($"[{upgradeID}] queijos_Por_Upgrade é 0.");
            return;
        }


        if (botaoUpgrade.tag != "Up_Botao_Segundos") return;
        tempo_Passado += Time.deltaTime;
        CalcularPorSegundo();

        

    }

    private void BloqueioBotao()
    {
        Color corIconBloqueado = new Color(31f / 255f, 24f / 255f, 2f / 255f);
        Color corButtonBloqueado = new Color(31f / 255f, 24f / 255f, 2f / 255f);

        


        if (desbloqueado == false)
        {
            
            botaoUpgrade.image.color = corButtonBloqueado;
            iconButton.color = corIconBloqueado;
        }
        else
        {
            botaoUpgrade.interactable = true;
            botaoUpgrade.image.color = Color.white;
            iconButton.color = Color.white;

        }
    }
    void UpdateUIUp()
    {
        upgrade_preco.text = CalcularPreco().ToString();
        upgrade_Lv.text = "Lv." + level_Upgrade.ToString();
        upgrade_Qtd_Queijo.text = "x " + valor_Total_Queijos_Upgrades.ToString();

        BloqueioBotao();
    }

    public int CalcularPreco()
    {
        int preco = Mathf.RoundToInt(preco_Inicial * Mathf.Pow(multiplicador_Preco, level_Upgrade));
        novo_Preco = preco;
        return preco;
        

    }

    public void CalcularPorSegundo()
    {
        if (tempo_Passado >= intervalo_geracao)
        {
            int ganho = queijos_Por_Upgrade * level_Upgrade;            
            gameManager.queijos_Por_Segundo = ganho;
            
            UpdateUIUp();
            
            gameManager.cont_Queijos += ganho;
            Debug.Log("Gerando " + ganho + " queijos a cada " + intervalo_geracao + " segundos.");
            tempo_Passado = 0;
        }
        
    }

   

    public void ComprarUpgrade()
    {
        int preco = CalcularPreco();
        if (gameManager.cont_Queijos >= preco_Inicial && desbloqueado == false)
        {
            gameManager.cont_Queijos -= preco_Inicial; // subtracao do preco inicial do up
            desbloqueado = true;
            Debug.Log($"{upgradeID} Desbloqueado | fun ComprarUp");

            PlayerPrefs.SetInt("upgradeDesbloqueado" + upgradeID, 1);

            if (botaoUpgrade.tag == "Up_Botao_Click")
            {
                gameManager.queijos_Por_Click += queijos_Por_Upgrade;
                botaoUpgrade.onClick.AddListener(UpLvUPClick);
                level_Upgrade++;
                UpdateUIUp();
            }
            if (botaoUpgrade.tag == "Up_Botao_Segundos")
            {
                botaoUpgrade.onClick.AddListener(UpLvUPSegundos);
                level_Upgrade++;
                AtualizarTotalQueijosSegundos();
                UpdateUIUp();

            }

            botaoUpgrade.onClick.RemoveListener(ComprarUpgrade);
            SaveUpgrades();
        }
        else
        {
            Debug.Log("Queijos insulficiente para desbloquear upgrade");
            UpdateUIUp();
        }

    }

    public void UpLvUPClick()
    {
        int preco = CalcularPreco();
        Debug.Log($"o preço do: {upgradeID} é {preco}");

        if (gameManager.cont_Queijos >= preco) //verifica se pode comprar
        {
            gameManager.cont_Queijos -= preco; //subtrai o custo do up
            gameManager.queijos_Por_Click += queijos_Por_Upgrade;
            valor_Total_Queijos_Upgrades += queijos_Por_Upgrade; //adiciona o upgrade de click
            Debug.Log($"a quantidade de queijos pro click é:{gameManager.queijos_Por_Click}");
            
            level_Upgrade++; //adiciona +1 level ao upgrade
            Debug.Log("Upgrade de click upado");
            UpdateUIUp();
            SaveUpgrades();
        }
        else
        {
            Debug.Log("Queijo insuficiente para upgrade de click");
            UpdateUIUp();
        }
    }
    public void UpLvUPSegundos()
    {
        int preco = CalcularPreco();

        if (gameManager.cont_Queijos >= preco ) //verifica se pode comprar
        {
            gameManager.cont_Queijos -= preco; //subtrai o custo do up
            valor_Total_Queijos_Upgrades += queijos_Por_Upgrade;
            level_Upgrade++; //adiciona +1 level ao upgrade
            Debug.Log("upgrade de segundos upado");
            
            AtualizarTotalQueijosSegundos();
            UpdateUIUp();
            SaveUpgrades();

        }
        else
        {
            Debug.Log("Queijo insuficiente para upgrade de segundos");
            UpdateUIUp();
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


    public void SaveUpgrades()
    {
        PlayerPrefs.SetInt("novoPreco" + upgradeID,novo_Preco);
        PlayerPrefs.SetInt("queijosLevelTotal" + upgradeID, queijos_Level_Total);
        PlayerPrefs.SetInt("levelUpgrade" + upgradeID, level_Upgrade);
        PlayerPrefs.SetInt("upgradeDesbloqueado" + upgradeID, desbloqueado ? 1 : 0);
    }
}