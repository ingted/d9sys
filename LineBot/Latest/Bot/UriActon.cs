// Decompiled with JetBrains decompiler
// Type: AL2018.LineBot.UriActon
// Assembly: LineBot, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2BBFEF47-CFFC-47E0-9A9F-E00E117F93C2
// Assembly location: C:\inetpub\linebottest\packages\LineBotSDK.0.5.1\lib\net45\LineBot.dll

using System;

namespace AL2018.LineBot
{
  public class UriActon : TemplateActionBase
  {
    public string label { get; set; }

    public Uri uri { get; set; }

    public UriActon()
    {
      this.type = "uri";
    }
  }
}
