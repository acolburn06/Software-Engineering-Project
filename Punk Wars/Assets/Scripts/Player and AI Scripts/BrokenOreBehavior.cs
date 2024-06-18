using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrokenOreBehavior : MonoBehaviour
{
    [SerializeField] GameObject regularOre, healthbarObject;
    float timer;


    // Start is called before the first frame update
    void Start()
    {
        timer = 0;        
    }


    // Update is called once per frame
    void Update()
    {
        //after 30 seconds, regens the ore
        timer += Time.deltaTime;
        if(timer >= 30){
            timer = 0;
            regularOre.SetActive(true);
            healthbarObject.SetActive(true);
            gameObject.SetActive(false);
        }
    }
}
