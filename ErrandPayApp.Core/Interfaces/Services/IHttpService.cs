﻿using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace ErrandPayApp.Core.Interfaces.Services
{
    public interface IHttpService
    {
        Task<HttpResponseMessage> Post(string url, HttpContent content, IDictionary<string, string> headers, string token);
        Task<HttpResponseMessage> Get(string url, IDictionary<string, string> headers, string token);
        Task<HttpResponseMessage> Put(string url, HttpContent content, IDictionary<string, string> headers, string token);
    }
}
