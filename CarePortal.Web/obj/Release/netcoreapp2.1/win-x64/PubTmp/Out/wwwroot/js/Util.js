var environment = 1; //1 for Dev, 2 for Prod
var AppUrl = '';
if (environment == 1) {

} else if (environment == 2) {
    AppUrl = '/healthportal';
}

function IsNullOrEmpty(text) {
    return text == null || text == '';
}

function encode(str) {
    var b64 = btoa(unescape(encodeURIComponent(str)));
    return b64;
}

function decode(b64) {
    var str = decodeURIComponent(escape(window.atob(b64)));
    return str;
}

function UpdateDateTimeFormat(datetime) {
    var dateTime = new Date(datetime);
    return moment(dateTime).format("YYYY-MMM-DD HH:mm:ss");
}

function GetDateDifference(startDate, endDate) {
    var start = moment(startDate);
    var end = moment(endDate);

    var duration = moment.duration(end.diff(start));
    var minutes = duration.asMinutes();

    return minutes;
}