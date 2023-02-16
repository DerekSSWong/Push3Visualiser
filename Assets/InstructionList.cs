using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InstructionList : MonoBehaviour
{   
    [SerializeField] GameObject prefab;
    private List<GameObject> IList;
    // Start is called before the first frame update
    void Start()
    {
        GameObject content = GameObject.Find("InstructionContent");
        string[] InsList = FunctionChecker.getList();
        IList = new List<GameObject>();
        foreach (string ins in InsList) {
            var insVis = Instantiate(prefab, content.transform);
            insVis.name = ins;
            insVis.GetComponent<TextMeshProUGUI>().text = ins;
            IList.Add(insVis);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        string searchText = GameObject.Find("SearchBar").GetComponentInChildren<TMP_InputField>().text;
        filter(searchText);
    }

    public void filter(string key) {
        foreach (GameObject ins in IList) {
            if (!ins.GetComponent<TextMeshProUGUI>().text.Contains(key)) {
                ins.SetActive(false);
            } 
            else {
                ins.SetActive(true);
            }
        }
    }

}
