//Make sure jQuery & D3 has been loaded before s-grantt
if (typeof jQuery === "undefined") {
    throw new Error("s-grantt requires jQuery");
}

if (typeof d3 === "undefined") {
    throw new Error("s-grantt requires D3");
}

$.SGrantt = {};

$(function () {
    "use strict";
    $.SGrantt.MakeGant = function (jQueryCnt, granttData, colorArray) {

        var $container = jQueryCnt,
            w = $container.width(),
            h = $container.height(),
            dateFormat = d3.time.format("%H:%M"),
            categories = ["状态", "原因"];

        var svg = d3.select($container.get(0))
                    .append("svg")
                    .attr("width", '100%')
                    .attr("height", '100%')
                    .attr('viewBox', '0 0 ' + Math.min(w, h) + ' ' + Math.min(w, h))
                    .attr('preserveAspectRatio', 'xMinYMin')
                    .append("g");


        var timeScale = d3.time.scale()
                        .domain([dateFormat.parse("00:00"), dateFormat.parse("24:00")])
                        .range([0, w - 150]);

        makeGant(granttData, w, h,$container);

        function makeGant(tasks, pageWidth, pageHeight,jQueryCnt) {

            var barHeight = 15;
            var gap = barHeight + 10;
            var topPadding = 10;
            var sidePadding = 75;
            var barTopPadding = 3;

            var colorScale = ["#001f3f","#605ca8"];

            makeGrid(sidePadding, topPadding, pageWidth, pageHeight);
            drawRects(tasks, gap, topPadding, sidePadding, barHeight, colorScale, pageWidth, pageHeight, barTopPadding,jQueryCnt);
            vertLabels(gap, topPadding, sidePadding, barHeight, colorScale);
        }


        function drawRects(theArray, theGap, theTopPad, theSidePad, theBarHeight, theColorScale, w, h, theBarTopPadding,jQueryCnt) {

            var bigRects = svg.append("g")
                .selectAll("rect")
                .data(theArray)
                .enter()
                .append("rect")
                .attr("x", 0)
                .attr("y", function (d, i) {
                    for (var i = 0; i < categories.length; i++) {
                        if (d.type == categories[i]) {
                            return i * theGap + theTopPad - 2;
                        }
                    }
                })
                .attr("width", function (d) {
                    return w - theSidePad / 2;
                })
                .attr("height", theGap)
                .attr("stroke", "none")
                .attr("fill", function (d) {
                    for (var i = 0; i < categories.length; i++) {
                        if (d.type == categories[i]) {
                            return d3.rgb(theColorScale[i]);
                        }
                    }
                })
                .attr("opacity", 0.2);

            var rectangles = svg.append('g')
                .selectAll("rect")
                .data(theArray)
                .enter();

            var innerRects = rectangles.append("rect")
                .attr("rx", 3)
                .attr("ry", 3)
                .attr("x", function (d) {
                    return timeScale(dateFormat.parse(d.startTime)) + theSidePad;
                })
                .attr("y", function (d, i) {
                    for (var i = 0; i < categories.length; i++) {
                        if (d.type == categories[i]) {
                            return i * theGap + theTopPad + theBarTopPadding;
                        }
                    }
                })
                .attr("width", function (d) {
                    return (timeScale(dateFormat.parse(d.endTime)) - timeScale(dateFormat.parse(d.startTime)));
                })
                .attr("height", theBarHeight)
                .attr("stroke", "none")
                .attr("fill", function (d) {
                    for (var i = 0; i < colorArray.length; i++) {
                        if (d.statusCodeNo == colorArray[i].code) {
                            return d3.rgb(colorArray[i].color);
                        }
                    }
                })

            innerRects.on('mouseover', function (e) {
                //console.log(this);
                var targetData = d3.select(this).data()[0];
                var tag = "状态: " + targetData.status + "<br/>" +
                    "开始时间: " + targetData.startTime + "<br/>" +
                    "结束时间: " + targetData.endTime;

                if (targetData.operator != undefined) {
                    tag += "<br/>操作人员: " + targetData.operator;
                }

                if (targetData.details != undefined) {
                    tag += "<br/>备注: " + targetData.details;
                }

                var output = jQueryCnt.parent().find(".tag").get(0);

                var x = (this.x.animVal.value + this.width.animVal.value / 2) + "px";
                var y = this.y.animVal.value + 25 + "px";

                output.innerHTML = tag;
                output.style.top = y;
                output.style.left = x;
                output.style.display = "block";
            }).on('mouseout', function () {
                var output = jQueryCnt.parent().find(".tag").get(0);
                output.style.display = "none";

            });
        }


        function makeGrid(theSidePad, theTopPad, w, h) {
            var firstXTime = true;
            var xAxis = d3.svg.axis()
                .scale(timeScale)
                .orient('bottom')
                .ticks(d3.time.hours, 4)
                .tickSize(-h + theTopPad + 20, 0, 0)
                .tickFormat(function (d) {
                    if (d.getHours() === 0 && !firstXTime) {
                        return "24:00";
                    }
                    else {
                        firstXTime = false;
                        return d3.time.format("%H:%M")(d);
                    }
                });

            var grid = svg.append('g')
                .attr('class', 'grid')
                .attr('transform', 'translate(' + theSidePad + ', ' + (h - 50) + ')')
                .call(xAxis)
                .selectAll("text")
                .style("text-anchor", "middle")
                .attr("fill", "#000")
                .attr("stroke", "none")
                .attr("font-size", 10)
                .attr("dy", "1em");
        }

        function vertLabels(theGap, theTopPad, theSidePad, theBarHeight, theColorScale) {
            var numOccurances = new Array();
            var prevGap = 0;

            for (var i = 0; i < categories.length; i++) {
                //numOccurances[i] = [categories[i], getCount(categories[i], catsUnfiltered)];
                numOccurances[i] = [categories[i], 1];
            }

            var axisText = svg.append("g") //without doing this, impossible to put grid lines behind text
                .selectAll("text")
                .data(numOccurances)
                .enter()
                .append("text")
                .text(function (d) {
                    return d[0];
                })
                .attr("x", 15)
                .attr("y", function (d, i) {
                    if (i > 0) {
                        for (var j = 0; j < i; j++) {
                            prevGap += numOccurances[i - 1][1];
                            // console.log(prevGap);
                            return d[1] * theGap / 2 + prevGap * theGap + theTopPad + 2;
                        }
                    } else {
                        return d[1] * theGap / 2 + theTopPad + 2;
                    }
                })
                .attr("text-anchor", "start")
                .attr("text-height", 15)
                .attr("class", "v-lable")
                .attr("fill", function (d) {
                    for (var i = 0; i < categories.length; i++) {
                        if (d[0] == categories[i]) {
                            //  console.log("true!");
                            return d3.rgb('#fff');
                        }
                    }
                });
        }

        function checkUnique(arr) {
            var hash = {}, result = [];
            for (var i = 0, l = arr.length; i < l; ++i) {
                if (!hash.hasOwnProperty(arr[i])) { //it works with objects! in FF, at least
                    hash[arr[i]] = true;
                    result.push(arr[i]);
                }
            }
            return result;
        }

        function getCounts(arr) {
            var i = arr.length, // var to loop over
                obj = {}; // obj to store results
            while (i) obj[arr[--i]] = (obj[arr[i]] || 0) + 1; // count occurrences
            return obj;
        }

        function getCount(word, arr) {
            return getCounts(arr)[word] || 0;
        }
    }

} (jQuery));

