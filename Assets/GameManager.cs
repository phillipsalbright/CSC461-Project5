using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject ballPrefab;
    [SerializeField] private int livesLeft;
    [SerializeField] private GameObject gameOverScreen;

    public void LifeLost()
    {
        livesLeft--;
        if (livesLeft >= 0)
        {
            Instantiate(ballPrefab, new Vector3(8.90999985f, 11.6899996f, -0.140000001f), Quaternion.identity);

        } else
        {
            gameOverScreen.SetActive(true);
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
