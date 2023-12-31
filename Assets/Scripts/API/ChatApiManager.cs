using System.Collections;
using System.Collections.Generic;
using EventBus;
using UnityEngine;

public class ChatApiManager 
{
    private OpenAIApiService apiService = new OpenAIApiService();
    private string threadId = "";
    private string message = "";

    public string assistantId = "asst_sgjlg3pltKFAzIu5t98oQMF6";
    private string instructions;// = "When the user inputs \"1\", call function \"function1\". When the call was a success, answer with \"1 was received\". When the user inputs anything else, just answer with \"ok\"";

    private string runId = "";


    public ChatApiManager(string assistantId)
    {
        this.assistantId = assistantId;
        instructions = AppData.instance.GetAsiistantInstruction();
    }

    public void InitiateAssistantRequest(string assistantCommand)
    {
        message = assistantCommand;
        if (threadId != null && threadId != "")
        {
            SendMessage();
            return;
        }

        CreateThread();
    }

    private void CreateThread()
    {
        apiService.CreateThread((isSuccess, id) =>
        {
            threadId = id;
           // Debug.Log("U>> 01 IsSuccess " + isSuccess + "Id " + id);
            EBM.TriggerEvent<CommonApiCompletationDataModel>(EventBusEnum.EventName.OnApiCallCompleted, new CommonApiCompletationDataModel(isSuccess,API_TYPE.CREATE_THREAD_API));
            SendMessage();
        });
    }

    private void SendMessage()
    {
        //Debug.Log("Send Message");
        SendMessageRequestModel requestModel = new SendMessageRequestModel("user", message);
        apiService.SendMessageToTheThread(requestModel, threadId, (isSuccess, model) =>
        {
          //  Debug.Log("U>> 02  IsSuccess " + isSuccess + "Send message  id " + model.id);
            EBM.TriggerEvent<CommonApiCompletationDataModel>(EventBusEnum.EventName.OnApiCallCompleted, new CommonApiCompletationDataModel(isSuccess, API_TYPE.SEND_MESSAGE_API));
            if (isSuccess)
            {
                RunThread();
            }
        });
    }

    private void RunThread()
    {
        CreateRunRequestModel requestModel = new CreateRunRequestModel(assistantId, instructions);
        apiService.RunThread(requestModel, threadId, (isSuccess, model) =>
        {
            runId = model.id;
          //  Debug.Log("U>> 03 IsSuccess in run " + isSuccess + "Run Id " + runId);
            EBM.TriggerEvent<CommonApiCompletationDataModel>(EventBusEnum.EventName.OnApiCallCompleted, new CommonApiCompletationDataModel(isSuccess, API_TYPE.RUN_THREAD_API));
            GetStatus();
        });
    }

    private void SubmitToolOption(List<ToolCall> tools)
    {
        SubmitToolRequestModel requestModel = GetSendToolApiRquestmodel(tools);
      //  Debug.Log("U>> 6.0 try submit tool  total tool option  " + requestModel.tool_outputs.Count);
        apiService.RunSubmitTool(requestModel, threadId, runId, (isSuccess, model) =>
        {
           // Debug.Log("U>> 06 IsSuccess in run " + isSuccess + "Run Id " + runId);
            EBM.TriggerEvent<CommonApiCompletationDataModel>(EventBusEnum.EventName.OnApiCallCompleted, new CommonApiCompletationDataModel(isSuccess, API_TYPE.SUBMIT_TOOL_API));

            GetStatus();
        });
    }

    private void GetStatus()
    {
        apiService.GetRunStatus(threadId, runId, (isSuccess, model) =>
        {
           // Debug.Log("U>> 04 IsSuccess status get " + isSuccess + " status " + model.status);
            EBM.TriggerEvent<CommonApiCompletationDataModel>(EventBusEnum.EventName.OnApiCallCompleted, new CommonApiCompletationDataModel(isSuccess, API_TYPE.GET_STATUS_API));
            OnStatusAction(model.status, model);
        });
    }

    private void GetThreadMessages()
    {
        apiService.GetMessages(threadId, (isSuccess, messages) =>
        {
            //Debug.Log("U>> 07 IsSuccess Get Messages  " + isSuccess + " Messages Counr " + messages.Count);

            for (int i = 0; i < messages.Count; i++)
            {
                Debug.Log("M>> role  " + messages[i].role + " text - " + messages[i].message);
            }
            EBM.TriggerEvent<CommonApiCompletationDataModel>(EventBusEnum.EventName.OnApiCallCompleted, new CommonApiCompletationDataModel(isSuccess, API_TYPE.GET_MESSAGE_API, messages));
        });
    }




    private void OnStatusAction(string action, GetStatusResponseModel model)
    {
       // Debug.Log("U>> 05.1 Action " + action);
        if (action == "requires_action")
        {
          //  Debug.Log("U>> 05.2 status checked in Require action");
            // Send tool
            SubmitToolOption(model.required_action.submit_tool_outputs.tool_calls);

        }
        else if (action == "completed")
        {
            //Debug.Log("U>> 05.3 status checked in Completed");
            GetThreadMessages();
        }
        else
        {
           // Debug.Log("U>> 05.4 Check status again ");
            GetStatus();
        }
    }




    private SubmitToolRequestModel GetSendToolApiRquestmodel(List<ToolCall> tools)
    {
        List<ToolOutputModel> tool_outputs = new List<ToolOutputModel>();

        for (int i = 0; i < tools.Count; i++)
        {
            ToolCall tool = tools[i];
            ToolOutputModel outputModel = new ToolOutputModel(tool.id, "success");
            tool_outputs.Add(outputModel);
        }

        return new SubmitToolRequestModel(tool_outputs);
    }
}
