using OMP.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OMP.Domain.Repositories
{
    public class UnitOfWork
    {
        private OMPContext db;

        public UnitOfWork(OMPContext db)
        {
            this.db = db;
        }

        public void Save()
        {
            db.SaveChanges();
        }

        public async Task<bool> SaveAsync()
        {
            await db.SaveChangesAsync();

            return true;
        }

        private IRepository<Cart> cartRepo;
        public IRepository<Cart> CartRepository
        {
            get
            {
                if (this.cartRepo == null)
                {
                    this.cartRepo = new Repository<Cart>(db);
                }
                return cartRepo;
            }
        }

        private IRepository<Category> categoryRepo;
        public IRepository<Category> categoryRepository
        {
            get
            {
                if (this.categoryRepo == null)
                {
                    this.categoryRepo = new Repository<Category>(db);
                }
                return categoryRepo;
            }
        }

        private IRepository<CategoryAttributes> categoryAttributesRepo;
        public IRepository<CategoryAttributes> CategoryAttributesRepository
        {
            get
            {
                if (this.categoryAttributesRepo == null)
                {
                    this.categoryAttributesRepo = new Repository<CategoryAttributes>(db);
                }
                return categoryAttributesRepo;
            }
        }

        private IRepository<CQRS_ProductSummary> cQRS_ProductSummaryRepo;
        public IRepository<CQRS_ProductSummary> CQRS_ProductSummaryRepository
        {
            get
            {
                if (this.cQRS_ProductSummaryRepo == null)
                {
                    this.cQRS_ProductSummaryRepo = new Repository<CQRS_ProductSummary>(db);
                }
                return cQRS_ProductSummaryRepo;
            }
        }

        private IRepository<Order> orderRepo;
        public IRepository<Order> OrderRepository
        {
            get
            {
                if (this.orderRepo == null)
                {
                    this.orderRepo = new Repository<Order>(db);
                }
                return orderRepo;
            }
        }

        private IRepository<OrderDetails> orderDetailsRepo;
        public IRepository<OrderDetails> OrderDetailsRepository
        {
            get
            {
                if (this.orderDetailsRepo == null)
                {
                    this.orderDetailsRepo = new Repository<OrderDetails>(db);
                }
                return orderDetailsRepo;
            }
        }

        private IRepository<Product> productRepo;
        public IRepository<Product> ProductRepository
        {
            get
            {
                if (this.productRepo == null)
                {
                    this.productRepo = new Repository<Product>(db);
                }
                return productRepo;
            }
        }

        private IRepository<ProductAttributesValue> productAttributesValueRepo;
        public IRepository<ProductAttributesValue> ProductAttributesValueRepository
        {
            get
            {
                if (this.productAttributesValueRepo == null)
                {
                    this.productAttributesValueRepo = new Repository<ProductAttributesValue>(db);
                }
                return productAttributesValueRepo;
            }
        }

        private IRepository<ProductImage> productImageRepo;
        public IRepository<ProductImage> ProductImageRepository
        {
            get
            {
                if (this.productImageRepo == null)
                {
                    this.productImageRepo = new Repository<ProductImage>(db);
                }
                return productImageRepo;
            }
        }

        private IRepository<ProductReview> productReviewRepo;
        public IRepository<ProductReview> ProductReviewRepository
        {
            get
            {
                if (this.productReviewRepo == null)
                {
                    this.productReviewRepo = new Repository<ProductReview>(db);
                }
                return productReviewRepo;
            }
        }

        private IRepository<ProductTag> productTagRepo;
        public IRepository<ProductTag> ProductTagRepository
        {
            get
            {
                if (this.productTagRepo == null)
                {
                    this.productTagRepo = new Repository<ProductTag>(db);
                }
                return productTagRepo;
            }
        }

        private IRepository<UserRating> userRatingRepo;
        public IRepository<UserRating> UserRatingRepository
        {
            get
            {
                if (this.userRatingRepo == null)
                {
                    this.userRatingRepo = new Repository<UserRating>(db);
                }
                return userRatingRepo;
            }
        }

        private IRepository<Search> searchRepo;
        public IRepository<Search> SearchRepository
        {
            get
            {
                if (this.searchRepo == null)
                {
                    this.searchRepo = new Repository<Search>(db);
                }
                return searchRepo;
            }
        }

        private IRepository<Shop> shopRepo;
        public IRepository<Shop> ShopRepository
        {
            get
            {
                if (this.shopRepo == null)
                {
                    this.shopRepo = new Repository<Shop>(db);
                }
                return shopRepo;
            }
        }

        private IRepository<Tag> tagRepo;
        public IRepository<Tag> TagRepository
        {
            get
            {
                if (this.tagRepo == null)
                {
                    this.tagRepo = new Repository<Tag>(db);
                }
                return tagRepo;
            }
        }

        private IRepository<UserProfile> userProfileRepo;
        public IRepository<UserProfile> userProfileRepository
        {
            get
            {
                if (this.userProfileRepo == null)
                {
                    this.userProfileRepo = new Repository<UserProfile>(db);
                }
                return userProfileRepo;
            }
        }

        private IRepository<WishList> wishListRepo;
        public IRepository<WishList> WishListRepository
        {
            get
            {
                if (this.wishListRepo == null)
                {
                    this.wishListRepo = new Repository<WishList>(db);
                }
                return wishListRepo;
            }
        }
    }
}
