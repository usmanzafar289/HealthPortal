if (UserRole == "Doctor") { var name = "Patient Name" };
if (UserRole == "Patient") { var name = "Doctor Name" };

var oTable = $('#tableQuestions').DataTable({
    data: [],
    filter: true,
    lengthChange: false,
    paging: true,
    info: false,
    autoWidth: false,
    pageLength: 5,
    aaSorting: [[2, 'desc']],
    columns: [
        { title: name, width: "20%" },
        { title: "Question", width: "50%" },
        { title: "Date", width: "20%" },
        { title: "Action", width: "10%" },
    ]
});

GetQuestions();

function GetQuestions() {
    $.ajax({
        type: "POST",
        url: AppUrl + "/Conversation/GetQuestions/",
        dataType: "json",
        success: function (resp) {
            if (resp.listConversationViewModel != null) {
                AddQuestions(resp.listConversationViewModel);
            }
        },
        failure: function (resp) {
            console.log("failure");
        },
        error: function (resp) {
            console.log("error");
        }
    });
}

function AddQuestions(questions) {
    for (var i = 0; i < questions.length; i++) {
        var item = questions[i];
        var detail = "<button class='btn btn-default'>Detail</button>";
        var userName = null;
        var userId = null;
        if (UserRole == "Doctor") { userName = item.patientName; }
        else if (UserRole == "Patient") { userName = item.doctorName; }
        var rowHtml = '<tr>' +
            '<td>' + userName + '</td>' +
            '<td>' + item.question + '</td>' +
            '<td>' + UpdateDateTimeFormat(item.timestamp) + '</td>' +
            '<td class="detail" question="' + item.question + '" id=' + item.conversationId + ' doctorId=' + item.doctorId + ' patientId=' + item.patientId + '>' + detail + '</td>' +
            '</tr>';
        oTable.row.add($(rowHtml)).draw();
    }
}

$('.summernote').summernote({
    height: 150,   //set editable area's height
    codemirror: { // codemirror options
        theme: 'monokai'
    }
});

var conversationId = null;
var doctorId = null;
var patientId = null;

oTable.on('click', ".detail", function () {
    conversationId = $(this).attr('id');
    doctorId = $(this).attr('doctorId');
    patientId = $(this).attr('patientId');

    var question = $(this).attr('question');
    $("#headerQuestion").text("QUESTION: " + question);
    $("#conversationSection").show();

    GetConversation(conversationId);
});

function GetConversation(questionId) {
    var dataContract = {
        questionId: questionId,
    };
    $.ajax({
        type: "Post",
        url: AppUrl + "/Conversation/GetConversation/",
        dataType: "json",
        data: dataContract,
        success: function (resp) {
            if (resp.listDecodedConversation != null && resp.listDecodedConversation.length > 0) {
                $("#messagesSection").empty();
                AddConversations(resp.listDecodedConversation);
            } else {
            }
        },
        failure: function (resp) {
            console.log("failure");
        },
        error: function (resp) {
            console.log("error");
        }
    });
}

function AddConversations(conversations) {
    for (var i = 0; i < conversations.length; i++) {
        var item = conversations[i];
        AddReply(item.patientName, item.doctorName, item.message, item.messageType, item.timestamp);
    }
}

function AddReply(patientName, doctorName, message, messageType, timestamp) {
    var IsSend = false;
    var username = '';

    if (messageType == 1 && UserRole == "Patient") {
        IsSend = true;
    } else if (messageType == 2 && UserRole == "Doctor") {
        IsSend = true;
    }
    if (messageType == 1) {
        username = patientName;
    } else if (messageType == 2) {
        username = doctorName;
    }

    var html = '';
    if (IsSend) {
        var sendHtml = '<div class="row">' +
            '<div class="col-sm-12">' +
            '<div class="row">' +
            '<div class="col-sm-6"></div>' +
            '<div class="col-sm-6">' +
            '<div class="panel panel-default panel-alt widget-messaging">' +
            '<div class="panel-heading">' +
            '<h3 class="panel-title">' + username + '<small class="pull-right" style="margin-top:3px;">' + UpdateDateTimeFormat(timestamp) + '</small></h3>' +
            '</div>' +
            '<div class="panel-body">' +
            '' + message + '' +
            '</div>' +
            '</div>' +
            '</div>' +
            '</div>' +
            '</div>' +
            '</div>';
        html = sendHtml;
    } else {
        var receiveHtml = '<div class="row">' +
            '<div class="col-sm-12" >' +
            '<div class="row">' +
            '<div class="col-sm-6">' +
            '<div class="panel panel-default panel-alt widget-messaging">' +
            '<div class="panel-heading">' +
            '<h3 class="panel-title">' + username + '<small class="pull-right" style="margin-top:3px;">' + UpdateDateTimeFormat(timestamp) + '</small></h3>' +
            '</div>' +
            '<div class="panel-body">' +
            '' + message + '' +
            '</div>' +
            '</div>' +
            '</div>' +
            '<div class="col-sm-6"></div>' +
            '</div>' +
            '</div>' +
            '</div>';
        html = receiveHtml;
    }

    $("#messagesSection").append(html);
}

var summernoteData = null;
$("#btnSubmitQuestion").click(function () {
    var messageType = null;
    if (UserRole == "Doctor") { messageType = 2; } else { messageType = 1; }

    var summernoteData = $('#summernote').summernote('code');
    var b64 = encode(summernoteData);
    var dataContract = {
        conversationId: conversationId,
        doctorId: doctorId,
        patientId: patientId,
        message: b64,
        messageType: messageType,
        category: 1,
    };
    $.ajax({
        type: "Post",
        url: AppUrl + "/Conversation/AddConversation/",
        dataType: "json",
        data: dataContract,
        success: function (resp) {
            AddReply(resp.conversation.patientName, resp.conversation.doctorName, summernoteData,
                resp.conversation.messageType, resp.conversation.timestamp);
            $('#summernote').summernote('reset');
        },
        failure: function (resp) {
            console.log("failure");
        },
        error: function (resp) {
            console.log("error");
        }
    });
});