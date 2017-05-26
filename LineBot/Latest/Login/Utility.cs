// Decompiled with JetBrains decompiler
// Type: AL2018.LineLogin.Utility
// Assembly: LineBot, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2BBFEF47-CFFC-47E0-9A9F-E00E117F93C2
// Assembly location: C:\inetpub\linebottest\packages\LineBotSDK.0.5.1\lib\net45\LineBot.dll

using Newtonsoft.Json;
using System;
using System.Collections.Specialized;
using System.IO;
using System.Net;
using System.Text;

namespace AL2018.LineLogin
{
  public class Utility
  {
    public static GetTokenFromCodeResult GetToeknFromCode(string code, string ClientId, string ClientSecret, string redirect_uri)
    {
      try
      {
        WebClient webClient = new WebClient();
        webClient.Encoding = Encoding.UTF8;
        webClient.Headers.Clear();
        NameValueCollection data = new NameValueCollection();
        data["grant_type"] = "authorization_code";
        data["code"] = code;
        data["redirect_uri"] = redirect_uri;
        data["client_id"] = ClientId;
        data["client_secret"] = ClientSecret;
        return (GetTokenFromCodeResult) JsonConvert.DeserializeObject<GetTokenFromCodeResult>(Encoding.UTF8.GetString(webClient.UploadValues("https://api.line.me/v1/oauth/accessToken", data)));
      }
      catch (WebException ex)
      {
        using (StreamReader streamReader = new StreamReader(ex.Response.GetResponseStream()))
          throw new Exception("GetToeknFromCode: " + streamReader.ReadToEnd(), (Exception) ex);
      }
    }

    public static Profile GetUserProfile(string access_token)
    {
      try
      {
        WebClient webClient = new WebClient();
        webClient.Headers.Clear();
        webClient.Headers.Add("Content-Type", "application/json");
        webClient.Headers.Add("Authorization", "Bearer " + access_token);
        return (Profile) JsonConvert.DeserializeObject<Profile>(Encoding.UTF8.GetString(webClient.DownloadData("https://api.line.me/v1/profile")));
      }
      catch (WebException ex)
      {
        using (StreamReader streamReader = new StreamReader(ex.Response.GetResponseStream()))
          throw new Exception("GetUserProfile: " + streamReader.ReadToEnd(), (Exception) ex);
      }
    }
  }
}
