(function ($) {

    $.fn.greenify = function (options) {

        // This is the easiest way to have default options.
        var settings = $.extend({
            // These are the defaults.
            color: "#556b2f",
            backgroundColor: "white"
        }, options);


        // Greenify the collection based on the settings variable.
        return this.css({
            color: settings.color,
            backgroundColor: settings.backgroundColor
        });

    };

    $.fn.popup = function (action) {

        if (action === "open") {
            // Open popup code.
            alert(action);
        }

        if (action === "close") {
            // Close popup code.
            alert(action);
        }

    };

    $.fn.showLinkLocation = function () {

        this.filter("a").each(function () {
            var link = $(this);
            link.append(" (" + link.attr("href") + ")");
        });

        return this;

    };

}(jQuery));
(function () {
    $(function () {
    });
})();