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
// Create and get status &tool
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

#region Tool

[Serializable]
public class SubmitToolRequestModel
{
    public List<ToolOutputModel> tool_outputs;

    public string ToBody()
    {
        var jsonString = JsonUtility.ToJson(this);
        Debug.Log("STR >> " + jsonString);
        return jsonString;
    }

    public SubmitToolRequestModel(List<ToolOutputModel> _outputs)
    {
        tool_outputs = _outputs;
    }
}

[Serializable]
public class ToolOutputModel
{
    public string tool_call_id;
    public string output;

    public ToolOutputModel(string callId, string _output)
    {
        tool_call_id = callId;
        output = _output;
    }
}

#endregion Tool


#region Status

[Serializable]
public class GetStatusResponseModel
{
    public string id;
    public int created_at;
    public string assistant_id;
    public string thread_id;
    public string status;
    public int started_at;
    public int expires_at;
 
    public RequiredAction required_action;
   
    public string model;
    public string instructions;
    public List<Tool> tools;
 
    
}
[Serializable]
public class RequiredAction
{
    public string type;
    public SubmitToolOutputs submit_tool_outputs;
}
[Serializable]
public class SubmitToolOutputs
{
    public List<ToolCall> tool_calls;
}
[Serializable]
public class Tool
{
    public string type;
    public Function function;
}
[Serializable]
public class ToolCall
{
    public string id;
    public string type;
    public Function function;
}
[Serializable]
public class Function
{
    public string name;
    public string arguments;
    public string description;
   // public Parameters parameters;
}

#endregion Status



#region Message
[Serializable]

public class ThreadMessagesResponseModel
{
    public List<ThreadMessagesModel> data;
    public string first_id;
    public string last_id;
    public bool has_more;
}


[Serializable]
public class ThreadMessagesModel
{
    public string id;
    public int created_at;
    public string thread_id;
    public string role;
    public List<MessageContent> content;
    public string assistant_id;
    public string run_id;
  
}


public class Message
{
    public int created_at;
    public string role;
    public string message;

    public Message(int _createdAt, string _role, string _message)
    {
        created_at = _createdAt;
        role = _role;
        message = _message;
    }
}

#endregion Message