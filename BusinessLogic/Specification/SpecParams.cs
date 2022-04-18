namespace BusinessLogic.BaseSpecification
{
    public class SpecParams
    {
        private const int MaxPageSize = 10000;
        public int PageIndex { get; set; } = 1;

        private int _pageSize = 24;
        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = (value > MaxPageSize) ? MaxPageSize : value;
        }
        public string Sort { get; set; }
        public bool? IsAccepted { get; set; }
        public bool? IsUsedInModel { get; set; }
    }
}
