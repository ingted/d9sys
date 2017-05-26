// Decompiled with JetBrains decompiler
// Type: AL2018.LineBot.Bot
// Assembly: LineBot, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2BBFEF47-CFFC-47E0-9A9F-E00E117F93C2
// Assembly location: C:\inetpub\linebottest\packages\LineBotSDK.0.5.1\lib\net45\LineBot.dll

using System;

namespace AL2018.LineBot
{
  public class Bot
  {
    private string channelAccessToken { get; set; }

    public Bot(string ChannelAccessToken)
    {
      this.channelAccessToken = ChannelAccessToken;
    }

    public string ReplyMessage(string ReplyToken, string Message)
    {
      if (Message.Length < 0)
        throw new Exception("訊息內容不正確");
      if (Message.Length > 1800)
        throw new Exception("訊息內容太長");
      return Utility.ReplyMessage(ReplyToken, Message, this.channelAccessToken);
    }

    public string ReplyMessage(string ReplyToken, int packageId, int stickerId)
    {
      return Utility.ReplyStickerMessage(ReplyToken, packageId, stickerId, this.channelAccessToken);
    }

    public string ReplyMessage(string ReplyToken, Uri ContentUrl)
    {
      return Utility.ReplyImageMessage(ReplyToken, ContentUrl.ToString(), ContentUrl.ToString(), this.channelAccessToken);
    }

    public string ReplyMessage(string ReplyToken, Uri originalContentUrl, Uri previewImageUrl)
    {
      return Utility.ReplyImageMessage(ReplyToken, originalContentUrl.ToString(), previewImageUrl.ToString(), this.channelAccessToken);
    }

    public string PushMessage(string ToUserID, ImagemapMessage Message)
    {
      return Utility.PushImageMapMessage(ToUserID, Message, this.channelAccessToken);
    }

    public string PushMessage(string ToUserID, ButtonsTemplate Message)
    {
      return Utility.PushTemplateMessage(ToUserID, Message, this.channelAccessToken);
    }

    public string PushMessage(string ToUserID, ConfirmTemplate Message)
    {
      return Utility.PushTemplateMessage(ToUserID, Message, this.channelAccessToken);
    }

    public string PushMessage(string ToUserID, CarouselTemplate Message)
    {
      return Utility.PushTemplateMessage(ToUserID, Message, this.channelAccessToken);
    }

    public string PushMessage(string ToUserID, string TextMessage)
    {
      if (TextMessage.Length < 0)
        throw new Exception("訊息內容不正確");
      if (TextMessage.Length > 1800)
        throw new Exception("訊息內容太長");
      return Utility.PushMessage(ToUserID, TextMessage, this.channelAccessToken);
    }

    public string PushMessage(string ToUserID, Uri ContentUrl)
    {
      return Utility.PushImageMessage(ToUserID, ContentUrl.ToString(), ContentUrl.ToString(), this.channelAccessToken);
    }

    public string PushMessage(string ToUserID, Uri originalContentUrl, Uri previewImageUrl)
    {
      return Utility.PushImageMessage(ToUserID, originalContentUrl.ToString(), previewImageUrl.ToString(), this.channelAccessToken);
    }

    public string PushMessage(string ToUserID, int packageId, int stickerId)
    {
      return Utility.PushStickerMessage(ToUserID, packageId, stickerId, this.channelAccessToken);
    }

    public string Leave(string GroupId)
    {
      return Utility.Leave(GroupId, this.channelAccessToken);
    }

    public LineUserInfo GetUserInfo(string UserUid)
    {
      if (string.IsNullOrEmpty(UserUid))
        throw new Exception("UserUid不得為空");
      return Utility.GetUserInfo(UserUid, this.channelAccessToken);
    }

    public byte[] GetUserUploadedContent(string ContentID)
    {
      return Utility.GetUserUploadedContent(ContentID, this.channelAccessToken);
    }
  }
}
