using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StringStackController : StackController
{

    public void process(string func) {
        GameObject[] blocks = blockStack.ToArray();
        Block blockData;
        string input1;
        string input2;
        int index;

        switch(func) {
            case "string_butlast":
                if (!isEmpty()) {
                    input1 = blocks[0].GetComponent<BlockVis>().ToString();
                    bin(0,1);

                    blockData = new Block(input1.Remove(input1.Length - 1, 1));
                    spawn(blockData, 0);
                }
                break;

            case "string_concat":
                if (blockStack.Count >= 2) {
                    input1 = blocks[0].GetComponent<BlockVis>().ToString();
                    input2 = blocks[1].GetComponent<BlockVis>().ToString();
                    bin(0,2);
                    
                    blockData = new Block(input2+input1);
                    spawn(blockData, 0);
                }
                
                break;

            case "string_contains":
                if (blockStack.Count >= 2) {
                    input1 = blocks[0].GetComponent<BlockVis>().ToString();
                    input2 = blocks[1].GetComponent<BlockVis>().ToString();
                    bin(0,2);

                    blockData = new Block("False");
                    if (input1.Contains(input2)) {
                        blockData = new Block("True");
                    }

                    GameObject.Find("Bool").GetComponent<BoolStackController>().spawn(blockData, 0);
                }
                
                break;

            case "string_deep_dup":
                if (!isEmpty() & !GameObject.Find("Int").GetComponent<IntStackController>().isEmpty()) {
                    index = GameObject.Find("Int").GetComponent<IntStackController>().blockStack.Peek().GetComponent<BlockVis>().getBlock().getIntValue();
                    GameObject.Find("Int").GetComponent<IntStackController>().bin(0,1);
                    if (index >= 0) {
                        blocks = blockStack.ToArray();
                        Array.Reverse(blocks);
                        if (index >= blocks.Length) {
                            index = blocks.Length - 1;
                        }
                        blockData = blocks[index].GetComponent<BlockVis>().getBlock();
                        spawn(blockData, 0);
                    }
                    
                }
                
                break;

            case "string_drop":
                if (!isEmpty() & !GameObject.Find("Int").GetComponent<IntStackController>().isEmpty()) {
                    input1 = blocks[0].GetComponent<BlockVis>().ToString();
                    bin(0,1);

                    int dropInt = GameObject.Find("Int").GetComponent<IntStackController>().blockStack.Peek().GetComponent<BlockVis>().getBlock().getIntValue();
                    GameObject.Find("Int").GetComponent<IntStackController>().bin(0,1);

                    if (dropInt > input1.Length) {
                        dropInt = input1.Length;
                    }
                    blockData = new Block(input1.Remove(0, dropInt));
                    spawn(blockData, 0);
                }
                
                break;

            case "string_dup":
                if (!isEmpty()) {
                    input1 = blocks[0].GetComponent<BlockVis>().getBlock().getStringValue();
                    blockData = new Block(input1);
                    spawn(blockData, 0);
                }
                break;

            case "string_dup_items":
                if (!isEmpty() & !GameObject.Find("Int").GetComponent<IntStackController>().isEmpty()) {
                    index = GameObject.Find("Int").GetComponent<IntStackController>().blockStack.Peek().GetComponent<BlockVis>().getBlock().getIntValue();
                    GameObject.Find("Int").GetComponent<IntStackController>().bin(0,1);
                    if (index >= 0) {
                        blocks = blockStack.ToArray();

                        if (index > blocks.Length) {
                            index = blocks.Length;
                        }

                        Block[] blocksToDupe = new Block[index];
                        
                        for (int i = 0; i < index; i++) {
                            blocksToDupe[i] = blocks[i].GetComponent<BlockVis>().getBlock();
                        }

                        spawn(blocksToDupe, 0);
                    }
                    
                }
                
                break;

            case "string_dup_times":
                if (!isEmpty() & !GameObject.Find("Int").GetComponent<IntStackController>().isEmpty()) {
                    index = GameObject.Find("Int").GetComponent<IntStackController>().blockStack.Peek().GetComponent<BlockVis>().getBlock().getIntValue() - 1;
                    GameObject.Find("Int").GetComponent<IntStackController>().bin(0,1);
                    if (index > 0) {
                        blocks = blockStack.ToArray();
                        Block[] dupedBlocks = new Block[index];
                        for (int i = 0; i < dupedBlocks.Length; i++) {
                            dupedBlocks[i] = blocks[0].GetComponent<BlockVis>().getBlock();
                        }
                        spawn(dupedBlocks, 0);
                    } else {
                        bin(0,1);
                    }
                    
                }
                
                break;
            
            case "string_empty":
                if (blockStack.Count > 0) {
                    blockData = new Block("False");
                }
                else {
                    blockData = new Block("True");
                }
                GameObject.Find("Bool").GetComponent<BoolStackController>().spawn(blockData, 0);
                break;

            case "string_empty_string":
                input1 = blocks[0].GetComponent<BlockVis>().ToString();
                bin(0,1);
                blockData = new Block("False");
                if (input1.Length == 2) {
                    blockData = new Block("True");
                }
                GameObject.Find("Bool").GetComponent<BoolStackController>().spawn(blockData, 0);
                break;

            case "string_eq":
                if (blockStack.Count >= 2) {
                    input1 = blocks[0].GetComponent<BlockVis>().ToString();
                    input2 = blocks[1].GetComponent<BlockVis>().ToString();
                    bin(0,2);

                    blockData = new Block("False");
                    if (input1 == (input2)) {
                        blockData = new Block("True");
                    }

                    GameObject.Find("Bool").GetComponent<BoolStackController>().spawn(blockData, 0);
                }
                
                break;

            case "string_flush":
                reset();
                break;

            case "string_from_boolean":
                if (!GameObject.Find("Bool").GetComponent<BoolStackController>().isEmpty()) {
                    bool fromBool = GameObject.Find("Bool").GetComponent<BoolStackController>().blockStack.Peek().GetComponent<BlockVis>().getBlock().getBoolValue();
                    GameObject.Find("Bool").GetComponent<BoolStackController>().bin(0,1);
                    string boolString = "";
                    boolString += '"';
                    print(fromBool);
                    if (fromBool) {
                        boolString += "True";
                    }
                    else {
                        boolString += "False";
                    }
                    boolString += '"';
                    blockData = new Block(boolString);
                    spawn(blockData, 0);
                }
                
                break;

            case "string_from_integer":
                if (!GameObject.Find("Int").GetComponent<IntStackController>().isEmpty()) {
                    string intString = GameObject.Find("Int").GetComponent<IntStackController>().blockStack.Peek().GetComponent<BlockVis>().ToString();
                    GameObject.Find("Int").GetComponent<IntStackController>().bin(0,1);
                    blockData = new Block('"' + intString + '"');
                    spawn(blockData, 0);
                }
                break;

            case "string_length":
                if (!isEmpty()) {
                    input1 = blocks[0].GetComponent<BlockVis>().ToString();
                    bin(0,1);
                    blockData = new Block((input1.Length-2).ToString());
                    GameObject.Find("Int").GetComponent<IntStackController>().spawn(blockData, 0);
                }
                
                break;

            case "string_parse_to_chars":
                if (!isEmpty()) {
                    input1 = blocks[0].GetComponent<BlockVis>().ToString();
                    bin(0,1);
                    char[] charArr = input1.ToCharArray();
                    Block[] charBlocks = new Block[charArr.Length];
                    for (int i = 1; i < charArr.Length - 1; i++) {
                        charBlocks[i] = new Block(charArr[i].ToString());
                    }
                    //Array.Reverse(charBlocks);
                    spawn(charBlocks, 0);
                }
                
                break;
            
            case "string_pop":
                if (!isEmpty()) {
                    bin(0,1);
                }
                break;

            case "string_reverse":
                if (!isEmpty()) {
                    input1 = blocks[0].GetComponent<BlockVis>().ToString();
                    bin(0,1);
                    char[] reversedChar = input1.ToCharArray();
                    Array.Reverse(reversedChar);
                    
                    blockData = new Block(new string(reversedChar));
                    spawn(blockData, 0);
                }
                break;

            case "string_rot":
                if (blockStack.Count >= 3) {
                    input1  = blocks[0].GetComponent<BlockVis>().getBlock().ToString();
                    input2 = blocks[1].GetComponent<BlockVis>().getBlock().ToString();
                    string rotBool = blocks[2].GetComponent<BlockVis>().getBlock().ToString();

                    bin(0,3);

                    Block[] rotArray = new Block[3];
                    rotArray[0] = new Block(rotBool.ToString());
                    rotArray[1] = new Block(input1.ToString());
                    rotArray[2] = new Block(input2.ToString());

                    spawn(rotArray, 0);
                }
                

                break;

            case "string_shove":
                StartCoroutine(string_shove());
                StartCoroutine(reshuffle());
                break;

            case "string_stack_depth":
                int stackDepth = blockStack.Count;
                blockData = new Block(stackDepth.ToString());
                GameObject.Find("Int").GetComponent<IntStackController>().spawn(blockData, 0);
                break;
            
            case "string_swap":
                if (blockStack.Count >= 2) {
                    input1  = blocks[0].GetComponent<BlockVis>().getBlock().ToString();
                    input2 = blocks[1].GetComponent<BlockVis>().getBlock().ToString();

                    bin(0,2);

                    Block[] swapArray = new Block[2];
                    swapArray[0] = new Block(input2.ToString());
                    swapArray[1] = new Block(input1.ToString());

                    spawn(swapArray, 0);
                }
                

                break;
            
            case "string_take":
                if (!isEmpty() & !GameObject.Find("Int").GetComponent<IntStackController>().isEmpty()) {
                    index = GameObject.Find("Int").GetComponent<IntStackController>().blockStack.Peek().GetComponent<BlockVis>().getBlock().getIntValue();
                    bin(0,1);
                    
                    GameObject.Find("Int").GetComponent<IntStackController>().bin(0,1);
                    input1  = blocks[0].GetComponent<BlockVis>().getBlock().ToString();
                    if (index >= input1.Length) {
                        index = input1.Length - 1;
                    }

                    blockData = new Block(input1.Substring(1,index));
                    spawn(blockData, 0);
                }
                
                break;

            case "string_yank":
                StartCoroutine(string_yank());
                StartCoroutine(reshuffle());
                break;

            case "string_yank_dup":
                if (!GameObject.Find("Int").GetComponent<IntStackController>().isEmpty() & !isEmpty()) {
                    index = GameObject.Find("Int").GetComponent<IntStackController>().blockStack.Peek().GetComponent<BlockVis>().getBlock().getIntValue();
                    GameObject.Find("Int").GetComponent<IntStackController>().bin(0,1);

                    if (index >= 0) {
                        blocks = blockStack.ToArray();
                        if (index >= blocks.Length) {
                            index = blocks.Length - 1;
                        }

                        blockData = blocks[index].GetComponent<BlockVis>().getBlock();
                        spawn(blockData, 0);
                    }
                    
                }
                
                break;

            IEnumerator string_yank() {
                if (blockStack.Count >= 2 & !GameObject.Find("Int").GetComponent<IntStackController>().isEmpty()) {
                    index = GameObject.Find("Int").GetComponent<IntStackController>().blockStack.Peek().GetComponent<BlockVis>().getBlock().getIntValue();
                    GameObject.Find("Int").GetComponent<IntStackController>().bin(0,1);

                    if (index >= 0) {
                        blocks = blockStack.ToArray();
                        if (index >= blockStack.Count - 1) {
                            index = blockStack.Count - 1;
                        }
                        blockData = blocks[index].GetComponent<BlockVis>().getBlock();
                        bin(index, 1);
                        spawn(blockData, 0);
                    }
                    
                }
                
                yield return new WaitForSeconds(0.1f);
            }

            IEnumerator string_shove() {
                if (blockStack.Count >= 2 & !GameObject.Find("Int").GetComponent<IntStackController>().isEmpty()) {
                    index = GameObject.Find("Int").GetComponent<IntStackController>().blockStack.Peek().GetComponent<BlockVis>().getBlock().getIntValue();
                    GameObject.Find("Int").GetComponent<IntStackController>().bin(0,1);

                    if (index >= 0) {
                        blocks = blockStack.ToArray();
                        if (index >= blockStack.Count - 1) {
                            index = blockStack.Count - 1;
                        }
                        blockData = blocks[index].GetComponent<BlockVis>().getBlock();
                        bin(index, 1);
                        spawn(blockData, blockStack.Count);
                    }
                    
                }
                
                yield return new WaitForSeconds(0.1f);
            }
        }
    }
    
}
