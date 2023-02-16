using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DocDisplay : MonoBehaviour
{   
    GameObject title;

    GameObject execReq;
    GameObject intReq;
    GameObject stringReq;
    GameObject boolReq;

    GameObject description;
    // Start is called before the first frame update
    void Start()
    {
        title = GameObject.Find("DocTitle");
        execReq = GameObject.Find("ExecReq");
        intReq = GameObject.Find("IntReq");
        stringReq = GameObject.Find("StringReq");
        boolReq = GameObject.Find("BoolReq");
        description = GameObject.Find("DocDescription");

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setDoc(string s) {
        for (int i = 0; i < instructionDoc.GetLength(0); i++) {
            if (instructionDoc[i,0] == s) {
                title.GetComponent<TextMeshProUGUI>().text = instructionDoc[i, 0];
                execReq.GetComponent<TextMeshProUGUI>().text = instructionDoc[i, 1];
                intReq.GetComponent<TextMeshProUGUI>().text = instructionDoc[i, 2];
                stringReq.GetComponent<TextMeshProUGUI>().text = instructionDoc[i, 3];
                boolReq.GetComponent<TextMeshProUGUI>().text = instructionDoc[i, 4];
                description.GetComponent<TextMeshProUGUI>().text = instructionDoc[i, 5];
            }
        }
        
    }

    //Instruction name, exec req, int req, string req, bool req, description
    private string[,] instructionDoc = new string[,] {
                                            {":boolean_and", 
                                            "0", "0", "0", "2", 
                                            "Returns True if and only if the first 2 items in the Boolean stack is True, else returns False"},
                                            {":boolean_deep_dup", 
                                            "0", "1", "0", "1", 
                                            "Duplicates the nth item in the Boolean stack counting from the bottom, n being the value of the topmost item taken from the Integer stack" +
                                                "If n is larger than the stack size, then the first item will be duplicated. If n is smaller than 0, then nothing will be duplicated"},
                                            {":boolean_dup", 
                                            "0", "0", "0", "1", 
                                            "Duplicates the topmost item in the Boolean stack"}, 
                                            {":boolean_dup_items",
                                            "0", "1", "0", "0+",
                                            "Duplicate the top n items in the boolean stack, n being the value of the topmost item taken from the Integer stack" + 
                                                "If n is smaller than 1, then nothing will be duplicated"}, 
                                            {":boolean_dup_times",
                                            "0", "1", "0", "1",
                                            "Takes the value of the topmost item in the Integer stack as n, if n is bigger than 0, takes the next topmost item in the Boolean stack and duplicates it n times. " +
                                                "If n is smaller than 0, the next topmost item is taken without duplication"}, 
                                            {":boolean_empty",
                                            "0", "0", "0", "0",
                                            "Returns True if the Boolean stack is empty, False if otherwise"}, 
                                            {":boolean_eq",
                                            "0", "0", "0", "2",
                                            "Takes the first 2 items in the Boolean stack, returns True if they are equal to each other, False if otherwise"}, 
                                            {":boolean_flush",
                                            "0", "0", "0", "0",
                                            "Clears the Boolean stack"},
                                            {":boolean_from_integer",
                                            "0", "1", "0", "0",
                                            "Takes the first item from the Integer stack, return True if it's a non-zero value, False if the value is 0"}, 
                                            {":boolean_invert_first_then_and",
                                            "0", "0", "0", "2",
                                            "Takes the first 2 items from the Boolean Stack, inverts the first value, then applies the 'And' operator to them"}, 
                                            {":boolean_invert_second_then_and",
                                            "0", "0", "0", "2",
                                            "Takes the first 2 items from the Boolean Stack, inverts the second value, then applies the 'And' operator to them"}, 
                                            {":boolean_not",
                                            "0", "0", "0", "1",
                                            "Takes the topmost item from the Boolean stack and inverts it"}, 
                                            {":boolean_or",
                                            "0", "0", "0", "2",
                                            "Takes the first 2 items from the Boolean stack, returns True is at least one of them is True, returns False otherwise"}, 
                                            {":boolean_pop",
                                            "0", "0", "0", "1",
                                            "Removes the topmost item in the Boolean stack"}, 
                                            {":boolean_rot",
                                            "0", "0", "0", "3",
                                            "Takes the first 3 items from the Boolean stack and rotates their position: item 3 now takes the topmost position, item 1 now takes the second position, and item 2 now takes the third position"}, 
                                            {":boolean_shove",
                                            "0", "1", "0", "1",
                                            "Shoves the nth item in the Boolean stack to the bottom, n being the value of the topmost item taken from the Integer stack" +
                                                "If n is larger than the stack size, then the last item will be shoved. If n is smaller than 0, then nothing will be shoved"}, 
                                            {":boolean_stack_depth",
                                            "0", "0", "0", "0+",
                                            "Returns the size of the Boolean stack as an integer"}, 
                                            {":boolean_swap",
                                            "0", "0", "0", "2",
                                            "Swaps the position of the top 2 items in the Boolean stack"}, 
                                            {":boolean_xor",
                                            "0", "0", "0", "2",
                                            "Takes the first 2 items in the Boolean stack, returns True if only one of them is True, returns False otherwise"}, 
                                            {":boolean_yank",
                                            "0", "1", "0", "1",
                                            "Brings the nth item in the Boolean stack to the top, n being the value of the topmost item taken from the Integer stack." +
                                                "If n is larger than the stack size, then the last item will be yanked. If n is smaller than 0, then nothing will be yanked"}, 
                                            {":boolean_yank_dup",
                                            "0", "1", "0", "1",
                                            "Returns a duplicate of the nth item in the Boolean stack, n being the value of the topmost item taken in the Integer stack" +
                                                "If n is larger than the stack size, then the last item will be duplicated. If n is smaller than 0, then nothing will be duplicated"},
                                            {":exec_deep_dup",
                                            "1", "1", "0", "0",
                                            "Duplicates the nth item in the Exec stack counting from the bottom of the stack, n being the value of the topmost item taken from the Integer stack" +
                                                "If n is larger than the stack size, then the first item will be duplicated. If n is smaller than 0, then nothing will be duplicated"}, 
                                            {":exec_do_count",
                                            "1", "1", "0", "0",
                                            "Takes the topmost item in the Exec stack and executes it n times, n being the value of the topmost item taken in the Integer stack. The number of times the instruction has been executed is returned to the Integer stack per execution"}, 
                                            {":exec_do_range",
                                            "1", "2", "0", "0",
                                            "Takes the first 2 items from the Integer stack, the first item being the 'start index' the second being the 'destination index'. The first item in the Exec stack will be continuously executed while the start index is returned to the Integer stack and incremented by 1 per execution. The instruciton will end when the start index reaches the destination index "}, 
                                            {":exec_do_times",
                                            "1", "1", "0", "0",
                                            "Takes the topmost item in the Exec stack and executs it n times, n being the value of the topmost item taken in the Integer stack"}, 
                                            {":exec_do_while",
                                            "1", "0", "0", "1",
                                            "Returns a duplicate of the topmost item in the Exec stack. Take the topmost item in the Boolean stack, if it is True, this instruction does not go away"}, 
                                            {":exec_dup",
                                            "1", "0", "0", "0",
                                            "Returns a duplicate of the topmost item in the Exec stack to the stack"},
                                            {":exec_dup_items",
                                            "0", "1", "0", "0",
                                            "Duplicate the first n items from the Exec stack, n being the value of the topmost item taken from the Integer stack" +
                                                "If n is smaller than 1, then nothing will be duplicated"}, 
                                            {":exec_dup_times",
                                            "1", "1", "0", "0",
                                            "Takes the value of the topmost item in the Integer stack as n, if n is bigger than 0, takes the next topmost item in the Exec stack and duplicates it n times. " +
                                                "If n is smaller than 0, the next topmost item is taken without duplication"}, 
                                            {":exec_empty",
                                            "0", "0", "0", "0",
                                            "Returns True if the Exec stack is empty, False if otherwise"}, 
                                            {":exec_eq",
                                            "2", "0", "0", "0",
                                            "Takes 2 items from the Exec stack, returns True if they are equal, False if otherwise"}, 
                                            {":exec_flush",
                                            "0", "0", "0", "0",
                                            "Empties the Exec stack"},
                                            {":exec_if",
                                            "2", "0", "0", "1",
                                            "Take the first 2 items from the Exec stack, and the topmost item in the Boolean stack. Returns the first Exec item if the Boolean item is True, returns the second Exec item if False"}, 
                                            {":exec_k",
                                            "2", "0", "0", "0",
                                            "Removes the second item from the Exec stack"}, 
                                            {":exec_pop",
                                            "1", "0", "0", "0",
                                            "Removes the first item in the Exec stack"}, 
                                            {":exec_rot",
                                            "3", "0", "0", "0",
                                            "Takes the first 3 items from the Exec stack and rotates their position: item 3 now takes the topmost position, item 1 now takes the second position, and item 2 now takes the third position"}, 
                                            {":exec_s",
                                            "3", "0", "0", "0",
                                            "Takes the top 3 items from the Exec stack (Now referred to as A, B, and C). Return them to the stack in the form of A C (B C)"}, 
                                            {":exec_shove",
                                            "1", "1", "0", "0",
                                            "Sends the nth item in the Exec stack to the bottom, n being the value of the topmost item taken from the Integer stack" +
                                                "If n is larger than the stack size, then the last item will be shoved. If n is smaller than 0, then nothing will be shoved"}, 
                                            {":exec_stack_depth",
                                            "0", "0", "0", "0",
                                            "Returns the size of the Exec stack to the Integer stack"}, 
                                            {":exec_swap",
                                            "2", "0", "0", "0",
                                            "Takes the first 2 items from the Exec stack, swaps their positions, then returns them"}, 
                                            {":exec_when",
                                            "1", "0", "0", "1",
                                            "Takes the topmost item in the Boolean stack, if False, removes the topmost item in the Exec stack, do nothing otherwise"}, 
                                            {":exec_while",
                                            "1", "0", "0", "1",
                                            "Takes the topmost item in the Boolean stack, if False, removes the topmost item in the Exec stack, if True, return a duplicate of the topmost item in the Exec stack along with this instruction"}, 
                                            {":exec_y",
                                            "1", "0", "0", "0",
                                            "Returns a copy of the topmost item in the Exec stack, this instruction does not go away"}, 
                                            {":exec_yank",
                                            "1", "1", "0", "0",
                                            "Brings the nth item in the Exec stack to the top, n being the value of the topmost item taken in the Integer stack" +
                                                "If n is larger than the stack size, then the last item will be yanked. If n is smaller than 0, then nothing will be yanked"}, 
                                            {":exec_yank_dup",
                                            "1", "1", "0", "0",
                                            "Duplicates the nth item in the Exec stack, n being the value of the topmost item taken in the Integer stack" + 
                                                "If n is larger than the stack size, then the last item will be duplicated. If n is smaller than 0, then nothing will be duplicated"}, 
                                            {":integer_add",
                                            "0", "2", "0", "0",
                                            "Takes the first 2 items in the Integer stack, adds them, then returns the result"}, 
                                            {":integer_dec",
                                            "0", "1", "0", "0",
                                            "Decreases the topmost item in the Integer stack by 1"}, 
                                            {":integer_deep_dup",
                                            "0", "2", "0", "0",
                                            "Takes the value of the topmost item in the Integer stack as n, then duplicates the nth item in the Integer stack counting from the bottom. " +
                                                "If n is larger than the stack size, then the first item will be duplicated. If n is smaller than 0, then nothing will be duplicated"}, 
                                            {":integer_dup",
                                            "0", "1", "0", "0",
                                            "Duplicates the topmost item in the Integer stack"}, 
                                            {":integer_dup_items",
                                            "0", "1", "0", "0",
                                            "Takes the value of the topmost item in the Integer stack as n, then duplicates the first n items in th Integer stack. " +
                                                "If n is smaller than 1, then nothing will be duplicated"}, 
                                            {":integer_dup_times",
                                            "0", "2", "0", "0",
                                            "Takes the value of the topmost item in the Integer stack as n, if n is bigger than 0, takes the next topmost item in the Integer stack and duplicates it n times. " +
                                                "If n is smaller than 0, the next topmost item is taken without duplication"}, 
                                            {":integer_empty",
                                            "0", "0", "0", "0",
                                            "Returns True if the Integer stack is empty, False if otherwise"}, 
                                            {":integer_eq",
                                            "0", "2", "0", "0",
                                            "Takes the first 2 items in the Integer stack, returns True if they are equal in value, False if otherwise"}, 
                                            {":integer_flush",
                                            "0", "0", "0", "0",
                                            "Empties the Integer stack"}, 
                                            {":integer_from_boolean",
                                            "0", "0", "0", "1",
                                            "Takes the topmost item in the Boolean stack, returns 1 if it is True, 0 if False"}, 
                                            {":integer_from_string",
                                            "0", "0", "1", "0",
                                            "Takes the topmost item in the String stack, if its value can be turned into an integer, return the value to the Integer stack, otherwise return 0"}, 
                                            {":integer_gt",
                                            "0", "2", "0", "0",
                                            "Takes the first 2 items from the Integer stack, returns True if the value of the second item taken is greater than the first, return False if otherwise"}, 
                                            {":integer_gte",
                                            "0", "2", "0", "0",
                                            "Takes the first 2 items from the Integer stack, returns True if the value of the second item taken is greater or equal than the first, return False if otherwise"}, 
                                            {":integer_inc",
                                            "0", "1", "0", "0",
                                            "Increases the topmost item in the Integer stack by 1"}, 
                                            {":integer_lt",
                                            "0", "2", "0", "0",
                                            "Takes the first 2 items from the Integer stack, returns True if the value of the second item is less than the first, return false if otherwise"}, 
                                            {":integer_lte",
                                            "0", "2", "0", "0",
                                            "Takes the first 2 items from the Integer stack, returns True if the value of the second item is less than or equal than the first, return False if otherwise"}, 
                                            {":integer_max",
                                            "0", "2", "0", "0",
                                            "Takes the first 2 items from the Integer stack, returns the one with the larger value"}, 
                                            {":integer_min",
                                            "0", "2", "0", "0",
                                            "Takes the first 2 items from the Integer stack, returns the one with the smaller value"}, 
                                            {":integer_mod",
                                            "0", "2", "0", "0",
                                            "Takes the first 2 items from the Integer stack, divides the value of the second item with the value of the first, then returns the remainder"}, 
                                            {":integer_mult",
                                            "0", "2", "0", "0",
                                            "Takes the first 2 items from the Integer stack, multiplies them, then returns the result"}, 
                                            {":integer_pop",
                                            "0", "1", "0", "0",
                                            "Removes the first item from the Integer stack"}, 
                                            {":integer_quot",
                                            "0", "2", "0", "0",
                                            "Takes the first 2 items from the Integer stack, divides the value of the second item with the value of the first, then returns the quotient"}, 
                                            {":integer_rot",
                                            "0", "3", "0", "0",
                                            "Takes the first 3 items from the Integer stack and rotates their position: item 3 now takes the topmost position, item 1 now takes the second position, and item 2 now takes the third position"}, 
                                            {":integer_shove",
                                            "0", "2", "0", "0",
                                            "Takes the value of the topmost item in the stack as n, then sends the nth item in the stack to the bottom" + 
                                                "If n is larger than the stack size, then the last item will be shoved. If n is smaller than 0, then nothing will be shoved"}, 
                                            {":integer_stack_depth",
                                            "0", "0", "0", "0",
                                            "Returns the size of the integer stack"}, 
                                            {":integer_subtract",
                                            "0", "2", "0", "0",
                                            "Takes the first 2 items from the Integer stack, substracts the second item by the first item, then returns the result"},
                                            {":integer_swap",
                                            "0", "2", "0", "0",
                                            "Swaps the position of the first and second item in the Integer stack"}, 
                                            {":integer_yank",
                                            "0", "2", "0", "0",
                                            "Takes the value of the topmost item in the Integer stack as n, then brings the nth item in the stack to the top. " +
                                                "If n is larger than the stack size, then the last item will be yanked. If n is smaller than 0, then nothing will be yanked"}, 
                                            {":integer_yank_dup",
                                            "0", "2", "0", "0",
                                            "Takes the value of the topmost item in the Integer stack as n, then returns a duplicate of the nth item in the stack. " +
                                                "If n is larger than the stack size, then the last item will be duplicated. If n is smaller than 0, then nothing will be duplicated"}, 
                                            {":string_butlast",
                                            "0", "0", "1", "0",
                                            "Removes the last character in the topmost item of the String stack"}, 
                                            {":string_concat",
                                            "0", "0", "2", "0",
                                            "Takes the first 2 items in the String stack, combines them, then returns the result"}, 
                                            {":string_contains",
                                            "0", "0", "2", "0",
                                            "Takes the first 2 items in the String stack, returns True if the second item is contained within the first, False if otherwise"}, 
                                            {":string_deep_dup",
                                            "0", "1", "1", "0",
                                            "Takes the value of the topmost item in the Integer stack as n, then duplicates the nth item in the Integer stack countring from the bottom." + 
                                                "If n is larger than the stack size, then the first item will be duplicated. If n is smaller than 0, then nothing will be duplicated"}, 
                                            {":string_drop",
                                            "0", "1", "1", "0",
                                            "Takes the topmost item of the string, remove the first n characters in it, then returns the result (the result can be an empty string). n being the value of the topmost item taken in the Integer stack"}, 
                                            {":string_dup",
                                            "0", "0", "1", "0",
                                            "Duplicates the topmost item in the String stack"}, 
                                            {":string_dup_items",
                                            "0", "1", "0", "0",
                                            "Takes the value of the topmost item in the Integer stack as n, then duplicates the first n items in th String stack" +
                                                "If n is smaller than 1, then nothing will be duplicated"}, 
                                            {":string_dup_times",
                                            "0", "1", "1", "0",
                                            "Takes the value of the topmost item in the Integer stack as n, if n is bigger than 0, takes the next topmost item in the String stack and duplicates it n times. " +
                                                "If n is smaller than 0, the next topmost item is taken without duplication"}, 
                                            {":string_empty",
                                            "0", "0", "0", "0",
                                            "Returns True if the String stack is empty, False if otherwise"}, 
                                            {":string_empty_string",
                                            "0", "0", "1", "0",
                                            "Takes the topmost item in the String stack, returns True if it is an empty string, False if otherwise"}, 
                                            {":string_eq",
                                            "0", "0", "2", "0",
                                            "Takes the first 2 items in the String stack, returns True if they are equal, False if otherwise"}, 
                                            {":string_flush",
                                            "0", "0", "0", "0",
                                            "Resets the String stack"}, 
                                            {":string_from_boolean",
                                            "0", "0", "0", "1",
                                            "Takes the topmost item in the Boolean stack and converts it into a string, returns the result to the top of the String stack"}, 
                                            {":string_from_integer",
                                            "0", "0", "1", "0",
                                            "Takes the topmost item in the Integer stack and converts it into a string, returns the result to the String stack"}, 
                                            {":string_length",
                                            "0", "0", "1", "0",
                                            "Takes the topmost item in the String stack, then returns the length of the string as an integer to the Integer stack"}, 
                                            {":string_parse_to_chars",
                                            "0", "0", "1", "0",
                                            "Takes the topmost item in the String stack, then returns each character in the string as individual items"}, 
                                            {":string_pop",
                                            "0", "0", "1", "0",
                                            "Removes the topmost item in the String stack"}, 
                                            //":string_replace", 
                                            //":string_replace_first", 
                                            {":string_rest",
                                            "0", "0", "1", "0",
                                            "Takes the topmost item in the String stack, removes the first character of the string, then returns the result"}, 
                                            {":string_reverse",
                                            "0", "0", "1", "0",
                                            "Takes the topmost item in the String stack, reverses the string, then returns the result"}, 
                                            {":string_rot",
                                            "0", "0", "3", "0",
                                            "Takes the first 3 items from the String stack and rotates their position: item 3 now takes the topmost position, item 1 now takes the second position, and item 2 now takes the third position"}, 
                                            {":string_shove",
                                            "0", "1", "1", "0",
                                            "Sends the nth item in the String stack to the bottom, n being the value of the topmost item taken in the Intger stack" +
                                                "If n is larger than the stack size, then the last item will be shoved. If n is smaller than 0, then nothing will be shoved"}, 
                                            //":string_split", 
                                            {":string_stack_depth",
                                            "0", "0", "0", "0",
                                            "Returns the size of the String stack to the Integer stack"},
                                            //":string_substr", 
                                            {":string_swap",
                                            "0", "0", "2", "0",
                                            "Swaps the positions of the first 2 items in the String stack"}, 
                                            {":string_take",
                                            "0", "1", "1", "0",
                                            "Takes the topmost item in the String stack, removes the last n characters, then returns the result (result can be an empty string). n being the value of the topmost item taken in the Integer stack"}, 
                                            {":string_yank",
                                            "0", "1", "1", "0",
                                            "Sends the nth item in the String stack to the top, n being the value of the topmost item taken in the Intger stack" +
                                                "If n is larger than the stack size, then the last item will be yanked. If n is smaller than 0, then nothing will be yanked"}, 
                                            {":string_yank_dup",
                                            "0", "1", "1", "0",
                                            "Returns a duplicate of the nth item in the String stack, n being the value of the topmost item taken in the Integer stack" + 
                                                "If n is larger than the stack size, then the last item will be duplicated. If n is smaller than 0, then nothing will be duplicated"},
    };
}
