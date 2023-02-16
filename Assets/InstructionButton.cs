using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InstructionButton : MonoBehaviour
{
    public void buttonClick() {
        //print(this.GetComponent<TextMeshProUGUI>().text);
        GameObject.Find("DocArea").GetComponent<DocDisplay>().setDoc(this.GetComponent<TextMeshProUGUI>().text);
    }
}
