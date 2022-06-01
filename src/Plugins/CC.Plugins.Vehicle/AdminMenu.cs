using SmartStore.Collections;
using SmartStore.Web.Framework.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CC.Plugins.Vehicle
{
    public class AdminMenu : AdminMenuProvider
    {
        protected override void BuildMenuCore(TreeNode<MenuItem> pluginsNode)
        {
            var customerNode = pluginsNode.Parent.SelectNode(x => x.Value.Id == "catalog");
            var menuItem = new MenuItem().ToBuilder()
                .Text("Vehicle")
                .ResKey("Plugins.FriendlyName.CC.Plugins.Vehicle")
                .Icon("fa fa-map-marker")
                .Action("Index", "Vehicle", new { area = "CC.Plugins.Vehicle" })
                .ToItem();

            customerNode.Prepend(menuItem);
        }

        public override int Ordinal => 100;
    }
}