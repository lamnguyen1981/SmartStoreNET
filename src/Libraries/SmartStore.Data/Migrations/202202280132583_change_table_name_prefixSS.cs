﻿namespace SmartStore.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class change_table_name_prefixSS : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Topic", newName: "SSTopic");
            RenameTable(name: "dbo.TaxCategory", newName: "SSTaxCategory");
            RenameTable(name: "dbo.Setting", newName: "SSSetting");
            RenameTable(name: "dbo.LocalizedProperty", newName: "SSLocalizedProperty");
            RenameTable(name: "dbo.Language", newName: "SSLanguage");
            RenameTable(name: "dbo.LocaleStringResource", newName: "SSLocaleStringResource");
            RenameTable(name: "dbo.PermissionRecord", newName: "SSPermissionRecord");
            RenameTable(name: "dbo.PermissionRoleMapping", newName: "SSPermissionRoleMapping");
            RenameTable(name: "dbo.CustomerRole", newName: "SSCustomerRole");
            RenameTable(name: "dbo.RuleSet", newName: "SSRuleSet");
            RenameTable(name: "dbo.Category", newName: "SSCategory");
            RenameTable(name: "dbo.Discount", newName: "SSDiscount");
            RenameTable(name: "dbo.Manufacturer", newName: "SSManufacturer");
            RenameTable(name: "dbo.MediaFile", newName: "SSMediaFile");
            RenameTable(name: "dbo.MediaFolder", newName: "SSMediaFolder");
            RenameTable(name: "dbo.MediaStorage", newName: "SSMediaStorage");
            RenameTable(name: "dbo.Product_MediaFile_Mapping", newName: "SSProduct_MediaFile_Mapping");
            RenameTable(name: "dbo.Product", newName: "SSProduct");
            RenameTable(name: "dbo.Country", newName: "SSCountry");
            RenameTable(name: "dbo.Currency", newName: "SSCurrency");
            RenameTable(name: "dbo.StateProvince", newName: "SSStateProvince");
            RenameTable(name: "dbo.DeliveryTime", newName: "SSDeliveryTime");
            RenameTable(name: "dbo.ProductBundleItem", newName: "SSProductBundleItem");
            RenameTable(name: "dbo.ProductBundleItemAttributeFilter", newName: "SSProductBundleItemAttributeFilter");
            RenameTable(name: "dbo.Product_Category_Mapping", newName: "SSProduct_Category_Mapping");
            RenameTable(name: "dbo.Product_Manufacturer_Mapping", newName: "SSProduct_Manufacturer_Mapping");
            RenameTable(name: "dbo.ProductReview", newName: "SSProductReview");
            RenameTable(name: "dbo.CustomerContent", newName: "SSCustomerContent");
            RenameTable(name: "dbo.BlogComment", newName: "SSBlogComment");
            RenameTable(name: "dbo.ProductReviewHelpfulness", newName: "SSProductReviewHelpfulness");
            RenameTable(name: "dbo.ForumPostVote", newName: "SSForumPostVote");
            RenameTable(name: "dbo.NewsComment", newName: "SSNewsComment");
            RenameTable(name: "dbo.PollVotingRecord", newName: "SSPollVotingRecord");
            RenameTable(name: "dbo.Customer", newName: "SSCustomer");
            RenameTable(name: "dbo.Address", newName: "SSAddress");
            RenameTable(name: "dbo.BlogPost", newName: "SSBlogPost");
            RenameTable(name: "dbo.Forums_Post", newName: "SSForums_Post");
            RenameTable(name: "dbo.Forums_Topic", newName: "SSForums_Topic");
            RenameTable(name: "dbo.Forums_Forum", newName: "SSForums_Forum");
            RenameTable(name: "dbo.Forums_Group", newName: "SSForums_Group");
            RenameTable(name: "dbo.News", newName: "SSNews");
            RenameTable(name: "dbo.PollAnswer", newName: "SSPollAnswer");
            RenameTable(name: "dbo.Poll", newName: "SSPoll");
            RenameTable(name: "dbo.CustomerRoleMapping", newName: "SSCustomerRoleMapping");
            RenameTable(name: "dbo.ExternalAuthenticationRecord", newName: "SSExternalAuthenticationRecord");
            RenameTable(name: "dbo.Order", newName: "SSOrder");
            RenameTable(name: "dbo.DiscountUsageHistory", newName: "SSDiscountUsageHistory");
            RenameTable(name: "dbo.GiftCardUsageHistory", newName: "SSGiftCardUsageHistory");
            RenameTable(name: "dbo.GiftCard", newName: "SSGiftCard");
            RenameTable(name: "dbo.OrderItem", newName: "SSOrderItem");
            RenameTable(name: "dbo.OrderNote", newName: "SSOrderNote");
            RenameTable(name: "dbo.RewardPointsHistory", newName: "SSRewardPointsHistory");
            RenameTable(name: "dbo.Shipment", newName: "SSShipment");
            RenameTable(name: "dbo.ShipmentItem", newName: "SSShipmentItem");
            RenameTable(name: "dbo.WalletHistory", newName: "SSWalletHistory");
            RenameTable(name: "dbo.ReturnRequest", newName: "SSReturnRequest");
            RenameTable(name: "dbo.ShoppingCartItem", newName: "SSShoppingCartItem");
            RenameTable(name: "dbo.Product_SpecificationAttribute_Mapping", newName: "SSProduct_SpecificationAttribute_Mapping");
            RenameTable(name: "dbo.SpecificationAttributeOption", newName: "SSSpecificationAttributeOption");
            RenameTable(name: "dbo.SpecificationAttribute", newName: "SSSpecificationAttribute");
            RenameTable(name: "dbo.ProductTag", newName: "SSProductTag");
            RenameTable(name: "dbo.ProductVariantAttributeCombination", newName: "SSProductVariantAttributeCombination");
            RenameTable(name: "dbo.QuantityUnit", newName: "SSQuantityUnit");
            RenameTable(name: "dbo.Product_ProductAttribute_Mapping", newName: "SSProduct_ProductAttribute_Mapping");
            RenameTable(name: "dbo.ProductAttribute", newName: "SSProductAttribute");
            RenameTable(name: "dbo.ProductAttributeOptionsSet", newName: "SSProductAttributeOptionsSet");
            RenameTable(name: "dbo.ProductAttributeOption", newName: "SSProductAttributeOption");
            RenameTable(name: "dbo.ProductVariantAttributeValue", newName: "SSProductVariantAttributeValue");
            RenameTable(name: "dbo.Download", newName: "SSDownload");
            RenameTable(name: "dbo.TierPrice", newName: "SSTierPrice");
            RenameTable(name: "dbo.MediaTag", newName: "SSMediaTag");
            RenameTable(name: "dbo.MediaTrack", newName: "SSMediaTrack");
            RenameTable(name: "dbo.PaymentMethod", newName: "SSPaymentMethod");
            RenameTable(name: "dbo.Rule", newName: "SSRule");
            RenameTable(name: "dbo.ShippingMethod", newName: "SSShippingMethod");
            RenameTable(name: "dbo.CheckoutAttribute", newName: "SSCheckoutAttribute");
            RenameTable(name: "dbo.CheckoutAttributeValue", newName: "SSCheckoutAttributeValue");
            RenameTable(name: "dbo.RecurringPaymentHistory", newName: "SSRecurringPaymentHistory");
            RenameTable(name: "dbo.RecurringPayment", newName: "SSRecurringPayment");
            RenameTable(name: "dbo.ActivityLog", newName: "SSActivityLog");
            RenameTable(name: "dbo.ActivityLogType", newName: "SSActivityLogType");
            RenameTable(name: "dbo.Log", newName: "SSLog");
            RenameTable(name: "dbo.Affiliate", newName: "SSAffiliate");
            RenameTable(name: "dbo.AclRecord", newName: "SSAclRecord");
            RenameTable(name: "dbo.UrlRecord", newName: "SSUrlRecord");
            RenameTable(name: "dbo.GenericAttribute", newName: "SSGenericAttribute");
            RenameTable(name: "dbo.ThemeVariable", newName: "SSThemeVariable");
            RenameTable(name: "dbo.Store", newName: "SSStore");
            RenameTable(name: "dbo.StoreMapping", newName: "SSStoreMapping");
            RenameTable(name: "dbo.ScheduleTaskHistory", newName: "SSScheduleTaskHistory");
            RenameTable(name: "dbo.ScheduleTask", newName: "SSScheduleTask");
            RenameTable(name: "dbo.QueuedEmailAttachment", newName: "SSQueuedEmailAttachment");
            RenameTable(name: "dbo.QueuedEmail", newName: "SSQueuedEmail");
            RenameTable(name: "dbo.EmailAccount", newName: "SSEmailAccount");
            RenameTable(name: "dbo.Campaign", newName: "SSCampaign");
            RenameTable(name: "dbo.MessageTemplate", newName: "SSMessageTemplate");
            RenameTable(name: "dbo.NewsLetterSubscription", newName: "SSNewsLetterSubscription");
            RenameTable(name: "dbo.Forums_Subscription", newName: "SSForums_Subscription");
            RenameTable(name: "dbo.Forums_PrivateMessage", newName: "SSForums_PrivateMessage");
            RenameTable(name: "dbo.MeasureWeight", newName: "SSMeasureWeight");
            RenameTable(name: "dbo.MeasureDimension", newName: "SSMeasureDimension");
            RenameTable(name: "dbo.ImportProfile", newName: "SSImportProfile");
            RenameTable(name: "dbo.SyncMapping", newName: "SSSyncMapping");
            RenameTable(name: "dbo.ExportDeployment", newName: "SSExportDeployment");
            RenameTable(name: "dbo.ExportProfile", newName: "SSExportProfile");
            RenameTable(name: "dbo.BackInStockSubscription", newName: "SSBackInStockSubscription");
            RenameTable(name: "dbo.ManufacturerTemplate", newName: "SSManufacturerTemplate");
            RenameTable(name: "dbo.CategoryTemplate", newName: "SSCategoryTemplate");
            RenameTable(name: "dbo.ProductTemplate", newName: "SSProductTemplate");
            RenameTable(name: "dbo.RelatedProduct", newName: "SSRelatedProduct");
            RenameTable(name: "dbo.CrossSellProduct", newName: "SSCrossSellProduct");
            RenameTable(name: "dbo.MenuRecord", newName: "SSMenuRecord");
            RenameTable(name: "dbo.MenuItemRecord", newName: "SSMenuItemRecord");
            RenameTable(name: "dbo.RuleSet_CustomerRole_Mapping", newName: "SSRuleSet_CustomerRole_Mapping");
            RenameTable(name: "dbo.RuleSet_Category_Mapping", newName: "SSRuleSet_Category_Mapping");
            RenameTable(name: "dbo.RuleSet_Discount_Mapping", newName: "SSRuleSet_Discount_Mapping");
            RenameTable(name: "dbo.RuleSet_PaymentMethod_Mapping", newName: "SSRuleSet_PaymentMethod_Mapping");
            RenameTable(name: "dbo.RuleSet_ShippingMethod_Mapping", newName: "SSRuleSet_ShippingMethod_Mapping");
            RenameTable(name: "dbo.Discount_AppliedToCategories", newName: "SSDiscount_AppliedToCategories");
            RenameTable(name: "dbo.Discount_AppliedToManufacturers", newName: "SSDiscount_AppliedToManufacturers");
            RenameTable(name: "dbo.Discount_AppliedToProducts", newName: "SSDiscount_AppliedToProducts");
            RenameTable(name: "dbo.MediaFile_Tag_Mapping", newName: "SSMediaFile_Tag_Mapping");
            RenameTable(name: "dbo.Product_ProductTag_Mapping", newName: "SSProduct_ProductTag_Mapping");
            RenameTable(name: "dbo.CustomerAddresses", newName: "SSCustomerAddresses");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.SSCustomerAddresses", newName: "CustomerAddresses");
            RenameTable(name: "dbo.SSProduct_ProductTag_Mapping", newName: "Product_ProductTag_Mapping");
            RenameTable(name: "dbo.SSMediaFile_Tag_Mapping", newName: "MediaFile_Tag_Mapping");
            RenameTable(name: "dbo.SSDiscount_AppliedToProducts", newName: "Discount_AppliedToProducts");
            RenameTable(name: "dbo.SSDiscount_AppliedToManufacturers", newName: "Discount_AppliedToManufacturers");
            RenameTable(name: "dbo.SSDiscount_AppliedToCategories", newName: "Discount_AppliedToCategories");
            RenameTable(name: "dbo.SSRuleSet_ShippingMethod_Mapping", newName: "RuleSet_ShippingMethod_Mapping");
            RenameTable(name: "dbo.SSRuleSet_PaymentMethod_Mapping", newName: "RuleSet_PaymentMethod_Mapping");
            RenameTable(name: "dbo.SSRuleSet_Discount_Mapping", newName: "RuleSet_Discount_Mapping");
            RenameTable(name: "dbo.SSRuleSet_Category_Mapping", newName: "RuleSet_Category_Mapping");
            RenameTable(name: "dbo.SSRuleSet_CustomerRole_Mapping", newName: "RuleSet_CustomerRole_Mapping");
            RenameTable(name: "dbo.SSMenuItemRecord", newName: "MenuItemRecord");
            RenameTable(name: "dbo.SSMenuRecord", newName: "MenuRecord");
            RenameTable(name: "dbo.SSCrossSellProduct", newName: "CrossSellProduct");
            RenameTable(name: "dbo.SSRelatedProduct", newName: "RelatedProduct");
            RenameTable(name: "dbo.SSProductTemplate", newName: "ProductTemplate");
            RenameTable(name: "dbo.SSCategoryTemplate", newName: "CategoryTemplate");
            RenameTable(name: "dbo.SSManufacturerTemplate", newName: "ManufacturerTemplate");
            RenameTable(name: "dbo.SSBackInStockSubscription", newName: "BackInStockSubscription");
            RenameTable(name: "dbo.SSExportProfile", newName: "ExportProfile");
            RenameTable(name: "dbo.SSExportDeployment", newName: "ExportDeployment");
            RenameTable(name: "dbo.SSSyncMapping", newName: "SyncMapping");
            RenameTable(name: "dbo.SSImportProfile", newName: "ImportProfile");
            RenameTable(name: "dbo.SSMeasureDimension", newName: "MeasureDimension");
            RenameTable(name: "dbo.SSMeasureWeight", newName: "MeasureWeight");
            RenameTable(name: "dbo.SSForums_PrivateMessage", newName: "Forums_PrivateMessage");
            RenameTable(name: "dbo.SSForums_Subscription", newName: "Forums_Subscription");
            RenameTable(name: "dbo.SSNewsLetterSubscription", newName: "NewsLetterSubscription");
            RenameTable(name: "dbo.SSMessageTemplate", newName: "MessageTemplate");
            RenameTable(name: "dbo.SSCampaign", newName: "Campaign");
            RenameTable(name: "dbo.SSEmailAccount", newName: "EmailAccount");
            RenameTable(name: "dbo.SSQueuedEmail", newName: "QueuedEmail");
            RenameTable(name: "dbo.SSQueuedEmailAttachment", newName: "QueuedEmailAttachment");
            RenameTable(name: "dbo.SSScheduleTask", newName: "ScheduleTask");
            RenameTable(name: "dbo.SSScheduleTaskHistory", newName: "ScheduleTaskHistory");
            RenameTable(name: "dbo.SSStoreMapping", newName: "StoreMapping");
            RenameTable(name: "dbo.SSStore", newName: "Store");
            RenameTable(name: "dbo.SSThemeVariable", newName: "ThemeVariable");
            RenameTable(name: "dbo.SSGenericAttribute", newName: "GenericAttribute");
            RenameTable(name: "dbo.SSUrlRecord", newName: "UrlRecord");
            RenameTable(name: "dbo.SSAclRecord", newName: "AclRecord");
            RenameTable(name: "dbo.SSAffiliate", newName: "Affiliate");
            RenameTable(name: "dbo.SSLog", newName: "Log");
            RenameTable(name: "dbo.SSActivityLogType", newName: "ActivityLogType");
            RenameTable(name: "dbo.SSActivityLog", newName: "ActivityLog");
            RenameTable(name: "dbo.SSRecurringPayment", newName: "RecurringPayment");
            RenameTable(name: "dbo.SSRecurringPaymentHistory", newName: "RecurringPaymentHistory");
            RenameTable(name: "dbo.SSCheckoutAttributeValue", newName: "CheckoutAttributeValue");
            RenameTable(name: "dbo.SSCheckoutAttribute", newName: "CheckoutAttribute");
            RenameTable(name: "dbo.SSShippingMethod", newName: "ShippingMethod");
            RenameTable(name: "dbo.SSRule", newName: "Rule");
            RenameTable(name: "dbo.SSPaymentMethod", newName: "PaymentMethod");
            RenameTable(name: "dbo.SSMediaTrack", newName: "MediaTrack");
            RenameTable(name: "dbo.SSMediaTag", newName: "MediaTag");
            RenameTable(name: "dbo.SSTierPrice", newName: "TierPrice");
            RenameTable(name: "dbo.SSDownload", newName: "Download");
            RenameTable(name: "dbo.SSProductVariantAttributeValue", newName: "ProductVariantAttributeValue");
            RenameTable(name: "dbo.SSProductAttributeOption", newName: "ProductAttributeOption");
            RenameTable(name: "dbo.SSProductAttributeOptionsSet", newName: "ProductAttributeOptionsSet");
            RenameTable(name: "dbo.SSProductAttribute", newName: "ProductAttribute");
            RenameTable(name: "dbo.SSProduct_ProductAttribute_Mapping", newName: "Product_ProductAttribute_Mapping");
            RenameTable(name: "dbo.SSQuantityUnit", newName: "QuantityUnit");
            RenameTable(name: "dbo.SSProductVariantAttributeCombination", newName: "ProductVariantAttributeCombination");
            RenameTable(name: "dbo.SSProductTag", newName: "ProductTag");
            RenameTable(name: "dbo.SSSpecificationAttribute", newName: "SpecificationAttribute");
            RenameTable(name: "dbo.SSSpecificationAttributeOption", newName: "SpecificationAttributeOption");
            RenameTable(name: "dbo.SSProduct_SpecificationAttribute_Mapping", newName: "Product_SpecificationAttribute_Mapping");
            RenameTable(name: "dbo.SSShoppingCartItem", newName: "ShoppingCartItem");
            RenameTable(name: "dbo.SSReturnRequest", newName: "ReturnRequest");
            RenameTable(name: "dbo.SSWalletHistory", newName: "WalletHistory");
            RenameTable(name: "dbo.SSShipmentItem", newName: "ShipmentItem");
            RenameTable(name: "dbo.SSShipment", newName: "Shipment");
            RenameTable(name: "dbo.SSRewardPointsHistory", newName: "RewardPointsHistory");
            RenameTable(name: "dbo.SSOrderNote", newName: "OrderNote");
            RenameTable(name: "dbo.SSOrderItem", newName: "OrderItem");
            RenameTable(name: "dbo.SSGiftCard", newName: "GiftCard");
            RenameTable(name: "dbo.SSGiftCardUsageHistory", newName: "GiftCardUsageHistory");
            RenameTable(name: "dbo.SSDiscountUsageHistory", newName: "DiscountUsageHistory");
            RenameTable(name: "dbo.SSOrder", newName: "Order");
            RenameTable(name: "dbo.SSExternalAuthenticationRecord", newName: "ExternalAuthenticationRecord");
            RenameTable(name: "dbo.SSCustomerRoleMapping", newName: "CustomerRoleMapping");
            RenameTable(name: "dbo.SSPoll", newName: "Poll");
            RenameTable(name: "dbo.SSPollAnswer", newName: "PollAnswer");
            RenameTable(name: "dbo.SSNews", newName: "News");
            RenameTable(name: "dbo.SSForums_Group", newName: "Forums_Group");
            RenameTable(name: "dbo.SSForums_Forum", newName: "Forums_Forum");
            RenameTable(name: "dbo.SSForums_Topic", newName: "Forums_Topic");
            RenameTable(name: "dbo.SSForums_Post", newName: "Forums_Post");
            RenameTable(name: "dbo.SSBlogPost", newName: "BlogPost");
            RenameTable(name: "dbo.SSAddress", newName: "Address");
            RenameTable(name: "dbo.SSCustomer", newName: "Customer");
            RenameTable(name: "dbo.SSPollVotingRecord", newName: "PollVotingRecord");
            RenameTable(name: "dbo.SSNewsComment", newName: "NewsComment");
            RenameTable(name: "dbo.SSForumPostVote", newName: "ForumPostVote");
            RenameTable(name: "dbo.SSProductReviewHelpfulness", newName: "ProductReviewHelpfulness");
            RenameTable(name: "dbo.SSBlogComment", newName: "BlogComment");
            RenameTable(name: "dbo.SSCustomerContent", newName: "CustomerContent");
            RenameTable(name: "dbo.SSProductReview", newName: "ProductReview");
            RenameTable(name: "dbo.SSProduct_Manufacturer_Mapping", newName: "Product_Manufacturer_Mapping");
            RenameTable(name: "dbo.SSProduct_Category_Mapping", newName: "Product_Category_Mapping");
            RenameTable(name: "dbo.SSProductBundleItemAttributeFilter", newName: "ProductBundleItemAttributeFilter");
            RenameTable(name: "dbo.SSProductBundleItem", newName: "ProductBundleItem");
            RenameTable(name: "dbo.SSDeliveryTime", newName: "DeliveryTime");
            RenameTable(name: "dbo.SSStateProvince", newName: "StateProvince");
            RenameTable(name: "dbo.SSCurrency", newName: "Currency");
            RenameTable(name: "dbo.SSCountry", newName: "Country");
            RenameTable(name: "dbo.SSProduct", newName: "Product");
            RenameTable(name: "dbo.SSProduct_MediaFile_Mapping", newName: "Product_MediaFile_Mapping");
            RenameTable(name: "dbo.SSMediaStorage", newName: "MediaStorage");
            RenameTable(name: "dbo.SSMediaFolder", newName: "MediaFolder");
            RenameTable(name: "dbo.SSMediaFile", newName: "MediaFile");
            RenameTable(name: "dbo.SSManufacturer", newName: "Manufacturer");
            RenameTable(name: "dbo.SSDiscount", newName: "Discount");
            RenameTable(name: "dbo.SSCategory", newName: "Category");
            RenameTable(name: "dbo.SSRuleSet", newName: "RuleSet");
            RenameTable(name: "dbo.SSCustomerRole", newName: "CustomerRole");
            RenameTable(name: "dbo.SSPermissionRoleMapping", newName: "PermissionRoleMapping");
            RenameTable(name: "dbo.SSPermissionRecord", newName: "PermissionRecord");
            RenameTable(name: "dbo.SSLocaleStringResource", newName: "LocaleStringResource");
            RenameTable(name: "dbo.SSLanguage", newName: "Language");
            RenameTable(name: "dbo.SSLocalizedProperty", newName: "LocalizedProperty");
            RenameTable(name: "dbo.SSSetting", newName: "Setting");
            RenameTable(name: "dbo.SSTaxCategory", newName: "TaxCategory");
            RenameTable(name: "dbo.SSTopic", newName: "Topic");
        }
    }
}
