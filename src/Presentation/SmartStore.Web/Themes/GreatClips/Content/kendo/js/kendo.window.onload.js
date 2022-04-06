function changePopupTab(el) {
    if($(el).closest("li").hasClass("active"))
    {
        // do nothing
    }
    else
    {
        // hide the old tab
        var currentActiveTab = $(el).closest("ul").find(".active");
        if (typeof currentActiveTab != typeof undefined)
        {
            var target = $(currentActiveTab).children("a").data("target");
            if (typeof target != typeof undefined)
            {
                if($(target).length > 0)
                {
                    $(target).removeClass("active");
                    currentActiveTab.removeClass("active");
                }
            }
        }

        // show the new tab
        var newTarget = $(el).data("target");
        if (typeof newTarget != typeof undefined) {
            if ($(newTarget).length > 0) {
                $(newTarget).addClass("active");
                $(el).closest("li").addClass("active");

                //var Event = createCustomEvent("onChangeTab", $(newTarget));
                $(el).closest("li").trigger("tabChanged");
            }
        }
    }
}

function reInitiateWindow()
{
    var kendoWindow = $(".gc-window");

    if (kendoWindow.length > 0) {
        kendoWindow.closest(".k-window").addClass("gc-window-wrapper");
    }
}