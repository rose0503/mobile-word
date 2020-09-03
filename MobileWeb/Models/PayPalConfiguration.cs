using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PayPal.Api;

namespace MobileWeb.Models
{
    public class PayPalConfiguration
    {
        public readonly static string ClientId;
        public readonly static string ClientSecret;
        static PayPalConfiguration()
        {
            //TODO
            var config = GetConfig();
            ClientId = "AYVaQ3WHNqADBlOFMrGsw-RUT3aVnt8Y0OJBQ76k8P72laWM7yqbIvTrTfjdgNu4PZDpqtcEDxkkSB5G";//config["clientId"]
            ClientSecret = "EJKybmekgAJ2_CUdyN6o1c8DM9gG-0kSMK-gQp0NkAzHSEWlHCGwtXySPFhKd-rQ8V-WVpQRGDepQTSc";//config["clientSecret"];

        }
         public static Dictionary<string, string> GetConfig()
        {
            return PayPal.Api.ConfigManager.Instance.GetProperties();
        }

        private static string GetAccessToken()
        {
            string accessToken = new OAuthTokenCredential(ClientId, ClientSecret, GetConfig()).GetAccessToken();
            return accessToken;
        }

        public static APIContext GetAPIContext()
        {
            var apiContext = new APIContext(GetAccessToken());
            apiContext.Config = GetConfig();
            return apiContext;
        }
    }
}