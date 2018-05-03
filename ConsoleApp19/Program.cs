using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebApiClient;
using System.Diagnostics;

namespace ConsoleApp19
{
    class Program
    {
        static void Main(string[] args)
        {

            var client = HttpApiClient.Create<IUserApi>("http://localhost:8083");
            var user = new UserInfo
            {
                Account = "laojiu",
                Password = "123456",
                BirthDay = DateTime.Parse("2018-01-01 12:30:30"),
                Email = "laojiu@webapiclient.com",
                //  Gender = Gender.Male
            };

            Stopwatch sw = new Stopwatch();
            sw.Start();
            for (int i = 0; i < 2; i++)
            {
                var a1 = client.GetDrugAttribute("");

                //Console.WriteLine(a1[0].Name);
            }
            sw.Stop();
            Console.WriteLine(sw.ElapsedMilliseconds+"");
            //var s = client.GetAboutAsync("aaaa","bbbbbb");
            //var a = client.UpdateWithFormAsync(user, "aaaa", 10);
            ////var s= client.GetByAccountAsync("iddddddd");
            //var user1 = client.GetByIdAsync("id001");
        }
    }
}
