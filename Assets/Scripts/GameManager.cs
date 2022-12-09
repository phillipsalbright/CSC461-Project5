using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject ballPrefab;
    [SerializeField] private int livesLeft;
    [SerializeField] private GameObject gameOverScreen;
    [SerializeField] private TextMeshProUGUI livesText;

    public void LifeLost()
    {
        livesLeft--;
        if (livesLeft >= 0)
        {
           // Instantiate(ballPrefab, new Vector3(8.90999985f, 11.6899996f, -0.140000001f), Quaternion.identity);

        } else
        {
            gameOverScreen.SetActive(true);
            return;
        }
        livesText.text = "Lives Left: " + livesLeft;
    }


    // Start is called before the first frame update
    void Start()
    {
        livesText.text = "Lives Left: " + livesLeft;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
