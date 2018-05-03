using System;
using WebApiClient.Defaults;
using HTTP = System.Net.Http;

namespace WebApiClient
{
    /// <summary>
    /// 表示Http接口的配置项
    /// </summary>
    public class HttpApiConfig 
    {
        /// <summary>
        /// 获取默认xml格式化工具唯一实例
        /// </summary>
       // public static readonly IXmlFormatter DefaultXmlFormatter = new XmlFormatter();

        /// <summary>
        /// 获取默认json格式化工具唯一实例
        /// </summary>
        public static readonly IJsonFormatter DefaultJsonFormatter = new JsonFormatter();

        /// <summary>
        /// 获取默认KeyValue格式化工具唯一实例
        /// </summary>
        public static readonly IKeyValueFormatter DefaultKeyValueFormatter = new KeyValueFormatter();


        /// <summary>
        /// 自定义数据容器
        /// </summary>
        private Tags tags;

        /// <summary>
        /// 同步锁
        /// </summary>
        private readonly object syncRoot = new object();


        /// <summary>
        /// 获取配置的自定义数据的存储和访问容器
        /// </summary>
        public Tags Tags
        {
            get => this.GetTagsSafeSync();
        }

        /// <summary>
        /// 获取或设置Http服务完整主机域名
        /// 例如http://www.webapiclient.com
        /// 设置了HttpHost值，HttpHostAttribute将失效  
        /// </summary>
        public Uri HttpHost { get; set; }

        /// <summary>
        /// 获取或设置请求时序列化使用的默认格式   
        /// 影响JsonFormatter或KeyValueFormatter的序列化
        /// </summary>
        public FormatOptions FormatOptions { get; set; } = new FormatOptions();

        /// <summary>
        /// 获取或设置Xml格式化工具
        /// </summary>
       // public IXmlFormatter XmlFormatter { get; set; } = DefaultXmlFormatter;

        /// <summary>
        /// 获取或设置Json格式化工具
        /// </summary>
        public IJsonFormatter JsonFormatter { get; set; } = DefaultJsonFormatter;

        /// <summary>
        /// 获取或设置KeyValue格式化工具
        /// </summary>
        public IKeyValueFormatter KeyValueFormatter { get; set; } = DefaultKeyValueFormatter;

        /// <summary>
        /// 获取全局过滤器集合
        /// 非线程安全类型
        /// </summary>
     //   public GlobalFilterCollection GlobalFilters { get; private set; } = new GlobalFilterCollection();

        /// <summary>
        /// 以同步安全方式获取Tags实例
        /// </summary>
        /// <returns></returns>
        private Tags GetTagsSafeSync()
        {
            lock (this.syncRoot)
            {
                if (this.tags == null)
                {
                    this.tags = new Tags();
                }
                return this.tags;
            }
        }
    }
}
