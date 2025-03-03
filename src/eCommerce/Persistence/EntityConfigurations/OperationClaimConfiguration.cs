using Application.Features.Auth.Constants;
using Application.Features.OperationClaims.Constants;
using Application.Features.UserOperationClaims.Constants;
using Application.Features.Users.Constants;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NArchitecture.Core.Security.Constants;
using Application.Features.Products.Constants;
using Application.Features.Orders.Constants;
using Application.Features.Categories.Constants;
using Application.Features.Baskets.Constants;
using Application.Features.BasketItems.Constants;
using Application.Features.OrderItems.Constants;
using Application.Features.Addresses.Constants;
using Application.Features.Discounts.Constants;
using Application.Features.ProductImages.Constants;
using Application.Features.ProductComments.Constants;
using Application.Features.OrderHistories.Constants;

namespace Persistence.EntityConfigurations;

public class OperationClaimConfiguration : BaseEntityConfiguration<OperationClaim, int>
{
    public override void Configure(EntityTypeBuilder<OperationClaim> builder)
    {
        base.Configure(builder);
        builder.Property(oc => oc.Name).IsRequired().HasMaxLength(50);

        builder.HasData(_seeds);
    }

    public static int AdminId => 1;
    private IEnumerable<OperationClaim> _seeds
    {
        get
        {
            yield return new() { Id = AdminId, Name = GeneralOperationClaims.Admin };

            IEnumerable<OperationClaim> featureOperationClaims = getFeatureOperationClaims(AdminId);
            foreach (OperationClaim claim in featureOperationClaims)
                yield return claim;
        }
    }

#pragma warning disable S1854 // Unused assignments should be removed
    private IEnumerable<OperationClaim> getFeatureOperationClaims(int initialId)
    {
        int lastId = initialId;
        List<OperationClaim> featureOperationClaims = new();

        #region Auth
        featureOperationClaims.AddRange(
            [
                new() { Id = ++lastId, Name = AuthOperationClaims.Admin },
                new() { Id = ++lastId, Name = AuthOperationClaims.Read },
                new() { Id = ++lastId, Name = AuthOperationClaims.Write },
                new() { Id = ++lastId, Name = AuthOperationClaims.RevokeToken },
            ]
        );
        #endregion

        #region OperationClaims
        featureOperationClaims.AddRange(
            [
                new() { Id = ++lastId, Name = OperationClaimsOperationClaims.Admin },
                new() { Id = ++lastId, Name = OperationClaimsOperationClaims.Read },
                new() { Id = ++lastId, Name = OperationClaimsOperationClaims.Write },
                new() { Id = ++lastId, Name = OperationClaimsOperationClaims.Create },
                new() { Id = ++lastId, Name = OperationClaimsOperationClaims.Update },
                new() { Id = ++lastId, Name = OperationClaimsOperationClaims.Delete },
            ]
        );
        #endregion

        #region UserOperationClaims
        featureOperationClaims.AddRange(
            [
                new() { Id = ++lastId, Name = UserOperationClaimsOperationClaims.Admin },
                new() { Id = ++lastId, Name = UserOperationClaimsOperationClaims.Read },
                new() { Id = ++lastId, Name = UserOperationClaimsOperationClaims.Write },
                new() { Id = ++lastId, Name = UserOperationClaimsOperationClaims.Create },
                new() { Id = ++lastId, Name = UserOperationClaimsOperationClaims.Update },
                new() { Id = ++lastId, Name = UserOperationClaimsOperationClaims.Delete },
            ]
        );
        #endregion

        #region Users
        featureOperationClaims.AddRange(
            [
                new() { Id = ++lastId, Name = UsersOperationClaims.Admin },
                new() { Id = ++lastId, Name = UsersOperationClaims.Read },
                new() { Id = ++lastId, Name = UsersOperationClaims.Write },
                new() { Id = ++lastId, Name = UsersOperationClaims.Create },
                new() { Id = ++lastId, Name = UsersOperationClaims.Update },
                new() { Id = ++lastId, Name = UsersOperationClaims.Delete },
            ]
        );
        #endregion

        
        #region Products CRUD
        featureOperationClaims.AddRange(
            [
                new() { Id = ++lastId, Name = ProductsOperationClaims.Admin },
                new() { Id = ++lastId, Name = ProductsOperationClaims.Read },
                new() { Id = ++lastId, Name = ProductsOperationClaims.Write },
                new() { Id = ++lastId, Name = ProductsOperationClaims.Create },
                new() { Id = ++lastId, Name = ProductsOperationClaims.Update },
                new() { Id = ++lastId, Name = ProductsOperationClaims.Delete },
            ]
        );
        #endregion
        
        
        #region Orders CRUD
        featureOperationClaims.AddRange(
            [
                new() { Id = ++lastId, Name = OrdersOperationClaims.Admin },
                new() { Id = ++lastId, Name = OrdersOperationClaims.Read },
                new() { Id = ++lastId, Name = OrdersOperationClaims.Write },
                new() { Id = ++lastId, Name = OrdersOperationClaims.Create },
                new() { Id = ++lastId, Name = OrdersOperationClaims.Update },
                new() { Id = ++lastId, Name = OrdersOperationClaims.Delete },
            ]
        );
        #endregion
        
        
        #region Categories CRUD
        featureOperationClaims.AddRange(
            [
                new() { Id = ++lastId, Name = CategoriesOperationClaims.Admin },
                new() { Id = ++lastId, Name = CategoriesOperationClaims.Read },
                new() { Id = ++lastId, Name = CategoriesOperationClaims.Write },
                new() { Id = ++lastId, Name = CategoriesOperationClaims.Create },
                new() { Id = ++lastId, Name = CategoriesOperationClaims.Update },
                new() { Id = ++lastId, Name = CategoriesOperationClaims.Delete },
            ]
        );
        #endregion
        
        
        #region Baskets CRUD
        featureOperationClaims.AddRange(
            [
                new() { Id = ++lastId, Name = BasketsOperationClaims.Admin },
                new() { Id = ++lastId, Name = BasketsOperationClaims.Read },
                new() { Id = ++lastId, Name = BasketsOperationClaims.Write },
                new() { Id = ++lastId, Name = BasketsOperationClaims.Create },
                new() { Id = ++lastId, Name = BasketsOperationClaims.Update },
                new() { Id = ++lastId, Name = BasketsOperationClaims.Delete },
            ]
        );
        #endregion
        
        
        #region BasketItems CRUD
        featureOperationClaims.AddRange(
            [
                new() { Id = ++lastId, Name = BasketItemsOperationClaims.Admin },
                new() { Id = ++lastId, Name = BasketItemsOperationClaims.Read },
                new() { Id = ++lastId, Name = BasketItemsOperationClaims.Write },
                new() { Id = ++lastId, Name = BasketItemsOperationClaims.Create },
                new() { Id = ++lastId, Name = BasketItemsOperationClaims.Update },
                new() { Id = ++lastId, Name = BasketItemsOperationClaims.Delete },
            ]
        );
        #endregion
        
        
        #region OrderItems CRUD
        featureOperationClaims.AddRange(
            [
                new() { Id = ++lastId, Name = OrderItemsOperationClaims.Admin },
                new() { Id = ++lastId, Name = OrderItemsOperationClaims.Read },
                new() { Id = ++lastId, Name = OrderItemsOperationClaims.Write },
                new() { Id = ++lastId, Name = OrderItemsOperationClaims.Create },
                new() { Id = ++lastId, Name = OrderItemsOperationClaims.Update },
                new() { Id = ++lastId, Name = OrderItemsOperationClaims.Delete },
            ]
        );
        #endregion
        
        
        #region Addresses CRUD
        featureOperationClaims.AddRange(
            [
                new() { Id = ++lastId, Name = AddressesOperationClaims.Admin },
                new() { Id = ++lastId, Name = AddressesOperationClaims.Read },
                new() { Id = ++lastId, Name = AddressesOperationClaims.Write },
                new() { Id = ++lastId, Name = AddressesOperationClaims.Create },
                new() { Id = ++lastId, Name = AddressesOperationClaims.Update },
                new() { Id = ++lastId, Name = AddressesOperationClaims.Delete },
            ]
        );
        #endregion
        
        
        #region Discounts CRUD
        featureOperationClaims.AddRange(
            [
                new() { Id = ++lastId, Name = DiscountsOperationClaims.Admin },
                new() { Id = ++lastId, Name = DiscountsOperationClaims.Read },
                new() { Id = ++lastId, Name = DiscountsOperationClaims.Write },
                new() { Id = ++lastId, Name = DiscountsOperationClaims.Create },
                new() { Id = ++lastId, Name = DiscountsOperationClaims.Update },
                new() { Id = ++lastId, Name = DiscountsOperationClaims.Delete },
            ]
        );
        #endregion
        
        
        #region ProductImages CRUD
        featureOperationClaims.AddRange(
            [
                new() { Id = ++lastId, Name = ProductImagesOperationClaims.Admin },
                new() { Id = ++lastId, Name = ProductImagesOperationClaims.Read },
                new() { Id = ++lastId, Name = ProductImagesOperationClaims.Write },
                new() { Id = ++lastId, Name = ProductImagesOperationClaims.Create },
                new() { Id = ++lastId, Name = ProductImagesOperationClaims.Update },
                new() { Id = ++lastId, Name = ProductImagesOperationClaims.Delete },
            ]
        );
        #endregion
        
        
        #region ProductComments CRUD
        featureOperationClaims.AddRange(
            [
                new() { Id = ++lastId, Name = ProductCommentsOperationClaims.Admin },
                new() { Id = ++lastId, Name = ProductCommentsOperationClaims.Read },
                new() { Id = ++lastId, Name = ProductCommentsOperationClaims.Write },
                new() { Id = ++lastId, Name = ProductCommentsOperationClaims.Create },
                new() { Id = ++lastId, Name = ProductCommentsOperationClaims.Update },
                new() { Id = ++lastId, Name = ProductCommentsOperationClaims.Delete },
            ]
        );
        #endregion
        
        
        #region OrderHistories CRUD
        featureOperationClaims.AddRange(
            [
                new() { Id = ++lastId, Name = OrderHistoriesOperationClaims.Admin },
                new() { Id = ++lastId, Name = OrderHistoriesOperationClaims.Read },
                new() { Id = ++lastId, Name = OrderHistoriesOperationClaims.Write },
                new() { Id = ++lastId, Name = OrderHistoriesOperationClaims.Create },
                new() { Id = ++lastId, Name = OrderHistoriesOperationClaims.Update },
                new() { Id = ++lastId, Name = OrderHistoriesOperationClaims.Delete },
            ]
        );
        #endregion
        
        return featureOperationClaims;
    }
#pragma warning restore S1854 // Unused assignments should be removed
}
