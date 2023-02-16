using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class StackController : MonoBehaviour
{   

    //private Block[] blockArray;

    private bool canSpawn;
    private int blockIndex;

    public GameObject BlockPrefab;
    public GameObject Wall;
    public float padding;

    //private GameObject prevBlock;
    public Stack<GameObject> blockStack;

    protected Stack<SaveData> undoStack;

    public bool spawning;
    
    // Start is called before the first frame update
    void Start()
    {
        canSpawn = true;
        blockIndex = -1;
        blockStack = new Stack<GameObject>();
        undoStack = new Stack<SaveData>();
        spawning = false;
    }

    //Update is called once per frame
    // void Update()
    // {
    //     if (canSpawn && blockIndex >= 0) {
    //         StartCoroutine(deploy(blockArray[blockIndex]));
    //     }
    // }

    //Reset
    //When parse button is clicked
    public void load(Block[] ba) {

        clearUndo();
        blockStack.Clear();
        spawn(ba, 0);

    }

    //Add more blocks without resetting
    public void spawn(Block[] ba, int index) {
        StartCoroutine(deploy(ba, index));
    }

    //Deprecated function
    public void spawn(Block b, int index) {
        spawn(new Block[] {b}, index);
    }

    public void bin(int startingPoint, int binAmount) {
        GameObject[] stackArray = blockStack.ToArray();
        GameObject[] newArray = new GameObject[stackArray.Length - binAmount];
        Block[] binArray = new Block[binAmount];

        for ( int i = 0; i < stackArray.Length; i++) {
            if (i >= startingPoint && i - startingPoint < binAmount) {
                GameObject block = stackArray[i];
                binArray[i - startingPoint] = block.GetComponent<BlockVis>().getBlock();
                StartCoroutine(block.GetComponent<BlockVis>().despawn());
                //Destroy(block);
            } 
            else if (i < startingPoint) {
                newArray[i] = stackArray[i];
            }
            else {
                newArray[i - binAmount] = stackArray[i];
            }
        }
        Array.Reverse(newArray);
        blockStack = new Stack<GameObject>(newArray);
    }

    protected IEnumerator reshuffle() {
        yield return new WaitForSeconds(0.1f);
        if (blockStack.Count > 0) {
            GameObject[] stackArray = blockStack.ToArray();
            Array.Reverse(stackArray);

            float wallLoc = Wall.GetComponent<BoxCollider2D>().bounds.min.x;
            float blockWidth = 0f;
            foreach (GameObject block in stackArray) {
                float currentLoc = block.GetComponentInChildren<BoxCollider2D>().bounds.max.x + block.GetComponentInChildren<BoxCollider2D>().edgeRadius;
                block.GetComponent<BlockMove>().travel(wallLoc - blockWidth - currentLoc, 50);
                blockWidth += getLength(block);
            }
        }
        yield return null;
    }

    IEnumerator deploy(Block[] blocks, int index) {
        
        spawning = true;
        float xLoc;
        GameObject[] stackArray = blockStack.ToArray();
        Array.Reverse(blocks);

        if (index > blockStack.Count) {
            index = blockStack.Count;
        }

        foreach (Block block in blocks) 
        {   
            stackArray = blockStack.ToArray();
            GameObject newBlock = Instantiate(BlockPrefab, this.transform.localPosition, Quaternion.identity);
            newBlock.GetComponent<BlockVis>().setBlock(block);
            yield return new WaitForSeconds(0.1f); 

            float dist;
            if (blockStack.Count == 0) {
                xLoc = Wall.GetComponent<BoxCollider2D>().bounds.min.x;
            } else {
                stackArray = blockStack.ToArray();
                xLoc = Wall.GetComponent<BoxCollider2D>().bounds.min.x;
                for (int i = stackArray.Length - 1; i >= index; i--) {
                    xLoc -= getLength(stackArray[i]);
                }
            }

            for (int i = 0; i < index; i++) {
                stackArray[i].GetComponent<BlockMove>().travel(-getLength(newBlock), 50);
            }
            //Make block move for insertion here

            dist = xLoc - getExtent(newBlock);
            newBlock.GetComponent<BlockMove>().travel(dist, 50);
            
            //blockStack.Push(newBlock);

            GameObject[] newStackArray = new GameObject[blockStack.Count + 1];
            for (int i = 0; i < newStackArray.Length; i++) {
                if (i == index) {
                    newStackArray[i] = newBlock;
                }
                else {
                    newStackArray[i] = blockStack.Pop();
                }
            }
            // string debugString = "";
            // foreach (GameObject b in newStackArray) {
            //     debugString += b.GetComponent<BlockVis>().ToString() + " ";
            // }
            // print(debugString);

            
            Array.Reverse(newStackArray);
            blockStack = new Stack<GameObject>(newStackArray);


            //print("Pushed block: " + newBlock.GetComponent<BlockVis>().ToString() + " Stack Size: " + blockStack.Count);

            yield return new WaitForSeconds(0.1f);
        }
        spawning = false;
    }

    public void snapshot() {

        GameObject[] stackArray = blockStack.ToArray();
        //print(stackArray.Length);
        if (stackArray.Length > 0){
            Block[] blocks = new Block[stackArray.Length];
            Vector3[] locations = new Vector3[stackArray.Length];

            for(int i = 0; i < stackArray.Length; i++) {
                blocks[i] = stackArray[i].GetComponent<BlockVis>().getBlock();
                locations[i] = stackArray[i].transform.localPosition;
            }

            SaveData save = new SaveData(blocks, locations);
            undoStack.Push(save);
        }
        else {
            SaveData emptySave = new SaveData();
            undoStack.Push(emptySave);
        }
    }

    public void undo() {
        SaveData save = undoStack.Pop();
        reset();

        if (!save.isEmpty) {
            Block[] blocks = save.blocks;
            Vector3[] locations = save.locations;
            Array.Reverse(blocks);
            Array.Reverse(locations);
            for (int i = 0; i <  blocks.Length; i++) {
                GameObject newBlock = Instantiate(BlockPrefab, locations[i], Quaternion.identity);
                newBlock.GetComponent<BlockVis>().setBlock(blocks[i]);
                blockStack.Push(newBlock);
            }
        }

        StartCoroutine(reshuffle());

    }

    public void clearUndo() {
        undoStack.Clear();
    }

    protected float getLength(GameObject block) {
        return 2 * block.GetComponentInChildren<BoxCollider2D>().bounds.extents.x + block.GetComponentInChildren<BoxCollider2D>().edgeRadius;
    }

    protected float getExtent(GameObject block) {
        return block.GetComponentInChildren<BoxCollider2D>().bounds.max.x + block.GetComponentInChildren<BoxCollider2D>().edgeRadius;
    }

    public void reset() {
        GameObject[] stackArray = blockStack.ToArray();
        foreach (GameObject b in stackArray) {
            Destroy(b);
        }
        blockStack = new Stack<GameObject>();
    }

    public string log() {
        string log = "";
        GameObject[] stackArray = blockStack.ToArray();
        for (int i = 0; i < stackArray.Length; i++) {
            log += "Index " + i + ": " + stackArray[i].GetComponent<BlockVis>().ToString() + "\n";
        }
        return log;
    }

    public bool isEmpty() {
        bool isEmpty = false;
        if (blockStack.Count == 0) {
            isEmpty = true;
        }
        return isEmpty;
    }

    public bool isUndoEmpty() {
        return undoStack.Count < 1;
    }
}

public class SaveData {
    public Block[] blocks;
    public Vector3[] locations;
    public bool isEmpty;

    public SaveData(Block[] b, Vector3[] l) {
        blocks = b;
        locations = l;
        isEmpty = false;
    }

    public SaveData() {
        isEmpty = true;
    }
}
