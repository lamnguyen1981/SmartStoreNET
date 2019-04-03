﻿using System.Collections.Generic;
using System.Linq;
using SmartStore.Collections;
using SmartStore.Core.Domain.Catalog;
using SmartStore.Core.Domain.Cms;
using SmartStore.Services;
using SmartStore.Services.Catalog;
using SmartStore.Services.Localization;
using SmartStore.Services.Media;
using SmartStore.Services.Seo;

namespace SmartStore.Web.Framework.UI
{
    [MenuItemProvider("catalog", AppendsMultipleItems = true)]
	public class CatalogMenuItemProvider : MenuItemProviderBase
	{
        private readonly ICommonServices _services;
        private readonly ICategoryService _categoryService;
        private readonly IPictureService _pictureService;

        public CatalogMenuItemProvider(
            ICommonServices services,
            ICategoryService categoryService,
            IPictureService pictureService)
        {
            _services = services;
            _categoryService = categoryService;
            _pictureService = pictureService;
        }

        public override void Append(MenuItemProviderRequest request)
		{
            if (request.Origin.IsCaseInsensitiveEqual("EditMenu"))
            {
                base.Append(request);
            }
            else
            {
                // INFO: Replaces CatalogSiteMap to a large extent and appends 
                // all catalog nodes (without root) to the passed parent node.

                var tree = _categoryService.GetCategoryTree(0, false, _services.StoreContext.CurrentStore.Id);
                var allPictureIds = tree.Flatten().Select(x => x.PictureId.GetValueOrDefault());
                var allPictureInfos = _pictureService.GetPictureInfos(allPictureIds);

                if (request.Entity.BeginGroup)
                {
                    request.Parent.Append(new MenuItem
                    {
                        IsGroupHeader = true,
                        Text = request.Entity.ShortDescription
                    });
                }

                // Do not append the root itself.
                foreach (var child in tree.Children)
                {
                    var node = ConvertNode(child, request.Entity, allPictureInfos);
                    request.Parent.Append(node);
                }
            }

			// TBD: Cache invalidation workflow changes, because the catalog tree 
			// is now contained within other menus. Invalidating the tree now means:
			// invalidate all containing menus also.
		}

		protected override void ApplyLink(MenuItemProviderRequest request, TreeNode<MenuItem> node)
		{
			// Void, does nothing here.
		}

        private TreeNode<MenuItem> ConvertNode(TreeNode<ICategoryNode> node, MenuItemRecord entity, IDictionary<int, PictureInfo> allPictureInfos)
        {
            var cat = node.Value;
            var name = cat.Id > 0 ? cat.GetLocalized(x => x.Name) : null;

            var menuItem = new MenuItem
            {
                EntityId = cat.Id,
                Text = name?.Value ?? cat.Name,
                Rtl = name?.CurrentLanguage?.Rtl ?? false,
                BadgeText = cat.Id > 0 ? cat.GetLocalized(x => x.BadgeText) : null,
                BadgeStyle = (BadgeStyle)cat.BadgeStyle,
                RouteName = cat.Id > 0 ? "Category" : "HomePage"
            };

            if (cat.Id > 0)
            {
                menuItem.RouteValues.Add("SeName", cat.GetSeName());

                if (cat.ParentCategoryId == 0 && cat.Published && cat.PictureId != null)
                {
                    menuItem.ImageId = cat.PictureId;
                }
            }

            // Apply inheritable properties.
            menuItem.Visible = entity.Published;
            menuItem.PermissionNames = entity.PermissionNames;

            if (entity.NoFollow)
            {
                menuItem.LinkHtmlAttributes.Add("rel", "nofollow");
            }

            if (entity.NewWindow)
            {
                menuItem.LinkHtmlAttributes.Add("target", "_blank");
            }

            var convertedNode = new TreeNode<MenuItem>(menuItem)
            {
                Id = node.Id
            };

            if (node.HasChildren)
            {
                foreach (var childNode in node.Children)
                {
                    convertedNode.Append(ConvertNode(childNode, entity, allPictureInfos));
                }
            }

            return convertedNode;
        }
    }
}
