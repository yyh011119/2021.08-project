using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Base : LivingEntity
{
    GameObject[] unit;
    GameObject[] enemy;
    GameObject[] UI;

    private Text resultText;
    private GameObject resultWindow;
    public float hpPercent;


    // Start is called before the first frame update
    protected override void Start()
    {
        if (this.tag == "Ally")
        {
            health += PlayerPrefs.GetInt("Upgrade_Hp")*50;
        }


        currentHealth = health;
        currentDamage = damage;
        currentAttackSpeed = attackSpeed;
        HpbarCreate(1);

        resultWindow = GameObject.Find("Canvas").transform.Find("ResultWindow").gameObject;
        resultText = resultWindow.transform.Find("ResultText").GetComponent<Text>();
    }

    // Update is called once per frame
    protected override void Update()
    {
        hpPercent = currentHealth / health;
        unit = GameObject.FindGameObjectsWithTag("Ally");
        enemy = GameObject.FindGameObjectsWithTag("Enemy");
        UI = GameObject.FindGameObjectsWithTag("UI");


        HpbarCreate(2);

        if (currentHealth <= 0 & !isDie)
        {
            isDie = true;
            gameOver();
            Destroy(gameObject, 0);
        }
    }

    void gameOver()
    {
        
        resultWindow.SetActive(true);
        resultWindow.transform.SetAsLastSibling();
        if(this.tag == "Ally")
        {
            resultText.text = "Game Over";
            resultWindow.transform.Find("Retry").gameObject.SetActive(true);
        }
        if (this.tag == "Enemy")
        {
            resultText.text = "You Win";
            resultWindow.transform.Find("ToStage").gameObject.SetActive(true);
            PlayerPrefs.SetInt("Level",(SceneManager.GetActiveScene().buildIndex));
        }



        for (int i = 0; i < unit.Length; i++)
        {
            Destroy(unit[i]);
        }

        for (int i = 0; i < enemy.Length; i++)
        {
            Destroy(enemy[i]);
        }

        for (int i = 0; i < UI.Length; i++)
        {
            Destroy(UI[i]);
        }
    }

}