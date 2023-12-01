using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowChatPanel : MonoBehaviour
{
    public GameObject parentObj;
    public MessageItem messageItemPrefab;

 
    private List<Message> messages = new List<Message>();
    private List<MessageItem> items = new List<MessageItem>();


    public void OnAllMessageUpdate(List<Message> messageList)
    {
        DeleteAll();
        messages = messageList;

        for (int i = 0; i < messages.Count; i++)
        {
            InstantiateItem(messages[i]);
        }
    }

    public void OnSingleMessageUpdate(Message message)
    {
        messages.Add(message);
        InstantiateItem(message);
        //for (int i = 0; i < messages.Count; i++)
        //{
        //    InstantiateItem(messages[i]);
        //}
    }


    private void InstantiateItem(Message message)
    {
        GameObject obj = Instantiate(messageItemPrefab.gameObject);
        obj.transform.parent = parentObj.transform;
        MessageItem script = obj.GetComponent<MessageItem>();
        script.Setup(message);
        items.Add(script);
    }

    private void DeleteAll()
    {

        for (int i = 0; i < items.Count; i++)
        {
            Destroy(items[i].gameObject);
        }

        items.Clear();
    }

}
