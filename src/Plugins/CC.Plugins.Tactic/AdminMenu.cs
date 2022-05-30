using SmartStore.Collections;
using SmartStore.Web.Framework.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CC.Plugins.Tactic
{
    public class AdminMenu : AdminMenuProvider
    {
        protected override void BuildMenuCore(TreeNode<MenuItem> pluginsNode)
        {
            var customerNode = pluginsNode.Parent.SelectNode(x => x.Value.Id == "catalog");
            var menuItem = new MenuItem().ToBuilder()
                .Text("Tactic")
                .ResKey("Plugins.FriendlyName.CC.Plugins.Tactic")
                .Icon("fa fa-map-marker")
                .Action("Index", "Tactic", new { area = "CC.Plugins.Tactic" })
                .ToItem();

            customerNode.Prepend(menuItem);
        }

        public override int Ordinal => 100;
    }
}