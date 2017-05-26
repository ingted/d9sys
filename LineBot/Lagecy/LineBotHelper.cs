// Decompiled with JetBrains decompiler
// Type: LineBot.LineBotHelper
// Assembly: LineBot, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2BBFEF47-CFFC-47E0-9A9F-E00E117F93C2
// Assembly location: C:\inetpub\linebottest\packages\LineBotSDK.0.5.1\lib\net45\LineBot.dll

using LineBot.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace LineBot
{
  [Obsolete("Line舊版API，已過時")]
  public class LineBotHelper
  {
    private string LineChannelID = "";
    private string LineChannelSecret = "";
    private string LineMid = "";

    public string AppInsightsInstrumentationKey { get; set; }

    [Obsolete("Line舊版API，已過時")]
    public LineBotHelper(string LineChannelID, string LineChannelSecret, string LineMid)
    {
      this.LineChannelID = LineChannelID;
      this.LineChannelSecret = LineChannelSecret;
      this.LineMid = LineMid;
    }

    [Obsolete("Line舊版API，已過時")]
    public string SendMessage(List<string> toUsers, string Msg)
    {
      try
      {
        HttpClient httpClient = new HttpClient();
        httpClient.BaseAddress = new Uri("https://trialbot-api.line.me/");
        httpClient.DefaultRequestHeaders.Add("X-Line-ChannelID", this.LineChannelID);
        httpClient.DefaultRequestHeaders.Add("X-Line-ChannelSecret", this.LineChannelSecret);
        httpClient.DefaultRequestHeaders.Add("X-Line-Trusted-User-With-ACL", this.LineMid);
        string content1 = "\r\n{\"to\":[\"{0}\"],\"toChannel\":1383378250,\"eventType\":\"138311608800106203\",\n \"content\":{\n    \"contentType\":1,\n    \"toType\":1,\n    \"text\":\"{1}\"\n  }}\r\n".Replace("{0}", toUsers[0]).Replace("{1}", Msg);
        HttpContent content2 = (HttpContent) new StringContent(content1, Encoding.UTF8, "application/json");
        httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        HttpResponseMessage result = httpClient.PostAsync("/v1/events", content2).Result;
        if (!string.IsNullOrEmpty(this.AppInsightsInstrumentationKey))
        {
          Dictionary<string, string> dictionary = new Dictionary<string, string>()
          {
            {
              "Send Json",
              content1
            },
            {
              "Send result",
              result.Content.ReadAsStringAsync().Result
            }
          };
        }
        return result.Content.ReadAsStringAsync().Result;
      }
      catch (Exception ex)
      {
        if (string.IsNullOrEmpty(this.AppInsightsInstrumentationKey))
          ;
        throw ex;
      }
    }

    [Obsolete("Line舊版API，已過時")]
    public ReceivedMessage GetReceivedMessage(string PostRawData)
    {
      return (ReceivedMessage) JsonConvert.DeserializeObject<ReceivedMessage>(PostRawData);
    }
  }
}
