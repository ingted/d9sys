// Decompiled with JetBrains decompiler
// Type: AL2018.LineBot.Utility
// Assembly: LineBot, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2BBFEF47-CFFC-47E0-9A9F-E00E117F93C2
// Assembly location: C:\inetpub\linebottest\packages\LineBotSDK.0.5.1\lib\net45\LineBot.dll

using Newtonsoft.Json;
using System;
using System.Drawing;
using System.IO;
using System.Net;
using System.Text;

namespace AL2018.LineBot
{
  public class Utility
  {
    public static byte[] GetUserUploadedContent(string ContentID, string ChannelAccessToken)
    {
      try
      {
        ContentID = ContentID.Trim();
        WebClient webClient = new WebClient();
        webClient.Headers.Clear();
        webClient.Headers.Add("Authorization", "Bearer " + ChannelAccessToken);
        return webClient.DownloadData("https://api.line.me/v2/bot/message/" + ContentID + "/content");
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    public static ReceievedMessage Parsing(string RawData)
    {
      return (ReceievedMessage) JsonConvert.DeserializeObject<ReceievedMessage>(RawData);
    }

    public static LineUserInfo GetUserInfo(string uid, string ChannelAccessToken)
    {
      try
      {
        WebClient webClient = new WebClient();
        webClient.Headers.Clear();
        webClient.Headers.Add("Content-Type", "application/json");
        webClient.Headers.Add("Authorization", "Bearer " + ChannelAccessToken);
        return (LineUserInfo) JsonConvert.DeserializeObject<LineUserInfo>(Encoding.UTF8.GetString(webClient.DownloadData(string.Format("https://api.line.me/v2/bot/profile/{0}", (object) uid))));
      }
      catch (WebException ex)
      {
        using (StreamReader streamReader = new StreamReader(ex.Response.GetResponseStream()))
          throw new Exception("GetUserInfo: " + streamReader.ReadToEnd(), (Exception) ex);
      }
    }

    [Obsolete("舊版API，已過時，請改用LeaveGroup或LeaveRoom")]
    public static string Leave(string groupId, string ChannelAccessToken)
    {
      return Utility.LeaveGroup(groupId, ChannelAccessToken);
    }

    public static string LeaveGroup(string groupId, string ChannelAccessToken)
    {
      try
      {
        WebClient webClient = new WebClient();
        webClient.Headers.Clear();
        webClient.Headers.Add("Content-Type", "application/json");
        webClient.Headers.Add("Authorization", "Bearer " + ChannelAccessToken);
        byte[] bytes = Encoding.UTF8.GetBytes("");
        return Encoding.UTF8.GetString(webClient.UploadData("https://api.line.me/v2/bot/group/" + groupId + "/leave", bytes));
      }
      catch (WebException ex)
      {
        using (StreamReader streamReader = new StreamReader(ex.Response.GetResponseStream()))
          throw new Exception("Leave API ERROR: " + streamReader.ReadToEnd(), (Exception) ex);
      }
    }

    public static string LeaveRoom(string roomId, string ChannelAccessToken)
    {
      try
      {
        WebClient webClient = new WebClient();
        webClient.Headers.Clear();
        webClient.Headers.Add("Content-Type", "application/json");
        webClient.Headers.Add("Authorization", "Bearer " + ChannelAccessToken);
        byte[] bytes = Encoding.UTF8.GetBytes("");
        return Encoding.UTF8.GetString(webClient.UploadData("https://api.line.me/v2/bot/room/" + roomId + "/leave", bytes));
      }
      catch (WebException ex)
      {
        using (StreamReader streamReader = new StreamReader(ex.Response.GetResponseStream()))
          throw new Exception("Leave API ERROR: " + streamReader.ReadToEnd(), (Exception) ex);
      }
    }

    public static string PushMessage(string ToUserID, string Message, string ChannelAccessToken)
    {
      string str = "\r\n{{\r\n    'to': '{0}',\r\n    'messages':[\r\n        {{\r\n            'type':'text',\r\n            'text':'{1}'\r\n        }}\r\n    ]\r\n}}\r\n";
      try
      {
        Message = Message.Replace("\n", "\\n");
        Message = Message.Replace("\r", "\\r");
        Message = Message.Replace("\"", "'");
        string s = string.Format(str.Replace("'", "\""), (object) ToUserID, (object) Message);
        WebClient webClient = new WebClient();
        webClient.Headers.Clear();
        webClient.Headers.Add("Content-Type", "application/json");
        webClient.Headers.Add("Authorization", "Bearer " + ChannelAccessToken);
        byte[] bytes = Encoding.UTF8.GetBytes(s);
        return Encoding.UTF8.GetString(webClient.UploadData("https://api.line.me/v2/bot/message/push", bytes));
      }
      catch (WebException ex)
      {
        using (StreamReader streamReader = new StreamReader(ex.Response.GetResponseStream()))
          throw new Exception("PushMessage API ERROR: " + streamReader.ReadToEnd(), (Exception) ex);
      }
    }

    public static string PushImageMessage(string ToUserID, string originalContentUrl, string previewImageUrl, string ChannelAccessToken)
    {
      string str = "\r\n{{\r\n    'to': '{0}',\r\n    'messages':[\r\n        {{\r\n            'type':'image',\r\n            'originalContentUrl':'{1}',\r\n            'previewImageUrl':'{2}'\r\n        }}\r\n    ]\r\n}}\r\n";
      try
      {
        if (!originalContentUrl.ToLower().StartsWith("https://") || !previewImageUrl.ToLower().StartsWith("https://"))
          throw new Exception("圖片網址必須是 https:// ");
        string s = string.Format(str.Replace("'", "\""), (object) ToUserID, (object) originalContentUrl, (object) previewImageUrl);
        WebClient webClient = new WebClient();
        webClient.Headers.Clear();
        webClient.Headers.Add("Content-Type", "application/json");
        webClient.Headers.Add("Authorization", "Bearer " + ChannelAccessToken);
        byte[] bytes = Encoding.UTF8.GetBytes(s);
        return Encoding.UTF8.GetString(webClient.UploadData("https://api.line.me/v2/bot/message/push", bytes));
      }
      catch (WebException ex)
      {
        using (StreamReader streamReader = new StreamReader(ex.Response.GetResponseStream()))
          throw new Exception("PushImageMessage API ERROR: " + streamReader.ReadToEnd(), (Exception) ex);
      }
    }

    public static string PushStickerMessage(string ToUserID, int packageId, int stickerId, string ChannelAccessToken)
    {
      string str = "\r\n{{\r\n    'to': '{0}',\r\n    'messages':[\r\n        {{\r\n            'type':'sticker',\r\n            'packageId':'{1}',\r\n            'stickerId':'{2}'\r\n        }}\r\n    ]\r\n}}\r\n";
      try
      {
        string s = string.Format(str.Replace("'", "\""), (object) ToUserID, (object) packageId, (object) stickerId);
        WebClient webClient = new WebClient();
        webClient.Headers.Clear();
        webClient.Headers.Add("Content-Type", "application/json");
        webClient.Headers.Add("Authorization", "Bearer " + ChannelAccessToken);
        byte[] bytes = Encoding.UTF8.GetBytes(s);
        return Encoding.UTF8.GetString(webClient.UploadData("https://api.line.me/v2/bot/message/push", bytes));
      }
      catch (WebException ex)
      {
        using (StreamReader streamReader = new StreamReader(ex.Response.GetResponseStream()))
          throw new Exception("PushStickerMessage API ERROR: " + streamReader.ReadToEnd(), (Exception) ex);
      }
    }

    public static string PushImageMapMessage(string ToUserID, ImagemapMessage Message, string ChannelAccessToken)
    {
      string str1 = "\r\n{{\r\n    'to': '{0}',\r\n    'messages':[\r\n       {1}\r\n    ]\r\n}}\r\n";
      try
      {
        string str2 = "\r\n{{\r\n 'type': 'imagemap',\r\n  'baseUrl': '{0}',\r\n  'altText': '{1}',\r\n 'baseSize': {{\r\n                         'height': {2},\r\n                         'width': {3}\r\n                    }},\r\n  'actions': {4}\r\n}}       \r\n                    ";
        if (Message == null)
          throw new Exception("Message.");
        if (Message.baseUrl == (Uri) null)
          throw new Exception("thumbnailImageUrl不得為null.");
        if (Message.actions == null || Message.actions.Count < 1)
          throw new Exception("actions數量必須>1");
        foreach (ImagemapActionBase action in Message.actions)
        {
          if (action.GetType().Equals(typeof (ImagemapUriAction)) && (action as ImagemapUriAction).linkUri == (Uri) null)
            throw new Exception("ImagemapUriAction 中的 linkUri 不得為null.");
        }
        string str3 = JsonConvert.SerializeObject((object) Message.actions);
        string format = str2.Replace("'", "\"");
        object[] objArray = new object[5]
        {
          (object) Message.baseUrl,
          (object) Message.altText,
          null,
          null,
          null
        };
        int index1 = 2;
        Size baseSize = Message.baseSize;
        int num = baseSize.Height;
        string str4 = num.ToString();
        objArray[index1] = (object) str4;
        int index2 = 3;
        baseSize = Message.baseSize;
        num = baseSize.Width;
        string str5 = num.ToString();
        objArray[index2] = (object) str5;
        int index3 = 4;
        string str6 = str3;
        objArray[index3] = (object) str6;
        string str7 = string.Format(format, objArray);
        string s = string.Format(str1.Replace("'", "\""), (object) ToUserID, (object) str7);
        WebClient webClient = new WebClient();
        webClient.Headers.Clear();
        webClient.Headers.Add("Content-Type", "application/json");
        webClient.Headers.Add("Authorization", "Bearer " + ChannelAccessToken);
        byte[] bytes = Encoding.UTF8.GetBytes(s);
        return Encoding.UTF8.GetString(webClient.UploadData("https://api.line.me/v2/bot/message/push", bytes));
      }
      catch (WebException ex)
      {
        using (StreamReader streamReader = new StreamReader(ex.Response.GetResponseStream()))
          throw new Exception("PushImageMapMessage API ERROR: " + streamReader.ReadToEnd(), (Exception) ex);
      }
    }

    public static string PushTemplateMessage(string ToUserID, ButtonsTemplate TemplateMessage, string ChannelAccessToken)
    {
      string str1 = "\r\n{{\r\n    'to': '{0}',\r\n    'messages':[\r\n       {1}\r\n    ]\r\n}}\r\n";
      try
      {
        string str2 = "\r\n{{\r\n 'type': 'template',\r\n  'altText': '{0}',\r\n  'template': {{\r\n      'type': 'buttons',\r\n      'thumbnailImageUrl': '{1}',\r\n      'title': '{2}',\r\n      'text': '{3}',\r\n      'actions': {4}\r\n  }}\r\n}}       \r\n                    ";
        if (TemplateMessage == null)
          throw new Exception("TemplateMessage不得為null.");
        if (TemplateMessage.thumbnailImageUrl == (Uri) null)
          throw new Exception("thumbnailImageUrl不得為null.");
        if (TemplateMessage.actions == null || TemplateMessage.actions.Count < 1 || TemplateMessage.actions.Count > 4)
          throw new Exception("actions數量必須是1-4之間");
        foreach (TemplateActionBase action in TemplateMessage.actions)
        {
          if (action.GetType().Equals(typeof (UriActon)) && (action as UriActon).uri == (Uri) null)
            throw new Exception("uriAction 中的 Url不得為null.");
        }
        string str3 = JsonConvert.SerializeObject((object) TemplateMessage.actions);
        string str4 = string.Format(str2.Replace("'", "\""), (object) TemplateMessage.altText, (object) TemplateMessage.thumbnailImageUrl.ToString(), (object) TemplateMessage.title, (object) TemplateMessage.text, (object) str3);
        string s = string.Format(str1.Replace("'", "\""), (object) ToUserID, (object) str4);
        WebClient webClient = new WebClient();
        webClient.Headers.Clear();
        webClient.Headers.Add("Content-Type", "application/json");
        webClient.Headers.Add("Authorization", "Bearer " + ChannelAccessToken);
        byte[] bytes = Encoding.UTF8.GetBytes(s);
        return Encoding.UTF8.GetString(webClient.UploadData("https://api.line.me/v2/bot/message/push", bytes));
      }
      catch (WebException ex)
      {
        using (StreamReader streamReader = new StreamReader(ex.Response.GetResponseStream()))
          throw new Exception("PushTemplateMessage(ButtonsTemplate) API ERROR: " + streamReader.ReadToEnd(), (Exception) ex);
      }
    }

    public static string PushTemplateMessage(string ToUserID, ConfirmTemplate TemplateMessage, string ChannelAccessToken)
    {
      string str1 = "\r\n{{\r\n    'to': '{0}',\r\n    'messages':[\r\n       {1}\r\n    ]\r\n}}\r\n";
      try
      {
        string str2 = "\r\n{{\r\n 'type': 'template',\r\n  'altText': '{0}',\r\n  'template': {{\r\n      'type': 'confirm',\r\n      'text': '{1}',\r\n      'actions': {2}\r\n  }}\r\n}}       \r\n                    ";
        if (TemplateMessage == null)
          throw new Exception("TemplateMessage不得為null.");
        if (TemplateMessage.actions == null || TemplateMessage.actions.Count < 1 || TemplateMessage.actions.Count > 2)
          throw new Exception("actions數量必須是1-2之間");
        foreach (TemplateActionBase action in TemplateMessage.actions)
        {
          if (action.GetType().Equals(typeof (UriActon)) && (action as UriActon).uri == (Uri) null)
            throw new Exception("uriAction 中的 Url不得為null.");
        }
        string str3 = JsonConvert.SerializeObject((object) TemplateMessage.actions);
        string str4 = string.Format(str2.Replace("'", "\""), (object) TemplateMessage.altText, (object) TemplateMessage.text, (object) str3);
        string s = string.Format(str1.Replace("'", "\""), (object) ToUserID, (object) str4);
        WebClient webClient = new WebClient();
        webClient.Headers.Clear();
        webClient.Headers.Add("Content-Type", "application/json");
        webClient.Headers.Add("Authorization", "Bearer " + ChannelAccessToken);
        byte[] bytes = Encoding.UTF8.GetBytes(s);
        return Encoding.UTF8.GetString(webClient.UploadData("https://api.line.me/v2/bot/message/push", bytes));
      }
      catch (WebException ex)
      {
        using (StreamReader streamReader = new StreamReader(ex.Response.GetResponseStream()))
          throw new Exception("PushTemplateMessage(ConfirmTemplate) API ERROR: " + streamReader.ReadToEnd(), (Exception) ex);
      }
    }

    public static string PushTemplateMessage(string ToUserID, CarouselTemplate TemplateMessage, string ChannelAccessToken)
    {
      string str1 = "\r\n{{\r\n    'to': '{0}',\r\n    'messages':[\r\n       {1}\r\n    ]\r\n}}\r\n";
      try
      {
        string str2 = "\r\n{{\r\n 'type': 'template',\r\n  'altText': '{0}',\r\n  'template': {{\r\n      'type': 'carousel',\r\n      'columns': {1}\r\n  }}\r\n}}       \r\n                    ";
        if (TemplateMessage == null)
          throw new Exception("TemplateMessage不得為null.");
        if (TemplateMessage.columns == null || TemplateMessage.columns.Count < 1 || TemplateMessage.columns.Count > 5)
          throw new Exception("columns數量必須是1-5之間");
        foreach (Column column in TemplateMessage.columns)
        {
          if (column.actions == null)
            throw new Exception("actions數量必須是1-3之間");
          if (column.actions.Count < 1 || column.actions.Count > 3)
            throw new Exception("actions數量必須是1-3之間");
          foreach (TemplateActionBase action in column.actions)
          {
            if (action.GetType().Equals(typeof (UriActon)) && (action as UriActon).uri == (Uri) null)
              throw new Exception("uriAction 中的 Url不得為null.");
          }
        }
        string str3 = JsonConvert.SerializeObject((object) TemplateMessage.columns);
        string str4 = string.Format(str2.Replace("'", "\""), (object) TemplateMessage.altText, (object) str3);
        string s = string.Format(str1.Replace("'", "\""), (object) ToUserID, (object) str4);
        WebClient webClient = new WebClient();
        webClient.Headers.Clear();
        webClient.Headers.Add("Content-Type", "application/json");
        webClient.Headers.Add("Authorization", "Bearer " + ChannelAccessToken);
        byte[] bytes = Encoding.UTF8.GetBytes(s);
        return Encoding.UTF8.GetString(webClient.UploadData("https://api.line.me/v2/bot/message/push", bytes));
      }
      catch (WebException ex)
      {
        using (StreamReader streamReader = new StreamReader(ex.Response.GetResponseStream()))
          throw new Exception("PushTemplateMessage API(CarouselTemplate) ERROR: " + streamReader.ReadToEnd(), (Exception) ex);
      }
    }

    public static string ReplyMessage(string ReplyToken, string Message, string ChannelAccessToken)
    {
      string str = "\r\n{{\r\n    'replyToken':'{0}',\r\n    'messages':[\r\n        {{\r\n            'type':'text',\r\n            'text':'{1}'\r\n        }}\r\n    ]\r\n}}";
      try
      {
        Message = Message.Replace("\n", "\\n");
        Message = Message.Replace("\r", "\\r");
        Message = Message.Replace("\"", "'");
        string s = string.Format(str.Replace("'", "\""), (object) ReplyToken, (object) Message);
        WebClient webClient = new WebClient();
        webClient.Headers.Clear();
        webClient.Headers.Add("Content-Type", "application/json");
        webClient.Headers.Add("Authorization", "Bearer " + ChannelAccessToken);
        byte[] bytes = Encoding.UTF8.GetBytes(s);
        return Encoding.UTF8.GetString(webClient.UploadData("https://api.line.me/v2/bot/message/reply", bytes));
      }
      catch (WebException ex)
      {
        using (StreamReader streamReader = new StreamReader(ex.Response.GetResponseStream()))
          throw new Exception("ReplyMessage API ERROR: " + streamReader.ReadToEnd(), (Exception) ex);
      }
    }

    public static string ReplyStickerMessage(string ReplyToken, int packageId, int stickerId, string ChannelAccessToken)
    {
      string str = "\r\n{{\r\n    'replyToken': '{0}',\r\n    'messages':[\r\n        {{\r\n            'type':'sticker',\r\n            'packageId':'{1}',\r\n            'stickerId':'{2}'\r\n        }}\r\n    ]\r\n}}\r\n";
      try
      {
        string s = string.Format(str.Replace("'", "\""), (object) ReplyToken, (object) packageId, (object) stickerId);
        WebClient webClient = new WebClient();
        webClient.Headers.Clear();
        webClient.Headers.Add("Content-Type", "application/json");
        webClient.Headers.Add("Authorization", "Bearer " + ChannelAccessToken);
        byte[] bytes = Encoding.UTF8.GetBytes(s);
        return Encoding.UTF8.GetString(webClient.UploadData("https://api.line.me/v2/bot/message/reply", bytes));
      }
      catch (WebException ex)
      {
        using (StreamReader streamReader = new StreamReader(ex.Response.GetResponseStream()))
          throw new Exception("ReplyStickerMessage API ERROR: " + streamReader.ReadToEnd(), (Exception) ex);
      }
    }

    public static string ReplyImageMessage(string ReplyToken, string originalContentUrl, string previewImageUrl, string ChannelAccessToken)
    {
      string str = "\r\n{{\r\n    'replyToken': '{0}',\r\n    'messages':[\r\n        {{\r\n            'type':'image',\r\n            'originalContentUrl':'{1}',\r\n            'previewImageUrl':'{2}'\r\n        }}\r\n    ]\r\n}}\r\n";
      try
      { 
        if (!originalContentUrl.ToLower().StartsWith("https://") || !previewImageUrl.ToLower().StartsWith("https://"))
          throw new Exception("圖片網址必須是 https:// ");
        string s = string.Format(str.Replace("'", "\""), (object) ReplyToken, (object) originalContentUrl, (object) previewImageUrl);
        WebClient webClient = new WebClient();
        webClient.Headers.Clear();
        webClient.Headers.Add("Content-Type", "application/json");
        webClient.Headers.Add("Authorization", "Bearer " + ChannelAccessToken);
        byte[] bytes = Encoding.UTF8.GetBytes(s);
        return Encoding.UTF8.GetString(webClient.UploadData("https://api.line.me/v2/bot/message/reply", bytes));
      }
      catch (WebException ex)
      {
        using (StreamReader streamReader = new StreamReader(ex.Response.GetResponseStream()))
          throw new Exception("PushImageMessage API ERROR: " + streamReader.ReadToEnd(), (Exception) ex);
      }
    }
  }
}
