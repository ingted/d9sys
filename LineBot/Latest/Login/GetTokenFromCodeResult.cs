// Decompiled with JetBrains decompiler
// Type: AL2018.LineLogin.GetTokenFromCodeResult
// Assembly: LineBot, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2BBFEF47-CFFC-47E0-9A9F-E00E117F93C2
// Assembly location: C:\inetpub\linebottest\packages\LineBotSDK.0.5.1\lib\net45\LineBot.dll

namespace AL2018.LineLogin
{
  public class GetTokenFromCodeResult
  {
    public string mid { get; set; }

    public string access_token { get; set; }

    public string token_type { get; set; }

    public string expires_in { get; set; }

    public string refresh_token { get; set; }

    public string scope { get; set; }
  }
}
