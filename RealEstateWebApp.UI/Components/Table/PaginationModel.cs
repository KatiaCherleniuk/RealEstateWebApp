namespace RealEstateWebApp.UI.Components.Table
{
    public class PaginationModel
    {
        private int _totalListSize;
        private int _pageSize;

        public int StepsSize { get; private set; }
        public int PageSize
        {
            get => _pageSize;
            set
            {
                _pageSize = value;
                RecalculateStepsSize();
            }
        }

        public int CurrentStep { get; set; } = 1;

        public int TotalListSize
        {
            get => _totalListSize;
            set
            {
                _totalListSize = value;
                RecalculateStepsSize();
            }
        }

        private void RecalculateStepsSize()
        {
            StepsSize = (_totalListSize + _pageSize - 1) / _pageSize;
        }
    }
}

