// Decompiled with JetBrains decompiler
// Type: AL2018.LineBot.PostbackActon
// Assembly: LineBot, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2BBFEF47-CFFC-47E0-9A9F-E00E117F93C2
// Assembly location: C:\inetpub\linebottest\packages\LineBotSDK.0.5.1\lib\net45\LineBot.dll

namespace AL2018.LineBot
{
  public class PostbackActon : TemplateActionBase
  {
    public string label { get; set; }

    public string data { get; set; }

    public string text { get; set; }

    public PostbackActon()
    {
      this.type = "postback";
    }
  }
}
