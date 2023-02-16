using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExecStackController : StackController
{   
    [SerializeField] GameObject intCarat;
    [SerializeField] GameObject stringCarat;
    [SerializeField] GameObject boolCarat;
    

    public void forward() {

        snapshot();
        intCarat.GetComponent<IntStackController>().snapshot();
        stringCarat.GetComponent<StringStackController>().snapshot();
        boolCarat.GetComponent<BoolStackController>().snapshot();

        GameObject blockObj = blockStack.Peek();
        print(blockObj.GetComponent<BlockVis>().ToString());
        Block blockData = blockObj.GetComponent<BlockVis>().getBlock();

        switch (blockData.getType()) {
            case "Integer":
                intCarat.GetComponent<IntStackController>().spawn(new Block[] {blockData}, 0);
                bin(0,1);
                break;

            case "String":
                stringCarat.GetComponent<StringStackController>().spawn(new Block[] {blockData}, 0);
                bin(0,1);
                break;

            case "Boolean":
                boolCarat.GetComponent<BoolStackController>().spawn(new Block[] {blockData}, 0);
                bin(0,1);
                break;

            case "Function":
                if (FunctionChecker.check(blockData.ToString())) {
                    string func = blockData.ToString().Remove(0,1);
                    processFunction(func);
                }
                bin(0,1);
                break;

            case "Bracket":
                unpack();
                break;
        }

        //System.Threading.Thread.Sleep(1000);
    }


    public void backward() {
        GameObject.Find("FFW").GetComponent<FFWController>().turnOff();
        this.undo();
        intCarat.GetComponent<IntStackController>().undo();
        stringCarat.GetComponent<StringStackController>().undo();
        boolCarat.GetComponent<BoolStackController>().undo();
    }

    private void processFunction(string f) {

        switch (f.Substring(0,3)) {
            case "int":
                GameObject intCarat = GameObject.Find("Int");
                intCarat.GetComponent<IntStackController>().process(f);
                break;
            case "boo":
                GameObject boolCarat = GameObject.Find("Bool");
                boolCarat.GetComponent<BoolStackController>().process(f);
                break;
            case "str":
                GameObject stringCarat = GameObject.Find("String");
                stringCarat.GetComponent<StringStackController>().process(f);
                break;
            case "exe":
                process(f);
                break;
        }
    }

    //Unpacks the next bracket pair
    private void unpack() {
        GameObject[] stackArray = blockStack.ToArray();
        int LBIndex = 0;
        int toSkip = 0;
        
        //first for loop to find the first bracket
        for (int i = 0; i < stackArray.Length; i++) {
            if (stackArray[i].GetComponent<BlockVis>().ToString() == "(") {
                LBIndex = i;
                break;
            }
        }

        //second for loop to find the pair
        for (int i = LBIndex + 1; i < stackArray.Length; i++) {
            if (stackArray[i].GetComponent<BlockVis>().ToString() == "(") {
                toSkip++;
            }
            else if (stackArray[i].GetComponent<BlockVis>().ToString() == ")" & toSkip != 0) {
                toSkip--;
            }
            else if (stackArray[i].GetComponent<BlockVis>().ToString() == ")" & toSkip == 0) {
                print("RB found");
                bin(i,1);
                bin(0,1);
                break;
            }
        }
        
        StartCoroutine(reshuffle());
    }

    //Extract blocks within the next bracket pair
    private Block[] extract() {
        GameObject[] stackArray = blockStack.ToArray();
        int index = 0;
        int toSkip = 0;

        List<Block> outputList = new List<Block>();
        Block[] outputArray = outputList.ToArray();

        for (int i = 0; i < stackArray.Length; i++) {
            if (stackArray[i].GetComponent<BlockVis>().ToString() == "(") {
                index = i;
                //print("Extracting at: " + i);
                break;
            }
        }

        for (int i = index + 1; i < stackArray.Length; i++) {
            if (stackArray[i].GetComponent<BlockVis>().ToString() == "(") {
                outputList.Add(stackArray[i].GetComponent<BlockVis>().getBlock());
                toSkip++;
            }
            else if (stackArray[i].GetComponent<BlockVis>().ToString() == ")" & toSkip != 0) {
                outputList.Add(stackArray[i].GetComponent<BlockVis>().getBlock());
                toSkip--;
            }
            else if (stackArray[i].GetComponent<BlockVis>().ToString() == ")" & toSkip == 0) {
                outputArray = outputList.ToArray();
                break;
            }
            else {
                outputList.Add(stackArray[i].GetComponent<BlockVis>().getBlock());
            }
        }

        return outputArray;

    }

    private Block[] extractAt(int index) {
        GameObject[] stackArray = blockStack.ToArray();
        int toSkip = 0;
        int LBIndex = 0;
        List<Block> outputList = new List<Block>();
        Block[] outputArray = outputList.ToArray();

        for (int i = index; i < stackArray.Length; i++) {
            if (stackArray[i].GetComponent<BlockVis>().ToString() == "(") {
                LBIndex = i;
                //print("Extracting at: " + i);
                break;
            }
        }

        for (int i = index + 1; i < stackArray.Length; i++) {
            if (stackArray[i].GetComponent<BlockVis>().ToString() == "(") {
                outputList.Add(stackArray[i].GetComponent<BlockVis>().getBlock());
                toSkip++;
            }
            else if (stackArray[i].GetComponent<BlockVis>().ToString() == ")" & toSkip != 0) {
                outputList.Add(stackArray[i].GetComponent<BlockVis>().getBlock());
                toSkip--;
            }
            else if (stackArray[i].GetComponent<BlockVis>().ToString() == ")" & toSkip == 0) {
                outputArray = outputList.ToArray();
                break;
            }
            else {
                outputList.Add(stackArray[i].GetComponent<BlockVis>().getBlock());
            }
        }

        return outputArray;

    }

    //Wrap in brackets
    private Block[] wrap(Block[] input) {

        Block[] output = new Block[input.Length + 2];
        
        output[0] = new Block("(");
        output[output.Length - 1] = new Block(")");

        for (int i = 1; i < output.Length - 1; i++) {
            output[i] = input[i-1];
        }

        return output;
    }

    private Block[] fetchParam() {
        Block[] param;
        GameObject[] blocks = blockStack.ToArray();
        if (blocks[1].GetComponent<BlockVis>().ToString() == "(") {
            param = wrap(extract());
        } else {param = new Block[1]{blocks[1].GetComponent<BlockVis>().getBlock()};}
        return param;
    }

    private Block[] fetchParamAt(int index) {
        Block[] param;
        GameObject[] blocks = blockStack.ToArray();
        if (blocks[index].GetComponent<BlockVis>().ToString() == "(") {
            param = wrap(extractAt(index));
        } else {param = new Block[1]{blocks[index].GetComponent<BlockVis>().getBlock()};}
        return param;

    }

    private List<Block[]> groupParams() {
        List<Block[]> paramGroup = new List<Block[]>();
        int index = 0;
        while (index < blockStack.Count) {
            Block[] paramArray = fetchParamAt(index);
            paramGroup.Add(paramArray);
            index += paramArray.Length;
        }
        return paramGroup;
    }

    private int index;
    private GameObject[] blocks;
    private List<Block[]> paramGroup;
    private Block blockData;
    private List<Block> outputList;
    private Block[] outputArray;
    private void process(string func) {
        switch(func) {
            case "exec_deep_dup":
                if (!intCarat.GetComponent<IntStackController>().isEmpty() & blockStack.Count > 1) {
                    index = intCarat.GetComponent<IntStackController>().blockStack.Peek().GetComponent<BlockVis>().getBlock().getIntValue();
                    intCarat.GetComponent<IntStackController>().bin(0,1);

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

            case "exec_do_count":
                if (!intCarat.GetComponent<IntStackController>().isEmpty() & blockStack.Count > 1) {
                    Block[] doCountArray = fetchParam();
                    index = intCarat.GetComponent<IntStackController>().blockStack.Peek().GetComponent<BlockVis>().getBlock().getIntValue();
                    intCarat.GetComponent<IntStackController>().bin(0,1);
                    bin(0, doCountArray.Length);

                    outputList = new List<Block>();
                    outputList.Add(new Block("0"));
                    outputList.Add(new Block((index-1).ToString()));
                    outputList.Add(new Block(":exec_do_range"));
                    foreach (Block b in doCountArray) {
                        outputList.Add(b);
                    }
                    outputArray = outputList.ToArray();
                    spawn(wrap(outputArray), 0);
                }
                break;

            case "exec_do_range":
                if (!intCarat.GetComponent<IntStackController>().isEmpty() & blockStack.Count > 1) {
                    Block[] doRangeArray = fetchParam();
                    //print(doRangeArray.Length);
                    index = intCarat.GetComponent<IntStackController>().blockStack.Peek().GetComponent<BlockVis>().getBlock().getIntValue();
                    intCarat.GetComponent<IntStackController>().bin(0,1);
                    int currentDoCount = intCarat.GetComponent<IntStackController>().blockStack.Peek().GetComponent<BlockVis>().getBlock().getIntValue();
                    bin(0, doRangeArray.Length);

                    outputList = new List<Block>();
                    if (currentDoCount != index) {
                        foreach (Block b in doRangeArray) {
                            outputList.Add(b);
                        }
                        outputList.Add(new Block("("));
                        if (currentDoCount > index) {currentDoCount--;} else {currentDoCount++;}
                        outputList.Add(new Block(currentDoCount.ToString()));
                        outputList.Add(new Block(index.ToString()));
                        outputList.Add(new Block(":exec_do_range"));
                        foreach (Block b in doRangeArray) {
                            outputList.Add(b);
                        }
                        outputList.Add(new Block(")"));
                    } 
                    else if (currentDoCount == index) {
                    foreach (Block b in doRangeArray) {
                            outputList.Add(b);
                        }
                    }
                    
                    outputArray = outputList.ToArray();
                    spawn(outputArray, 0);
                }
                
                break;

            case "exec_do_times":
                if (!intCarat.GetComponent<IntStackController>().isEmpty() & blockStack.Count > 1) {
                    Block[] doTimesArray = fetchParam();
                    index = intCarat.GetComponent<IntStackController>().blockStack.Peek().GetComponent<BlockVis>().getBlock().getIntValue();

                    if (index > 0) {
                        intCarat.GetComponent<IntStackController>().bin(0,1);
                        bin(0, doTimesArray.Length);

                        outputList = new List<Block>();
                        outputList.Add(new Block("0"));
                        outputList.Add(new Block((index-1).ToString()));
                        outputList.Add(new Block(":exec_do_range"));
                        outputList.Add(new Block("("));
                        outputList.Add(new Block(":integer_pop"));
                        foreach (Block b in doTimesArray) {
                            outputList.Add(b);
                        }
                        outputList.Add(new Block(")"));
                        
                        outputArray = outputList.ToArray();
                        spawn(outputArray, 0);
                    }
                }
                //(0 index-1 :exec_do_range_ (:integer_pop doTimesArray))
                
                
                break;

            case "exec_do_while":
                if (blockStack.Count > 1) {
                    Block[] doWhileArray = fetchParam();
                    bin(0, doWhileArray.Length);
                    outputList = new List<Block>();
                    foreach (Block b in doWhileArray) {
                        outputList.Add(b);
                    }
                    outputList.Add(new Block(":exec_while"));
                    foreach (Block b in doWhileArray) {
                        outputList.Add(b);
                    }
                        
                    outputArray = outputList.ToArray();
                    spawn(outputArray, 0);
                }
                break;

            case "exec_dup":
                if (blockStack.Count > 1) {
                    Block[] execDup = fetchParam();
                    spawn(execDup,0);
                }
                
                break;

            case "exec_dup_times":
                if (!intCarat.GetComponent<IntStackController>().isEmpty() & blockStack.Count > 1) {
                    Block[] dupTimes = fetchParam();
                    index = intCarat.GetComponent<IntStackController>().blockStack.Peek().GetComponent<BlockVis>().getBlock().getIntValue();
                    intCarat.GetComponent<IntStackController>().bin(0,1);

                    if (index > 0) {
                        outputList = new List<Block>();

                        for (int i = 0; i < index - 1; i++) {
                            foreach (Block b in dupTimes) {
                                outputList.Add(b);
                            }
                        }
                        outputArray = outputList.ToArray();
                        if (outputArray.Length > 0) {
                            spawn(outputArray, 0);
                        }
                    } else {
                        blocks = blockStack.ToArray();
                        if(blocks[1].GetComponent<BlockVis>().ToString() != "(") {
                            bin(0,1);
                        } else {
                            int brCounter = 1;
                            int blocksCovered = 1;
                            for (int i = 2; i < blocks.Length; i++) {
                                blocksCovered++;
                                if (blocks[i].GetComponent<BlockVis>().ToString() == "(") {
                                    brCounter++;
                                }
                                else if (brCounter > 1 && blocks[i].GetComponent<BlockVis>().ToString() == ")") {
                                    brCounter--;
                                }
                                else if (blocks[i].GetComponent<BlockVis>().ToString() == ")") {
                                    bin(0,blocksCovered);
                                    break;
                                }
                            }
                        }
                    }
                    
                }
                
                break;

            case "exec_empty":
                index = blockStack.Count;
                blockData = new Block("False");
                if (index <= 1) {
                    blockData = new Block("True");
                }
                boolCarat.GetComponent<BoolStackController>().spawn(blockData, 0);
                break;

            case "exec_eq":
                if (blockStack.Count > 2) {
                    blocks = blockStack.ToArray();
                    Block eq1 = blocks[1].GetComponent<BlockVis>().getBlock();
                    Block eq2 = blocks[2].GetComponent<BlockVis>().getBlock();
                    Block eqOutput = new Block("False");
                    if (eq1.getType() == eq2.getType() & eq1.ToString() == eq2.ToString()) {
                        eqOutput = new Block("True");
                    }
                    boolCarat.GetComponent<BoolStackController>().spawn(eqOutput, 0);
                }
                
                break;

            case "exec_flush":
                reset();
                break;

            case "exec_if":
                if (!boolCarat.GetComponent<BoolStackController>().isEmpty() & blockStack.Count > 2) {
                    bool ifBool = boolCarat.GetComponent<BoolStackController>().blockStack.Peek().GetComponent<BlockVis>().getBlock().getBoolValue();
                    boolCarat.GetComponent<BoolStackController>().bin(0,1);
                    Block[] ifArray1 = fetchParam();
                    Block[] ifArray2 = fetchParamAt(ifArray1.Length + 1);

                    // string log1 = "";
                    // foreach (Block b in ifArray1) {
                    //     log1 += b.ToString() + " ";
                    // }
                    // string log2 = "";
                    // foreach (Block b in ifArray2) {
                    //     log2 += b.ToString() + " ";
                    // }
                    // print(log1);
                    // print(log2);

                    if (ifBool) {
                        bin(ifArray1.Length + 1, ifArray2.Length);
                    } else {
                        bin(0, ifArray1.Length);
                    }

                    StartCoroutine(reshuffle());
                }
                break;

            case "exec_k":
                if (blockStack.Count > 2) {
                    bin(2,1);
                    StartCoroutine(reshuffle());
                }
                break;

            case "exec_pop":
                if (blockStack.Count > 1) {
                    Block[] popArray = fetchParam();
                    bin(0,popArray.Length);
                }   
                break;

            case "exec_rot":
                
                if (blockStack.Count > 3) {
                    paramGroup = groupParams();
                    // foreach (Block[] ba in paramGroup) {
                    //     string debugString = "";
                    //     foreach(Block b in ba) {
                    //         debugString += b.ToString();
                    //     }
                    //     print("Param group: " + debugString);
                    // } 
                    if (paramGroup.Count > 3) {
                        Block[] rot1 = paramGroup[1];
                        Block[] rot2 = paramGroup[2];
                        Block[] rot3 = paramGroup[3];
                        bin(0, rot1.Length + rot2.Length + rot3.Length);

                        outputList = new List<Block>();
                        foreach (Block b in rot3) {
                            outputList.Add(b);
                        }
                        
                        foreach (Block b in rot1) {
                            outputList.Add(b);
                        }

                        foreach (Block b in rot2) {
                            outputList.Add(b);
                        }

                        outputArray = outputList.ToArray();
                        spawn(outputArray, 0);
                    }
                
                }
                
                
                break;

            case "exec_s":
                paramGroup = groupParams();
                if (paramGroup.Count > 3) {
                    Block[] A = fetchParam();
                    Block[] B = fetchParamAt(A.Length + 1);
                    Block[] C = fetchParamAt(A.Length + B.Length + 1);
                    bin(0, A.Length + B.Length + C.Length);

                    outputList = new List<Block>();
                    foreach(Block b in A) {
                        outputList.Add(b);
                    }
                    foreach(Block b in C) {
                        outputList.Add(b);
                    }
                    outputList.Add(new Block("("));
                    foreach(Block b in B) {
                        outputList.Add(b);
                    }
                    foreach(Block b in C) {
                        outputList.Add(b);
                    }
                    outputList.Add(new Block(")"));
                    
                    outputArray = outputList.ToArray();
                    spawn(outputArray, 0);
                }
                
                break; 

            case "exec_shove":
                paramGroup = groupParams();
                if (!intCarat.GetComponent<IntStackController>().isEmpty() & paramGroup.Count > 2) {
                    index = intCarat.GetComponent<IntStackController>().blockStack.Peek().GetComponent<BlockVis>().getBlock().getIntValue() + 1;
                    intCarat.GetComponent<IntStackController>().bin(0,1);

                    if (index >= 0) {
                        if (index > paramGroup.Count - 1) {
                            index = paramGroup.Count - 1;
                        }
                        Block[] shoveArray = paramGroup[index];
                        int binIndex = 0;
                        for (int i = 0; i < index; i++) {
                            binIndex += paramGroup[i].Length;
                        }
                        bin(binIndex, shoveArray.Length);
                        spawn(shoveArray, blockStack.Count - 1);
                        StartCoroutine(reshuffle());
                    }
                    
                }
                
                break;

            case "exec_stack_depth":
                index = groupParams().Count - 1;
                spawn(new Block(index.ToString()), 0);
                break;

            case "exec_swap":
                paramGroup = groupParams();
                if (paramGroup.Count > 2) {
                    Block[] swap1 = paramGroup[1];
                    Block[] swap2 = paramGroup[2];
                    bin(0, swap1.Length + swap2.Length);

                    outputList = new List<Block>();
                    foreach (Block b in swap2) {
                        outputList.Add(b);
                    }
                    foreach (Block b in swap1) {
                        outputList.Add(b);
                    }

                    outputArray = outputList.ToArray();
                    spawn(outputArray, 0);
                }
                break;

            case "exec_when":
                paramGroup = groupParams();
                if (paramGroup.Count >= 2 & !boolCarat.GetComponent<BoolStackController>().isEmpty()) {
                    bool whenBool = boolCarat.GetComponent<BoolStackController>().blockStack.Peek().GetComponent<BlockVis>().getBlock().getBoolValue();
                    boolCarat.GetComponent<BoolStackController>().bin(0,1);
                    if (!whenBool) {
                        bin(0, paramGroup[1].Length);
                    }
                }
                break;

            case "exec_while":
                
                if (!boolCarat.GetComponent<BoolStackController>().isEmpty() & blockStack.Count > 1) {
                    Block[] whileArray = fetchParam();
                    bool whileBool = boolCarat.GetComponent<BoolStackController>().blockStack.Peek().GetComponent<BlockVis>().getBlock().getBoolValue();
                    boolCarat.GetComponent<BoolStackController>().bin(0,1);
                    bin(0, whileArray.Length);

                    if (whileBool) {
                        outputList = new List<Block>();
                        foreach (Block b in whileArray) {
                            outputList.Add(b);
                        }
                        outputList.Add(new Block(":exec_while"));
                        foreach (Block b in whileArray) {
                            outputList.Add(b);
                        }
                        
                        outputArray = outputList.ToArray();
                        spawn(outputArray, 0);
                    }
                }
                
                
                break;

            case "exec_y":
                if (blockStack.Count > 1) {
                    Block[] yArray= fetchParam();
                    bin(0, yArray.Length);

                    outputList = new List<Block>();
                    foreach(Block b in yArray) {
                        outputList.Add(b);
                    }
                    outputList.Add(new Block("("));
                    outputList.Add(new Block(":exec_y"));
                    foreach(Block b in yArray) {
                        outputList.Add(b);
                    }
                    outputList.Add(new Block(")"));

                    outputArray = outputList.ToArray();
                    spawn(outputArray, 0);
                }
                
                break;

            case "exec_yank":
                if (!intCarat.GetComponent<IntStackController>().isEmpty() & blockStack.Count > 2) {
                    index = intCarat.GetComponent<IntStackController>().blockStack.Peek().GetComponent<BlockVis>().getBlock().getIntValue() + 1;
                    intCarat.GetComponent<IntStackController>().bin(0,1);

                    if (index >= 0) {
                        paramGroup = groupParams();

                        if (index > paramGroup.Count - 1) {
                            index = paramGroup.Count - 1;
                        }
                        Block[] yankArray = paramGroup[index];
                        int binI = 0;
                        for (int i = 0; i < index; i++) {
                            binI += paramGroup[i].Length;
                        }
                        bin(binI, yankArray.Length);
                        spawn(yankArray, 0);
                        StartCoroutine(reshuffle());
                    }
                    
                }
                
                break;

            case "exec_yank_dup":
                if (!intCarat.GetComponent<IntStackController>().isEmpty() & blockStack.Count > 1) {
                    index = intCarat.GetComponent<IntStackController>().blockStack.Peek().GetComponent<BlockVis>().getBlock().getIntValue() + 1;
                    intCarat.GetComponent<IntStackController>().bin(0,1);
                    if (index >= 0) {
                        paramGroup = groupParams();

                        if (index > paramGroup.Count - 1) {
                            index = paramGroup.Count - 1;
                        }
                        Block[] yankDupArray = paramGroup[index];
                        spawn(yankDupArray, 0);
                        StartCoroutine(reshuffle());
                    }
                    
                }
                
                break;
        }
    }
}
