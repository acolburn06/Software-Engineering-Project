using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    public float maxHealth, health;

    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
    }


    // Update is called once per frame
    void Update()
    {
        if(health>maxHealth) {health = maxHealth;}
        if(health<0) {health = 0;}
    }


    public void loseHealth(float damage){
        health -= damage;
    }
    

    public void gainHealth(float healing){
        health += healing;
    }
}
