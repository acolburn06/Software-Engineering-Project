using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mono.Data.Sqlite;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Data;
using TMPro;

public class Gameloop : MonoBehaviour
{
    public int id = 1, copper = 0, currentUnit = 0;
    private SaveManager savemanager;
    private Menu1 menu1;
    private float timer = 0, timer2 = 0;
    private TMP_Text scoreCounter, copperCounter;
    [SerializeField] private Healthbar healthbar;
    private HealthManager hm;
    private GameObject unitParent;
    private GameObject[] units;
    public int points = 0;
    private GameObject loseText;

    void Awake() {
        units = GameObject.FindGameObjectsWithTag("Unused Unit");
        
        loseText = GameObject.FindWithTag("Lose");
        loseText.SetActive(false);

        hm = gameObject.GetComponent<HealthManager>();
        scoreCounter = GameObject.FindWithTag("ScoreCounter").GetComponent<TMP_Text>();
        copperCounter = GameObject.FindWithTag("CopperCounter").GetComponent<TMP_Text>();
    }
    void Start() {
        foreach(GameObject tempUnit in units){
            tempUnit.SetActive(false);
        }
        healthbar.UpdateHealthbar(hm.maxHealth, hm.health);
    }
    void Update()
    {
        // Create a temporary reference to the current scene.
		Scene currentScene = SceneManager.GetActiveScene();

		// Retrieve the name of this scene.
		string sceneName = currentScene.name;

        if(sceneName == "MainLevel")
        {
            if(hm.health <= 0)
            {
                loseText.SetActive(true);
                Time.timeScale = 0f;
            }
        }

        timer += Time.deltaTime;
        if(timer >= 1){
            IncrementPoints(1);
            timer = 0;
        }

        timer2 += Time.deltaTime;
        if(timer2 >= 3){
            hm.loseHealth(points/10);
            timer2 = 0;
        }

        copperCounter.text = "Copper: " + copper.ToString();

        healthbar.UpdateHealthbar(hm.maxHealth, hm.health);
    }

    public void IncrementPoints(int reward)
    {
        points = points + reward;
        scoreCounter.text = "Score: " + points.ToString();
    }

    public void SpawnUnit(){
        //moves an unloaded unit to the arena if the player has 5 copper and there are still units available
        if(copper >= 5 && currentUnit < 100){
            units[currentUnit].SetActive(true);
            units[currentUnit].gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
            units[currentUnit].gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionY;
            units[currentUnit].gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotationX;
            units[currentUnit].gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotationZ;
            units[currentUnit].tag = "Unit";
            units[currentUnit].transform.SetParent(GameObject.FindWithTag("UnitParent").transform);
            // units[currentUnit].transform.position = new Vector3(26.43f,0.55f,-34.35f);
            currentUnit++;
            copper -= 5;
        }
    }
        public void heal(){
            if(copper >= 1){
                hm.gainHealth(1);
                copper--;
            }
        }
}