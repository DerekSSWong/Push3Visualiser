using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;

public class Parser
{   
    public string[] parse(string input) {

        string processedString = input;
        processedString = processedString.Replace(")", " ) ");
        processedString = processedString.Replace("(", " ( ");
        processedString = Regex.Replace(processedString, @"\s+", " ");
        processedString = processedString.Trim();
        string[] output = processedString.Split(" ");
        output = joinQuotes(output);
        if(output != null && !checkValidity(output)) {
            output = null;
        }
        return output;

    }

    private string[] joinQuotes(string[] input) {
        List<int> startIndexes = new List<int>();
        List<int> endIndexes = new List<int>();
        List<string> outputList = new List<string>();

        for (int i = 0; i < input.Length; i++) {
            string s = input[i];
            if (s[0] == '"' & s[s.Length - 1] != '"') {
                startIndexes.Add(i);
            }
            else if (s[0] != '"' & s[s.Length - 1] == '"') {
                endIndexes.Add(i);
            }
        }

        if (startIndexes.Count != endIndexes.Count) {
                //Debug.Log(startIndexes.Count + " " + endIndexes.Count);
                return null;
        }

        for (int i = 0; i < input.Length; i++) {
            if (startIndexes.Count > 0 && i == startIndexes[0]) {
                outputList.Add(input[i]);
                outputList[outputList.Count - 1] += " ";
                startIndexes.RemoveAt(0);
            }
            else if (endIndexes.Count > 0 && startIndexes.Count < endIndexes.Count & i < endIndexes[0]) {
                outputList[outputList.Count - 1] += input[i];
                outputList[outputList.Count - 1] += " ";
            }
            else if (endIndexes.Count > 0 && i == endIndexes[0]) {
                outputList[outputList.Count - 1] += input[i];
                endIndexes.RemoveAt(0);
            }
            else {
                outputList.Add(input[i]);
            }
        }

        return outputList.ToArray();
    }

    private bool checkBrackets(string[] input) {
        int counter = 0;
        bool validity = true;
        foreach (string s in input) {
            if (s == "(") {
                counter++;
            }
            else if (s == ")") {
                counter--;
            }
            if (counter < 0) {
                validity = false;
                break;
            }
        }
        if (counter > 0) {
            validity = false;
        }
        Debug.Log("Brackets: "  + validity);
        return validity;
    }

    private bool checkInstructions(string[] input) {
        bool validity = true;
        foreach (string s in input) {
            if (s[0] == ':') {
                if (!FunctionChecker.check(s)) {
                    validity = false;
                }
            }
            
        }
        Debug.Log("Instructions: "  + validity);
        return validity;
    }

    private bool checkLooseStrings(string[] input) {
        bool validity = true;
        foreach (string s in input) {
            if ( !int.TryParse(s, out _) && !Boolean.TryParse(s, out _) && s[0] != ':' && s[0] != '(' && s[0] != ')') {
                if (s[0] != '"' || s[s.Length - 1] != '"') {
                    validity = false;
                }
            }
        }
        Debug.Log("String: "  + validity);
        return validity;
    }

    private bool checkValidity(string[] input) {
        return checkBrackets(input) && checkInstructions(input) && checkLooseStrings(input);
    }
}
