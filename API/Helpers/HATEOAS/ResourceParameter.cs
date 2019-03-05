namespace izibongo.api.API.Helpers.HATEOAS
{
    public class ResourceParameter
    {
        private const uint MaxPageSize = 20;
        public uint PageNumber { get; set; } = 1;
        private uint pageSize = 10;

        public uint PageSize{
            get => pageSize;
            set => pageSize = value > MaxPageSize ? MaxPageSize: value;
        }

    }
}