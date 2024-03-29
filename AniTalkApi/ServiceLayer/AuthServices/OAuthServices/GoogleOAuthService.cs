﻿using System.Security.Cryptography;
using System.Text;
using AniTalkApi.DataLayer.Models.Auth;
using AniTalkApi.DataLayer.Settings;
using AniTalkApi.Helpers;
using AniTalkApi.ServiceLayer.CryptoGeneratorServices;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace AniTalkApi.ServiceLayer.AuthServices.OAuthServices;

public class GoogleOAuthService : IOAuthService
{
    private readonly GoogleOAuthSettings _settings;

    private readonly HttpClientHelper _httpClient;

    private readonly ICryptoGeneratorService _cryptoGenerator;

    public GoogleOAuthService(
        IOptions<GoogleOAuthSettings> options,
        HttpClientHelper httpClient, 
        ICryptoGeneratorService cryptoGenerator)
    {
        _settings = options.Value;
        _httpClient = httpClient;
        _cryptoGenerator = cryptoGenerator;
    }

    public string GetOAuthUrl(string codeChallenge)
    {
        var url = _settings.AuthorizationEndpoint;

        var parameters = new Dictionary<string, string>
        {
            {"client_id", _settings.ClientId!},
            {"redirect_uri", _settings.RedirectUrl!},
            {"response_type", "code"},
            {"scope", _settings.Scope!},
            {"code_challenge", codeChallenge},
            {"code_challenge_method", "S256"},
        };
        var encodedParameters = string.Join("&", parameters
            .Select(x => $"{x.Key}={x.Value}"));
        return $"{url}?{encodedParameters}";
    }

    public async Task<string> ExchangeCodeToIdTokenAsync(string code, string codeVerifier)
    {
        var url = _settings.TokenEndpoint!;

        var parameters = new Dictionary<string, string>
        {
            {"code", code},
            {"client_id", _settings.ClientId!},
            {"client_secret", _settings.ClientSecret!},
            {"code_verifier", codeVerifier},
            {"grant_type", "authorization_code"},
            {"redirect_uri", _settings.RedirectUrl!}
        };

        var token = await _httpClient.SendPostRequest<IdTokenModel>(url, parameters);
        return token is null ?
            throw new Exception("Token is null") :
            token.IdToken!;
    }

    public string GenerateCodeVerifier()
    {
        return _cryptoGenerator.GenerateRandomString(_settings.CodeVerifierLength);
    }

    public string GenerateCodeChallenge(string codeVerifier)
    {
        return Base64UrlEncoder.Encode(SHA256
            .HashData(Encoding.UTF8.GetBytes(codeVerifier)));
    }
}