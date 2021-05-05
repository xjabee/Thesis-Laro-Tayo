using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PaloseboManagement : MonoBehaviour
{
    public List<PlayerNumber> playerNumberList;

    public Text p1Text;
    public Text p2Text;
    public bool pause;

    // Start is called before the first frame update
    void Start()
    {
        pause = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddToFinished(PlayerNumber p)
    {
        playerNumberList.Add(p);
        if ( playerNumberList.Count == 1 )
        {
            if ( playerNumberList[0] == PlayerNumber.Player1 )
            {
                p1Text.text = "Player 1 is first place";
            }
            if ( playerNumberList[0] == PlayerNumber.Player2 )
            {
                p2Text.text = "Player 2 is first place";
            }
        }
        if (playerNumberList.Count == 2)
        {
            if (playerNumberList[1] == PlayerNumber.Player1)
            {
                p1Text.text = "Player 1 is second place";
            }
            if (playerNumberList[1] == PlayerNumber.Player2)
            {
                p2Text.text = "Player 2 is second place";
            }
        }
    }

    public void StartGame()
    {
        pause = false;
    }
    
}
