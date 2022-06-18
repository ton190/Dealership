namespace Shared;

public static class ApiRoutes
{
    private const string Root = "api";

    public static class DbValidator
    {
        private const string Base = $"{Root}/dbvalidator";
        public const string IsBrandNameExists = $"{Base}/isbrandexists";
        public const string IsUserEmailExists = $"{Base}/isuseremailexists";
    }
    public static class Brands
    {
        private const string Base = $"{Root}/brands";
        public const string Create = $"{Base}/create";
        public const string Update = $"{Base}/update";
        public const string Remove = $"{Base}/remove";
        public const string GetAll = $"{Base}/getall";
    }

    public static class Records
    {
        private const string Base = $"{Root}/records";
        public const string Create = $"{Base}/create";
        public const string Update = $"{Base}/update";
        public const string Remove = $"{Base}/remove";
        public const string GetAll = $"{Base}/getall";
        public const string GetStatistics = $"{Base}/getstatistics";
    }

    public static class Orders
    {
        private const string Base = $"{Root}/orders";
        public const string Create = $"{Base}/create";
        public const string Search = $"{Base}/search";
        public const string GetAll = $"{Base}/getall";
        public const string GetOrder = $"{Base}/getorder";
        public const string GetStatistics = $"{Base}/getstatistics";
    }

    public static class Account
    {
        private const string Base = $"{Root}/account";
        public const string Login = $"{Base}/login";
        public const string Logout = $"{Base}/logout";
        public const string UpdateAdminProfile = $"{Base}/updateadminprofile";
        public const string GetAdminProfile = $"{Base}/getadminprofile";
        public const string GetIdentity = $"{Base}/getidentity";
    }
}
