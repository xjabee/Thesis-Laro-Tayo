using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawn : MonoBehaviour
{
    public GameObject respawnPrefab;
    public GameObject respawnLoc;
    public GameObject[] respawns;
    public GameObject[] tempRespawns;
    // Start is called before the first frame update
    void Start()
    {
        if (respawns == null)
            respawns = GameObject.FindGameObjectsWithTag("Finish");

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void PogRespawn()
    {
        foreach (GameObject respawn in respawns)
        {
            TransferPog(respawn);
        }

    }

    IEnumerator TransferPog(GameObject respawn)
    {
        yield return new WaitForSeconds(0.5f);
        respawn.transform.position = respawnLoc.transform.position;
        yield return null;

    }
}
