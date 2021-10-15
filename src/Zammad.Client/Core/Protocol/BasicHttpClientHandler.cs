﻿using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Zammad.Client.Core.Protocol
{
    public class BasicHttpClientHandler : HttpClientHandlerBase
    {
        private readonly AuthenticationHeaderValue _authenticationHeader;

        public BasicHttpClientHandler(string user, string password, string onBehalfOf)
            : base(onBehalfOf)
        {
            ArgumentCheck.ThrowIfNullOrEmpty(user, nameof(user));
            ArgumentCheck.ThrowIfNullOrEmpty(password, nameof(password));
            _authenticationHeader = CreateAuthenticationHeader(user, password);
            ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => { return true; }; //no SSL check needed yet
        }

        private AuthenticationHeaderValue CreateAuthenticationHeader(string user, string password)
        {
            return new AuthenticationHeaderValue("Basic",
                Convert.ToBase64String(
                    Encoding.GetEncoding("ISO-8859-1").GetBytes($"{user}:{password}")));
        }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            request.Headers.Authorization = _authenticationHeader;
            return base.SendAsync(request, cancellationToken);
        }
    }
}
