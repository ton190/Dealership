namespace Shared;

public static class UIRoutes
{
    public const string Error = "/error";
    public static class Administration
    {
        public const string Root = $"/admin/";
        public const string Index = $"{Root}";

        public static class Brands
        {
            public const string Base = $"{Root}brands/";
            public const string Index = $"{Base}";
        }
        public static class Records
        {
            public const string Base = $"{Root}records/";
            public const string Index = $"{Base}";
        }
        public static class Orders
        {
            public const string Base = $"{Root}orders/";
            public const string Index = $"{Base}";
        }
        public static class Account
        {
            public const string Base = $"{Root}account/";
            public const string Login = $"{Base}login";
            public const string UpdateAdminProfile
                = $"{Base}updateadminprofile";
        }
    }
    public static class Orders
    {
        public const string Root = "/orders/";
        public const string Search = $"{Root}search/";
        public const string SearchResult = $"{Root}searchresult/";
    }
}
