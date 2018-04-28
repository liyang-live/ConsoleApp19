﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using WebApiClient;
using WebApiClient.Attributes;
using WebApiClient.DataAnnotations;
using WebApiClient.Parameterables;

namespace ConsoleApp19
{
    /// <summary>
    /// 用户操作接口
    /// </summary>
    //[LogFilter] // 记录请求日志
    //[HttpHost("http://localhost:9999")] // HttpHost可以在Config传入覆盖    
    //[AutoReturn(EnsureSuccessStatusCode = true)]
    public interface IUserApi : IHttpApiClient
    {
        // GET {url}?account={account}&password={password}&something={something}
        [HttpGet]
        [Header("Cookie", "a=1; b=2", EncodeCookie = false)]
        [Timeout(10 * 1000)] // 10s超时
        string GetAboutAsync(
             string authorization,
            string something);

        // /GET webapi/user/GetById?id=id001
        // Return HttpResponseMessage
        [HttpGet("/webapi/user/GetById/{id}")]
        //[BasicAuth("userName", "password")]
        UserInfo GetByIdAsync(
            string id);

        // GET /webapi/user/GetByAccount?account=laojiu
        // Return 原始string内容
        [HttpGet("/webapi/user/GetByAccount")]
        string GetByAccountAsync(
            string account);


        // POST /webapi/user/UpdateWithForm  
        // Body Account=laojiu&Password=123456&name=value&nickName=老九&age=18
        // Return json或xml内容
        [HttpPost("/webapi/user/UpdateWithForm")]
        ITask<UserInfo> UpdateWithFormAsync([FormContent]UserInfo user,
            string nickName, int? nullableAge);

        // POST /webapi/user/UpdateWithJson
        // Body {"Account":"laojiu","Password":"123456"}
        // Return json或xml内容
        [HttpPost("/webapi/user/UpdateWithJson")]
        ITask<UserInfo> UpdateWithJsonAsync(
            [JsonContent("yyyy-MM-dd HH:mm:ss")] UserInfo user);

        // POST /webapi/user/UpdateWithXml 
        // Body <?xml version="1.0" encoding="utf-8"?><UserInfo><Account>laojiu</Account><Password>123456</Password></UserInfo>
        // Return xml内容
        //[XmlReturn]
        [HttpPost("/webapi/user/UpdateWithXml")]
        ITask<UserInfo> UpdateWithXmlAsync(
             UserInfo user);

        // POST /webapi/user/UpdateWithMulitpart  
        // Body multipart/form-data
        // Return json或xml内容
        [HttpPost("/webapi/user/UpdateWithMulitpart")]
        ITask<UserInfo> UpdateWithMulitpartAsync(
             UserInfo user,
             string nickName,
            MulitpartText age,
            params MulitpartFile[] files);
    }

    /// <summary>
    /// 表示用户模型
    /// </summary>
    public class UserInfo
    {
        public string Account { get; set; }

        public string Password { get; set; }

        [IgnoreWhenNull]
        [DateTimeFormat("yyyy-MM-dd")]
        public DateTime? BirthDay { get; set; }

        // public Gender Gender { get; set; }

        [IgnoreSerialized]
        public string Email { get; set; }
    }
}
