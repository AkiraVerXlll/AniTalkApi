﻿using Microsoft.AspNetCore.WebUtilities;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace AniTalkApi.Helpers;

public class HttpClientHelper
{
    private readonly HttpClient _httpClient;

    public HttpClientHelper(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<T> SendGetRequest<T>(string endpoint, Dictionary<string, string> queryParams,
        string accessToken)
    {
        return await SendHttpRequestAsync<T>(HttpMethod.Get, endpoint, accessToken, queryParams);
    }

    public async Task<T> SendPostRequest<T>(string endpoint, Dictionary<string, string> bodyParams)
    {
        var httpContent = new FormUrlEncodedContent(bodyParams);
        return await SendHttpRequestAsync<T>(HttpMethod.Post, endpoint, httpContent: httpContent);
    }

    public async Task SendPutRequest(string endpoint, Dictionary<string, string> queryParams,
        object body, string accessToken)
    {
        var bodyJson = JsonConvert.SerializeObject(body);
        var httpContent = new StringContent(bodyJson, Encoding.UTF8, "application/json");
        await SendHttpRequestAsync<dynamic>(HttpMethod.Put, endpoint, accessToken, queryParams, httpContent);
    }

    private async Task<T> SendHttpRequestAsync<T>(HttpMethod httpMethod, string endpoint,
        string? accessToken = null, Dictionary<string, string>? queryParams = null, HttpContent? httpContent = null)
    {
        var url = queryParams is not null
            ? QueryHelpers.AddQueryString(endpoint, queryParams!)
            : endpoint;

        var request = new HttpRequestMessage(httpMethod, url);

        if (accessToken != null)
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

        if (httpContent != null)
            request.Content = httpContent;

        using var response = await _httpClient.SendAsync(request);

        var resultJson = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
            throw new HttpRequestException(resultJson);

        var result = JsonConvert.DeserializeObject<T>(resultJson);
        return result!;
    }

    public async Task<IFormFile> GetImageAsFormFileAsync(string imageUrl)
    {
        var response = await _httpClient.GetAsync(imageUrl);

        if (response.IsSuccessStatusCode)
        {
            await using var stream = await response.Content.ReadAsStreamAsync();
            var memoryStream = new MemoryStream();
            await stream.CopyToAsync(memoryStream);

            IFormFile imageFile = new FormFile
                (memoryStream, 0, memoryStream.Length, null!, new Guid().ToString());
            return imageFile;
        }
        else
        {
            throw new HttpRequestException
                ($"Failed to retrieve image. Status code: {response.StatusCode}");
        }

    }
}
