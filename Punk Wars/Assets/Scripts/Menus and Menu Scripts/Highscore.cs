using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Highscore : MonoBehaviour
{
    private SaveManager savemanager;
    private TMP_Text score;

    // Start is called before the first frame update
    void Start()
    {
        score = GameObject.FindWithTag("Score").GetComponent<TMP_Text>();
        score.text = savemanager.Read();
    }

    void Update()
    {
        score.text = savemanager.Read();
        Debug.Log(savemanager.Read());
    }
}