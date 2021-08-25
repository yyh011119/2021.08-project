using UnityEngine;
using UnityEngine.UI;

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

        currentHealth = health;
        currentDamage = damage;
        currentAttackSpeed = attackSpeed;
        Declare(1);
        
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


        Declare(2);

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
        if(this.tag == "Ally")
        {
            resultText.text = "Game Over";
        }
        if (this.tag == "Enemy")
        {
            resultText.text = "You Win";
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