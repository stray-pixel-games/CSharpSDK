using System.Collections.Generic;
using System.Text;

namespace PlayFab
{
    public class PlayFabSettings
    {
        public const string SdkVersion = "1.42.181218";
        public const string BuildIdentifier = "jbuild_csharpsdk__sdk-slave2016-3_1";
        public const string SdkVersionString = "CSharpSDK-1.42.181218";
        public static readonly Dictionary<string, string> RequestGetParams = new Dictionary<string, string> {
            { "sdk", SdkVersionString }
        };

        /// <summary> This is only for customers running a private cluster.  Generally you shouldn't touch this </summary>
        public static string ProductionEnvironmentUrl = ".playfabapi.com";
        /// <summary> The name of a customer vertical. This is only for customers running a private cluster.  Generally you shouldn't touch this </summary>
        public static string VerticalName = null;
        /// <summary> Session token for Entity API. Auto-Populated by GetEntityToken method. </summary>
        internal static string EntityToken = null;
        /// <summary> Session ticket for Client API. Auto-Populated by any login or registration call. </summary>
        internal static string ClientSessionTicket = null;
        /// <summary> You must set this value for PlayFabSdk to work properly (Found in the Game Manager for your title, at the PlayFab Website) </summary>
        public static string DeveloperSecretKey = null;
        /// <summary> You must set this value for PlayFabSdk to work properly (Found in the Game Manager for your title, at the PlayFab Website) </summary>
        public static string TitleId;
        public static ErrorCallback GlobalErrorHandler;
        /// <summary> Set this to the appropriate AD_TYPE_X constant below </summary>
        public static string AdvertisingIdType = null;
        /// <summary> Set this to corresponding device value </summary>
        public static string AdvertisingIdValue = null;

        // DisableAdvertising is provided for completeness, but changing it is not suggested
        // Disabling this may prevent your advertising-related PlayFab marketplace partners from working correctly
        public static bool DisableAdvertising = false;
        public static readonly string AD_TYPE_IDFA = "Idfa";
        public static readonly string AD_TYPE_ANDROID_ID = "Adid";

        public static string GetFullUrl(string apiCall, Dictionary<string, string> getParams)
        {
            StringBuilder sb = new StringBuilder(1000);
        
            var baseUrl = ProductionEnvironmentUrl;
            if (!baseUrl.StartsWith("http"))
            {
                if (VerticalName != null)
                {
                    sb.Append("https://").Append(VerticalName);
                }
                else
                {
                    sb.Append("https://").Append(TitleId);
                }
            }
                
            sb.Append(baseUrl).Append(apiCall);
        
            if (getParams != null)
            {
                bool firstParam = true;
                foreach (var paramPair in getParams)
                {
                    if (firstParam)
                    {
                        sb.Append("?");
                        firstParam = false;
                    }
                    else
                    {
                        sb.Append("&");
                    }
                    sb.Append(paramPair.Key).Append("=").Append(paramPair.Value);
                }
            }
        
            return sb.ToString();
        }
    }
}
