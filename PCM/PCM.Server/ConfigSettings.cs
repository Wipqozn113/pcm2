using static System.Net.WebRequestMethods;

namespace PCM.Server
{
    public static class ConfigSettings
    {
        /*********************/
        /* Directus Settings */
        /*********************/
        public static string DirectusUrl => "http://127.0.0.1:8055";

        public static string DirectusAdminUrl => $"{DirectusUrl}/admin";

        public static string DirectusContentUrl => $"{DirectusAdminUrl}/content";

        /********/
        /* Misc */
        /********/
        public static int PartyLevel => 4;
    }
}
