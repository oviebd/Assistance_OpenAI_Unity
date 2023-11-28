using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[Serializable]
public class CreateThreadResponseModel
{
    public string id;
    public int created_at;
}

#region Message

[Serializable]
public class SendMessageRequestModel
{
    public string role;
    public string content;

    public SendMessageRequestModel(string _role, string _content)
    {
        role = _role;
        content = _content;
    }

    public string ToBody()
    {
        var jsonString = JsonUtility.ToJson(this);
        Debug.Log("STR >> " + jsonString);
        return jsonString;
    }
}

[Serializable]
public class SendMessageResponseModel
{
    public string id;
    public int created_at;
    public string thread_id;
    public string role;
    public List<MessageContent> content;
    public object run_id;
}
[Serializable]
public class MessageContent
{
    public string type;
    public TextMessage text;
}

[Serializable]
public class TextMessage
{
    public string value;
}

#endregion Message


#region Run

[Serializable]
public class CreateRunRequestModel
{
    public string assistant_id;
    public string instructions;
    public CreateRunRequestModel(string _assistant_id, string _instructions)
    {
        assistant_id = _assistant_id;
        instructions = _instructions;
    }

    public string ToBody()
    {
        var jsonString = JsonUtility.ToJson(this);
        Debug.Log("STR >> " + jsonString);
        return jsonString;
    }
    
}

[Serializable]
// Create and get status
public class CreateRunResponseModel
{
    public string id;
    public int created_at;
    public string assistant_id;
    public string thread_id;
    public string status;
    public object started_at;
  //  public int expires_at;
    public string model;
    public string instructions;
}

#endregion Run


