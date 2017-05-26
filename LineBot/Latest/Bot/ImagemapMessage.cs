// Decompiled with JetBrains decompiler
// Type: AL2018.LineBot.ImagemapMessage
// Assembly: LineBot, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2BBFEF47-CFFC-47E0-9A9F-E00E117F93C2
// Assembly location: C:\inetpub\linebottest\packages\LineBotSDK.0.5.1\lib\net45\LineBot.dll

using System;
using System.Collections.Generic;
using System.Drawing;

namespace AL2018.LineBot
{
  public class ImagemapMessage
  {
    private Uri _baseUrl;
    private List<ImagemapActionBase> _actions;

    public Uri baseUrl
    {
      get
      {
        return this._baseUrl;
      }
      set
      {
        this._baseUrl = value;
        if (!this._baseUrl.ToString().ToLower().StartsWith("https"))
          throw new Exception("baseUrl必須是https");
      }
    }

    public string altText { get; set; }

    public Size baseSize { get; set; }

    public List<ImagemapActionBase> actions
    {
      get
      {
        if (this._actions == null)
          this._actions = new List<ImagemapActionBase>();
        return this._actions;
      }
      set
      {
        this._actions = value;
      }
    }
  }
}
