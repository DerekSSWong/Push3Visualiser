using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoolStackController : StackController
{
    public void process(string func) {
        GameObject[] blocks = blockStack.ToArray();
        Block blockData;
        bool input1;
        bool input2;
        int index;

        switch(func) {
            case "boolean_and":
                if (blockStack.Count >= 2) {
                    input1 = blocks[0].GetComponent<BlockVis>().getBlock().getBoolValue();
                    input2 = blocks[1].GetComponent<BlockVis>().getBlock().getBoolValue();
                    bin(0,2);
                    blockData = new Block("False");
                    if (input1 == input2 && input1 == true) {
                        blockData = new Block("True");
                    }
                    spawn(blockData, 0);
                }
                
                break;

            case "boolean_deep_dup":
                if (blockStack.Count >= 1 & !GameObject.Find("Int").GetComponent<IntStackController>().isEmpty()) {
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
            
            case "boolean_dup":
                if (!isEmpty()) {
                    input1 = blocks[0].GetComponent<BlockVis>().getBlock().getBoolValue();
                    blockData = new Block(input1.ToString());
                    spawn(blockData, 0);
                }
                
                break;

            case "boolean_dup_items":
                if (blockStack.Count >= 1 & !GameObject.Find("Int").GetComponent<IntStackController>().isEmpty()) {
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

            case "boolean_dup_times":
                if (blockStack.Count >= 1 & !GameObject.Find("Int").GetComponent<IntStackController>().isEmpty()) {
                    index = GameObject.Find("Int").GetComponent<IntStackController>().blockStack.Peek().GetComponent<BlockVis>().getBlock().getIntValue() - 1;
                    GameObject.Find("Int").GetComponent<IntStackController>().bin(0,1);

                    if (index >= 0) {
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

            
            case "boolean_empty":
                if (blockStack.Count > 0) {
                    blockData = new Block("False");
                }
                else {
                    blockData = new Block("True");
                }
                spawn(blockData, 0);
                break;

            case "boolean_eq":
                if (blockStack.Count >= 2) {
                    input1 = blocks[0].GetComponent<BlockVis>().getBlock().getBoolValue();
                    input2 = blocks[1].GetComponent<BlockVis>().getBlock().getBoolValue();
                    bin(0,2);
                    blockData = new Block("False");
                    if (input1 == input2) {
                        blockData = new Block("True");
                    }
                    spawn(blockData, 0);
                }
                
                break;

            case "boolean_flush":
                reset();
                break;

            case "boolean_from_integer":
                int fromInteger = GameObject.Find("Int").GetComponent<IntStackController>().blockStack.Peek().GetComponent<BlockVis>().getBlock().getIntValue();
                blockData = new Block("True");
                if (fromInteger == 0) {
                    blockData = new Block("False");
                }
                spawn(blockData, 0);
                break;

            case "boolean_invert_first_then_and":
                if (blockStack.Count >= 2) {
                    input1 = blocks[0].GetComponent<BlockVis>().getBlock().getBoolValue();
                    input2 = blocks[1].GetComponent<BlockVis>().getBlock().getBoolValue();
                    bin(0,2);
                    blockData = new Block("False");
                    if (!input1 == input2 && input2 == true) {
                        blockData = new Block("True");
                    }
                    spawn(blockData, 0);
                }
                
                break;

            case "boolean_invert_second_then_and":
                if (blockStack.Count >= 2) {
                    input1 = blocks[0].GetComponent<BlockVis>().getBlock().getBoolValue();
                    input2 = blocks[1].GetComponent<BlockVis>().getBlock().getBoolValue();
                    bin(0,2);
                    blockData = new Block("False");
                    if (input1 == !input2 && input1 == true) {
                        blockData = new Block("True");
                    }
                    spawn(blockData, 0);
                }
                
                break;

            case "boolean_not":
                if (!isEmpty()) {
                    input1 = blocks[0].GetComponent<BlockVis>().getBlock().getBoolValue();
                    blockData = new Block((!input1).ToString());
                    bin(0,1);
                    spawn(blockData, 0);
                }
                
                break;

            case "boolean_or":
                if (blockStack.Count >= 2) {
                    input1 = blocks[0].GetComponent<BlockVis>().getBlock().getBoolValue();
                    input2 = blocks[1].GetComponent<BlockVis>().getBlock().getBoolValue();
                    bin(0,2);
                    blockData = new Block("False");
                    if (input1 || input2) {
                        blockData = new Block("True");
                    }
                    spawn(blockData, 0);
                }
                
                break;

            case "boolean_pop":
                if (!isEmpty()) {
                    bin(0,1);
                }
                
                break;

            case "boolean_rot":
                if (blockStack.Count >= 3) {
                    input1  = blocks[0].GetComponent<BlockVis>().getBlock().getBoolValue();
                    input2 = blocks[1].GetComponent<BlockVis>().getBlock().getBoolValue();
                    bool rotBool = blocks[2].GetComponent<BlockVis>().getBlock().getBoolValue();

                    bin(0,3);

                    Block[] rotArray = new Block[3];
                    rotArray[0] = new Block(rotBool.ToString());
                    rotArray[1] = new Block(input1.ToString());
                    rotArray[2] = new Block(input2.ToString());

                    spawn(rotArray, 0);
                }
                

                break;
            
            case "boolean_shove":
                StartCoroutine(boolean_shove());
                StartCoroutine(reshuffle());
                break;

            case "boolean_stack_depth":
                int stackDepth = blockStack.Count;
                blockData = new Block(stackDepth.ToString());
                GameObject.Find("Int").GetComponent<IntStackController>().spawn(blockData, 0);
                break;

            case "boolean_swap":
                if (blockStack.Count >= 2) {
                    input1  = blocks[0].GetComponent<BlockVis>().getBlock().getBoolValue();
                    input2 = blocks[1].GetComponent<BlockVis>().getBlock().getBoolValue();

                    bin(0,2);

                    Block[] swapArray = new Block[2];
                    swapArray[0] = new Block(input2.ToString());
                    swapArray[1] = new Block(input1.ToString());

                    spawn(swapArray, 0);
                }
                
                break;

            case "boolean_xor":
                if (blockStack.Count >= 2) {
                    input1 = blocks[0].GetComponent<BlockVis>().getBlock().getBoolValue();
                    input2 = blocks[1].GetComponent<BlockVis>().getBlock().getBoolValue();
                    bin(0,2);
                    blockData = new Block("True");
                    if (input1 == input2) {
                        blockData = new Block("False");
                    }
                    spawn(blockData, 0);
                }
                
                break;

            case "boolean_yank_dup":
                if (blockStack.Count >= 1 & !GameObject.Find("Int").GetComponent<IntStackController>().isEmpty()) {
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


            IEnumerator boolean_yank() {
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

            IEnumerator boolean_shove() {
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
