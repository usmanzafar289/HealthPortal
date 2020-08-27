var emailList = $('[data-email="list"]');
var emailOpened = $('[data-email="opened"]');

var editorTemplate = {
    "font-styles": function (locale) {
        return '<li class="dropdown dropup">' +
            '<a data-toggle="dropdown" class="btn btn-default dropdown-toggle ">' +
            '<span class="glyphicon glyphicon-font"></span>    <span class="current-font">Normal text</span>    <b class="caret"></b>  </a>' +
            '<ul class="dropdown-menu">' +
            '<li><a tabindex="-1" data-wysihtml5-command-value="p" data-wysihtml5-command="formatBlock" href="javascript:;" unselectable="on">Normal text</a></li>' +
            '<li><a tabindex="-1" data-wysihtml5-command-value="h1" data-wysihtml5-command="formatBlock" href="javascript:;" unselectable="on">Heading 1</a></li>' +
            '<li><a tabindex="-1" data-wysihtml5-command-value="h2" data-wysihtml5-command="formatBlock" href="javascript:;" unselectable="on">Heading 2</a></li>' +
            '<li><a tabindex="-1" data-wysihtml5-command-value="h3" data-wysihtml5-command="formatBlock" href="javascript:;" unselectable="on">Heading 3</a></li>' +
            '<li><a tabindex="-1" data-wysihtml5-command-value="h4" data-wysihtml5-command="formatBlock" href="javascript:;" unselectable="on">Heading 4</a></li>' +
            '<li><a tabindex="-1" data-wysihtml5-command-value="h5" data-wysihtml5-command="formatBlock" href="javascript:;" unselectable="on">Heading 5</a></li>' +
            '<li><a tabindex="-1" data-wysihtml5-command-value="h6" data-wysihtml5-command="formatBlock" href="javascript:;" unselectable="on">Heading 6</a></li>' +
            '</ul>' + '</li>';
    },

    emphasis: function (locale) {
        return '<li>' +
            '<div class="btn-group">' +
            '<a tabindex="-1" title="CTRL+B" data-wysihtml5-command="bold" class="btn  btn-default" href="javascript:;" unselectable="on">' +
            '<i class="editor-icon editor-icon-bold"></i></a>' +
            '<a tabindex="-1" title="CTRL+I" data-wysihtml5-command="italic" class="btn  btn-default" href="javascript:;" unselectable="on">' +
            '<i class="editor-icon editor-icon-italic"></i></a>' +
            '<a tabindex="-1" title="CTRL+U" data-wysihtml5-command="underline" class="btn  btn-default" href="javascript:;" unselectable="on">' +
            '<i class="editor-icon editor-icon-underline"></i></a>' +
            '</div>' +
            '</li>';
    },

    blockquote: function (locale) {
        return '<li>' +
            '<a tabindex="-1" data-wysihtml5-display-format-name="false" data-wysihtml5-command-value="blockquote" data-wysihtml5-command="formatBlock" class="btn  btn-default" href="javascript:;" unselectable="on">' +
            '<i class="editor-icon editor-icon-quote"></i>' +
            '</a>' +
            '</li>'
    },

    lists: function (locale) {
        return '<li>' +
            '<div class="btn-group">' +
            '<a tabindex="-1" title="Unordered list" data-wysihtml5-command="insertUnorderedList" class="btn  btn-default" href="javascript:;" unselectable="on">' +
            '<i class="editor-icon editor-icon-ul"></i></a>' +
            '<a tabindex="-1" title="Ordered list" data-wysihtml5-command="insertOrderedList" class="btn  btn-default" href="javascript:;" unselectable="on">' +
            '<i class="editor-icon editor-icon-ol"></i></a>' +
            '<a tabindex="-1" title="Outdent" data-wysihtml5-command="Outdent" class="btn  btn-default" href="javascript:;" unselectable="on">' +
            '<i class="editor-icon editor-icon-outdent"></i></a>' +
            '<a tabindex="-1" title="Indent" data-wysihtml5-command="Indent" class="btn  btn-default" href="javascript:;" unselectable="on">' +
            '<i class="editor-icon editor-icon-indent"></i></a>' +
            '</div>' +
            '</li>'
    },

    image: function (locale) {
        return '<li>' +
            '<div class="bootstrap-wysihtml5-insert-image-modal modal fade">' +
            '<div class="modal-dialog ">' +
            '<div class="modal-content">' +
            '<div class="modal-header">' +
            '<a data-dismiss="modal" class="close">×</a>' +
            '<h3>Insert image</h3>' +
            '</div>' +
            '<div class="modal-body">' +
            '<input class="bootstrap-wysihtml5-insert-image-url form-control" value="http://">' +
            '</div>' + '<div class="modal-footer">' +
            '<a data-dismiss="modal" class="btn btn-default">Cancel</a>' +
            '<a data-dismiss="modal" class="btn btn-primary">Insert image</a>' +
            '</div>' +
            '</div>' +
            '</div>' +
            '</div>' +
            '<a tabindex="-1" title="Insert image" data-wysihtml5-command="insertImage" class="btn  btn-default" href="javascript:;" unselectable="on">' +
            '<i class="editor-icon editor-icon-image"></i>' +
            '</a>' +
            '</li>'
    },

    link: function (locale) {
        return '<li>' +
            '<div class="bootstrap-wysihtml5-insert-link-modal modal fade">' +
            '<div class="modal-dialog ">' +
            '<div class="modal-content">' +
            '<div class="modal-header">' +
            '<a data-dismiss="modal" class="close">×</a>' +
            '<h3>Insert link</h3>' +
            '</div>' +
            '<div class="modal-body">' +
            '<input class="bootstrap-wysihtml5-insert-link-url form-control" value="http://">' +
            '<div class="checkbox check-success"> <input type="checkbox" class="bootstrap-wysihtml5-insert-link-target" checked="checked" value="1" id="link-checkbox"> <label for="link-checkbox">Open link in new window</label></div>' +
            '</div>' + '<div class="modal-footer">' + '<a data-dismiss="modal" class="btn btn-default">Cancel</a>' +
            '<a data-dismiss="modal" class="btn btn-primary" href="#">Insert link</a>' +
            '</div>' +
            '</div>' +
            '</div>' +
            '</div>' +
            '<a tabindex="-1" title="Insert link" data-wysihtml5-command="createLink" class="btn  btn-default" href="javascript:;" unselectable="on">' +
            '<i class="editor-icon editor-icon-link"></i>' +
            '</a>' +
            '</li>'
    }
}

var editorOptions = {
    "font-styles": true,
    "emphasis": true,
    "lists": false,
    "html": false,
    "link": true,
    "image": true,
    "color": false,
    "blockquote": true,
    stylesheets: ["../pages/css/editor.css"],
    customTemplates: editorTemplate
};

$('#mark-email').click(function () {
    $('.item .checkbox').toggle();
    debugger;
});

$('#aSent').click(function () {
    GetEmails("Sent");
});

$('#aInbox').click(function () {
    GetEmails("Inbox");
});

$('.main-menu li').click(function () {
    $('li').removeClass("active");
    $(this).addClass("active");

    var selText = $.trim($(this).text());
    localStorage.setItem('MailType', selText);
});

//$('.main-menu > li > a').click(function () {
//    $('li').removeClass();
//    $(this).parent().addClass('active');
//});

GetEmails("Inbox");

function GetEmails(emailType) {

    var body = {
        MailType: emailType
    };

    emailList.length && $.ajax({
        type: "Post",
        dataType: "json",
        url: AppUrl + "/Email/GetEmails/",
        data: body,

        success: function (data) {
            // $.each(data, function (i)
            // {

            $(".list-view-group-container").empty();
            $("#txtUM").empty();

            $('.no-result').show();
            $('.actions-dropdown').toggle();
            $('.actions, .email-content-wrapper').hide();

            debugger;
            if (data.userRole == "Doctor") {
                $("#btnCompose").css("display", "none");
            }
            else {
                $("#btnCompose").css("display", "block");
            }

            if (data.emails != null) {

                var obj = data.emails;
                //var group = obj.group;
                var list = obj;

                if (list.length > 0) {
                    if (emailType == "Inbox") {
                        $("#txtUM").append(list[0].unreadMail);
                    }
                }

                localStorage.setItem('Email', JSON.stringify(data.emails));

                var listViewGroupCont = $('<div/>', { "class": "list-view-group-container" });
                listViewGroupCont.append('<div class="list-view-group-header"><span>' + "Today" + " " + dateFormat(new Date(), "dddd, d") + '</span></div>');

                var ul = $('<ul/>', { "class": "no-padding" });

                $.each(list, function (j) {
                    var $this = list[j];
                    var id = $this.emailId;
                    var dp = $this.picture;
                    var dpRetina = $this.picture;
                    var to = $this.emailAddress;
                    var subject = $this.subject;
                    var body = $this.body;//.replace(/<(?:.|\n)*?>/gm, '');
                    var time = $this.timestamp;
                    var name = $this.firstName + ' ' + $this.lastName;
                    var isRead = $this.isRead;

                    if (IsNullOrEmpty(dp)) {
                        dp = "../images/user.jpg";
                        dpRetina = "../images/user.jpg";
                    }

                    var li = '<li class="item padding-15" data-email-id="' + id + '"> \
                                <div class="thumbnail-wrapper d32 circular"> \
                                    <img width="40" height="40" alt=""  data-src-retina="'+ dpRetina + '" data-src="' + dp + '" src="' + dpRetina + '"> \
                                </div> \
                                <div class="checkbox  no-margin p-l-10"> \
                                    <input type="checkbox" value="1" id="emailcheckbox-'+ j + '"> \
                                    <label for="emailcheckbox-'+ j + '"></label> \
                                </div> ';
                    if (isRead == 0) {
                        li = li + '<div class="inline m-l-15"> \
                                    <p class="recipients no-margin hint-text small boldName" id= "NR'+ id + '">' + name + '</p> \
                                    <p class="subject no-margin boldSubject" id= "SR'+ id + '">' + subject + '</p> \
                                    <p class="body no-margin"  > \
                                     <strong>'+ body + '</strong> \
                                    </p> \
                                </div > ';
                    }
                    else {
                        li = li + '<div class="inline m-l-15"> \
                                    <p class="recipients no-margin hint-text small" id="NR'+ id + '">' + name + '</p> \
                                    <p class="subject no-margin" id= "SR'+ id + '">' + subject + '</p> \
                                    <p class="body no-margin"> \
                                     '+ body + ' \
                                    </p> \
                                </div > ';
                    }

                    li = li + '<div class="datetime">' + dateFormat(time, "ddd, d, yyyy, h:MM TT") + '</div> \
                                <div class="clearfix"></div> \
                            </li>';
                    ul.append(li);
                });
                listViewGroupCont.append(ul); emailList.append(listViewGroupCont);
                // });
                emailList.ioslist();
            }
        },
        failure: function (resp) {
            console.log("failure: " + resp);
        },
        error: function (resp) {
            console.log("error: " + resp);
        }
    });
}

$('body').on('click', '.item .checkbox', function (e) {
    e.stopPropagation();
    debugger;
});

function ReadMail() {
    var id = localStorage.getItem("SelEmailId");
    var emails = JSON.parse(localStorage.getItem('Email'));

    var obj = emails;
    var list = obj;
    var email1 = null;

    $.each(list, function (j) {
        if (list[j].emailId == id) {
            email1 = list[j]; return;
        }
    });

    $.ajax({
        type: "Post",
        dataType: "json",
        url: AppUrl + "/Email/ReadEmail/",
        data: email1,

        success: function (data) {
            console.log("Read");
            //GetEmails(localStorage.getItem('MailType'));
        },
        failure: function (resp) {
            console.log("failure: " + resp);
        },
        error: function (resp) {
            console.log("error: " + resp);
        }
    });
}

$('body').on('click', '.rply', function (e) {

    var comment = $.trim($("#repyTxt").val());

    if (comment != "") {
        var id = localStorage.getItem("SelEmailId");
        var emails = JSON.parse(localStorage.getItem('Email'));

        var obj = emails;
        var list = obj;
        var email1 = null;

        $.each(list, function (j) {
            if (list[j].emailId == id) {
                email1 = list[j]; return;
            }
        });

        email1.body = comment;
        var EmailViewModel = {
            body: comment
        };

        $.ajax({
            type: "Post",
            dataType: "json",
            url: AppUrl + "/Email/EmailReply/",
            data: email1,

            success: function (data) {
                alert("Email Sent!");
                $("#repyTxt").val('');
                GetEmails(localStorage.getItem('MailType'));
            },
            failure: function (resp) {
                console.log("failure: " + resp);
            },
            error: function (resp) {
                console.log("error: " + resp);
            }
        });
    }
});

$('body').on('click', '.ERead', function (e) {

    var id = localStorage.getItem("SelEmailId");
    var emails = JSON.parse(localStorage.getItem('Email'));

    var obj = emails;
    var list = obj;
    var email1 = null;

    $.each(list, function (j) {
        if (list[j].emailId == id) {
            email1 = list[j]; return;
        }
    });

    $.ajax({
        type: "Post",
        dataType: "json",
        url: AppUrl + "/Email/ReadEmail/",
        data: email1,

        success: function (data) {
            console.log("Read");
            GetEmails(localStorage.getItem('MailType'));
        },
        failure: function (resp) {
            console.log("failure: " + resp);
        },
        error: function (resp) {
            console.log("error: " + resp);
        }
    });

});

$('body').on('click', '.EDelete', function (e) {

    var id = localStorage.getItem("SelEmailId");
    var emails = JSON.parse(localStorage.getItem('Email'));

    var obj = emails;
    var list = obj;
    var email1 = null;

    $.each(list, function (j) {
        if (list[j].emailId == id) {
            email1 = list[j]; return;
        }
    });

    $.ajax({
        type: "Post",
        dataType: "json",
        url: AppUrl + "/Email/DeleteEmail/",
        data: email1,

        success: function (data) {
            alert("Mail Deleted.");
            console.log("Delete");
            GetEmails(localStorage.getItem('MailType'));
        },
        failure: function (resp) {
            console.log("failure: " + resp);
        },
        error: function (resp) {
            console.log("error: " + resp);
        }
    });
});

$('body').on('click', '.item', function (e) {
    e.stopPropagation();

    var id = $(this).attr('data-email-id');
    var email = null;
    var thumbnailWrapper = $(this).find('.thumbnail-wrapper');

    localStorage.setItem("SelEmailId", id);

    var emails = JSON.parse(localStorage.getItem('Email'));

    var obj = emails;
    var list = obj;

    $.each(list, function (j) {
        if (list[j].emailId == id) {
            email = list[j]; return;
        }
    });

    debugger;

    if (email.isRead == 0) {
        $('#SR' + id).removeClass('boldSubject');
        $('#NR' + id).removeClass('boldName');
    }

    if (email == null) return;

    emailOpened.find('.sender .name').text(email.firstName + ' ' + email.lastName);
    emailOpened.find('.sender .datetime').text(dateFormat(email.timestamp, "ddd, d, yyyy, h:MM TT"));
    emailOpened.find('.subject').text(email.subject);

    emailOpened.find('.email-content-body').html(email.body);

    var thumbnailClasses = thumbnailWrapper.attr('class').replace('d32', 'd48');
    emailOpened.find('.thumbnail-wrapper').html(thumbnailWrapper.html()).attr('class', thumbnailClasses);

    $('.no-result').hide();
    $('.actions-dropdown').toggle();
    $('.actions, .email-content-wrapper').show();

    if ($.Pages.isVisibleSm() || $.Pages.isVisibleXs()) {
        $('.split-list').toggleClass('slideLeft');
    }

    !$('.email-reply').data('wysihtml5') && $('.email-reply').wysihtml5(editorOptions);
    $(".email-content-wrapper").scrollTop(0); $('.menuclipper').menuclipper({ bufferWidth: 50 });

    $('.item').removeClass('active');
    $(this).addClass('active');

    ReadMail();

    //$.ajax({
    //    dataType: "json",
    //    url: "http://pages.revox.io/json/emails.json",
    //    success: function (data)
    //    {
    //        debugger;
    //        $.each(data.emails, function (i)
    //        {
    //            var obj = data.emails[i];
    //            var list = obj.list;

    //            $.each(list, function (j) {
    //                if (list[j].id == id)
    //                {
    //                    email = list[j]; return;
    //                }
    //            });

    //            if (email != null) return;
    //        });

    //        emailOpened.find('.sender .name').text(email.from);
    //        emailOpened.find('.sender .datetime').text(email.datetime);
    //        emailOpened.find('.subject').text(email.subject);
    //        emailOpened.find('.email-content-body').html(email.body);
    //        var thumbnailClasses = thumbnailWrapper.attr('class').replace('d32', 'd48');
    //        emailOpened.find('.thumbnail-wrapper').html(thumbnailWrapper.html()).attr('class', thumbnailClasses);
    //        $('.no-result').hide(); $('.actions-dropdown').toggle();
    //        $('.actions, .email-content-wrapper').show();

    //        if ($.Pages.isVisibleSm() || $.Pages.isVisibleXs()) {
    //            $('.split-list').toggleClass('slideLeft');
    //        }

    //        !$('.email-reply').data('wysihtml5') && $('.email-reply').wysihtml5(editorOptions);
    //        $(".email-content-wrapper").scrollTop(0); $('.menuclipper').menuclipper({ bufferWidth: 50 });
    //    }
    //});

    //$('.item').removeClass('active');
    //$(this).addClass('active');
});

$('.toggle-secondary-sidebar').click(function (e) {
    e.stopPropagation(); $('.secondary-sidebar').toggle();
});

$('.split-list-toggle').click(function () {
    $('.split-list').toggleClass('slideLeft');
});

$('.secondary-sidebar').click(function (e) {
    e.stopPropagation();
})

$(window).resize(function () {
    if ($(window).width() <= 1024) {
        $('.secondary-sidebar').hide();
    } else {
        $('.split-list').length && $('.split-list').removeClass('slideLeft'); $('.secondary-sidebar').show();
    }
});

var emailComposerToolbarTemplate = {
    "font-styles": function (locale) {
        return '<li class="dropdown">' +
            '<a data-toggle="dropdown" class="btn btn-default dropdown-toggle ">' +
            '<span class="editor-icon editor-icon-headline"></span>' +
            '<span class="current-font">Normal</span>' +
            '<b class="caret"></b>' +
            '</a>' +
            '<ul class="dropdown-menu">' +
            '<li><a tabindex="-1" data-wysihtml5-command-value="p" data-wysihtml5-command="formatBlock" href="javascript:;" unselectable="on">Normal</a></li>' +
            '<li><a tabindex="-1" data-wysihtml5-command-value="h1" data-wysihtml5-command="formatBlock" href="javascript:;" unselectable="on">1</a></li>' +
            '<li><a tabindex="-1" data-wysihtml5-command-value="h2" data-wysihtml5-command="formatBlock" href="javascript:;" unselectable="on">2</a></li>' +
            '<li><a tabindex="-1" data-wysihtml5-command-value="h3" data-wysihtml5-command="formatBlock" href="javascript:;" unselectable="on">3</a></li>' +
            '<li><a tabindex="-1" data-wysihtml5-command-value="h4" data-wysihtml5-command="formatBlock" href="javascript:;" unselectable="on">4</a></li>' +
            '<li><a tabindex="-1" data-wysihtml5-command-value="h5" data-wysihtml5-command="formatBlock" href="javascript:;" unselectable="on">5</a></li>' +
            '<li><a tabindex="-1" data-wysihtml5-command-value="h6" data-wysihtml5-command="formatBlock" href="javascript:;" unselectable="on">6</a></li>' +
            '</ul>' + '</li>';
    },

    emphasis: function (locale) {
        return '<li>' +
            '<div class="btn-group">' +
            '<a tabindex="-1" title="CTRL+B" data-wysihtml5-command="bold" class="btn  btn-default" href="javascript:;" unselectable="on"><i class="editor-icon editor-icon-bold"></i></a>' +
            '<a tabindex="-1" title="CTRL+I" data-wysihtml5-command="italic" class="btn  btn-default" href="javascript:;" unselectable="on"><i class="editor-icon editor-icon-italic"></i></a>' +
            '<a tabindex="-1" title="CTRL+U" data-wysihtml5-command="underline" class="btn  btn-default" href="javascript:;" unselectable="on"><i class="editor-icon editor-icon-underline"></i></a>' +
            '</div>' +
            '</li>';
    },

    blockquote: function (locale) {
        return '<li>' +
            '<a tabindex="-1" data-wysihtml5-display-format-name="false" data-wysihtml5-command-value="blockquote" data-wysihtml5-command="formatBlock" class="btn  btn-default" href="javascript:;" unselectable="on">' +
            '<i class="editor-icon editor-icon-quote"></i>' +
            '</a>' +
            '</li>'
    },

    lists: function (locale) {
        return '<li>' +
            '<div class="btn-group">' +
            '<a tabindex="-1" title="Unordered list" data-wysihtml5-command="insertUnorderedList" class="btn  btn-default" href="javascript:;" unselectable="on"><i class="editor-icon editor-icon-ul"></i></a>' +
            '<a tabindex="-1" title="Ordered list" data-wysihtml5-command="insertOrderedList" class="btn  btn-default" href="javascript:;" unselectable="on"><i class="editor-icon editor-icon-ol"></i></a>' +
            '<a tabindex="-1" title="Outdent" data-wysihtml5-command="Outdent" class="btn  btn-default" href="javascript:;" unselectable="on"><i class="editor-icon editor-icon-outdent"></i></a>' +
            '<a tabindex="-1" title="Indent" data-wysihtml5-command="Indent" class="btn  btn-default" href="javascript:;" unselectable="on"><i class="editor-icon editor-icon-indent"></i></a>' +
            '</div>' +
            '</li>'
    },

    image: function (locale) {
        return '<li>' +
            '<div class="bootstrap-wysihtml5-insert-image-modal modal fade">' +
            '<div class="modal-dialog ">' +
            '<div class="modal-content">' +
            '<div class="modal-header">' +
            '<a data-dismiss="modal" class="close">×</a>' +
            '<h3>Insert image</h3>' +
            '</div>' +
            '<div class="modal-body">' +
            '<input class="bootstrap-wysihtml5-insert-image-url form-control" value="http://">' +
            '</div>' + '<div class="modal-footer">' +
            '<a data-dismiss="modal" class="btn btn-default">Cancel</a>' +
            '<a data-dismiss="modal" class="btn btn-primary">Insert image</a>' +
            '</div>' +
            '</div>' +
            '</div>' +
            '</div>' +
            '<a tabindex="-1" title="Insert image" data-wysihtml5-command="insertImage" class="btn  btn-default" href="javascript:;" unselectable="on">' +
            '<i class="editor-icon editor-icon-image"></i>' +
            '</a>' +
            '</li>'
    },

    link: function (locale) {
        return '<li>' +
            '<div class="bootstrap-wysihtml5-insert-link-modal modal fade">' +
            '<div class="modal-dialog ">' +
            '<div class="modal-content">' +
            '<div class="modal-header">' +
            '<a data-dismiss="modal" class="close">×</a>' +
            '<h3>Insert link</h3>' +
            '</div>' +
            '<div class="modal-body">' +
            '<input class="bootstrap-wysihtml5-insert-link-url form-control" value="http://">' +
            '<label class="checkbox"> <input type="checkbox" checked="" class="bootstrap-wysihtml5-insert-link-target">Open link in new window</label>' +
            '</div>' +
            '<div class="modal-footer">' +
            '<a data-dismiss="modal" class="btn btn-default">Cancel</a>' +
            '<a data-dismiss="modal" class="btn btn-primary" href="#">Insert link</a>' +
            '</div>' +
            '</div>' +
            '</div>' +
            '</div>' +
            '<a tabindex="-1" title="Insert link" data-wysihtml5-command="createLink" class="btn  btn-default" href="javascript:;" unselectable="on">' +
            '<i class="editor-icon editor-icon-link"></i>' +
            '</a>' +
            '</li>'
    },

    html: function (locale) {
        return '<li>' +
            '<div class="btn-group">' +
            '<a tabindex="-1" title="Edit HTML" data-wysihtml5-action="change_view" class="btn  btn-default" href="javascript:;" unselectable="on">' +
            '<i class="editor-icon editor-icon-html"></i>' +
            '</a>' +
            '</div>' +
            '</li>'
    }
}

setTimeout(function () {
    $('.email-body').length && $('.email-body').wysihtml5({
        html: true,
        stylesheets: ["../pages/css/editor.css"],
        customTemplates: emailComposerToolbarTemplate
    });
    $('.email-composer .wysihtml5-toolbar').appendTo('.email-toolbar-wrapper');
}, 500);

//=======================================[Model]===============================================

$('#btnCompose').click(function () {
    GetDoctors();
    var size = $('input[name=slideup_toggler]:checked').val()
    var modalElem = $('#modalSlideUp');
    if (size == "mini") {
        $('#modalSlideUpSmall').modal('show')
    } else {
        $('#modalSlideUp').modal('show')
        if (size == "default") {
            modalElem.children('.modal-dialog').removeClass('modal-lg');
        }
        else if (size == "full") {
            modalElem.children('.modal-dialog').addClass('modal-lg');
        }
    }
});

$('#btnEmailSend').click(function () {
    SendEmail();
});

function GetDoctors() {

    $.ajax({
        type: "Get",
        dataType: "json",
        url: AppUrl + "/Email/GetDoctors/",

        success: function (data) {
            debugger;
            var sel = $('#selDoctors');
            for (var i = 0; i < data.doctors.listDoctorsItems.length; i++) {
                var e = data.doctors.listDoctorsItems[i];
                $('<option>').text(e.text).val(e.value).appendTo(sel);
            }
        },
        failure: function (resp) {
            console.log("failure: " + resp);
        },
        error: function (resp) {
            console.log("error: " + resp);
        }
    });

}

function SendEmail() {
    debugger;
    var composeComment = $.trim($("#composeTextArea").val());
    var doctorId = $("#selDoctors").val();
    var subject = $.trim($("#subject").val());

    if (subject == "") {
        alert("Please enter subject of mail.");
        return;
    }

    if (composeComment != "") {

        var email = {
            PatientId: null,
            DoctorId: doctorId,
            Subject: subject,
            body: composeComment,
            EmailType: 1
        };

        $.ajax({
            type: "Post",
            dataType: "json",
            url: AppUrl + "/Email/SendEmail/",
            data: email,

            success: function (data) {
                alert("Email Sent!");
                $("#composeTextArea").val('');
                $("#subject").val('');                
                $('#modalSlideUp').modal('hide');

                //this.GetEmails();
            },
            failure: function (resp) {
                console.log("failure: " + resp);
            },
            error: function (resp) {
                console.log("error: " + resp);
            }
        });
    }
    else {
        alert("Please insert text in mail body.");
        return;
    }

}

//===================================[DATETIME Format]=========================================

var dateFormat = function () {
    var token = /d{1,4}|m{1,4}|yy(?:yy)?|([HhMsTt])\1?|[LloSZ]|"[^"]*"|'[^']*'/g,
        timezone = /\b(?:[PMCEA][SDP]T|(?:Pacific|Mountain|Central|Eastern|Atlantic) (?:Standard|Daylight|Prevailing) Time|(?:GMT|UTC)(?:[-+]\d{4})?)\b/g,
        timezoneClip = /[^-+\dA-Z]/g,
        pad = function (val, len) {
            val = String(val);
            len = len || 2;
            while (val.length < len) val = "0" + val;
            return val;
        };

    // Regexes and supporting functions are cached through closure
    return function (date, mask, utc) {
        var dF = dateFormat;

        // You can't provide utc if you skip other args (use the "UTC:" mask prefix)
        if (arguments.length == 1 && Object.prototype.toString.call(date) == "[object String]" && !/\d/.test(date)) {
            mask = date;
            date = undefined;
        }

        // Passing date through Date applies Date.parse, if necessary
        date = date ? new Date(date) : new Date;
        if (isNaN(date)) throw SyntaxError("invalid date");

        mask = String(dF.masks[mask] || mask || dF.masks["default"]);

        // Allow setting the utc argument via the mask
        if (mask.slice(0, 4) == "UTC:") {
            mask = mask.slice(4);
            utc = true;
        }

        var _ = utc ? "getUTC" : "get",
            d = date[_ + "Date"](),
            D = date[_ + "Day"](),
            m = date[_ + "Month"](),
            y = date[_ + "FullYear"](),
            H = date[_ + "Hours"](),
            M = date[_ + "Minutes"](),
            s = date[_ + "Seconds"](),
            L = date[_ + "Milliseconds"](),
            o = utc ? 0 : date.getTimezoneOffset(),
            flags = {
                d: d,
                dd: pad(d),
                ddd: dF.i18n.dayNames[D],
                dddd: dF.i18n.dayNames[D + 7],
                m: m + 1,
                mm: pad(m + 1),
                mmm: dF.i18n.monthNames[m],
                mmmm: dF.i18n.monthNames[m + 12],
                yy: String(y).slice(2),
                yyyy: y,
                h: H % 12 || 12,
                hh: pad(H % 12 || 12),
                H: H,
                HH: pad(H),
                M: M,
                MM: pad(M),
                s: s,
                ss: pad(s),
                l: pad(L, 3),
                L: pad(L > 99 ? Math.round(L / 10) : L),
                t: H < 12 ? "a" : "p",
                tt: H < 12 ? "am" : "pm",
                T: H < 12 ? "A" : "P",
                TT: H < 12 ? "AM" : "PM",
                Z: utc ? "UTC" : (String(date).match(timezone) || [""]).pop().replace(timezoneClip, ""),
                o: (o > 0 ? "-" : "+") + pad(Math.floor(Math.abs(o) / 60) * 100 + Math.abs(o) % 60, 4),
                S: ["th", "st", "nd", "rd"][d % 10 > 3 ? 0 : (d % 100 - d % 10 != 10) * d % 10]
            };

        return mask.replace(token, function ($0) {
            return $0 in flags ? flags[$0] : $0.slice(1, $0.length - 1);
        });
    };
}();

// Some common format strings
dateFormat.masks = {
    "default": "ddd mmm dd yyyy HH:MM:ss",
    shortDate: "m/d/yy",
    mediumDate: "mmm d, yyyy",
    longDate: "mmmm d, yyyy",
    fullDate: "dddd, mmmm d, yyyy",
    shortTime: "h:MM TT",
    mediumTime: "h:MM:ss TT",
    longTime: "h:MM:ss TT Z",
    isoDate: "yyyy-mm-dd",
    isoTime: "HH:MM:ss",
    isoDateTime: "yyyy-mm-dd'T'HH:MM:ss",
    isoUtcDateTime: "UTC:yyyy-mm-dd'T'HH:MM:ss'Z'"
};

// Internationalization strings
dateFormat.i18n = {
    dayNames: [
        "Sun", "Mon", "Tue", "Wed", "Thu", "Fri", "Sat",
        "Sunday", "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday"
    ],
    monthNames: [
        "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec",
        "January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December"
    ]
};

// For convenience...
Date.prototype.format = function (mask, utc) {
    return dateFormat(this, mask, utc);
};
