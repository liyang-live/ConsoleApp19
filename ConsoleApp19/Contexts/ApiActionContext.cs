using Newtonsoft.Json;
using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace WebApiClient.Contexts
{
    /// <summary>
    /// 表示请求Api的上下文
    /// </summary>
    public class ApiActionContext
    {
        /// <summary>
        /// Http客户端
        /// </summary>
        public static HttpClient client = new HttpClient();

        /// <summary>
        /// 自定义数据的存储和访问容器
        /// </summary>
        private Tags tags;

        /// <summary>
        /// 获取本次请求相关的自定义数据的存储和访问容器
        /// </summary>
        public Tags Tags
        {
            get
            {
                if (this.tags == null)
                {
                    this.tags = new Tags();
                }
                return this.tags;
            }
        }

        /// <summary>
        /// 获取关联的HttpApiConfig
        /// </summary>
        public HttpApiConfig HttpApiConfig { get; internal set; }

        /// <summary>
        /// 获取关联的ApiActionDescriptor
        /// </summary>
        public ApiActionDescriptor ApiActionDescriptor { get; internal set; }

        /// <summary>
        /// 获取关联的HttpRequestMessage
        /// </summary>
        public HttpApiRequestMessage RequestMessage { get; internal set; }

        /// <summary>
        /// 获取关联的HttpResponseMessage
        /// </summary>
        public HttpResponseMessage ResponseMessage { get; internal set; }

        /// <summary>
        /// 获取调用Api得到的结果
        /// </summary>
        public object Result { get; internal set; }

        /// <summary>
        /// 获取调用Api产生的异常
        /// </summary>
        public Exception Exception { get; internal set; }


        /// <summary>
        /// 准备请求数据
        /// </summary>
        /// <returns></returns>
        internal void PrepareRequestAsync()
        {
            var apiAction = this.ApiActionDescriptor;
            foreach (var actionAttribute in apiAction.Attributes)
            {
                actionAttribute.BeforeRequestAsync(this);
            }

            foreach (var parameter in apiAction.Parameters)
            {
                foreach (var parameterAttribute in parameter.Attributes)
                {
                    parameterAttribute.BeforeRequestAsync(this, parameter);
                }
            }
        }


        /// <summary>
        /// 执行请求
        /// 返回是否执行成功
        /// </summary>
        /// <returns></returns>
        internal bool ExecRequestAsync()
        {
            try
            {
                this.ResponseMessage = client.SendAsync(RequestMessage).Result;

                var json = ResponseMessage.Content.ReadAsStringAsync().Result;

                var contentType = new ContentType(ResponseMessage.Content.Headers.ContentType);

                if (ApiActionDescriptor.Return.ReturnType.IsPrimitive || ApiActionDescriptor.Return.ReturnType == typeof(string))
                {
                    this.Result = Convert.ChangeType(json, ApiActionDescriptor.Return.ReturnType);
                }
                else if (contentType.IsApplicationJson())
                {
                    this.Result = JsonConvert.DeserializeObject(json, ApiActionDescriptor.Return.ReturnType);
                }
                else if (contentType.IsApplicationXml())
                {
                    this.Result = Deserialize(json, ApiActionDescriptor.Return.ReturnType);
                }
                return true;
            }
            catch (Exception ex)
            {
                this.Exception = ex;
                return false;
            }
        }


        /// <summary>
        /// 反序列化xml为对象
        /// </summary>
        /// <param name="xml">xml</param>
        /// <param name="objType">对象类型</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="InvalidOperationException"></exception>
        /// <returns></returns>
        public virtual object Deserialize(string xml, Type objType)
        {
            if (objType == null)
            {
                throw new ArgumentNullException(nameof(objType));
            }

            if (string.IsNullOrEmpty(xml))
            {
                return null;
            }

            var xmlSerializer = new XmlSerializer(objType);
            using (var reader = new StringReader(xml))
            {
                return xmlSerializer.Deserialize(reader);
            }
        }

        ///// <summary>
        ///// 执行所有过滤器
        ///// </summary>
        ///// <param name="funcSelector">方法选择</param>
        ///// <returns></returns>
        //internal async Task ExecFiltersAsync(Func<IApiActionFilter, Func<ApiActionContext, Task>> funcSelector)
        //{
        //    foreach (var filter in this.HttpApiConfig.GlobalFilters)
        //    {
        //        await funcSelector(filter)(this);
        //    }

        //    foreach (var filter in this.ApiActionDescriptor.Filters)
        //    {
        //        await funcSelector(filter)(this);
        //    }
        //}


        /// <summary>
        /// 表示回复的ContentType
        /// </summary>
        private struct ContentType
        {
            /// <summary>
            /// ContentType内容
            /// </summary>
            private readonly string contentType;

            /// <summary>
            /// 回复的ContentType
            /// </summary>
            /// <param name="contentType">ContentType内容</param>
            public ContentType(MediaTypeHeaderValue contentType)
            {
                this.contentType = contentType?.MediaType;
            }

            public bool IsPrimitive(Type type)
            {
                if (type.IsPrimitive || type == typeof(string))
                {
                    return true;
                }
                return false;
            }

            /// <summary>
            /// 是否为某个Mime
            /// </summary>
            /// <param name="mediaType"></param>
            /// <returns></returns>
            public bool Is(string mediaType)
            {
                return this.contentType != null && this.contentType.StartsWith(mediaType, StringComparison.OrdinalIgnoreCase);
            }

            /// <summary>
            /// 是否为json
            /// </summary>
            /// <returns></returns>
            public bool IsApplicationJson()
            {
                return this.Is("application/json") || this.Is("text/json");
            }

            /// <summary>
            /// 是否为xml
            /// </summary>
            /// <returns></returns>
            public bool IsApplicationXml()
            {
                return this.Is("application/xml") || this.Is("text/xml");
            }
        }
    }

}
