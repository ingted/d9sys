// Decompiled with JetBrains decompiler
// Type: AL2018.LineBot.TemplateMessageBase
// Assembly: LineBot, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2BBFEF47-CFFC-47E0-9A9F-E00E117F93C2
// Assembly location: C:\inetpub\linebottest\packages\LineBotSDK.0.5.1\lib\net45\LineBot.dll

namespace AL2018.LineBot
{
  public abstract class TemplateMessageBase
  {
    public string type
    {
      get
      {
        return "template";
      }
    }

    public string altText { get; set; }

    public TemplateMessageBase()
    {
      if (!string.IsNullOrEmpty(this.altText))
        return;
      this.altText = "我傳送給您了一則訊息，但由於您的裝置不支援TemplateMessage，因此無法顯示。";
    }
  }
}
