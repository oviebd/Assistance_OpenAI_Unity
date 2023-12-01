using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssistantPanel : MonoBehaviour
{

    public List<AssistantData> assistantDataList;


    public GameObject parentObj;
    public AssistantItem assistantItemPrefab;

    private List<AssistantItem> items = new List<AssistantItem>();


    private void Start()
    {
        AppData.instance.SetAssistantData(null);
        LoadAssistants();
    }

    public void LoadAssistants()
    {
        //DeleteAll();
       

        for (int i = 0; i < assistantDataList.Count; i++)
        {
            InstantiateItem(assistantDataList[i]);
        }
    }


    private void InstantiateItem(AssistantData assistantData)
    {
        GameObject obj = Instantiate(assistantItemPrefab.gameObject);
        obj.transform.parent = parentObj.transform;
        AssistantItem script = obj.GetComponent<AssistantItem>();
        script.Setup(assistantData);
        items.Add(script);
    }

    //private void DeleteAll()
    //{

    //    for (int i = 0; i < items.Count; i++)
    //    {
    //        Destroy(items[i].gameObject);
    //    }

    //    items.Clear();
    //}
}
