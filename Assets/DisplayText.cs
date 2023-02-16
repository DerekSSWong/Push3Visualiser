using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

//Run this when Parse button is pressed
//Sends block data to respective block spawners
public class DisplayText : MonoBehaviour
{
    public string inputText;
    private string[] parsedArray;
    public GameObject inputField;
    public GameObject displayField;
    public bool buttonEnabled;
    public GameObject textBoxText;
    
    void Start() {
        //textBox.GetComponent<inputField>().text = "2 3 :integer_mult :exec_swap (" + "String " + "False) :string_dup True :boolean_and";
    }
    void Update() {
        displayField.GetComponent<TextMeshProUGUI>().text = GameObject.Find("Exec").GetComponent<ExecStackController>().log();
        //Disables FWD
        bool execReady = !GameObject.Find("Exec").GetComponent<ExecStackController>().spawning & !GameObject.Find("Exec").GetComponent<ExecStackController>().isEmpty();
        bool intReady = !GameObject.Find("Int").GetComponent<IntStackController>().spawning;
        bool stringReady = !GameObject.Find("String").GetComponent<StringStackController>().spawning;
        bool boolReady = !GameObject.Find("Bool").GetComponent<BoolStackController>().spawning;
        buttonEnabled = execReady && intReady && stringReady && boolReady;
        GameObject.Find("FWStepButton").GetComponent<Button>().interactable = buttonEnabled;

        //Disables BWD
        bool execHistory = !GameObject.Find("Exec").GetComponent<ExecStackController>().isUndoEmpty();
        bool intHistory = !GameObject.Find("Int").GetComponent<IntStackController>().isUndoEmpty();
        bool stringHistory = !GameObject.Find("String").GetComponent<StringStackController>().isUndoEmpty();
        bool boolHistory = !GameObject.Find("Bool").GetComponent<BoolStackController>().isUndoEmpty();
        bool undoEnabled = execHistory && intHistory && stringHistory && boolHistory;
        GameObject.Find("BWD").GetComponent<Button>().interactable = undoEnabled;


        //Exit
        if (Input.GetKey("escape"))
        {
            Application.Quit();
        }

    }   

    public void displayText() {
        clear();
        inputText = inputField.GetComponentInChildren<TMP_InputField>().text;
        
        Parser parser = new Parser();
        parsedArray = parser.parse(inputText);

        if (parsedArray != null) {
            displayField.GetComponent<TextMeshProUGUI>().text = inputText;

            Block[] blockArray = new Block[parsedArray.Length];
            for (int i = 0; i < parsedArray.Length; i++) {
                blockArray[i] = new Block(parsedArray[i]);
            }

            GameObject.Find("Exec").GetComponent<ExecStackController>().load(blockArray);
        }
        
    }

    public void clear() {
        foreach (GameObject g in GameObject.FindGameObjectsWithTag("Block")) {
            Destroy(g);
        }
        GameObject.Find("Exec").GetComponent<ExecStackController>().reset();
        GameObject.Find("Int").GetComponent<IntStackController>().reset();
        GameObject.Find("String").GetComponent<StringStackController>().reset();
        GameObject.Find("Bool").GetComponent<BoolStackController>().reset();
    }
}
