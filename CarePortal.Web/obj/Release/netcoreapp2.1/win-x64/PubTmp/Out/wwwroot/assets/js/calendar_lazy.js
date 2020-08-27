/** Event mapping with local variables */
//Event Index:
//event.index
//Event Title:
//event.title
//Event Notes:
//event.other.notes
//DoctorId:
//event.other.doctorId
//PatientId:
//event.other.patientId
//CalendarId:
//event.other.calendarId
//DoctorName:
//event.other.doctorName
//PatientName:
//event.other.patientName
//Status:
//event.other.status

var AJAX = null;
var selectedEvent;
var startTime;
var endTime;
var duration;

var currentDate = moment(new Date()).format("YYYY-MM-DD");
var currentYear = moment().subtract(1, 'year').format('YYYY');
$('#myCalendar').pagescalendar({
    now: currentDate,//"2015-11-23",
    ui: {
        //Year Selector
        year: {
            visible: true,
            format: 'YYYY',
            startYear: currentYear,
            endYear: moment().add(10, 'year').format('YYYY'),
            eventBubble: true
        },
        //Month Selector
        month: {
            visible: true,
            format: 'MMM',
            eventBubble: true
        },
        dateHeader: {
            format: 'MMMM YYYY, D dddd',
            visible: true,
        },
        //Mini Week Day Selector
        week: {
            day: {
                format: 'D'
            },
            header: {
                format: 'dd'
            },
            eventBubble: true,
            startOfTheWeek: '0',
            endOfTheWeek: '6'
        },
        //Week view Grid Options
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
        editable: true,
    },
    view: 'week',
    now: null,
    locale: 'en',
    //Event display time format
    timeFormat: 'h:mm a',
    minTime: 0,
    maxTime: 24,
    dateFormat: 'MMMM Do YYYY',
    slotDuration: '30', //In Mins : supports 15, 30 and 60
    events: [],
    eventOverlap: false,
    weekends: true,
    disableDates: [],
    onViewRenderComplete: function (range) {
        GetEvents();
        //var start = range.start.format();
        //var end = range.end.format();
        //if ($("body").hasClass('pending')) {
        //    return;
        //}
        //$.ajax({
        //    type: "GET",
        //    url: "http://pages.revox.io/json/events.json",
        //    data: "",
        //    success: function (data) {
        //        $("#myCalendar").pagescalendar("setState", "loaded");
        //        $("body").removeClass('pending');
        //        $("#myCalendar").pagescalendar("removeAllEvents");
        //        $("#myCalendar").pagescalendar("addEvents", data);
        //    },
        //    error: function (ajaxContext) {
        //        $("#myCalendar").pagescalendar("error", ajaxContext.status + ": Something horribly went wrong :(");
        //        $("body").removeClass('pending');
        //    }
        //});
    },
    onEventDblClick: function () { },
    onEventClick: function (event) {
        if (userRole == 'Doctor') {
            $('#eventSave').hide();
            $('#eventDelete').hide();
            $('#eventAccept').show();
            $('#eventReject').show();

            $('#divDoctor').hide();
            $('#divPatient').show();
        } else if (userRole == 'Patient') {
            $('#eventSave').show();
            $('#eventDelete').show();
            $('#eventAccept').hide();
            $('#eventReject').hide();

            $('#divDoctor').show();
            $('#divPatient').hide();
        }
        if (!$('#calendar-event').hasClass('open'))
            $('#calendar-event').addClass('open');
        selectedEvent = event;
        setEventDetailsToForm(selectedEvent);
    },
    onEventRender: function () { },
    onEventDragComplete: function (event) {
        selectedEvent = event;
        setEventDetailsToForm(selectedEvent);
    },
    onEventResizeComplete: function (event) {
        selectedEvent = event;
        setEventDetailsToForm(selectedEvent);
    },
    onTimeSlotDblClick: function (timeSlot) {
        if (userRole == 'Doctor') { return; }
        $('#calendar-event').removeClass('open');
        var newEvent = {
            title: '',
            other: {
                notes: '',
                calendarId: 0,
                doctorId: 0,
                patientId: 0,
                doctorName: '',
                patientName: '',
            },
            class: 'bg-warning',
            start: timeSlot.date,
            end: moment(timeSlot.date).add(30, 'minute').format(),
            allDay: false,
        };
        selectedEvent = newEvent;
        $('#myCalendar').pagescalendar('addEvent', newEvent);
        setEventDetailsToForm(selectedEvent);
        $("div.resizable-handle").remove();
    },
    onDateChange: function (range) {
        //$("#myCalendar").pagescalendar("setState", "loaded");
        //var start = range.start.format();
        //var end = range.end.format();
        //if ($("body").hasClass('pending')) {
        //    return;
        //}
        //$("body").addClass('pending');
        //$("#myCalendar").pagescalendar("setState", "loading");
        //$.ajax({
        //    type: "GET",
        //    url: "http://pages.revox.io/json/events.json",
        //    data: "",
        //    success: function (data) {
        //        $("#myCalendar").pagescalendar("setState", "loaded");
        //        $("body").removeClass('pending');
        //        $("#myCalendar").pagescalendar("removeAllEvents");
        //        $("#myCalendar").pagescalendar("addEvents", data);
        //    },
        //    error: function (ajaxContext) {
        //        $("#myCalendar").pagescalendar("error", ajaxContext.status + ": Something horribly went wrong :(");
        //        $("body").removeClass('pending');
        //    }
        //});
    }
});

function setEventDetailsToForm(event) {
    $('#eventIndex').val();
    $('#txtTitle').val();
    $('#txtNote').val();
    $('#drpDoctor').val(null).trigger('change');

    $('#event-date').html(moment(event.start).format('MMM, D dddd'));
    $('#lblfromTime').html(moment(event.start).format('h:mm A'));
    $('#lbltoTime').html(moment(event.end).format('H:mm A'));

    $('#eventIndex').val(event.index);
    $('#txtTitle').val(event.title);
    $('#txtNote').val(event.other.notes);
    $('#drpDoctor').val(event.other.doctorId).trigger('change');

    $('#patientName').text(event.other.patientName);

    duration = GetDateDifference(event.start, event.end);
    startTime = event.start;
    endTime = event.end;

    ShowHideActions(event.other.status);
    ShowStatus(event.other.status);
}

$('#eventSave').on('click', function () {
    selectedEvent.title = $('#txtTitle').val();
    selectedEvent.other.notes = $('#txtNote').val();
    selectedEvent.other.doctorId = $('#drpDoctor').val();
    selectedEvent.other.doctorName = $("#drpDoctor option:selected").text();
    $('#myCalendar').pagescalendar('updateEvent', selectedEvent);
    $('#calendar-event').removeClass('open');

    var item = {};
    item.DoctorId = selectedEvent.other.doctorId;
    item.StartTime = startTime;
    item.EndTime = endTime;
    item.Title = selectedEvent.title;
    item.Notes = selectedEvent.other.notes;
    item.Duration = duration;
    item.Status = 0;
    AddEvent(item);
});

$('#eventDelete').on('click', function () {
    var item = {};
    item.CalendarId = selectedEvent.other.calendarId;
    var eventIndex = $('#eventIndex').val();
    DeleteEvent(item, eventIndex);
});

$('#eventAccept').on('click', function () {
    var item = {};
    item.calendarId = selectedEvent.other.calendarId;
    item.StartTime = startTime;
    item.EndTime = endTime;
    item.Duration = GetDateDifference(item.StartTime, item.EndTime);
    item.Title = selectedEvent.title;
    item.Notes = selectedEvent.other.notes;
    item.Status = 1;
    UdpateEvent(item);
});

$('#eventReject').on('click', function () {
    var item = {};
    item.calendarId = selectedEvent.other.calendarId;
    item.StartTime = startTime;
    item.EndTime = endTime;
    item.Duration = GetDateDifference(item.StartTime, item.EndTime);
    item.Title = selectedEvent.title;
    item.Notes = selectedEvent.other.notes;
    item.Status = 2;
    UdpateEvent(item);
});

function GetEvents() {
    $.ajax({
        type: "POST",
        url: AppUrl + "/Calendar/GetEvents/",
        contentType: "application/json; charset=utf-8",
        cache: false,
        processData: false,
        dataType: "json",
        data: '',
        success: function (resp) {
            var data = resp.eventRoot
            $("#myCalendar").pagescalendar("setState", "loaded");
            $("body").removeClass('pending');
            $("#myCalendar").pagescalendar("removeAllEvents");
            $("#myCalendar").pagescalendar("addEvents", data);
        },
        failure: function (resp) {
            console.log("failure");
        },
        error: function (resp) {
            console.log("error");
        }
    });
}

function AddEvent(item) {
    var data = JSON.stringify(item);
    $.ajax({
        type: "POST",
        url: AppUrl + "/Calendar/AddEvent/",
        contentType: "application/json; charset=utf-8",
        cache: false,
        processData: false,
        dataType: "json",
        data: data,
        success: function (resp) {
            var calendarId = resp.content.model.calendarId;
            selectedEvent.other.calendarId = calendarId;
            var status = resp.content.model.status;
            selectedEvent.other.status = status;
            $('#myCalendar').pagescalendar('updateEvent', selectedEvent);
            alert("Event has been added successfully!");
            ShowHideActions(selectedEvent.other.status)
        },
        failure: function (resp) {
            console.log("failure");
        },
        error: function (resp) {
            console.log("error");
        }
    });
}

function UdpateEvent(item) {
    var data = JSON.stringify(item);
    $.ajax({
        type: "POST",
        url: AppUrl + "/Calendar/UpdateEvent/",
        contentType: "application/json; charset=utf-8",
        cache: false,
        processData: false,
        dataType: "json",
        data: data,
        success: function (resp) {
            var status = resp.content.model.status;
            selectedEvent.other.status = status;
            $('#myCalendar').pagescalendar('updateEvent', selectedEvent);
            alert("Event has been updated successfully!");
            ShowHideActions(status);
            ShowStatus(status);
        },
        failure: function (resp) {
            console.log("failure");
        },
        error: function (resp) {
            console.log("error");
        }
    });
}

function DeleteEvent(item, eventIndex) {
    var data = JSON.stringify(item);
    $.ajax({
        type: "POST",
        url: AppUrl + "/Calendar/DeleteEvent/",
        contentType: "application/json; charset=utf-8",
        cache: false,
        processData: false,
        dataType: "json",
        data: data,
        success: function (resp) {
            $('#myCalendar').pagescalendar('removeEvent', eventIndex);
            $('#calendar-event').removeClass('open');
            alert("Event has been deleted successfully!");
        },
        failure: function (resp) {
            console.log("failure");
        },
        error: function (resp) {
            console.log("error");
        }
    });
}

function ShowHideActions(status) {
    if (status == 1) {
        $('#eventSave').hide();
        $('#eventDelete').hide();
        $('#eventAccept').hide();
        $('#eventReject').hide();
    } else if (status == 2) {
        $('#eventSave').hide();
        if (userRole == 'Doctor') {
            $('#eventDelete').hide();
        } else if (userRole == 'Patient') {
            $('#eventDelete').show();
        }
        $('#eventAccept').hide();
        $('#eventReject').hide();
    }
    else if (status == 0) {
        if (userRole == 'Patient') {
            $('#eventSave').hide();
            $('#eventDelete').show();
        }
    }
}

function ShowStatus(status) {
    if (IsNullOrEmpty(status) && status != 0) {
        $("#divStatus").hide();
        $("#lblStatus").hide();
        return;
    }
    $("#divStatus").show();
    $("#lblStatus").show();
    if (status == 0) {
        $("#divStatus").attr('class', 'alert alert-warning');
        $("#lblStatus").text("Pending");
    }
    else if (status == 1) {
        $("#divStatus").attr('class', 'alert alert-success');
        $("#lblStatus").text("Accepted");
    }
    else if (status == 2) {
        $("#divStatus").attr('class', 'alert alert-danger');
        $("#lblStatus").text("Rejected");
    }
}

