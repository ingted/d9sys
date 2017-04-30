using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebApplication1.Controllers
{
    public static class ControllShare
    {
        public static void POST(string ChannelAccessToken, HttpRequestMessage Request)
        {
            //try
            //{
                //取得 http Post RawData(should be JSON)
                string postData = Request.Content.ReadAsStringAsync().Result;
                //剖析JSON
                var ReceivedMessage = isRock.LineBot.Utility.Parsing(postData);
                //回覆訊息
                string Message;
                Message = "你說了:" + ReceivedMessage.events[0].message.text;
                //回覆用戶
                isRock.LineBot.Utility.ReplyMessage(ReceivedMessage.events[0].replyToken, Message, ChannelAccessToken);
                //回覆API OK
                
            //}
            //catch (Exception ex)
            //{
                
            //}
        }
    }
    public class LineChatController : ApiController
    {
        [HttpPost]
        public IHttpActionResult POST()
        {
            ControllShare.POST("dJ+ypQkLvesQNHMLugcUabRdGI1Kespl3MQp1WD+eqFjf3/xrx2GEzSsGVFoDRD+Gh9L9KhN44G8d5QDuOohcNbIJfRRzSr19mD9yKeSIH22QpMGiDL0XPflkS0nhafH5KSuI5s6HX/Tw7biGQ5qvwdB04t89/1O/w1cDnyilFU=", Request);
            return Ok();
            //string ChannelAccessToken = "dJ+ypQkLvesQNHMLugcUabRdGI1Kespl3MQp1WD+eqFjf3/xrx2GEzSsGVFoDRD+Gh9L9KhN44G8d5QDuOohcNbIJfRRzSr19mD9yKeSIH22QpMGiDL0XPflkS0nhafH5KSuI5s6HX/Tw7biGQ5qvwdB04t89/1O/w1cDnyilFU=";

            //try
            //{
            //    //取得 http Post RawData(should be JSON)
            //    string postData = Request.Content.ReadAsStringAsync().Result;
            //    //剖析JSON
            //    var ReceivedMessage = isRock.LineBot.Utility.Parsing(postData);
            //    //回覆訊息
            //    string Message;
            //    Message = "你說了:" + ReceivedMessage.events[0].message.text;
            //    //回覆用戶
            //    isRock.LineBot.Utility.ReplyMessage(ReceivedMessage.events[0].replyToken, Message, ChannelAccessToken);
            //    //回覆API OK
            //    return Ok();
            //}
            //catch (Exception ex)
            //{
            //    return Ok();
            //}
        }
    }

    public class LineChat2Controller : ApiController
    {
        [HttpPost]
        public IHttpActionResult POST() {
            ControllShare.POST("FNCe0FEXzPr4JRIT7sD5GUZsYQE3tZww5WRhKv+6bc8+Qy42oJYr0RcMu+zw+9Jflawac+yWWKRF2c3IGG6eALL5GEhwzt0mHVP5//50Y//dgcjG3LeWEI74DGZrgUpl2hbZEDoyyTJwa4eOOl7JSwdB04t89/1O/w1cDnyilFU=", Request);
            return Ok();
        }
    }
}
