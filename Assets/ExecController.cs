using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Obsolete Code
public class ExecController : MonoBehaviour
{   
    private Block[] blockArray;
    private bool isOn;
    private bool canSpawn;
    private int blockIndex;
    public GameObject BlockPrefab;
    private GameObject prevBlock;
    public Stack<GameObject> blockStack;

    // Start is called before the first frame update
    void Start()
    {
        isOn = false;
        canSpawn = true;
        blockIndex = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (isOn && blockIndex >= 0 && canSpawn) {
            StartCoroutine(deployBlock(blockArray[blockIndex]));
        }
        
    }

    public void load(Block[] ba) {
        blockArray = ba;
        isOn = true;
        blockIndex = blockArray.Length - 1;
        prevBlock = GameObject.Find("ExecWall");
        blockStack = new Stack<GameObject>();
    }

    public void forward() {
        GameObject block = blockStack.Pop();
        string text = block.GetComponent<BlockVis>().ToString();
        print(text);
        Destroy(block);
    }

    IEnumerator deployBlock(Block block) {
        canSpawn = false;
        
        float xLoc;
        if (blockIndex == blockArray.Length - 1) {
            print("if");
            xLoc = prevBlock.GetComponent<BoxCollider2D>().bounds.min.x;
        } else {
            print("else");
            xLoc = prevBlock.GetComponentInChildren<BoxCollider2D>().bounds.min.x;
        }

        GameObject newBlock = Instantiate(BlockPrefab, this.transform.localPosition, Quaternion.identity);
        newBlock.GetComponent<BlockVis>().setBlock(block);

        yield return new WaitForSeconds(0.1f);

        blockStack.Push(newBlock);
        float dist = xLoc - blockStack.Peek().GetComponentInChildren<BoxCollider2D>().bounds.max.x;
        print(blockStack.Peek().GetComponentInChildren<BoxCollider2D>().bounds.extents.x);
        blockStack.Peek().transform.localPosition += new Vector3 (dist - 0.02f, 0f, 0f);

        prevBlock = blockStack.Peek();

        blockIndex -= 1;
        canSpawn = true;
    }
}
