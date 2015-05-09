namespace OMP.API
{
    using System.Web.Http;
    using SimpleInjector;
    using SimpleInjector.Integration.WebApi;
    using OMP.Service.Interface;
    using OMP.Service.Service;
    using OMP.Domain.Repositories;
    using OMP.Domain.Model;
    using OMP.API.Models;
    
    public static class SimpleInjectorWebApiInitializer
    {
        public static void Initialize(HttpConfiguration config)
        {
            var container = new Container();
            
            InitializeContainer(container);

            container.RegisterWebApiControllers(config);
            container.RegisterWebApiFilterProvider(config);
       
            //container.Verify();

            config.DependencyResolver = new SimpleInjectorWebApiDependencyResolver(container);
        }
     
        private static void InitializeContainer(Container container)
        {
            container.Register<UnitOfWork>();
            container.Register<OMPContext>();

            container.Register<ICategoryService, CategoryService>();
            container.Register<ICQRSService, CQRSService>();
            container.Register<IOrderService, OrderService>();
            container.Register<IProductReviewService, ProductReviewService>();
            container.Register<IProductService, ProductService>();
            container.Register<IProductTagService, ProductTagService>();
            container.Register<ISearchService, SearchService>();
            container.Register<IShopService, ShopService>();
            container.Register<ITagService, TagService>();
            container.Register<IUserProfileService, UserProfileService>();
            container.Register<IUserRatingService, UserRatingService>();
            container.Register<IWishListService, WishListService>();
        }
    }
}