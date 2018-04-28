﻿using System;
using System.Net;
using System.Net.Http;
using System.Net.Security;
using System.Security.Authentication;
using System.Security.Cryptography.X509Certificates;

namespace WebApiClient.Defaults
{
    /// <summary>
    /// 表示HttpClientHandler包装为IHttpClientHandler
    /// </summary>
    class HttpHandlerOfHttpClientHandler : IHttpHandler
    {
        /// <summary>
        /// 外部HttpClientHandler
        /// </summary>
        private readonly HttpClientHandler handler;

        /// <summary>
        /// 获取内部原始的Handler对象
        /// </summary>
        public HttpMessageHandler InnerHanlder { get; private set; }

        /// <summary>
        /// 获取或设置是否使用CookieContainer来管理Cookies
        /// </summary>
        public bool UseCookies
        {
            get => this.handler.UseCookies;
            set => this.handler.UseCookies = value;
        }

        /// <summary>
        /// 获取是否支持重定向设置
        /// </summary>
        public bool SupportsRedirectConfiguration
        {
            get => this.handler.SupportsRedirectConfiguration;
        }

        /// <summary>
        /// 获取是否支持代理
        /// </summary>
        public bool SupportsProxy
        {
            get => this.handler.SupportsProxy;
        }

        /// <summary>
        /// 获取是否支持压缩传输
        /// </summary>
        public bool SupportsAutomaticDecompression
        {
            get => this.handler.SupportsAutomaticDecompression;
        }

        /// <summary>
        /// 获取或设置代理
        /// </summary>
        public IWebProxy Proxy
        {
            get => this.handler.Proxy;
            set => this.handler.Proxy = value;
        }

        /// <summary>
        /// 获取或设置是否对请求进行预身份验证
        /// </summary>
        public bool PreAuthenticate
        {
            get => this.handler.PreAuthenticate;
            set => this.handler.PreAuthenticate = value;
        }

        /// <summary>
        /// 获取或设置每个响应的最大重定向次数
        /// </summary>
        public int MaxAutomaticRedirections
        {
            get => this.handler.MaxAutomaticRedirections;
            set => this.handler.MaxAutomaticRedirections = value;
        }

        /// <summary>
        /// 获取或设置最大请求内容字节长度
        /// </summary>
        public long MaxRequestContentBufferSize
        {
            get => this.handler.MaxRequestContentBufferSize;
            set => this.handler.MaxRequestContentBufferSize = value;
        }

        /// <summary>
        /// 获取或设置凭证信息
        /// </summary>
        public ICredentials Credentials
        {
            get => this.handler.Credentials;
            set => this.handler.Credentials = value;
        }

        /// <summary>
        /// 获取或设置Cookie管理容器
        /// </summary>
        public CookieContainer CookieContainer
        {
            get => this.handler.CookieContainer;
            set => this.handler.CookieContainer = value;
        }

        /// <summary>
        /// 获取或设置客户端证书选项
        /// </summary>
        public ClientCertificateOption ClientCertificateOptions
        {
            get => this.handler.ClientCertificateOptions;
            set => this.handler.ClientCertificateOptions = value;
        }

        /// <summary>
        /// 获取或设置压缩方式
        /// </summary>
        public DecompressionMethods AutomaticDecompression
        {
            get => this.handler.AutomaticDecompression;
            set => this.handler.AutomaticDecompression = value;
        }

        /// <summary>
        /// 获取或设置是否支持自动重定向
        /// </summary>
        public bool AllowAutoRedirect
        {
            get => this.handler.AllowAutoRedirect;
            set => this.handler.AllowAutoRedirect = value;
        }

        /// <summary>
        /// 获取或设置是否使用默认的凭证信息
        /// </summary>
        public bool UseDefaultCredentials
        {
            get => this.handler.UseDefaultCredentials;
            set => this.handler.UseDefaultCredentials = value;
        }

        /// <summary>
        /// 获取或设置是否使用代理
        /// </summary>
        public bool UseProxy
        {
            get => this.handler.UseProxy;
            set => this.handler.UseProxy = value;
        }

        /// <summary>
        /// HttpClientHandler包装为IHttpClientHandler
        /// </summary>
        /// <param name="handler"></param>
        public HttpHandlerOfHttpClientHandler(HttpClientHandler handler)
        {
            this.handler = handler;
            this.InnerHanlder = handler;
        }

        /// <summary>
        /// HttpClientHandler包装为IHttpClientHandler
        /// </summary>
        /// <param name="handler">设置属性的handler</param>
        /// <param name="sourceHandler">原始Handler</param>
        public HttpHandlerOfHttpClientHandler(HttpClientHandler handler, HttpMessageHandler sourceHandler)
        {
            this.handler = handler;
            this.InnerHanlder = sourceHandler;
        }

        /// <summary>
        /// 释放资源
        /// </summary>
        public void Dispose()
        {
            this.handler.Dispose();
        }
    }
}