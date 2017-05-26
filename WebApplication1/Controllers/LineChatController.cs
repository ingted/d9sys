using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Utility;

namespace WebApplication1.Controllers
{
    public static class ControllShare
    {
        public static void POST(string ChannelAccessToken, HttpRequestMessage Request)
        {
            string rtk = "";
            try
            {
                //取得 http Post RawData(should be JSON)
                string postData = Request.Content.ReadAsStringAsync().Result;
                //剖析JSON
                var ReceivedMessage = AL2018.LineBot.Utility.Parsing(postData);
                //回覆訊息
                var rm = ReceivedMessage.events[0].message.text;
                rtk = ReceivedMessage.events[0].replyToken;
                var typ = ReceivedMessage.events[0].type;
                string Message;
                if (rm == "top cust")
                {
                    var sc = new SQLContext();
                
                    sc.cmd = @"SELECT E.USER_NAME, E.SUM_PRICE  
                                FROM( 
                                  SELECT D.USER_NAME, D.SUM_PRICE, MAX (D.SUM_PRICE) OVER (PARTITION BY 1) MAX_SUM_PRICE FROM  
                                  ( 
                                   SELECT C.USER_NAME, SUM(A.PRICE) AS SUM_PRICE FROM [dbo].[CONSUM_DETAIL] A 
                                   LEFT JOIN  
                                    (SELECT [CONSUM_DETAIL_ID],[USER_ID_PARENT]  FROM [dbo].[CONSUM_DETAIL_GROUP] 
                                    GROUP BY [CONSUM_DETAIL_ID],[USER_ID_PARENT]) B ON A.[CONSUM_DETAIL_ID] = B.[CONSUM_DETAIL_ID] 
                                   LEFT JOIN [dbo].[CUSTOM_MAIN] C ON B. USER_ID_PARENT = C.[USER_ID] 
                                   GROUP BY C.USER_NAME 
                                  )D 
                                 )E 
                                WHERE E.SUM_PRICE = E.MAX_SUM_PRICE";
                    var data = new Data("DATA SOURCE=dknet.database.windows.net,1433;User=yuen;Password=@a&z*u$rJe.com;Database=dknet");
                    var rr = data.QuerySql.Invoke(sc);

                    Message = rr.resultDS.Tables[0].Rows[0][0].ToString() + " : " + rr.resultDS.Tables[0].Rows[0][1].ToString();

                }
                else {
                    Message = "你說了:" + rm;
                }
                //回覆用戶
                try
                {
                    AL2018.LineBot.Utility.ReplyMessage(rtk, Message, ChannelAccessToken);
                }
                catch { }
                AL2018.LineBot.Utility.ReplyMessage(rtk, Message, ChannelAccessToken);
                //回覆API OK

            }
            catch (Exception ex)
            {
                if (rtk != "00000000000000000000000000000000")
                {
                    AL2018.LineBot.Utility.ReplyMessage(rtk, ex.Message, ChannelAccessToken);
                }
            }
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
            //    var ReceivedMessage = AL2018.LineBot.Utility.Parsing(postData);
            //    //回覆訊息
            //    string Message;
            //    Message = "你說了:" + ReceivedMessage.events[0].message.text;
            //    //回覆用戶
            //    AL2018.LineBot.Utility.ReplyMessage(ReceivedMessage.events[0].replyToken, Message, ChannelAccessToken);
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
