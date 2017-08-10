$(document).ready(function () {
    var sectionsColor = ['#1bbc9b', '#4BBFC3', '#7BAABE', 'whitesmoke', '#ccddff', '#1b5c9b', '#188c9b'];
    var anchors = ['each', 'each-nest', 'each-this', 'html-encode', 'if', 'index', 'with'];

    var menu = anchors;
    var menuTemplate = Handlebars.compile($("#menu-template").html());
    $('#menu').html(menuTemplate(menu));

    var fullpage =
        [
            { classvalue: "section active", h1value: "each", srcvalue: "Usage-each.html" },
            { classvalue: "section", h1value: "each-nest", srcvalue: "Usage-each-nest.html" },
            { classvalue: "section", h1value: "each-this", srcvalue: "Usage-each-this.html" },
            { classvalue: "section", h1value: "html-encode", srcvalue: "Usage-html-encode.html" },
            { classvalue: "section", h1value: "if", srcvalue: "Usage-if.html" },
            { classvalue: "section", h1value: "index", srcvalue: "Usage-index.html" },
            { classvalue: "section", h1value: "with", srcvalue: "Usage-with.html" }
        ];
    var fullpageTemplate = Handlebars.compile($("#fullpage-template").html());
    $('#fullpage').html(fullpageTemplate(fullpage));

    $('#fullpage').fullpage({
        sectionsColor: sectionsColor,
        anchors: anchors,
        menu: '#menu',
        scrollingSpeed: 1000
    });
});