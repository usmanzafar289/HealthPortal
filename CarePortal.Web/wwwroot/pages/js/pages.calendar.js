(function (factory) {
    if (typeof define === 'function' && define.amd) {
        define(['jquery', 'moment'], factory);
    } else if (typeof exports === 'object') {
        module.exports = factory(require('jquery'), require('moment'));
    } else {
        factory(jQuery, moment);
    }
})(function ($, moment) {
    var PagesCalendar = function (element, options) {
        this.$element = $(element);
        this.$html = '<div class="calendar">'
        this.$html += '<!-- START CALENDAR HEADER-->'
        this.$html += '<div class="calendar-header">'
        this.$html += '<div class="drager">'
        this.$html += '<div class="years" id="years"></div>'
        this.$html += '</div>'
        this.$html += '</div>'
        this.$html += '<div class="options">'
        this.$html += '<div class="months-drager drager">'
        this.$html += '<div class="months" id="months"></div>'
        this.$html += '</div>'
        this.$html += '<h4 class="semi-bold date" id="currentDate">&amp;</h4>'
        this.$html += '<div class="drager week-dragger">'
        this.$html += '<div class="weeks-wrapper" id="weeks-wrapper">'
        this.$html += ' </div>'
        this.$html += '</div>'
        this.$html += '</div>'
        this.$html += '<!-- START CALENDAR GRID-->'
        this.$html += '<div id="calendar" class="calendar-container">'
        this.$html += '</div>'
        this.$html += '<!-- END CALENDAR GRID-->'
        this.$html += '</div>'
        this.$element.append(this.$html);
        var plugin = this;
        plugin.settings = $.extend(true, {}, $.fn.pagescalendar.defaults, options);
        var EventManager = EventManager || {};
        var EventManager = {
            getEventByID: function (id) {
                for (var i = 0; i < this.events.length; i++) {
                    if (Calendar.settings.events[i]._id == id) {
                        return setting.events[i];
                        break;
                    }
                }
                return null;
            },
            getEventsByDate: function (date) {
                date = moment(date).format("YYYY-MM-DD");
                for (var i = 0; i < this.events.length; i++) {
                    var eventStartDate = moment(Calendar.settings.events[i].start).format("YYYY-MM-DD");
                    if (date.localeCompare(eventStartDate) == 0) {
                        return setting.events[i];
                        break;
                    }
                }
                return null;
            },
            getAllEvents: function () {
                return this.events;
            },
            addEvent: function () {
                this.events.push(event);
            },
            lazyFetch: function () { },
            byDateRange: function (startDate, endDate) {
                var start = moment(startDate).format("YYYY-MM-DD");
                var end = moment(endDate).add(24, 'hours').format("YYYY-MM-DD");
                var range = moment.range(start, end);
                var disableDatesInRange = [];
                for (var i = 0; i < Calendar.settings.disableDates.length; i++) {
                    var when = moment(Calendar.settings.disableDates[i]);
                    if (when.within(range)) {
                        disableDatesInRange.push(Calendar.settings.disableDates[i]);
                    }
                }
                var eventIndex = [];
                for (var i = 0; i < Calendar.settings.events.length; i++) {
                    var when = moment(Calendar.settings.events[i].start);
                    if (when.within(range)) {
                        var disable = false;
                        for (var j = 0; j < disableDatesInRange.length; j++) {
                            var s = moment(disableDatesInRange[j]).format("YYYY-MM-DD");
                            var r = moment.range(s, moment(s).add(24, "hours"));
                            if (when.within(r)) {
                                disable = true;
                                break;
                            }
                        }
                        if (!disable)
                            eventIndex.push(i);
                    }
                }
                return eventIndex;
            },
            hasEventByDate: function (date, format) {
                date = moment(date).format(format);
                for (var i = 0; i < Calendar.settings.events.length; i++) {
                    var event = moment(Calendar.settings.events.start).format(format);
                    if (date.localeCompare(event) == 0) {
                        return true;
                    }
                }
                return false;
            },
            eventCountByDate: function (date, format) {
                date = moment(date).format(format);
                var count = 0;
                for (var i = 0; i < Calendar.settings.events.length; i++) {
                    var event = moment(Calendar.settings.events.start).format(format);
                    if (date.localeCompare(event) == 0) {
                        count++;
                    }
                }
                return count;
            },
            getDisableDateIndexInDateRange: function (start, end, returnFormat) {
                start = moment(start).format("YYYY-MM-DD")
                end = moment(end).format("YYYY-MM-DD");
                var range = moment.range(start, end);
                var dateIndex = [];
                for (var i = 0; i < Calendar.settings.disableDates.length; i++) {
                    var when = moment(Calendar.settings.disableDates[i]);
                    if (when.within(range)) {
                        dateIndex.push(moment(Calendar.settings.disableDates[i]).format(returnFormat));
                    }
                }
                return dateIndex;
            },
            eventCountByDateRange: function (startDate, endDate, format) {
                var start = moment(startDate).format("YYYY-MM-DD");
                var end = moment(endDate).format("YYYY-MM-DD");
                var range = moment.range(start, end);
                var count = [];
                for (var i = 0; i < Calendar.settings.events.length; i++) {
                    var when = moment(Calendar.settings.events[i].start);
                    if (when.within(range)) {
                        count++;
                    }
                }
                return count;
            }
        }
        var yearSelector = (function () {
            function yearSelector(container) {
                this.container = container;
                this.render();
            }
            var _setActive = function () {
                var diff = calendar.year - Calendar.settings.ui.year.startYear
                $('.year a').removeClass('active');
                $('.year:nth-child(' + diff + ') > a').addClass('active')
            }
            var _bindEvents = function () {
                var $this = this;
                $(document).off("click", "body:not(.pending) .year-selector");
                $(document).on("click", "body:not(.pending) .year-selector", function (e) {
                    var year = $(this).attr('data-year');
                    calendar.year = moment(year, Calendar.settings.ui.year.format).year();
                    _setActive();
                    Calendar._onYearChange();
                });
            }
            yearSelector.prototype.render = function () {
                $(this.container).html("");
                Calendar.content = "";
                var diffYears = Calendar.settings.ui.year.endYear - Calendar.settings.ui.year.startYear
                diffYears = (diffYears > 90) ? 90 : diffYears;
                var yearInc = Calendar.settings.ui.year.startYear;
                for (var i = 1; i <= diffYears; i++) {
                    yearInc = moment(yearInc, Calendar.settings.ui.year.format).add(1, 'year').format(Calendar.settings.ui.year.format);
                    var activeClass = (calendar.year == yearInc) ? 'active' : '';
                    Calendar.content += '<div class="year">';
                    Calendar.content += '<a href="#" class="year-selector ' + activeClass + '" data-year=' + yearInc + '>' + yearInc + '</a>';
                    Calendar.content += '</div>';
                }
                $(this.container).append(Calendar.content);
                Calendar.dragHandler('years');
                _setActive();
                _bindEvents();
            }
            return yearSelector;
        })();
        var monthSelector = function (container) {
            this.container = container;
            this.render();
        }
        monthSelector.prototype = {
            render: function () {
                $(this.container).html("");
                Calendar.content = "";
                var months = moment.monthsShort();
                var currentMonth = moment([calendar.year, calendar.month, calendar.date]).format(Calendar.settings.ui.month.format);
                for (var i = 0; i < months.length; i++) {
                    var formatedMonth = moment(months[i], 'MMMM').format(Calendar.settings.ui.month.format);
                    var activeClass = currentMonth == formatedMonth ? 'active' : '';
                    Calendar.content += '<div class="month">';
                    Calendar.content += '<a href="#" class="month-selector ' + activeClass + '" data-month="' + formatedMonth + '">' + formatedMonth + '</a>';
                    Calendar.content += '</div>';
                }
                $(this.container).append(Calendar.content);
                Calendar.dragHandler('months');
                this._bindEvents();
            },
            _setActive: function () {
                $('.month a').removeClass('active');
                $('.month:nth-child(' + (parseInt(calendar.month) + 1) + ') > a').addClass('active');
            },
            _bindEvents: function () {
                var $this = this;
                $(document).off("click", "body:not(.pending) .month-selector")
                $(document).on("click", "body:not(.pending) .month-selector", function (e) {
                    var month = $(this).attr('data-month');
                    calendar.month = moment(month, Calendar.settings.ui.month.format).month();
                    Calendar.daysOfMonth = moment([calendar.year, calendar.month]).daysInMonth();
                    if (Calendar.daysOfMonth < calendar.date) {
                        calendar.date = Calendar.daysOfMonth;
                    }
                    Calendar._onMonthChange();
                    $this._setActive();
                });
            }
        }
        var dateSelector = function (container) {
            this.container = container;
            this.render();
            this._bindEvents();
        }
        dateSelector.prototype = {
            render: function () {
                $(this.container).html("");
                Calendar.daysOfMonth = moment([calendar.year, calendar.month]).daysInMonth();
                Calendar.content = "";
                var weekStart = parseInt(moment(Calendar.settings.ui.week.startOfTheWeek, 'd').format('d'));
                var weekEnd = parseInt(moment(Calendar.settings.ui.week.endOfTheWeek, 'd').format('d'));
                for (var i = 1; i <= Calendar.daysOfMonth; i++) {
                    var date = moment([calendar.year, calendar.month, i]);
                    var t = parseInt(moment([calendar.year, calendar.month, i]).format('d'));
                    var activeClass = (calendar.date == i) ? 'active current-date' : '';
                    (t == weekStart || i == 1) ? Calendar.content += '<div class="week ' + activeClass + '">' : '';
                    if (weekStart >= weekEnd) {
                        if (t >= weekStart || t == weekEnd) {
                            _construcWeekday()
                        }
                    } else {
                        if (t >= weekStart && t <= weekEnd) {
                            _construcWeekday();
                        }
                    }
                    (t == weekEnd) ? Calendar.content += '</div>' : '';

                    function _construcWeekday() {
                        Calendar.content += '<div class="day-wrapper date-selector">';
                        Calendar.content += '<div class="week-day">';
                        Calendar.content += '<div class="day week-header">' + date.format(Calendar.settings.ui.week.header.format) + '</div>';
                        Calendar.content += '</div>';
                        Calendar.content += '<div class="week-date ' + activeClass + '">';
                        Calendar.content += '<div class="day"><a href="#" data-date=' + date.format(Calendar.settings.ui.week.day.format) + '>' + i + '</a></div>';
                        Calendar.content += '</div>';
                        Calendar.content += '</div>';
                    }
                }
                Calendar.content += '</div>';
                $(this.container).append(Calendar.content);
                $('.weeks-wrapper .week .day-wrapper .week-date.active').closest(".week").addClass('active');
                Calendar.dragHandler('weeks-wrapper');
            },
            _setActive: function () {
                $('.week').removeClass('active');
                $(elem).closest('.week').addClass('active');
            },
            _bindEvents: function () {
                var $this = this;
                $(document).off("click", "body:not(.pending) .date-selector");
                $(document).on("click", "body:not(.pending) .date-selector", function (e) {
                    $(".week-date").removeClass('active')
                    $(this).children('.week-date').addClass('active');
                    calendar.date = parseInt($(this).children('.week-date').children('.day').children('a').attr('data-date'));
                    Calendar._onDayChange();
                });
            }
        }
        var gridFractory = function (layout) {
            this.grid = null;
            this.miniCalendar = null;
            this.layout = layout;
            this.build();
        }
        gridFractory.prototype.build = function () {
            switch (this.layout) {
                case "month":
                    this.grid = new MonthView(".calendar-container");
                    break;
                case "week":
                    this.miniCalendar = new dateSelector(".weeks-wrapper");
                    this.grid = new wView(".calendar-container", this.layout);
                    break;
                case "day":
                    this.miniCalendar = new dateSelector(".weeks-wrapper");
                    this.grid = new wView(".calendar-container", this.layout);
                    break;
            }
        }
        gridFractory.prototype.refresh = function () {
            return this.grid.refresh();
        }
        gridFractory.prototype.rebuild = function () {
            this.build();
        }
        gridFractory.prototype.reloadEvents = function () {
            this.grid.reloadEvents();
        }
        gridFractory.prototype.doubletap = function (obj) {
            return this.grid.doubletap(obj);
        }
        gridFractory.prototype.getCurrentDateRange = function () {
            return this.grid.getCurrentDateRange();
        }
        gridFractory.prototype.scrollToFirstEvent = function () {
            return this.grid.scrollToFirstEvent();
        }
        var view = function (container) {
            this.container = container;
        }
        var wView = function (container, layout) {
            this.layout = layout;
            view.call(this, container);
            this.render();
            this.snapGridWidth = null;
            this.snapGridHeight = null;
        }
        wView.prototype = Object.create(view.prototype);
        wView.prototype.render = function () {
            this.weekStart = parseInt(moment(Calendar.settings.ui.week.startOfTheWeek, 'd').format('d'));
            this.weekEnd = parseInt(moment(Calendar.settings.ui.week.endOfTheWeek, 'd').format('d'));
            this.startOfWeek = moment([calendar.year, calendar.month, calendar.date]).startOf('week');
            if (this.weekStart >= this.weekEnd)
                this.endOfWeek = moment([calendar.year, calendar.month, calendar.date]).startOf('week').add("days", 7 + parseInt(Calendar.settings.ui.week.endOfTheWeek));
            else
                this.endOfWeek = moment([calendar.year, calendar.month, calendar.date]).startOf('week').add("days", parseInt(Calendar.settings.ui.week.endOfTheWeek));
            this._buildLayout();
            this._timeslots();
            this._loadDates();
            this._bindEventDraggers();
            this._setActive();
            this.windowResize();
            if (Calendar.settings.ui.grid.scrollToFirstEvent)
                this.scrollToFirstEvent();
        }
        wView.prototype._buildLayout = function () {
            var headerContent = '<div class="thead" >';
            var weekend = this.weekEnd;
            if (this.weekStart >= this.weekEnd) {
                weekend = 7;
            }
            for (var j = this.weekStart; j <= weekend; j++) {
                headerContent += '<div class="tcell"></div>';
            }
            headerContent += '</div>';
            $(this.container).html("");
            Calendar.content = '';
            Calendar.content += '<div class="view ' + this.layout + '-view">';
            Calendar.content += '<div class="allday-cell">';
            Calendar.content += '</div>';
            Calendar.content += '<div class="tble" id="viewTableHead">';
            Calendar.content += headerContent
            Calendar.content += '</div>';
            Calendar.content += '<div class="grid slot-' + Calendar.settings.slotDuration + '">';
            Calendar.content += '<div class="time-slot-wrapper" id="time-slots">';
            Calendar.content += '</div>';
            Calendar.content += '<div class="tble" id="weekGrid">';
            var slot = parseInt(Calendar.settings.slotDuration);
            var slotCount = (60 / Math.round(slot)) - 1;
            for (var i = Calendar.settings.minTime; i < Calendar.settings.maxTime; i++) {
                Calendar.content += '<div class="trow" >';
                for (var j = this.weekStart; j <= weekend; j++) {
                    Calendar.content += '<div class="tcell">';
                    Calendar.content += '<div class="cell-inner" data-time-slot="' + i + ':00" ></div>';
                    var _slot = 0;
                    for (var s = 0; s < slotCount; s++) {
                        Calendar.content += '<div class="cell-inner" data-time-slot="' + i + ':' + (_slot = _slot + slot) + '" ></div>';
                    }
                    Calendar.content += '</div>';
                }
                Calendar.content += '</div>';
            }
            Calendar.content += '</div>';
            Calendar.content += '</div>';
            Calendar.content += '</div>';
            $(this.container).append(Calendar.content);
            Calendar.cellHeight = $('.tcell').innerHeight();
            calendar.startOfWeekDate = moment([calendar.year, calendar.month, calendar.date]).startOf('week');
            calendar.endOfWeek = moment([calendar.year, calendar.month, calendar.date]).endOf('week').format('D');
            this.maxMinsPerDay = moment(Calendar.settings.maxTime + ":00", ["H:mm"]).diff(moment(Calendar.settings.minTime + ":00", ["H:mm"]), "minutes");
            $(".calendar").removeClass("month").addClass("week");
        }
        wView.prototype._loadDates = function () {
            var column = 1;
            var weekend = this.weekEnd;
            if (this.weekStart >= this.weekEnd) {
                weekend = 7;
            }
            for (var i = this.weekStart; i <= weekend; i++) {
                var date = moment(this.startOfWeek).add(i, 'days');
                $("#viewTableHead").find(".thead .tcell:nth-child(" + column + ")").html('<div class="weekdate">' + moment(date).format('D') + '</div><div class="weekday">' + moment(date).format('dddd') + '</div>').attr('data-day', moment(date).format('YYYY-MM-DD'));
                column++;
            }
            calendar.startOfWeekDate = moment([calendar.year, calendar.month, calendar.date]).startOf('week');
            this.showDisableDates();
            this._drawEvent();
        }
        wView.prototype.renderEvent = function (eventStartHours, eventStartMins, eventEndHours, evenEndMins, cellNo, arrayIndex, eventDuration) {
            var minsRemoved = Math.round(moment(eventStartHours + ":" + eventStartMins, ["H:mm"]).diff(moment("0:00", ["H:mm"]), "minutes") / Calendar.settings.slotDuration) * Calendar.settings.slotDuration;
            var MAXMINS = this.maxMinsPerDay - minsRemoved;
            var slotNumber = (Math.round(eventStartMins / Calendar.settings.slotDuration) * Calendar.settings.slotDuration);
            if (slotNumber == 60) {
                eventStartHours = parseInt(eventStartHours) + 1;
                slotNumber = 0;
            }
            slotNumber = slotNumber / Calendar.settings.slotDuration;
            var slotParent = $($(".tble .cell-inner[data-time-slot='" + eventStartHours + ":00']")[cellNo]).parent();
            var duration = moment(Calendar.settings.events[arrayIndex].end).diff(Calendar.settings.events[arrayIndex].start, 'minutes')
            duration = Math.round(duration / Calendar.settings.slotDuration) * Calendar.settings.slotDuration;
            var remainingMins = duration;
            var height;
            while (remainingMins > 0) {
                if (remainingMins > this.maxMinsPerDay) {
                    height = (MAXMINS / Calendar.settings.slotDuration) * this.snapGridHeight;
                    remainingMins = remainingMins - MAXMINS;
                    remainingMins = (Math.round(remainingMins / Calendar.settings.slotDuration) * Calendar.settings.slotDuration);
                    $(slotParent.children()[slotNumber]).append(this._buildEventElement(height, arrayIndex, cellNo, eventStartHours, eventStartMins, eventEndHours, evenEndMins));
                    eventStartHours = 0;
                    eventStartMins = 0;
                    cellNo = parseInt(cellNo) + 1;
                    slotParent = $($(".tble .cell-inner[data-time-slot='" + eventStartHours + ":00']")[cellNo]).parent();
                } else {
                    height = (remainingMins / Calendar.settings.slotDuration) * this.snapGridHeight;
                    remainingMins = 0;
                    $(slotParent.children()[slotNumber]).append(this._buildEventElement(height, arrayIndex, cellNo, eventStartHours, eventStartMins, eventEndHours, evenEndMins));
                }
            }
            plugin.settings.onEventRender.call(this);
        }, wView.prototype._buildEventElement = function (h, arrayIndex, cellNo, eventStartHours, eventStartMins, eventEndHours, evenEndMins) {
            h = "height:" + h + "px;";
            var id = 'ca_' + moment(Calendar.settings.events[arrayIndex].start).unix() + arrayIndex;
            var readonly = (Calendar.settings.events[arrayIndex].readOnly == true) ? "readonly" : "";
            var eventContent = "<div class='event-container " + Calendar.settings.events[arrayIndex].class + " " + readonly + "' data-event-duration=" + moment(Calendar.settings.events[arrayIndex].end).diff(Calendar.settings.events[arrayIndex].start, 'minutes') + " data-index=" + arrayIndex + " data-startTime=" + Calendar.settings.events[arrayIndex].start + " data-endTime=" + Calendar.settings.events[arrayIndex].end + " id=" + id + " data-id=" + id + " data-row=" + (parseInt(eventStartHours)) + " data-cell=" + cellNo + " style=" + h + ">"
            eventContent += "<div class='event-inner'>";
            eventContent += "<div class='event-title'>" + Calendar.settings.events[arrayIndex].title + "</div>";
            eventContent += "<div class='time-wrap'><span class='event-start-time'>" + moment(Calendar.settings.events[arrayIndex].start).format(Calendar.settings.timeFormat) + "</span> - ";
            eventContent += "<span class='event-end-time'>" + moment(Calendar.settings.events[arrayIndex].end).format(Calendar.settings.timeFormat) + "</span></div>";
            eventContent += "</div>"
            if (Calendar.settings.events[arrayIndex].readOnly == false || Calendar.settings.events[arrayIndex].readOnly == undefined) {
                eventContent += ""
            }
            eventContent += "</div>"
            Calendar.settings.events[arrayIndex].dataID = id;
            return eventContent;
        }
        wView.prototype._drawEvent = function () {
            this.snapGridWidth = parseInt($('.tcell').outerWidth());
            this.snapGridHeight = parseInt($('.cell-inner').outerHeight());
            var weekend = this.weekEnd;
            if (this.weekStart >= this.weekEnd) {
                weekend = 7;
            }
            var _eventIndex = EventManager.byDateRange(this.startOfWeek, this.endOfWeek);
            $('#weekGrid').find('.event-container').remove();
            $('.tble > .thead > .tcell').find(".event-bubble").remove();
            for (var i = 0; i < _eventIndex.length; i++) {
                var index = _eventIndex[i];
                var e = moment(Calendar.settings.events[index].start);
                var dayOfTheWeek = moment(Calendar.settings.events[index].start).format("e");
                if (dayOfTheWeek == "0" && parseInt(Calendar.settings.ui.week.startOfTheWeek) > 0) {
                    dayOfTheWeek = 7;
                }
                var cellNo = parseInt(dayOfTheWeek) - parseInt(Calendar.settings.ui.week.startOfTheWeek);
                var eventStartHours = e.format('H');
                var eventStartMins = e.format("m");
                var eventEndHours, evenEndMins;
                if (Calendar.settings.events[index].end == null) {
                    eventEndHours = parseInt(eventStartHours) + 1;
                    evenEndMins = "0";
                } else {
                    eventEndHours = moment(Calendar.settings.events[index].end).format('H');
                    evenEndMins = moment(Calendar.settings.events[index].end).format('m');
                }
                var eventDuration = moment(eventEndHours + ":" + evenEndMins, "h:mm").diff(moment(eventStartHours + ":" + eventStartMins, "h:mm"), 'minutes', true);
                if (Calendar.settings.ui.grid.eventBubble) {
                    if (Calendar.settings.events[index].class != null) {
                        this._setEventBubble(Calendar.settings.events[index].class, cellNo);
                    }
                }
                this.renderEvent(eventStartHours, eventStartMins, eventEndHours, evenEndMins, cellNo, index, eventDuration);
            }
            if (Calendar.settings.eventOverlap == false)
                this.collisionGroups(_eventIndex);
            this.setEventBubbles(Calendar.settings.events);
        }
        wView.prototype.collisionGroups = function (_eventIndex) {
            var collisionGroups = [];
            var tempGroups = [];
            for (var i = 1; i < _eventIndex.length; i++) {
                var event1Range = moment.range(Calendar.settings.events[_eventIndex[i]].start, Calendar.settings.events[_eventIndex[i]].end);
                var j = i - 1;
                do {
                    var temp = [];
                    event2Range = moment.range(Calendar.settings.events[_eventIndex[j]].start, Calendar.settings.events[_eventIndex[j]].end);
                    if (event1Range.overlaps(event2Range)) {
                        temp.push(_eventIndex[i]);
                        temp.push(_eventIndex[j]);
                        collisionGroups.push(temp);
                    }
                    j--;
                }
                while (j === 0);
            }

            function union_arrays(array) {
                var a = array.concat();
                var startLength = a.length;
                for (var i = 0; i < a.length; ++i) {
                    for (var j = i + 1; j < a.length; ++j) {
                        if (a[i] === a[j]) {
                            a.splice(j--, 1);
                        }
                    }
                }
                if (startLength != a.length) {
                    return a;
                }
                return [];
            }
            for (var i = 0; i < collisionGroups.length; ++i) {
                for (var j = i + 1; j < collisionGroups.length; ++j) {
                    var newArray = union_arrays(collisionGroups[i].concat(collisionGroups[j]));
                    if (newArray.length > 0) {
                        collisionGroups[j] = newArray;
                        collisionGroups.splice(i, 1);
                        j--;
                    }
                }
            }
            for (var i = 0; i < collisionGroups.length; i++) {
                var width = 100 / collisionGroups[i].length;
                var left = 0;
                for (var j = 0; j < collisionGroups[i].length; j++) {
                    var el = $(".event-container[data-index='" + collisionGroups[i][j] + "']");
                    el.css({
                        "width": width + "%",
                        "left": left + "%"
                    });
                    left = left + width
                }
            }
        }
        wView.prototype._bindEventDraggers = function () {
            var mins = 0;
            var days = 0;
            var duration = 0;
            var parent = $("#weekGrid")[0];
            var $snapGridHeight = this.snapGridHeight;
            var $snapGridWidth = this.snapGridWidth;
            var $this = this;
            interact('.event-container:not(.readonly)').draggable({
                snap: {
                    targets: [interact.createSnapGrid({
                        x: this.snapGridWidth,
                        y: this.snapGridHeight
                    })]
                },
                inertia: true,
                restrict: {
                    restriction: parent,
                    elementRect: {
                        top: 0,
                        left: 0,
                        bottom: 0,
                        right: 0
                    },
                    endOnly: true
                },
                autoScroll: true,
                onmove: function (event) {
                    mins = mins + (event.dy / $snapGridHeight) * Calendar.settings.slotDuration;
                    days = days + (event.dx / $snapGridWidth);
                    Calendar.dragMoveListener(event);
                    event.target.classList.add('dragging');
                },
                onend: function (event) {
                    var el = event.target;
                    el = $(el);
                    var eventData = $(el).data();
                    var eventO = Calendar.constructEventForUser(eventData.index);
                    if (eventO.readOnly) {
                        $this._drawEvent();
                        return;
                    }
                    eventO.start = moment(eventO.start).add(days, 'days');
                    eventO.start = moment(eventO.start).add(mins, 'minutes').format();
                    eventO.end = moment(eventO.start).add(el.attr("data-event-duration"), 'minutes').format();
                    Calendar.settings.events[eventData.index] = eventO;
                    mins = 0;
                    days = 0;
                    event.target.classList.remove('dragging');
                    $this._drawEvent();
                    plugin.settings.onEventDragComplete(eventO);
                }
            }).resizable({
                edges: {
                    left: false,
                    right: false,
                    bottom: false,
                    top: false
                },
                snap: {
                    targets: [interact.createSnapGrid({
                        x: this.snapGridWidth,
                        y: this.snapGridHeight
                    })]
                }
            }).on('resizemove', function (event) {
                var target = event.target
                var height = event.rect.height;
                if (height < $snapGridHeight) {
                    height = $snapGridHeight;
                    event.rect.height = $snapGridHeight;
                }
                if (event.dx != 0) {
                    days = days + (event.dx / this.snapGridWidth);
                    var elem = $(target);
                    var eventData = $(target).data();
                    var eventO = Calendar.constructEventForUser(eventData.index);
                    var difference = moment(eventO.start).format("H");
                    duration = 24 - difference;
                    height = 80 * duration;
                }
                duration = (height / $snapGridHeight) * Calendar.settings.slotDuration;
                target.style.height = height + 'px';
            }).on('resizeend', function (event) {
                var target = event.target
                var elem = $(target);
                var eventData = $(target).data();
                var eventO = Calendar.constructEventForUser(eventData.index);
                if (eventO.readOnly) {
                    $this._drawEvent();
                    return;
                }
                eventO.end = moment(eventO.start).add(duration, "minutes").format();
                duration = 0;
                days = 0;
                Calendar.settings.events[eventData.index] = eventO;
                $this._drawEvent();
                plugin.settings.onEventDragComplete(eventO);
            });
        }, wView.prototype._setEventBubble = function (className, cellNo) {
            var $elem = $("#viewTableHead").find(".thead .tcell:nth-child(" + (parseInt(cellNo) + 1) + ")");
            if ($elem.children('.' + className).length == 0) {
                $elem.append('<div class="event-bubble ' + className + '"></div>');
            }
        }
        wView.prototype._timeslots = function () {
            var container = '#time-slots';
            Calendar.content = '';
            for (var i = Calendar.settings.minTime; i < Calendar.settings.maxTime; i++) {
                Calendar.content += '<div class="time-slot"><span>' + moment().hour(i).format(Calendar.settings.ui.grid.timeFormat) + '</span></div>';
            }
            $(container).append(Calendar.content);
        }
        wView.prototype._setActive = function () {
            $('#viewTableHead').find('.thead .tcell').removeClass('active');
            $('#weekGrid').find('.trow .tcell').removeClass('active');
            var d = moment([calendar.year, calendar.month, calendar.date]).format("e");
            var index = 0;
            if (d == 0 && parseInt(Calendar.settings.ui.week.startOfTheWeek) > d) {
                index = 7
            } else {
                index = (parseInt(d) + (1 - parseInt(Calendar.settings.ui.week.startOfTheWeek)))
            }
            $('#viewTableHead').find('.thead .tcell:nth-child(' + (index) + ')').addClass('active');
            $('#weekGrid').find('.trow .tcell:nth-child(' + (index) + ')').addClass('active');
        }
        wView.prototype.scrollToFirstEvent = function () {
            var _eventIndex = EventManager.byDateRange(this.startOfWeek, this.endOfWeek);
            if (_eventIndex.length == 0)
                return false;
            var el = $(".cell-inner [data-index='" + _eventIndex[0] + "']");
            var grid = $('.calendar .calendar-container .grid');
            var parentOffSet = $('.calendar .calendar-container .grid').get(0).getBoundingClientRect();
            var elRect = el.get(0).getBoundingClientRect();
            var position = (elRect.top - parentOffSet.top) - Calendar.settings.ui.grid.scrollToGap;
            var currentPosition = grid.scrollTop();
            grid.animate({
                scrollTop: currentPosition + position
            }, Calendar.settings.ui.grid.scrollToAnimationSpeed);
        }
        wView.prototype.doubletap = function (obj) {
            var elem = $(obj);
            var date = $('#viewTableHead').find('.thead .tcell:nth-child(' + (elem.parent().index() + 1) + ')').attr("data-day");
            var time = moment(elem.attr('data-time-slot'), ["H:mm"]).format(' H:mm');
            date = moment(date + time, 'YYYY/MM/D h:mm').format();
            var timeSlot = {
                date: date,
            }
            return timeSlot;
        }
        wView.prototype.showDisableDates = function () {
            $(".tble .trow .tcell").removeClass("disable").children(".cell-inner").removeClass("disable");
            this.disableDates = EventManager.getDisableDateIndexInDateRange(this.startOfWeek, this.endOfWeek, "e");
            for (var i = 0; i < this.disableDates.length; i++) {
                $(".tble .trow .tcell:nth-child(" + (parseInt(this.disableDates[i]) + 1) + ")").addClass("disable").children(".cell-inner").addClass("disable");
            }
        }
        wView.prototype.refresh = function () {
            var currentDate = moment([calendar.year, calendar.month, calendar.date]);
            this.weekStart = parseInt(moment(Calendar.settings.ui.week.startOfTheWeek, 'd').format('d'));
            this.weekEnd = parseInt(moment(Calendar.settings.ui.week.endOfTheWeek, 'd').format('d'));
            if (this.weekEnd == 0) {
                this.weekEnd = 7;
            }
            if (currentDate.weekday() == 0 && this.weekStart > 0) {
                this.startOfWeek = moment([calendar.year, calendar.month, calendar.date]).subtract("days", this.weekEnd).startOf('week');
            } else {
                this.startOfWeek = moment([calendar.year, calendar.month, calendar.date]).startOf('week');
            }
            this.endOfWeek = currentDate.startOf('week').add("days", this.weekEnd);
            this._loadDates();
            this._setActive();
            var dates = [this.startOfWeek, this.endOfWeek];
            this.setEventBubbles(Calendar.settings.events)
            return dates;
        }
        wView.prototype.reloadEvents = function () {
            this._drawEvent();
        }
        wView.prototype.rebuild = function () {
            this.grid._buildLayout();
            this.refresh();
        }
        wView.prototype.windowResize = function () {
            var $this = this;
            $(window).resize(function () {
                $this.snapGridWidth = parseInt($('.tcell').outerWidth());
                $this._bindEventDraggers();
            });
        }
        wView.prototype.getCurrentDateRange = function () {
            var dates = [this.startOfWeek, this.endOfWeek];
            return dates;
        }
        wView.prototype.setEventBubbles = function (eventArray) {
            $('.has-event').removeClass('has-event');
            for (var item in Calendar.settings.events) {
                var eventYear = moment(Calendar.settings.events[item].start).format(Calendar.settings.ui.year.format);
                var eventMonth = moment(Calendar.settings.events[item].start).format(Calendar.settings.ui.month.format);
                var eventDate = moment(Calendar.settings.events[item].start).format('D');
                if (Calendar.settings.ui.year.eventBubble)
                    $('.year > [data-year="' + eventYear + '"]').addClass('has-event');
                if (calendar.year == moment(Calendar.settings.events[item].start).format("YYYY")) {
                    if (Calendar.settings.ui.month.eventBubble)
                        $('.month > [data-month="' + eventMonth + '"]').addClass('has-event');
                    if (calendar.month + 1 == moment(Calendar.settings.events[item].start).format("M")) {
                        if (Calendar.settings.ui.week.eventBubble)
                            $('.date-selector > .week-date > .day > [data-date="' + eventDate + '"]').addClass('has-event');
                    }
                }
            }
        }
        var MonthView = function (container) {
            this.monthViewStartDate = "";
            this.monthViewEndDate = "";
            view.call(this, container);
            this.gridElem = "";
            this.render();
        }
        MonthView.prototype = Object.create(view.prototype);
        MonthView.prototype.render = function () {
            this.weekStart = parseInt(moment(Calendar.settings.ui.week.startOfTheWeek, 'd').format('d'));
            this.weekEnd = parseInt(moment(Calendar.settings.ui.week.endOfTheWeek, 'd').format('d'));
            this.startOfWeek = moment([calendar.year, calendar.month, calendar.date]).startOf('week');
            this.endOfWeek = moment([calendar.year, calendar.month, calendar.date]);
            this._buildLayout();
            this._loadDates();
            this._bindEvents();
            this.bindEventDraggers();
        }
        MonthView.prototype._buildLayout = function () {
            var headerContent = '<div class="thead" >';
            for (var j = this.weekStart; j <= this.weekEnd; j++) {
                headerContent += '<div class="tcell"></div>';
            }
            headerContent += '</div>';
            $(this.container).html("");
            Calendar.content = '';
            Calendar.content += '<div class="view month-view">';
            Calendar.content += '<div class="tble" id="viewTableHead">';
            Calendar.content += headerContent;
            Calendar.content += '</div>';
            Calendar.content += '<div class="grid">';
            Calendar.content += '<div class="tble" id="monthGrid">';
            for (var i = 0; i < 6; i++) {
                Calendar.content += '<div class="trow" >';
                for (var j = this.weekStart; j <= this.weekEnd; j++) {
                    Calendar.content += '<div class="tcell">';
                    Calendar.content += '<div class="month-date"></div>';
                    Calendar.content += '<div class="cell-inner"><div class="holder"></div></div>';
                    Calendar.content += '</div>';
                }
                Calendar.content += '</div>';
            }
            Calendar.content += '</div>';
            Calendar.content += '</div>';
            Calendar.content += '</div>';
            $(this.container).append(Calendar.content);
            cellHeight = $('.tcell').innerHeight();
            calendar.startOfWeekDate = moment([calendar.year, calendar.month, calendar.date]).startOf('week');
            calendar.endOfWeek = moment([calendar.year, calendar.month, calendar.date]).endOf('week').format('D');
            this.snapGridWidth = parseInt($('.tcell').outerWidth());
            this.snapGridHeight = parseInt($('.cell-inner').outerHeight());
            this.holderHeight = parseInt($(".holder").outerHeight());
            $(".calendar").removeClass("week").addClass("month");
        }
        MonthView.prototype._loadDates = function () {
            $(".tcell").removeClass("not active current-date");
            $(".month-date").removeClass("not active current-date");
            var column = 1;
            for (var i = this.weekStart; i <= this.weekEnd; i++) {
                var date = moment(this.startOfWeek).add(i, 'days');
                $("#viewTableHead").find(".thead .tcell:nth-child(" + column + ")").html('</div><div class="weekday">' + moment(date).format('dddd') + '</div>').attr('data-day', moment(date).format('YYYY-MM-DD'));
                column++;
            }
            calendar.startOfWeekDate = moment([calendar.year, calendar.month, calendar.date]).startOf('week');
            var startDate = moment([calendar.year, calendar.month, calendar.date]).startOf('month').startOf('week');
            this.monthViewStartDate = moment([calendar.year, calendar.month, calendar.date]).startOf('month').startOf('week');
            var count = 0;
            var extraClass = null;
            this.gridElem = $(this.container).find(".view .grid");
            for (var i = 1; i <= 6; i++) {
                for (var j = this.weekStart; j <= this.weekEnd; j++) {
                    var monthOfDate = parseInt(startDate.format("M")) - 1;
                    if (calendar.month != monthOfDate) extraClass = "not";
                    if (calendar.date == startDate.format("D") && calendar.month == monthOfDate) extraClass = "current-date active";
                    this.gridElem.find(".tble .trow:nth-child(" + i + ") .tcell:nth-child(" + (j + 1) + ")").addClass(extraClass).attr("data-date", startDate.format("YYYY-MM-DD")).children(".month-date").html(startDate.format("D")).addClass(extraClass);
                    startDate = startDate.add(1, 'days');
                    extraClass = "";
                }
            }
            this.monthViewEndDate = startDate;
            this._showDisableDates();
            this._drawEvent();
            this.setEventBubbles();
        }
        MonthView.prototype._bindEvents = function () {
            var $this = this;
            $(".month-view .tcell").off("click");
            $(".month-view .tcell").on("click", function () {
                var d = moment($(this).attr("data-date"));
                calendar.date = d.format("D");
                calendar.month = parseInt(d.format("M")) - 1;
                $this.setActive($(this));
                Calendar._buildCurrentDateHeader();
            });
        }
        MonthView.prototype._drawEvent = function () {
            var _eventIndex = EventManager.byDateRange(this.monthViewStartDate.format("YYYY-MM-DD"), this.monthViewEndDate.format("YYYY-MM-DD"));
            $('#monthGrid').find('.event-container').remove();
            $('#monthGrid').find('.ghost-element').remove();
            for (var i = 0; i < _eventIndex.length; i++) {
                var index = _eventIndex[i];
                var cell = $(".tble [data-date='" + moment(Calendar.settings.events[index].start).format("YYYY-MM-DD") + "']")
                var days = moment(moment(Calendar.settings.events[index].end)).diff(moment(Calendar.settings.events[index].start), "days");
                days = (days === 0) ? days = 1 : days = days;
                this.renderEvent(cell, index, days);
            }
        }
        MonthView.prototype.renderEvent = function (elem, arrayIndex, days) {
            var width = "style='width:" + (parseInt(days) * 100) + "%'";
            var id = 'ca_' + moment(Calendar.settings.events[arrayIndex].start).unix() + arrayIndex;
            var readonly = (Calendar.settings.events[arrayIndex].readOnly == true) ? "readonly" : "";
            var eventContent = "<div class='event-container " + Calendar.settings.events[arrayIndex].class + " " + readonly + "' " + width + " data-index=" + arrayIndex + " data-startTime=" + Calendar.settings.events[arrayIndex].start + " data-endTime=" + Calendar.settings.events[arrayIndex].end + " id=" + id + " data-id=" + id + ">"
            eventContent += "<div class='event-inner'>";
            eventContent += "<div class='event-title'>" + Calendar.settings.events[arrayIndex].title + "</div>";
            eventContent += "</div>"
            eventContent += "</div>"
            elem.children(".cell-inner").children(".holder").append(eventContent);
            Calendar.settings.events[arrayIndex].dataID = id;
            plugin.settings.onEventRender.call(this);
        }
        MonthView.prototype.doubletap = function (obj) {
            var el = $(obj).parent();
            var timeSlot = {
                date: moment(el.attr("data-date")).format(),
            }
            return timeSlot;
        }
        MonthView.prototype.bindEventDraggers = function () {
            var days = 0;
            var duration = 0;
            var parent = $("#monthGrid")[0];
            var startPos = null
            var $this = this;
            interact('.event-container:not(.readonly)').draggable({
                inertia: true,
                restrict: {
                    restriction: parent,
                    elementRect: {
                        top: 0,
                        left: 0,
                        bottom: 1,
                        right: 1
                    },
                    endOnly: true,
                    drag: ""
                },
                mode: 'anchor',
                anchors: [],
                autoScroll: true,
                onmove: function (event) {
                    Calendar.dragMoveListener(event);
                    event.target.classList.add('dragging');
                },
                onend: function (event) {
                    event.target.classList.remove('dragging');
                }
            }).snap({
                mode: 'anchor',
                anchors: [],
                range: Infinity,
                elementOrigin: {
                    x: 0.5,
                    y: 0.5
                },
                endOnly: true
            }).on('dragstart', function (event) {
                if (!startPos) {
                    var rect = interact.getElementRect(event.target);
                    startPos = {
                        x: rect.left + rect.width / 2,
                        y: rect.top + rect.height / 2
                    }
                }
                event.interactable.snap({
                    anchors: [startPos]
                });
            });
            interact('.tcell').dropzone({
                overlap: 'center',
                accept: '.event-container'
            }).on('dragenter', function (event) {
                var dropRect = interact.getElementRect($(event.target).children(".cell-inner").children(".holder")[0]),
                    elementRect = interact.getElementRect(event.relatedTarget),
                    dropCenter = {
                        x: dropRect.left + elementRect.width / 2,
                        y: dropRect.top + elementRect.height / 2
                    };
                event.draggable.snap({
                    anchors: [dropCenter]
                });
                var draggableElement = event.relatedTarget,
                    dropzoneElement = event.target;
                dropzoneElement.classList.add('drop-target');
            }).on('dragleave', function (event) {
                event.draggable.snap(false);
                event.draggable.snap({
                    anchors: [startPos]
                });
                event.target.classList.remove('drop-target');
                $(event.target).find(".ghost-element").remove();
            }).on('dropactivate', function (event) { }).on('dropdeactivate', function (event) {
                event.target.classList.remove('drop-active');
                event.target.classList.remove('drop-target');
            }).on('drop', function (event) {
                var drop = event.target;
                var draggableElement = event.relatedTarget;
                drop = $(drop);
                draggableElement = $(draggableElement);
                var eventData = draggableElement.data();
                var eventO = Calendar.constructEventForUser(eventData.index);
                if (eventO.readOnly) {
                    $this._drawEvent();
                    return;
                }
                eventO.start = moment(eventO.start).set('month', moment(drop.attr("data-date")).get('month')).format();
                eventO.start = moment(eventO.start).set('date', moment(drop.attr("data-date")).get('date')).format();
                eventO.end = moment(eventO.start).set('date', moment(drop.attr("data-date")).get('date')).format();
                Calendar.settings.events[eventData.index] = eventO;
                $this._drawEvent();
                plugin.settings.onEventDragComplete(eventO);
            });
        }, MonthView.prototype._showDisableDates = function () {
            $(".tble .trow .tcell").removeClass("disable").children(".cell-inner").removeClass("disable");
            this.disableDates = EventManager.getDisableDateIndexInDateRange(this.monthViewStartDate.format("YYYY-MM-DD"), this.monthViewEndDate.format("YYYY-MM-DD"), "YYYY-MM-DD");
            for (var i = 0; i < this.disableDates.length; i++) {
                $(".tble .trow .tcell[data-date='" + this.disableDates[i] + "']").addClass("disable").children(".cell-inner").addClass("disable");
            }
        }
        MonthView.prototype.setActive = function (el) {
            $(".month-view .tcell").children(".month-date").removeClass("active");
            el.children(".month-date").addClass("active");
        }
        MonthView.prototype.refresh = function () {
            this._loadDates();
            this._drawEvent();
            var dates = [this.monthViewStartDate, this.monthViewEndDate];
            return dates;
        }
        MonthView.prototype.reloadEvents = function () {
            this._drawEvent();
        }
        MonthView.prototype.getCurrentDateRange = function () {
            var dates = [this.monthViewStartDate, this.monthViewEndDate];
            return dates;
        }, MonthView.prototype.setEventBubbles = function (eventArray) {
            $('.has-event').removeClass('has-event');
            for (var item in Calendar.settings.events) {
                var eventYear = moment(Calendar.settings.events[item].start).format(Calendar.settings.ui.year.format);
                var eventMonth = moment(Calendar.settings.events[item].start).format(Calendar.settings.ui.month.format);
                var eventDate = moment(Calendar.settings.events[item].start).format('D');
                $('.year > [data-year="' + eventYear + '"]').addClass('has-event');
                if (calendar.year == moment(Calendar.settings.events[item].start).format("YYYY")) {
                    $('.month > [data-month="' + eventMonth + '"]').addClass('has-event');
                    if (calendar.month + 1 == moment(Calendar.settings.events[item].start).format("M")) {
                        $('.date-selector > .week-date > .day > [data-date="' + eventDate + '"]').addClass('has-event');
                    }
                }
            }
        }
        plugin.Calendar = plugin.Calendar || {};
        plugin.Calendar = {
            settings: plugin.settings,
            yearPicker: null,
            monthPicker: null,
            gridLayout: null,
        }
        Calendar = {
            Init: function (rebuild) {
                this.settings = plugin.settings;
                if (Calendar.settings.ui.year == null || Calendar.settings.ui.month == null || Calendar.settings.ui.dateHeader == null || Calendar.settings.ui.week == null || Calendar.settings.ui.grid == null) {
                    alert("You have not included the proper ui[] settings, please refer docs");
                }
                this._setLocale();
                calendar.monthLong = moment().format(this.settings.ui.month.format);
                if (this.settings.now != null) {
                    calendar.month = moment(plugin.settings.now).month();
                    calendar.year = moment(plugin.settings.now).year();
                    calendar.date = moment(plugin.settings.now).format("D");
                    calendar.dayOfWeek = moment(plugin.settings.now).day();
                    calendar.monthLong = moment(plugin.now).format('MMM');
                } else {
                    calendar.month = moment().month();
                    calendar.year = moment().year();
                    calendar.date = moment().format("D");
                    calendar.dayOfWeek = moment().day();
                    calendar.monthLong = moment().format('MMM');
                }
                if (!Calendar.settings.ui.year.visible)
                    $(".calendar-header").hide();
                if (!Calendar.settings.ui.month.visible)
                    $("#months").hide();
                if (!Calendar.settings.ui.dateHeader.visible)
                    $("#currentDate").hide();
                if (!Calendar.settings.ui.week.visible)
                    $("#weeks-wrapper").hide();
                this.yearPicker = new yearSelector("#years");
                if (!Calendar.settings.weekends) {
                    Calendar.settings.ui.week.startOfTheWeek = 1;
                    Calendar.settings.ui.week.endOfTheWeek = 5;
                }
                this.monthPicker = new monthSelector("#months");
                this._buildCurrentDateHeader();
                this.gridLayout = new gridFractory(Calendar.settings.view);
                var val = this.gridLayout.getCurrentDateRange();
                var range = {
                    start: val[0],
                    end: val[1]
                }
                plugin.settings.onViewRenderComplete(range);
                (!rebuild) ? this.bindEventHanders() : null;
                this.autoFocusActiveElement();
                this.scrollToElement('#weeks-wrapper .active');
                var optionsHeight = $(".calendar .options").get(0).getBoundingClientRect().height;
                var headerHeight = $(".calendar-header").get(0).getBoundingClientRect().height;
                var calendarHeight = "calc(100% - " + (optionsHeight + headerHeight) + "px)";
                $("#calendar").css({
                    height: calendarHeight
                })
            },
            rebuild: function () {
                Calendar.Init(true);
            },
            _highlightWeek: function (elem) {
                $('.week').removeClass('active');
                $(elem).closest('.week').addClass('active');
            },
            _buildCurrentDateHeader: function () {
                $("#currentDate").text(moment([calendar.year, calendar.month, calendar.date]).format(Calendar.settings.ui.dateHeader.format));
            },
            _onYearChange: function () {
                this._onMonthChange();
            },
            _onMonthChange: function () {
                this._onDayChange();
            },
            _onDayChange: function () {
                this._buildCurrentDateHeader();
                if (this.gridLayout.miniCalendar != null)
                    this.gridLayout.miniCalendar.render();
                var val = this.gridLayout.refresh();
                var range = {
                    start: val[0],
                    end: val[1]
                }
                plugin.settings.onDateChange(range);
            },
            _loading: function () {
                $('.grid .tble').prepend("<div class='loading'><div class='progress progress-small'><div class='progress-bar-indeterminate' style='display: block;'></div></div></div>");
            },
            _loaded: function () {
                $('.grid .tble .loading').remove();
            },
            _errorMessage: function (msg) {
                $('.grid .tble').prepend("<div class='loading'></div>");
                if (msg) {
                    $('.grid .tble .loading').pgNotification({
                        style: 'bar',
                        message: msg,
                        position: 'top',
                        timeout: 0,
                        type: 'danger',
                        onClosed: function () {
                            $('.grid .tble .loading').remove();
                        }
                    }).show();
                }
            },
            dragHandler: function (element) {
                var el = $('#' + element);
                var parent = el.parent();
                if ($('body').hasClass('mobile'))
                    return
                if (el.length != 1)
                    return
                $('.drager').scrollbar();
                var lP = parent.scrollLeft();
                interact('#' + element).draggable({
                    preventDefault: "auto",
                    onmove: function (event) {
                        var target = event.target,
                            x = (parseFloat(target.getAttribute('data-x')) || 0) + event.dx;
                        inverseX = -(x);
                        parent.scrollLeft(inverseX);
                        target.setAttribute('data-x', x);
                    }
                })
            },
            autoFocusActiveElement: function () {
                var timer;
                $(window).resize(function () {
                    clearTimeout(timer);
                    timer = setTimeout(function () {
                        Calendar.scrollToElement('#weeks-wrapper .active');
                    }, 500);
                });
            },
            scrollToElement: function (el) {
                el = $(el);
                if (!el.length != 0)
                    return
                var par = $(el).parent();
                var t = this._isElementInViewport(el);
                if (!t) {
                    var elOffset = el.offset().left;
                    var elHeight = par.children().width();
                    var windowHeight = $(window).width();
                    var offset;
                    if (elHeight < windowHeight) {
                        offset = elOffset - ((windowHeight / 2) - (elHeight / 2));
                    } else {
                        offset = elOffset;
                    }
                    $('#weeks-wrapper').parent().animate({
                        scrollLeft: offset
                    }, 10);
                }
            },
            _isElementInViewport: function (el) {
                if (typeof jQuery === "function" && el instanceof jQuery) {
                    el = el[0];
                }
                var rect = el.getBoundingClientRect();
                return (rect.top >= 0 && rect.left >= 0 && rect.bottom <= (window.innerHeight || document.documentElement.clientHeight) && rect.right <= (window.innerWidth || document.documentElement.clientWidth));
            },
            dragMoveListener: function (event) {
                var target = event.target,
                    x = (parseFloat(target.getAttribute('data-x')) || 0) + event.dx,
                    y = (parseFloat(target.getAttribute('data-y')) || 0) + event.dy;
                target.style.webkitTransform = target.style.transform = 'translate(' + x + 'px, ' + y + 'px)';
                target.setAttribute('data-x', x);
                target.setAttribute('data-y', y);
            },
            nextMonth: function () {
                currentYear = moment([calendar.year, calendar.month, calendar.date]).add(1, 'months').year();
                currentMonth = moment([calendar.year, calendar.month, calendar.date]).add(1, 'months').month();
                this._refreshViewOnDateChange();
            },
            previousMonth: function () {
                currentYear = moment([calendar.year, calendar.month, calendar.date]).subtract(1, 'months').year();
                currentMonth = moment([calendar.year, calendar.month, calendar.date]).subtract(1, 'months').month();
                this._refreshViewOnDateChange();
            },
            today: function () {
                calendar.year = moment().year();
                calendar.month = moment().month();
                calendar.date = moment().format("D");
                this._refreshViewOnDateChange();
            },
            _addEvent: function (event) {
                Calendar.settings.events.push(event);
                this.gridLayout.reloadEvents();
            },
            _addEvents: function (eventArray) {
                Calendar.settings.events.concat(eventArray);
                for (var i = 0; i < eventArray.length; i++) {
                    Calendar.settings.events.push(eventArray[i]);
                }
                this.gridLayout.reloadEvents();
            },
            _deleteEvent: function (index) {
                var eventO = Calendar.constructEventForUser(index);
                if (eventO.readOnly) {
                    return false;
                }
                Calendar.settings.events.splice(parseInt(index), 1);
                this.gridLayout.reloadEvents();
            },
            _removeAllEvents: function () {
                Calendar.settings.events = [];
            },
            _updateEvent: function (eventObj) {
                var eventO = Calendar.constructEventForUser(eventObj.index);
                if (eventO.readOnly) {
                    return false;
                }
                Calendar.settings.events[eventObj.index] = eventObj;
                this.gridLayout.reloadEvents();
            },
            fitEventsToSlot: function (startSlot) {
                if (Calendar.settings.eventOverlap == true) {
                    var events = $(startSlot).children().length;
                    var elem = $(startSlot).children();
                    if (events > 1) {
                        var width = elem.width();
                        for (var i = 0; i < events; i++) {
                            $(elem[i]).width(width / events);
                            var next = i + 1;
                            $(elem[next]).css('left', width / 2);
                        }
                    } else {
                        elem.width('100%');
                        $(elem).css('left', 0);
                    }
                }
            },
            _changeView: function (view) {
                this.gridLayout.layout = view
                this.gridLayout.rebuild();
            },
            _getView: function () {
                return Calendar.settings.view
            },
            timeSlotDblClick: function (obj) {
                plugin.settings.onTimeSlotDblClick(this.gridLayout.doubletap(obj));
            },
            bindEventHanders: function () {
                interact(".cell-inner:not(.disable)").on('doubletap', function (event) {
                    Calendar.timeSlotDblClick($(event.currentTarget));
                    event.preventDefault();
                })
                interact('.event-container').on('tap', function (event) {
                    var eventO = Calendar.constructEventForUser($(event.currentTarget).attr('data-index'));
                    plugin.settings.onEventClick(eventO);
                });
            },
            constructEventForUser: function (i) {
                var eventO = {
                    index: i,
                    title: Calendar.settings.events[i].title,
                    class: Calendar.settings.events[i].class,
                    start: Calendar.settings.events[i].start,
                    end: Calendar.settings.events[i].end,
                    allDay: Calendar.settings.events[i].allDay,
                    other: Calendar.settings.events[i].other,
                    readOnly: Calendar.settings.events[i].readOnly,
                };
                return eventO
            },
            _setLocale: function () {
                moment.locale(Calendar.settings.locale);
            },
            _refreshViewOnDateChange: function () {
                this._buildCurrentDateHeader();
                this.yearPicker.render();
                this.monthPicker.render();
                this.gridLayout.refresh();
                if (this.gridLayout.miniCalendar != null)
                    this.gridLayout.miniCalendar.render();
            },
            _setDate: function (d) {
                calendar.month = moment(d).month();
                calendar.year = moment(d).year();
                calendar.date = moment(d).format("D");
                calendar.dayOfWeek = moment(d).day();
                calendar.monthLong = moment(d).format('MMM');
                this._refreshViewOnDateChange();
            },
            _getDate: function (format) {
                if (format == null) {
                    format = 'MMMM Do YYYY'
                }
                return moment([calendar.year, calendar.month, calendar.date]).format(format);
            },
            _getEventArray: function (option) {
                if (option == null || option == 'all')
                    return Calendar.settings.events;
            },
            _getDateRangeInView: function () {
                return this.gridLayout.getCurrentDateRange();
            },
            _scrollToFirstEvent: function () {
                this.gridLayout.scrollToFirstEvent();
            }
        }
        Calendar.Init();
        return this;
    }
    PagesCalendar.prototype.rebuild = function () {
        Calendar.rebuild();
    };
    PagesCalendar.prototype.today = function () {
        Calendar.today();
    };
    PagesCalendar.prototype.next = function () {
        Calendar.nextMonth();
    };
    PagesCalendar.prototype.prev = function () {
        Calendar.previousMonth();
    }
    PagesCalendar.prototype.setDate = function (date) {
        Calendar._setDate(date);
    };
    PagesCalendar.prototype.getDate = function (format) {
        return Calendar._getDate(format);
    };
    PagesCalendar.prototype.destroy = function () { };
    PagesCalendar.prototype.setLocale = function (lang) {
        Calendar.settings.locale = lang;
        Calendar._setLocale();
        Calendar.rebuild();
    };
    PagesCalendar.prototype.reloadEvent = function () {
        Calendar.loadEvents();
    };
    PagesCalendar.prototype.addEvent = function (event) {
        Calendar._addEvent(event);
    };
    PagesCalendar.prototype.addEvents = function (eventArray) {
        Calendar._addEvents(eventArray);
    };
    PagesCalendar.prototype.removeEvent = function (index) {
        Calendar._deleteEvent(index)
    };
    PagesCalendar.prototype.removeAllEvents = function () {
        Calendar._removeAllEvents()
    };
    PagesCalendar.prototype.updateEvent = function (eventObj) {
        Calendar._updateEvent(eventObj);
    };
    PagesCalendar.prototype.getEvents = function (option) {
        return Calendar._getEventArray(option);
    };
    PagesCalendar.prototype.view = function (option) {
        return Calendar._changeView(option);
    };
    PagesCalendar.prototype.getView = function () {
        return Calendar._getView();
    };
    PagesCalendar.prototype.getDateRangeInView = function () {
        return Calendar._getDateRangeInView();
    };
    PagesCalendar.prototype.scrollToFirstEvent = function () {
        return Calendar._scrollToFirstEvent();
    };
    PagesCalendar.prototype.setState = function (state) {
        switch (state) {
            case "loading":
                Calendar._loading();
                break;
            case "loaded":
                Calendar._loaded();
                break;
        }
    };
    PagesCalendar.prototype.error = function (msg) {
        Calendar._errorMessage(msg);
    }

    function Plugin(option, obj) {
        var $this = $(this),
            data = $this.data('pagescalendar'),
            options = typeof option == 'object' && option
        if (typeof option == 'string') {
            return data[option](obj)
        }
        return this.each(function () {
            if (!data) {
                $this.data('pagescalendar', (data = new PagesCalendar(this, options)))
            }
        });
    }
    var old = $.fn.pagescalendar
    $.fn.pagescalendar = Plugin
    $.fn.pagescalendar.Constructor = PagesCalendar
    $.fn.pagescalendar.defaults = {
        ui: {
            year: {
                visible: true,
                format: 'YYYY',
                startYear: '2000',
                endYear: moment().add(10, 'year').format('YYYY'),
                eventBubble: true
            },
            month: {
                visible: true,
                format: 'MMM',
                eventBubble: true
            },
            dateHeader: {
                format: 'MMMM YYYY, D dddd',
                visible: true,
            },
            week: {
                day: {
                    format: 'D'
                },
                header: {
                    format: 'dd'
                },
                eventBubble: true,
                startOfTheWeek: '0',
                endOfTheWeek: '6',
                visible: true
            },
            grid: {
                dateFormat: 'D dddd',
                timeFormat: 'h A',
                eventBubble: true,
                scrollToFirstEvent: false,
                scrollToAnimationSpeed: 300,
                scrollToGap: 20
            }
        },
        eventObj: {
            editable: true
        },
        view: 'week',
        now: null,
        locale: 'en',
        timeFormat: 'h:mm a',
        minTime: 0,
        maxTime: 24,
        dateFormat: 'MMMM Do YYYY',
        slotDuration: '30',
        events: [],
        eventOverlap: false,
        weekends: true,
        disableDates: [],
        onViewRenderComplete: function () { },
        onEventDblClick: function () { },
        onEventClick: function (event) { },
        onEventRender: function () { },
        onEventDragComplete: function (event) { },
        onEventResizeComplete: function (event) { },
        onTimeSlotDblClick: function (timeSlot) { },
        onDateChange: function (range) { }
    }
    $.fn.pagescalendar.noConflict = function () {
        $.fn.pagescalendar = old;
        return this;
    }
});
(function (root, factory) {
    if (typeof define === 'function' && define.amd) {
        define(["moment"], function (a0) {
            return (root['DateRange'] = factory(a0));
        });
    } else if (typeof exports === 'object') {
        module.exports = factory(require("moment"));
    } else {
        root['DateRange'] = factory(moment);
    }
}(this, function (moment) {
    var INTERVALS = {
        year: true,
        month: true,
        week: true,
        day: true,
        hour: true,
        minute: true,
        second: true
    };

    function DateRange(start, end) {
        var parts;
        var s = start;
        var e = end;
        if (arguments.length === 1 || end === undefined) {
            if (typeof start === 'object' && start.length === 2) {
                s = start[0];
                e = start[1];
            } else if (typeof start === 'string') {
                parts = start.split('/');
                s = parts[0];
                e = parts[1];
            }
        }
        this.start = (s === null) ? moment(-8640000000000000) : moment(s);
        this.end = (e === null) ? moment(8640000000000000) : moment(e);
    }
    DateRange.prototype.constructor = DateRange;
    DateRange.prototype.clone = function () {
        return moment().range(this.start, this.end);
    };
    DateRange.prototype.contains = function (other, exclusive) {
        var start = this.start;
        var end = this.end;
        if (other instanceof DateRange) {
            return start <= other.start && (end > other.end || (end.isSame(other.end) && !exclusive));
        } else {
            return start <= other && (end > other || (end.isSame(other) && !exclusive));
        }
    };
    DateRange.prototype.overlaps = function (range) {
        return this.intersect(range) !== null;
    };
    DateRange.prototype.intersect = function (other) {
        var start = this.start;
        var end = this.end;
        if ((start <= other.start) && (other.start < end) && (end < other.end)) {
            return new DateRange(other.start, end);
        } else if ((other.start < start) && (start < other.end) && (other.end <= end)) {
            return new DateRange(start, other.end);
        } else if ((other.start < start) && (start <= end) && (end < other.end)) {
            return this;
        } else if ((start <= other.start) && (other.start <= other.end) && (other.end <= end)) {
            return other;
        }
        return null;
    };
    DateRange.prototype.add = function (other) {
        if (this.overlaps(other)) {
            return new DateRange(moment.min(this.start, other.start), moment.max(this.end, other.end));
        }
        return null;
    };
    DateRange.prototype.subtract = function (other) {
        var start = this.start;
        var end = this.end;
        if (this.intersect(other) === null) {
            return [this];
        } else if ((other.start <= start) && (start < end) && (end <= other.end)) {
            return [];
        } else if ((other.start <= start) && (start < other.end) && (other.end < end)) {
            return [new DateRange(other.end, end)];
        } else if ((start < other.start) && (other.start < end) && (end <= other.end)) {
            return [new DateRange(start, other.start)];
        } else if ((start < other.start) && (other.start < other.end) && (other.end < end)) {
            return [new DateRange(start, other.start), new DateRange(other.end, end)];
        } else if ((start < other.start) && (other.start < end) && (other.end < end)) {
            return [new DateRange(start, other.start), new DateRange(other.start, end)];
        }
    };
    DateRange.prototype.toArray = function (by, exclusive) {
        var acc = [];
        this.by(by, function (unit) {
            acc.push(unit);
        }, exclusive);
        return acc;
    };
    DateRange.prototype.by = function (range, hollaback, exclusive) {
        if (typeof range === 'string') {
            _byString.call(this, range, hollaback, exclusive);
        } else {
            _byRange.call(this, range, hollaback, exclusive);
        }
        return this;
    };

    function _byString(interval, hollaback, exclusive) {
        var current = moment(this.start);
        while (this.contains(current, exclusive)) {
            hollaback.call(this, current.clone());
            current.add(1, interval);
        }
    }

    function _byRange(interval, hollaback, exclusive) {
        var div = this / interval;
        var l = Math.floor(div);
        if (l === Infinity) {
            return;
        }
        if (l === div && exclusive) {
            l--;
        }
        for (var i = 0; i <= l; i++) {
            hollaback.call(this, moment(this.start.valueOf() + interval.valueOf() * i));
        }
    }
    DateRange.prototype.toString = function () {
        return this.start.format() + '/' + this.end.format();
    };
    DateRange.prototype.valueOf = function () {
        return this.end - this.start;
    };
    DateRange.prototype.center = function () {
        var center = this.start + this.diff() / 2;
        return moment(center);
    };
    DateRange.prototype.toDate = function () {
        return [this.start.toDate(), this.end.toDate()];
    };
    DateRange.prototype.isSame = function (other) {
        return this.start.isSame(other.start) && this.end.isSame(other.end);
    };
    DateRange.prototype.diff = function (unit) {
        return this.end.diff(this.start, unit);
    };
    moment.range = function (start, end) {
        if (start in INTERVALS) {
            return new DateRange(moment(this).startOf(start), moment(this).endOf(start));
        } else {
            return new DateRange(start, end);
        }
    };
    moment.range.constructor = DateRange;
    moment.fn.range = moment.range;
    moment.fn.within = function (range) {
        return range.contains(this._d);
    };
    return DateRange;
}));