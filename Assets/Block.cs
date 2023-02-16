using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block
{   
    private string type = "null";
    private int intValue;
    private bool boolValue;
    private string stringValue;

    private string label;

    public Block(string input) {
        typeChecker(input);
    }

    private void typeChecker(string input) {
        if (int.TryParse(input, out intValue)) {
            type = "Integer";
        }
        else if (string.Equals(input, "true")  || string.Equals(input, "True")) {
            label = "True";
            type = "Boolean";
            boolValue = true;
        }
        else if (string.Equals(input, "false") || string.Equals(input, "False")) {
            label = "False";
            type = "Boolean";
            boolValue = false;
        }
        else if (input.Substring(0,1) == ":") {
            type = "Function";
        }
        else if (input == "(" ^ input == ")") {
            type = "Bracket";
        }
        else if (input[0] == '"' & input[input.Length - 1] == '"'){

            type = "String";
            stringValue = input;
        }
        else {
            type = "Invalid";
        }
        label = input;
    }

    public string getType() {
        return type;
    }

    public int getIntValue() {
        return intValue;
    }

    public string getStringValue() {
        return stringValue;
    }
    
    public bool getBoolValue() {
        return boolValue;
    }

    public override string ToString() {
        return label;
    }

    
}
