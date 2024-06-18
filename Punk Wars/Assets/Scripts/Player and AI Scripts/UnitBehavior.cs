using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using Unity.VisualScripting;
// using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class UnitBehavior : MonoBehaviour
{
    public bool selected, atGoal, inHitRange = false, inDanger = false;
    [SerializeField] private Healthbar healthbar;
    [SerializeField] NavMeshAgent agent;
    [SerializeField] Behaviour halo;
    float timer = 0, timer2 = 0;
    private HealthManager hm;
    GameObject enemy;
    


    private void Start() {
        hm = gameObject.GetComponent<HealthManager>();
        selected = false;
        healthbar.UpdateHealthbar(hm.maxHealth, hm.health);
    }


    private void Update()
    {
        //signals if unit is selected
        if(selected) {halo.enabled = true;}
        else {halo.enabled = false;}

        //checks if unit is within vicinity of goal
        //it looks bad i know shut up
        if ((transform.position.x-1 <= agent.destination.x) && (agent.destination.x <= transform.position.x +1) && (transform.position.y-1 <= agent.destination.y) && (agent.destination.y <= transform.position.y +1)){
            atGoal = true;
        }

        //if in range to damage something, do so once per second
        timer += Time.deltaTime;
        if(timer >= 1){
            if(inHitRange){
                attack();
                if(inDanger){
                    hm.loseHealth(1);
                    healthbar.UpdateHealthbar(hm.maxHealth, hm.health);
                }
            }
            timer = 0;
        }

        //heal every 20 seconds
        timer2 += Time.deltaTime;
        if(timer2 >= 20){
            hm.gainHealth(1);
            healthbar.UpdateHealthbar(hm.maxHealth, hm.health);
        }

        //dies at 0 health
        if(hm.health == 0){Destroy(gameObject);}
    }


    private void OnCollisionEnter(Collision other) {
        //stops unit if its going to the same place and is at the goal
        //yes this also looks bad, see previous comment
        try{
            if(other.gameObject.GetComponent<UnitBehavior>().atGoal && other.gameObject.GetComponent<NavMeshAgent>().destination != agent.destination){
                agent.destination = transform.position;
                atGoal = true;
            }
        }catch(Exception e){}

        //if unit collides with a unit that isnt going to the same place it just goes through to avoid annoyances
        try{
            if(other.gameObject.GetComponent<NavMeshAgent>().destination != agent.destination){
                other.gameObject.GetComponent<BoxCollider>().enabled = false;
                Invoke("wait", 2.0f);
                other.gameObject.GetComponent<BoxCollider>().enabled = true;
            }
        }catch(Exception e){}
    }


    void OnTriggerEnter(Collider other) {
        //if unit is in the collider of an ore/enemy, set a bool to true
        //if said bool is true, unit attacks the object it's colliding with repeatedly in the update function
        //ores and enemies have two colliders, one for actual collisions, and a bigger one for triggers
        if(other.gameObject.CompareTag("Ore") || other.gameObject.CompareTag("Enemy")){
            enemy = other.gameObject;
            inHitRange = true;
            if(other.gameObject.CompareTag("Enemy")){
                inDanger = true;
            }
        }
    }


    void OnTriggerExit(Collider other) {
        //sets unit to not attacking the object it was previously attacking
        if(other.gameObject.CompareTag("Ore") || other.gameObject.CompareTag("Enemy")){
            enemy = null;
            inHitRange = false;
            inDanger = false;
        }
    }


    void attack(){
        //throws a ton of errors once the target gets destroyed
        //the try catch just prevents error clutter
        try{
            enemy.GetComponent<HealthManager>().loseHealth(1);
            //does this since OnTriggerExit doesnt work when triggers are deleted
            if(enemy.GetComponent<HealthManager>().health == 0){
                enemy = null;
                inHitRange = false;
                inDanger = false;
            }
        }catch(Exception e){}
    }


    void wait(){
        //does nothing, used to just delay something using invoke
    }
}