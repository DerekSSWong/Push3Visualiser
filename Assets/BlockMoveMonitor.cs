using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlockMoveMonitor : MonoBehaviour
{   
    GameObject[] blocks;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        blocks =  GameObject.FindGameObjectsWithTag("Block");
        foreach(GameObject b in blocks) {
            if (b.GetComponent<BlockMove>().isMoving) {
                StartCoroutine(disableButtons());
            }
        }
    }

    public IEnumerator disableButtons()
    {
        GameObject.Find("FWStepButton").GetComponent<Button>().interactable = false;
        print("disabled");
        yield return null;
    }
}
