using System;
using RickandMorty.Interfaces;
using RickandMorty.iOS.Renderer;
using RickandMorty.Pages;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(CharactersList), typeof(SearchPageRenderer))]
namespace RickandMorty.iOS.Renderer
{
    public class SearchPageRenderer : PageRenderer, IUISearchResultsUpdating
    {
        readonly UISearchController _searchController;

        public SearchPageRenderer()
        {
            _searchController = new UISearchController(searchResultsController: null)
            {
                SearchResultsUpdater = this,
                DimsBackgroundDuringPresentation = false,
                HidesNavigationBarDuringPresentation = false,
                HidesBottomBarWhenPushed = true
            };
            _searchController.SearchBar.Placeholder = string.Empty;
        }

        public override void ViewDidAppear(bool animated)
        {
            base.ViewDidAppear(animated);

            if (ParentViewController.NavigationItem.SearchController is null)
            {
                ParentViewController.NavigationItem.SearchController = _searchController;
                DefinesPresentationContext = true;

                //Work-around to ensure the SearchController appears when the page first appears https://stackoverflow.com/a/46313164/5953643
                ParentViewController.NavigationItem.SearchController.Active = true;
                ParentViewController.NavigationItem.SearchController.Active = false;
            }
        }

        public override void ViewWillDisappear(bool animated)
        {
            base.ViewWillDisappear(animated);

            ParentViewController.NavigationItem.SearchController = null;
        }

        public void UpdateSearchResultsForSearchController(UISearchController searchController)
        {
            if (Element is ISearchPage searchPage)
                searchPage.OnSearchBarTextChanged(searchController.SearchBar.Text);
        }

        protected override void OnElementChanged(VisualElementChangedEventArgs e)
        {
            base.OnElementChanged(e);

            if (e.NewElement != null)
                e.NewElement.SizeChanged += HandleSizeChanged;
        }

        //Work-around to accommodate UISearchController height, https://github.com/brminnick/GitTrends/issues/171
        void HandleSizeChanged(object sender, EventArgs e)
        {
            if (ParentViewController?.NavigationItem.SearchController != null
                && Element.Height > -1
                && Element is Page page)
            {
                Element.SizeChanged -= HandleSizeChanged;

                if (NavigationController.NavigationBar.PrefersLargeTitles is true)
                {
                    var statusBarSize = UIApplication.SharedApplication.StatusBarFrame.Size;
                    var statusBarHeight = Math.Min(statusBarSize.Height, statusBarSize.Width);

                    page.Padding = new Thickness(page.Padding.Left,
                                                    page.Padding.Top,
                                                    page.Padding.Right,
                                                    page.Padding.Bottom + statusBarHeight);
                }
            }
        }
    }
}