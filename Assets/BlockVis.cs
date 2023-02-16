using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class BlockVis : MonoBehaviour
{   
    string text;
    Block block;
    float extents;
    bool blockSet;

    bool deleting;
    float alpha;
    [SerializeField] GameObject square;

    // Start is called before the first frame update
    void Start()
    {
        blockSet = false;
        deleting = false;
        alpha = 1f;
    }

    // Update is called once per frame
    void Update()
    {   
        if (block != null) {
            this.GetComponentInChildren<TMP_Text>().text = block.ToString();
            int textSize = block.ToString().Length;
            float center = this.GetComponentInChildren<TMP_Text>().textBounds.center.x;
            extents = this.GetComponentInChildren<TMP_Text>().textBounds.extents.x;
            // print("Center: " + center + "Extents: " + extents);

            GameObject child = this.transform.GetChild(0).gameObject;
            child.transform.localScale = new Vector3(extents*2f + 0.001f, 0.17f, 1f);
            setColour(block.getType());
        }

        if (deleting) {
            square.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0f);
            alpha -= 0.1f;
        }

    }

    public void setBlock(Block b) {
        block = b;
        blockSet = true;
    }

    public float getExtent() {
        extents = this.GetComponentInChildren<BoxCollider2D>().bounds.max.x + this.GetComponentInChildren<BoxCollider2D>().edgeRadius;
        return extents;
    }

    public override string ToString() {
        return block.ToString();
    }

    public Block getBlock() {
        return block;
    }

    public bool getBlockSet() {
        return blockSet;
    }

    private void setColour(string type) {
        switch (type) {
            case "Integer":
                square.GetComponent<SpriteRenderer>().color = new Color(170f/255f, 255f/255f, 170f/255f, 1f);
                break;
            case "String":
                square.GetComponent<SpriteRenderer>().color = new Color(170f/255f, 255f/255f, 255f/255f, 1f);
                break;
            case "Boolean":
                square.GetComponent<SpriteRenderer>().color = new Color(255f/255f, 200f/255f, 117f/255f, 1f);
                break;
            case "Function":
                square.GetComponent<SpriteRenderer>().color = new Color(200f/255f, 200f/255f, 255f/255f, 1f);
                break;
            case "Bracket":
                square.GetComponent<SpriteRenderer>().color = new Color(180f/255f, 180f/255f, 180f/255f, 1f);
                break;
        }
    }
    
    public IEnumerator despawn() {
        deleting = true;
        yield return new WaitForSeconds(0.1f);
        Destroy(gameObject);
    }
}
