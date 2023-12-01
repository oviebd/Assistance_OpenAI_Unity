using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataPassingModel 
{
    
}

public enum API_TYPE
{

    CREATE_THREAD_API,
    SEND_MESSAGE_API,
    RUN_THREAD_API,
    GET_STATUS_API,
    SUBMIT_TOOL_API,
    GET_MESSAGE_API
}

public class CommonApiCompletationDataModel
{
    public bool isSuccess;
    public API_TYPE apiType;
    public List<Message> messageList;

    public CommonApiCompletationDataModel (bool isSuccess, API_TYPE apiType, List<Message> messageList = null)
    {
        this.isSuccess = isSuccess;
        this.apiType = apiType;
        this.messageList = messageList;
    }
}
