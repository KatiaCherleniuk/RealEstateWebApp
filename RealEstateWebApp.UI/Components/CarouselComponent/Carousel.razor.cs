using System.Collections.Generic;
using System.Timers;
using Microsoft.AspNetCore.Components;
using Timer = System.Timers.Timer;

namespace RealEstateWebApp.UI.Components.CarouselComponent
{
    public partial class Carousel
    {
        [Parameter] public string Id { get; set; }

        [Parameter] public List<BaseFile> Imageset { get; set; }

        [Parameter] public string Name { get; set; }
        
        [Parameter] public bool IsFullField { get; set; }

        [Parameter] public string CssClass { get; set; }

        [Parameter] public string ImageClass { get; set; }

        [Parameter] public string PreviewImageClass { get; set; }

        [Parameter] public int? AutoScrollInterval { get; set; }

        public delegate void CarouselItemClicked(object sender, int index);

        [Parameter] public CarouselItemClicked OnCarouselItemClicked { get; set; }

        [Parameter] public bool ShowNavigation { get; set; } = true;

        [Parameter] public bool IsShowPreview { get; set; } = false;

        [Parameter] public string BaseCarouselClass { get; set; }


        private int _carouselRenderIndex = -1;
        private bool _isShowPreview = false;
        private int _activeImageIndex = 0;
        private Timer _scrollTimer = null;

        protected override async Task OnParametersSetAsync()
        {
            if ((this.AutoScrollInterval ?? 0) > 0)
            {
                int scrollMilliseconds = ((int)(this.AutoScrollInterval)) * 1000;

                _scrollTimer?.Stop();

                if (_scrollTimer == null)
                {
                    _scrollTimer = new Timer();
                    _scrollTimer.Elapsed += (o, e) =>
                    {
                        _activeImageIndex += 1;

                        if (_activeImageIndex > (Imageset?.Count - 1 ?? 0))
                        {
                            _activeImageIndex = 0;
                        }

                        this.InvokeAsync(() => { this.StateHasChanged(); });
                    };
                }

                _scrollTimer.Interval = scrollMilliseconds;
                _scrollTimer?.Start();
            }
            else
            {
                _scrollTimer?.Stop();
            }

            _isShowPreview = IsShowPreview;
        }

        protected void OnNextClicked()
        {
            _carouselRenderIndex = -1;
            _activeImageIndex += 1;
            StateHasChanged();
        }

        protected void OnPreviousClicked()
        {
            _carouselRenderIndex = -1;
            _activeImageIndex -= 1;
            StateHasChanged();
        }

        private void SetActiveImageIndex(int newIndex)
        {
            _carouselRenderIndex = -1;
            _activeImageIndex = newIndex;

            StateHasChanged();
        }

        private string GetClasses()
        {
            string imageStateClass = "hidden";

            _carouselRenderIndex++;

            if (_carouselRenderIndex == _activeImageIndex)
            {
                imageStateClass = null;
            }

            imageStateClass += OnCarouselItemClicked != null ? " blazor-carousel-pointer" : "";
            return imageStateClass.Trim();
        }

        private string GetIndicatorState()
        {
            string imageStateClass = "";

            _carouselRenderIndex++;

            if (_carouselRenderIndex == _activeImageIndex)
            {
                imageStateClass = "blazor-carousel-indicator-active active";
            }

            return imageStateClass;
        }

        private string GetIndicatorPreviewState()
        {
            string imageStateClass = "";

            _carouselRenderIndex++;

            if (_carouselRenderIndex == _activeImageIndex)
            {
                imageStateClass = "blazor-carousel-preview-active";
            }

            return imageStateClass;
        }

        private string GetImageSource(BaseFile imageFile)
        {
            var imageSrc = imageFile.Url;
            if (string.IsNullOrEmpty(imageFile.Url) && imageFile.FileContent?.Length > 0)
            {
                imageSrc = imageFile.Base64Image;
            }

            return imageSrc;
        }

        private string ResetRenderCounter()
        {
            _carouselRenderIndex = -1;
            return null;
        }


        private void OnCarouselClick(Carousel carousel)
        {
            if(IsFullField)
                return;
            OnCarouselItemClicked?.Invoke(carousel, _activeImageIndex);
            // if (_activeImageIndex == Imageset.Count - 1)
            // {
            //     _activeImageIndex = 0;
            //     return;
            // }
            //
            // _carouselRenderIndex = -1;
            // _activeImageIndex += 1;

          
        }

        public void ShowFullFiled(int index)
        {
            _activeImageIndex = index;
            IsFullField = true;
            IsShowPreview = false;
            StateHasChanged();
        }

        private void CloseFullFill()
        {
            IsFullField = false;
            IsShowPreview = _isShowPreview;
            StateHasChanged();
        }
    }
}