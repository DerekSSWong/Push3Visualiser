using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutScripts : MonoBehaviour
{   
    bool tutOpen;
    [SerializeField] GameObject tutWindows;
    [SerializeField] Sprite greenTut;
    [SerializeField] Sprite redTut;
    [SerializeField] GameObject tutDocs;
    Transform[] docArray;
    // Start is called before the first frame update
    void Start()
    {   
        tutOpen = false;
        tutWindows.SetActive(false);
        docArray = tutDocs.GetComponentsInChildren<Transform>(true);
        this.GetComponent<Image>().sprite = greenTut;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void TutButton() {
        tutOpen = !tutOpen;
        tutWindows.SetActive(tutOpen);
        if (tutOpen) {
            this.GetComponent<Image>().sprite = redTut;
        }
        else {
            this.GetComponent<Image>().sprite = greenTut;
        }
    }

    void enableDoc(string buttonName) {
        foreach (Transform tr in docArray) {
            if(tr.name != "TutDocs") {
                tr.gameObject.SetActive(false);
            }
        }
        string docName = buttonName + "Doc";
        foreach (Transform tr in docArray) {
            if (tr.name == docName) {
                tr.gameObject.SetActive(true);
                Transform[] children = tr.gameObject.GetComponentsInChildren<Transform>(true);
                foreach (Transform ch in children) {
                    ch.gameObject.SetActive(true);
                }
            }
        }
    }

    public void quickRulesButton() {
        enableDoc("QuickRules");
    }
    public void stacksButton() {
        enableDoc("Stacks");
    }
    public void StackTypesButton() {
        enableDoc("StackTypes");
    }
    public void stackTypesExample() {
        GameObject.Find("BlockDisplayController").GetComponent<DisplayText>().clear();
        List<Block> blockList = new List<Block>();
        blockList.Add(new Block("True"));
        blockList.Add(new Block("408"));
        blockList.Add(new Block(":exec_dup"));
        blockList.Add(new Block(quote("10")));
        blockList.Add(new Block(quote("The 10 is a string")));

        Block[] blockArray = blockList.ToArray();
        GameObject.Find("Exec").GetComponent<ExecStackController>().load(blockArray);
        
    }
    public void instructionsButton(){
        enableDoc("Instructions");
    }
    public void instructionsExample() {
        GameObject.Find("BlockDisplayController").GetComponent<DisplayText>().clear();
        List<Block> blockList = new List<Block>();
        blockList.Add(new Block("1"));
        blockList.Add(new Block("2"));
        blockList.Add(new Block(":integer_add"));
        blockList.Add(new Block(":exec_dup"));
        blockList.Add(new Block("("));
        blockList.Add(new Block("23"));
        blockList.Add(new Block("false"));
        blockList.Add(new Block(")"));
        blockList.Add(new Block(":string_dup"));

        Block[] blockArray = blockList.ToArray();
        GameObject.Find("Exec").GetComponent<ExecStackController>().load(blockArray);
    }

    private string quote(string str) {
        string output = "";
        output += '"';
        output += str;
        output += '"';
        return output;
    }

}
