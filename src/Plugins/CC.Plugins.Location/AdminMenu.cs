using SmartStore.Collections;
using SmartStore.Web.Framework.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CC.Plugins.Location
{
    public class AdminMenu : AdminMenuProvider
    {
        protected override void BuildMenuCore(TreeNode<MenuItem> pluginsNode)
        {
            var customerNode = pluginsNode.Parent.SelectNode(x => x.Value.Id == "catalog");
            var menuItem = new MenuItem().ToBuilder()
                .Text("Location")
                .ResKey("Plugins.FriendlyName.CC.Plugins.Location")
                .Icon("fa fa-map-marker")
                .Action("LoadLocations", "Location", new { area = "CC.Plugins.Location" })
                .ToItem();

            customerNode.Prepend(menuItem);
        }

        public override int Ordinal => 100;
    }
}