using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using Unity.VisualScripting;
using UnityEngine;

public class OreBehavior : MonoBehaviour
{
    [SerializeField] GameObject regularOre, brokenOre, healthbarObject;
    [SerializeField] private Healthbar healthbar;
    private bool isCopper = true;
    private Gameloop gameloop;
    private HealthManager hm;
    


    void Awake() {
        hm = gameObject.GetComponent<HealthManager>();
        gameloop = GameObject.FindWithTag("HQ").GetComponent<Gameloop>();
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }


    // Update is called once per frame
    void Update()
    {
        if(hm.health <= 0) {
            GameObject.FindWithTag("HQ").GetComponent<Gameloop>().copper = destroyed(GameObject.FindWithTag("HQ").GetComponent<Gameloop>().copper);
        }
        //prevents health from going down if the ore is destroyed
        if(!regularOre.activeSelf){hm.health = hm.maxHealth;}
        else{healthbar.UpdateHealthbar(hm.maxHealth, hm.health);}
    }


    public int destroyed(int ore){
        //deactivates the regular ore and activates the broken ore
        brokenOre.SetActive(true);
        //resets health for when it regens
        regularOre.SetActive(false);
        healthbarObject.SetActive(false);
        gameloop.IncrementPoints(2);
        //adds 1 to 3 to an ore counter
        return (ore += Random.Range(1,4));
    }
}
