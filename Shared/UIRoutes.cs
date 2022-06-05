namespace Shared;

public static class UIRoutes
{
    public static class Administration
    {
        private const string Root = $"/administration/";

        public static class CarBrands
        {
            private const string Base = $"{Root}carbrands/";
            public const string Index = $"{Base}";
        }
        public static class CarRecords
        {
            private const string Base = $"{Root}carrecords/";
            public const string Index = $"{Base}";
        }

    }
    public static class CarRecordOrders
    {
        private const string Root = "/carrecordorders/";
        public const string Search = $"{Root}search/";
        public const string Payment = $"{Root}payment/";
        public const string SearchResult = $"{Root}searchresult/";
    }
}
