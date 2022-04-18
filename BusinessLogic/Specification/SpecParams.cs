namespace BusinessLogic.BaseSpecification
{
    public class SpecParams
    {
        private const int MaxPageSize = 10000;

        private int _pageIndex = 1;
        public int PageIndex { 
            get => _pageIndex;
            set => _pageIndex = (value > 0)
                ? value
                : 1;
        }

        private int _pageSize = 24;
        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = (value > MaxPageSize) 
                ? MaxPageSize 
                : value > 0 
                    ? value
                    : _pageSize;
        }
        public string Sort { get; set; }
        public bool? IsAccepted { get; set; }
        public bool? IsUsedInModel { get; set; }
    }
}
