using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class IntStackController : StackController
{
    public void process(string func) {
        GameObject[] blocks = blockStack.ToArray();
        Block blockData;
        int intInput_1;
        int intInput_2;

        switch (func) {
            case "integer_add":
                if (blockStack.Count >= 2) {
                    intInput_1  = blocks[0].GetComponent<BlockVis>().getBlock().getIntValue();
                    intInput_2 = blocks[1].GetComponent<BlockVis>().getBlock().getIntValue();
                    int result = intInput_1 + intInput_2;
                    bin(0,2);
                    Block resultBlock = new Block(result.ToString());
                    spawn(new Block[] {resultBlock}, 0);
                }
                
                break;

            case "integer_dec":
                if (!isEmpty()) {
                    intInput_1 = blocks[0].GetComponent<BlockVis>().getBlock().getIntValue();
                    intInput_1--;
                    bin(0,1);
                    spawn(new Block(intInput_1.ToString()), 0);
                }
                break;

            case "integer_dup":
                if (!isEmpty()) {
                    intInput_1 = blocks[0].GetComponent<BlockVis>().getBlock().getIntValue();
                    spawn(new Block(intInput_1.ToString()), 0);
                }
                break;

            case "integer_deep_dup":
                StartCoroutine(integer_deep_dup());
                StartCoroutine(reshuffle());
                break;

            case "integer_yank":
                StartCoroutine(integer_yank());
                StartCoroutine(reshuffle());
                break;

            case "integer_shove":
                StartCoroutine(integer_shove());
                StartCoroutine(reshuffle());
                break;

            case "integer_dup_items":
                StartCoroutine(integer_dup_items());
                break;
            
            case "integer_dup_times":
                StartCoroutine(integer_dup_times());
                break;

            case "integer_empty":
                if (blockStack.Count > 0) {
                    blockData = new Block("False");
                }
                else {
                    blockData = new Block("True");
                }
                GameObject.Find("Bool").GetComponent<BoolStackController>().spawn(blockData, 0);
                break;

            case "integer_flush":
                reset();
                break;

            case "integer_eq":
                if (blockStack.Count >= 2) {
                    intInput_1  = blocks[0].GetComponent<BlockVis>().getBlock().getIntValue();
                    intInput_2 = blocks[1].GetComponent<BlockVis>().getBlock().getIntValue();
                    
                    bin(0,2);

                    if (intInput_1 == intInput_2) {
                        blockData = new Block("True");
                    } else {blockData = new Block("False");}
                    
                    GameObject.Find("Bool").GetComponent<BoolStackController>().spawn(blockData, 0);
                }
                break;

            case "integer_gt":
                if (blockStack.Count >= 2) {
                    intInput_1  = blocks[0].GetComponent<BlockVis>().getBlock().getIntValue();
                    intInput_2 = blocks[1].GetComponent<BlockVis>().getBlock().getIntValue();
                    bin(0,2);

                    if (intInput_2 > intInput_1) {
                    blockData = new Block("True");
                    } else {blockData = new Block("False");}

                    GameObject.Find("Bool").GetComponent<BoolStackController>().spawn(blockData, 0);
                }
                break;

            case "integer_gte":
                if (blockStack.Count >= 2) {
                    intInput_1  = blocks[0].GetComponent<BlockVis>().getBlock().getIntValue();
                    intInput_2 = blocks[1].GetComponent<BlockVis>().getBlock().getIntValue();
                    bin(0,2);

                    if (intInput_2 >= intInput_1) {
                    blockData = new Block("True");
                    } else {blockData = new Block("False");}

                    GameObject.Find("Bool").GetComponent<BoolStackController>().spawn(blockData, 0);
                }
                
                break;

            case "integer_inc":
                if (!isEmpty()) {
                    intInput_1 = blocks[0].GetComponent<BlockVis>().getBlock().getIntValue();
                    intInput_1++;
                    bin(0,1);
                    spawn(new Block(intInput_1.ToString()), 0);
                }
                
                break;

            case "integer_lt":
                if (blockStack.Count >= 2) {
                    intInput_1  = blocks[0].GetComponent<BlockVis>().getBlock().getIntValue();
                    intInput_2 = blocks[1].GetComponent<BlockVis>().getBlock().getIntValue();
                    bin(0,2);

                    if (intInput_2 < intInput_1) {
                    blockData = new Block("True");
                    } else {blockData = new Block("False");}

                    GameObject.Find("Bool").GetComponent<BoolStackController>().spawn(blockData, 0);
                }
                break;

            case "integer_lte":
                if (blockStack.Count >= 2) {
                    intInput_1  = blocks[0].GetComponent<BlockVis>().getBlock().getIntValue();
                    intInput_2 = blocks[1].GetComponent<BlockVis>().getBlock().getIntValue();
                    bin(0,2);

                    if (intInput_2 <= intInput_1) {
                    blockData = new Block("True");
                    } else {blockData = new Block("False");}
                }
                
                break;

            case "integer_max":
                if (blockStack.Count >= 2) {
                    intInput_1 = blocks[0].GetComponent<BlockVis>().getBlock().getIntValue();
                    intInput_2 = blocks[1].GetComponent<BlockVis>().getBlock().getIntValue();
                    bin(0,2);

                    if (intInput_1 >= intInput_2) {
                    blockData = new Block(intInput_1.ToString());
                    } else {blockData = new Block(intInput_2.ToString());}

                    spawn(blockData, 0);
                }
                
                break;

            case "integer_min":
                if (blockStack.Count >= 2) {
                    intInput_1  = blocks[0].GetComponent<BlockVis>().getBlock().getIntValue();
                    intInput_2 = blocks[1].GetComponent<BlockVis>().getBlock().getIntValue();
                    bin(0,2);

                    if (intInput_1 <= intInput_2) {
                    blockData = new Block(intInput_1.ToString());
                    } else {blockData = new Block(intInput_2.ToString());}

                    spawn(blockData, 0);
                }
                break;

            case "integer_mod":
                if (blockStack.Count >= 2) {
                    intInput_1  = blocks[0].GetComponent<BlockVis>().getBlock().getIntValue();
                    intInput_2 = blocks[1].GetComponent<BlockVis>().getBlock().getIntValue();
                    bin(0,2);

                    blockData = new Block((intInput_2 % intInput_1).ToString());
                    spawn(blockData, 0);
                }
                break;

            case "integer_mult":
                if (blockStack.Count >= 2) {
                    intInput_1  = blocks[0].GetComponent<BlockVis>().getBlock().getIntValue();
                    intInput_2 = blocks[1].GetComponent<BlockVis>().getBlock().getIntValue();
                    bin(0,2);

                    blockData = new Block((intInput_2 * intInput_1).ToString());
                    spawn(blockData, 0);
                }
                
                break;

            case "integer_pop":
                if (!isEmpty()) {
                    bin(0,1);
                }
                break;

            case "integer_quot":
                if (blockStack.Count >= 2) {
                    intInput_1  = blocks[0].GetComponent<BlockVis>().getBlock().getIntValue();
                    intInput_2 = blocks[1].GetComponent<BlockVis>().getBlock().getIntValue();
                    bin(0,2);

                    blockData = new Block((intInput_2 / intInput_1).ToString());
                    spawn(blockData, 0);
                }   
                break;

            case "integer_rot":
                if (blockStack.Count >= 3) {
                    intInput_1  = blocks[0].GetComponent<BlockVis>().getBlock().getIntValue();
                    intInput_2 = blocks[1].GetComponent<BlockVis>().getBlock().getIntValue();
                    int rotInt = blocks[2].GetComponent<BlockVis>().getBlock().getIntValue();

                    bin(0,3);

                    Block[] rotArray = new Block[3];
                    rotArray[0] = new Block(rotInt.ToString());
                    rotArray[1] = new Block(intInput_1.ToString());
                    rotArray[2] = new Block(intInput_2.ToString());

                    spawn(rotArray, 0);
                }
                

                break;

            case "integer_stack_depth":
                blockData = new Block(blockStack.Count().ToString());
                spawn(blockData, 0);
                break;

            case "integer_subtract":
                if (blockStack.Count >= 2) {
                    intInput_1  = blocks[0].GetComponent<BlockVis>().getBlock().getIntValue();
                    intInput_2 = blocks[1].GetComponent<BlockVis>().getBlock().getIntValue();
                    bin(0,2);

                    blockData = new Block((intInput_2 - intInput_1).ToString());
                    spawn(blockData, 0);
                }
                
                break;

            case "integer_swap":
                if (blockStack.Count >= 2) {
                    intInput_1  = blocks[0].GetComponent<BlockVis>().getBlock().getIntValue();
                    intInput_2 = blocks[1].GetComponent<BlockVis>().getBlock().getIntValue();

                    bin(0,2);

                    Block[] swapArray = new Block[2];
                    swapArray[0] = new Block(intInput_2.ToString());
                    swapArray[1] = new Block(intInput_1.ToString());

                    spawn(swapArray, 0);
                }
                

                break;

            case "integer_yank_dup":
                if (blockStack.Count >= 2) {
                    int yankIndex = blocks[0].GetComponent<BlockVis>().getBlock().getIntValue();
                    bin(0,1);

                    if (yankIndex >= 0) {
                        blocks = blockStack.ToArray();
                        if (yankIndex >= blocks.Length) {
                            yankIndex = blocks.Length - 1;
                        }

                        blockData = blocks[yankIndex].GetComponent<BlockVis>().getBlock();
                        spawn(blockData, 0);
                    }
                    
                }
                

                break;

            case "integer_from_boolean":
                if (!GameObject.Find("Bool").GetComponent<BoolStackController>().isEmpty()) {
                    bool boolInput = GameObject.Find("Bool").GetComponent<BoolStackController>().blockStack.Peek().GetComponent<BlockVis>().getBlock().getBoolValue();
                    GameObject.Find("Bool").GetComponent<BoolStackController>().bin(0,1);
                    
                    blockData = new Block("0");
                    if (boolInput) {
                        blockData = new Block("1");
                    }
                    spawn(blockData, 0);
                }
                
                break;

            case "integer_from_string":
                if (!GameObject.Find("String").GetComponent<StringStackController>().isEmpty()) {
                    string stringInput = GameObject.Find("String").GetComponent<StringStackController>().blockStack.Peek().GetComponent<BlockVis>().ToString();
                    GameObject.Find("String").GetComponent<StringStackController>().bin(0,1);
                    
                    int fromString = 0;
                    stringInput = stringInput.Substring(1,stringInput.Length -2 );
                    print(stringInput);
                    int.TryParse(stringInput, out fromString);
                    blockData = new Block(fromString.ToString());
                    spawn(blockData, 0);
                }
                
                break;
        }

        IEnumerator integer_deep_dup() {
            if (blockStack.Count >= 2) {
                int index = blocks[0].GetComponent<BlockVis>().getBlock().getIntValue();
                bin(0,1);

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
            
            yield return null;
        }

        IEnumerator integer_dup_items() {
            if (blockStack.Count >= 2) {
                int index = blocks[0].GetComponent<BlockVis>().getBlock().getIntValue();
                bin(0,1);
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
            
            yield return null;
        }

        IEnumerator integer_dup_times() {
            if (blockStack.Count >= 2) {
                int index = blocks[0].GetComponent<BlockVis>().getBlock().getIntValue() - 1;
                bin(0,1);
                if (index > 0) {
                    blocks = blockStack.ToArray();
                    Block[] blocksToDupe = new Block[index];
                    for (int i = 0; i < blocksToDupe.Length; i++) {
                        blocksToDupe[i] = blocks[0].GetComponent<BlockVis>().getBlock();
                    }
                    spawn(blocksToDupe, 0);
                }
                else {
                    bin(0,1);
                }
                
            }
            
            yield return null;
        }

        IEnumerator integer_yank() {
            if (blockStack.Count >= 3 ) {
                int index = blocks[0].GetComponent<BlockVis>().getBlock().getIntValue();
                bin(0,1);
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

        IEnumerator integer_shove() {
            if (blockStack.Count >= 3) {
                int index = blocks[0].GetComponent<BlockVis>().getBlock().getIntValue();
                bin(0,1);
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
