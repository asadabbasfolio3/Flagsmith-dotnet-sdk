﻿using Moq;
using Moq.Protected;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Flagsmith.DotnetClient.Test
{
    internal static class HttpMocker
    {
        public static Mock<HttpClient> MockHttpResponse(HttpResponseMessage httpResponseMessage)
        {
            var httpClientMock = new Mock<HttpClient>();
            httpClientMock.Setup(x => x.SendAsync(It.IsAny<HttpRequestMessage>(), It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(new HttpResponseMessage()
                {
                    StatusCode = httpResponseMessage.StatusCode,
                    Content = httpResponseMessage.Content
                }));
            return httpClientMock;
        }
        public static Mock<HttpMessageHandler> MockHttpMessageHandler(HttpResponseMessage httpResponseMessage)
        {
            var handlerMock = new Mock<HttpMessageHandler>();
            handlerMock
               .Protected()
               .Setup<Task<HttpResponseMessage>>(
                  "SendAsync",
                  ItExpr.IsAny<HttpRequestMessage>(),
                  ItExpr.IsAny<CancellationToken>())
               .ReturnsAsync(httpResponseMessage);
            return handlerMock;
        }
        public static Mock<HttpClient> MockHttpThrowConnectionError()
        {
            var httpClientMock = new Mock<HttpClient>();
            httpClientMock.Setup(x => x.SendAsync(It.IsAny<HttpRequestMessage>(), It.IsAny<CancellationToken>()))
                .ThrowsAsync(new HttpRequestException());
            return httpClientMock;
        }
    }
    
}
