using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using REST_API_HANDLER;
using System;
using EventBus;
using UnityEngine.SceneManagement;

public class ChatPanel : MonoBehaviour
{
    public string assistantId;
    public LoadingPanel loadingPanel;
    private ChatApiManager apiManager;
    private ShowChatPanel chatLoaderPanel;

    private void Awake()
    {
        assistantId = AppData.instance.GetAssistantData().assistantId;
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

    public void GoBack()
    {
        AppData.instance.SetAssistantData(null);
        SceneManager.LoadScene("SelectAssistantScece", LoadSceneMode.Single);
    }


}


