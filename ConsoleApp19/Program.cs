using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebApiClient;

namespace ConsoleApp19
{
    class Program
    {
        static void Main(string[] args)
        {

            using (var client = HttpApiClient.Create<IUserApi>("http://localhost:9999"))
            {
                var user = new UserInfo
                {
                    Account = "laojiu",
                    Password = "123456",
                    BirthDay = DateTime.Parse("2018-01-01 12:30:30"),
                    Email = "laojiu@webapiclient.com",
                    //  Gender = Gender.Male
                };
                //var s = client.GetAboutAsync("aaaa","bbbbbb");
                var a = client.UpdateWithFormAsync(user,"aaaa",10);
               //var s= client.GetByAccountAsync("iddddddd");
                var user1 = client.GetByIdAsync("id001");
            }
        }
    }
}
