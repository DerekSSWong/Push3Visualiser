using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FFWController : MonoBehaviour
{   
    [SerializeField] Sprite stop;
    [SerializeField] Sprite ff;

    bool enabled;
    // Start is called before the first frame update
    void Start()
    {
        enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (enabled) {
            this.GetComponent<Image>().sprite = stop;
            if (GameObject.Find("BlockDisplayController").GetComponent<DisplayText>().buttonEnabled) {
                GameObject.Find("FWStepButton").GetComponent<Button>().interactable = false;
                GameObject.Find("Exec").GetComponent<ExecStackController>().forward();
            }
        }
        else {
            this.GetComponent<Image>().sprite = ff;
        }
    }

    public void turnOn() {
        enabled = true;
    }

    public void turnOff() {
        enabled = false;
    }

    public void toggle() {
        enabled = !enabled;
    }
}
