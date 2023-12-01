using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using REST_API_HANDLER;
using System;
using EventBus;

public class ChatPanel : MonoBehaviour
{
    public string assistantId = "asst_sgjlg3pltKFAzIu5t98oQMF6";
    public LoadingPanel loadingPanel;
    private ChatApiManager apiManager;
    private ShowChatPanel chatLoaderPanel;

    private void Awake()
    {
        apiManager = new ChatApiManager(assistantId);
        chatLoaderPanel = GetComponent<ShowChatPanel>();
    }


    void OnEnable()
    {
        EBM.StartListening<CommonApiCompletationDataModel>(EventBusEnum.EventName.OnApiCallCompleted, OnApiCallCompleted);
    }

    void OnDisable()
    {
        EBM.StopListening<CommonApiCompletationDataModel>(EventBusEnum.EventName.OnApiCallCompleted, OnApiCallCompleted);
    }

    public void OnChatMessageOptionButtonPressed(int option)
    {
        loadingPanel.Show();
        apiManager.InitiateAssistantRequest(option.ToString());
    }

    private void OnApiCallCompleted(CommonApiCompletationDataModel apiData)
    {
        Debug.Log("E>> API DATA COMPLETED ----- " + apiData.apiType);
        if (apiData.isSuccess == false)
        {
            // Api call failed. Show appropriate message
            loadingPanel.Hide();
            Debug.Log("U>> Api failed for " + apiData.apiType);
            return;
        }

        if (apiData.apiType == API_TYPE.GET_MESSAGE_API)
        {
            Debug.Log("U>> Show Messages");
            chatLoaderPanel.OnAllMessageUpdate(apiData.messageList);
            loadingPanel.Hide();
        }
    }

 
}


