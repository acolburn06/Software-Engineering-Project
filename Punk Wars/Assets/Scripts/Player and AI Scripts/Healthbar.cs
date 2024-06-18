using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour
{
    [SerializeField] private Image healthbarSprite;
    [SerializeField] private float reduceSpeed = 2;
    private float target;
    Camera cam;


    void Start() {
        cam = Camera.main;
    }


    void Update() {
        transform.rotation = Quaternion.LookRotation(transform.position - cam.transform.position);
        //makes healthbar drain smooth instead of snapping
        healthbarSprite.fillAmount = Mathf.MoveTowards(healthbarSprite.fillAmount, target, reduceSpeed * Time.deltaTime);
    }


    public void UpdateHealthbar(float maxHealth, float currentHealth){
        target = currentHealth/maxHealth;
    }
}
