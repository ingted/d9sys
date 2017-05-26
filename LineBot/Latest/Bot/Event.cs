// Decompiled with JetBrains decompiler
// Type: AL2018.LineBot.Event
// Assembly: LineBot, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2BBFEF47-CFFC-47E0-9A9F-E00E117F93C2
// Assembly location: C:\inetpub\linebottest\packages\LineBotSDK.0.5.1\lib\net45\LineBot.dll

namespace AL2018.LineBot
{
  public class Event
  {
    public string type { get; set; }

    public string replyToken { get; set; }

    public Source source { get; set; }

    public long timestamp { get; set; }

    public Message message { get; set; }

    public Postback postback { get; set; }
  }
}
