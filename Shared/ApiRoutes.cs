namespace Shared;

public static class ApiRoutes
{
    private const string Root = "api/";

    public static class DbValidator
    {
        private const string Base = $"{Root}dbvalidator/";
        public const string IsCarBrandNameExists = $"{Base}iscarbrandexists/";
    }
    public static class CarBrands
    {
        private const string Base = $"{Root}carbrands/";
        public const string Create = $"{Base}create/";
        public const string Update = $"{Base}update/";
        public const string Remove = $"{Base}remove/";
        public const string GetAll = $"{Base}getall/";
    }

    public static class CarRecords
    {
        private const string Base = $"{Root}carrecords/";
        public const string Create = $"{Base}create/";
        public const string Update = $"{Base}update/";
        public const string Remove = $"{Base}remove/";
        public const string GetAll = $"{Base}getall/";
    }

    public static class CarRecordOrders
    {
        private const string Base = $"{Root}carrecordorders/";
        public const string Create = $"{Base}create/";
        public const string Search = $"{Base}search/";
        public const string GetByToken = $"{Base}getbytoken/";
        public const string GetAll = $"{Base}getall/";
    }
}
