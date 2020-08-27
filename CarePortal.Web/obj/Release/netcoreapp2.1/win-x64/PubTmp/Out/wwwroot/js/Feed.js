window.onbeforeunload = function () {
    window.scrollTo(0, 0);
}

$('.summernote').summernote(
    {
        height: 150,   //set editable area's height
        width: '78%',//1215
        codemirror: { // codemirror options
            theme: 'monokai'
        },
        placeholder: "Share something...",
        disableDragAndDrop: false,
        toolbar: [
            ['style', ['style']],
            ['font', ['bold', 'italic', 'underline', 'clear']],
            //['font', ['bold', 'italic', 'underline', 'strikethrough', 'superscript', 'subscript', 'clear']],
            ['fontname', ['fontname']],
            ['fontsize', ['fontsize']],
            ['color', ['color']],
            ['para', ['ul', 'ol', 'paragraph']],
            ['height', ['height']],
            ['table', ['table']],
            ['insert', ['link', 'picture', 'hr']],
            //['view', ['fullscreen', 'codeview']],
            //['help', ['help']]
        ],
    });

$("#btnSubmitPost").click(function () {
    var data = $('#summernote').summernote('code');
    var b64 = encode(data);
    var departmentId = $('#Department').val();
    var dataContract = {
        data: b64,
        departmentId: departmentId,
    };
    $.ajax({
        type: "POST",
        url: AppUrl + "/Feed/AddFeed/",
        dataType: "json",
        data: dataContract,
        success: function (resp) {
            AppendRecentPost(resp.model.feedId, resp.model.data, resp.model.timestamp, UserPicture, 0, 0, true, resp.model.commentsViewModel);
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

$("#btnLoadMore").click(function () {
    var PageNumber = $(this).attr("page-number");
    var PageSize = $(this).attr("page-size");
    $.ajax({
        url: AppUrl + "/Feed/LoadMore/",
        data: { PageNumber: PageNumber, PageSize: PageSize },
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        type: "GET",
        success: function (response) {
            var listFeed = response.model.listFeed;
            if (listFeed.length > 0) {
                jQuery.each(listFeed, function (i, val) {
                    AppendRecentPost(val.feedId, val.data, val.timestamp, val.picture, val.likes, val.dislikes, false, val.commentsViewModel);
                });
                UpdateDateTimeFormat();
                $("#btnLoadMore").attr("page-number", response.model.pageNumber);
                $("#btnLoadMore").attr("page-size", response.model.pageSize);
            }
            else {
                $("#btnLoadMore").hide();
            }
        },
        failure: function (response) {
            console.log("failure");
        },
        error: function (response) {
            console.log("error");
        }
    });
});

function AppendRecentPost(feedId, data, timestamp, picture, likes, dislikes, prepend, comments) {
    if (IsNullOrEmpty(picture))
        picture = "user.jpg";
    var commentHtml = '';
    if (!(typeof comments === 'undefined')) {
        for (i = 0; i < comments.length; i++) {
            var commentItem = comments[i];
            var newComment = '';
            newComment = '<div class="media">'
                + '<a href = "javascript:;" class="pull-left" ><img src="' + commentItem.profilePic + '" class="media-object"></a>'
                + '<div class="media-body">'
                + '<h4 class="text-primary">' + commentItem.userName + '</h4>'
                + '<text class="form-control posted-comment" feed-id="' + commentItem.feedId + '">' + commentItem.comments + '</text>'
                + '<small class="text-muted timestamp">' + commentItem.timestamp + '</small>'
                + '</div>'
                + '</div >';
            commentHtml = commentHtml + newComment;
        }
    }
    var html =
        "<div class='row'>" +
        "<div class='col-sm-8'>" +
        "<div class='panel panel-default panel-timeline feedIdComment_" + feedId + "'>" +
        "<div class='panel-heading'>" +
        "<div class='media'>" +
        "<a href='javascript:;' class='pull-left'>" +
        "<img src='" + picture + "' class='media-object'>" +
        "</a>" +
        "<div class='media-body'>" +
        "<h4 class='text-primary'>" + Username + "</h4>" +
        "<small class='text-muted'>" + timestamp + "</small>" +
        "</div>" +
        "</div>" +
        "</div>" +
        "<div class='panel-body'>" +
        "<div id='content'>" + data + "</div>" +
        "<div class='timeline-btns'>" +
        "<div class='pull-left' id='btnResponse'>" +
        "<a href='javascript:;' class='tooltips like' feed-id=" + feedId + " data-toggle='tooltip' title='Like'><i class='glyphicon glyphicon-thumbs-up'></i></a>" +
        "<a href='javascript:;' class='tooltips dislike' feed-id=" + feedId + " data-toggle='tooltip' title='Dislike'><i class='glyphicon glyphicon-thumbs-down'></i></a>" +
        "</div>" +
        "<div class='pull-right'>" +
        "<small class='text-muted' id='like_" + feedId + "'>" + likes + "</small>" +
        "<small class='text-muted'> people like this - </small> " +
        "<small class='text-muted' id='dislike_" + feedId + "'>" + dislikes + "</small> " +
        "<small class='text-muted'> people dislike this</small > " +
        "</div>" +
        "</div>" +
        "</div>" +
        "<div class='panel-footer'>" +
        "<div class='posted-comments'>" + commentHtml + "</div>" +
        "<div class='media'>" +
        "<a href='javascript:;' class='pull-left'>" +
        "<img src='" + UserPicture + "' class='media-object'>" +
        "</a>" +
        "<div class='media-body'>" +
        "<h4 class='text-primary'>" + Username + "</h4>" +
        "<input type='text' class='form-control user-comment' placeholder='Write a comment' comment-feed-id='" + feedId + "'/>" +
        "</div>" +
        "</div>" +
        "</div>" +
        "</div>" +
        "</div>" +
        "</div>";
    if (prepend) {
        $(html).prependTo("#ListFeed");
    } else {
        $(html).appendTo("#ListFeed");
    }
}

UpdateDateTimeFormat();

function UpdateDateTimeFormat() {
    $(".timestamp").map(function () {
        var dateTime = new Date(this.innerHTML);
        this.innerHTML = moment(dateTime).format("YYYY-MMM-DD HH:mm:ss");
    }).get();
}

//$('#ListFeed').on('click', 'a.like', function (e) {
$('#modalSlideUp').on('click', 'i.like', function (e) {
    var feedId = $(this).attr("feed-id");
    var response = 1;
    //var like = $("#like_" + feedId).text();
    debugger;
    var like = $("#modalLikes").text();
    var newLike = parseInt(like, 10) + 1;
    //$("#like_" + feedId).text(newLike);
    $("#modalLikes").text(newLike);
    responseSubmit = true;
    AddFeedResponse(feedId, response);
});

//$('#ListFeed').on('click', 'a.dislike', function (e) {
$('#modalSlideUp').on('click', 'i.dislike', function (e) {
    debugger;
    var feedId = $(this).attr("feed-id");
    var response = 2;
    //var dislike = $("#dislike_" + feedId).text();
    var dislike = $("#modalDislikes").text();
    var newDislike = parseInt(dislike, 10) + 1;
    //$("#dislike_" + feedId).text(newDislike);
    $("#modalDislikes").text(newDislike);
    responseSubmit = true;
    AddFeedResponse(feedId, response);
});

function AddFeedResponse(feedId, response) {
    var dataContract = {
        feedId: feedId,
        feedResponse: response,
    };
    $.ajax({
        type: "Post",
        url: AppUrl + "/Feed/AddFeedResponse/",
        dataType: "json",
        data: dataContract,
        sucess: function (resp) {
        },
        failure: function (resp) {
            console.log("failure");
        },
        error: function (resp) {
            console.log('Error:' + resp.error);
        }
    });
}

$('#ListFeed').on('keypress', 'input.user-comment', function (e) {
    var keycode = (e.keyCode ? e.keyCode : e.which);
    if (keycode == '13' && !e.shiftKey) {
        var feedId = $(this).attr("comment-feed-id");
        var newComment = $(this).val();
        AddComment(feedId, newComment);
    } else {
        return;
    }
});
$('#modalSlideUp').on('keypress', 'input.user-comment', function (e) {
    var keycode = (e.keyCode ? e.keyCode : e.which);
    if (keycode == '13' && !e.shiftKey) {
        var feedId = $(this).attr("comment-feed-id");
        var newComment = $(this).val();
        AddComment(feedId, newComment);
    } else {
        return;
    }
});

function AddComment(feedId, newComment) {
    var dataComment = {
        feedId: feedId,
        newComment: newComment,
    };
    $.ajax({
        type: "Post",
        url: AppUrl + "/Feed/AddComment/",
        dataType: "json",
        data: dataComment,
        success: function (resp) {
            debugger;
            var feedId = resp.model.feedId;
            var imageURL = resp.imageURL;
            var userName = resp.userName;
            var timestamp = resp.model.timestamp;
            var comment = resp.model.comments;

            var commentHtml = '';
            commentHtml = '<div class="media">'
                + '<a href = "javascript:;" class="pull-left" ><img src="' + imageURL + '" class="media-object"></a>'
                + '<div class="media-body">'
                + '<h4 class="text-primary">' + userName + '</h4>'
                + '<text class="form-control posted-comment" feed-id="' + feedId + '">' + comment + '</text>'
                + '<small class="text-muted timestamp">' + timestamp + '</small>'
                + '</div>'
                + '</div >';
            debugger;
            var newCommentHtml =
                '<div class="form-group form-group-default not-allowed">' +

                '<div class="row padding-10">' +
                '<img src="' + imageURL + '" style="width:30px;height:30px;" class="modalImg"/>' +
                '<label  style="padding-left:10px;">' + userName + '</label>' +
                '</div>' +

                '<p class="font-montserrat" style="font-size:12px!important;">' + comment + '</p>' +
                '</div>';

            //'<div class="user-pic pull-left">' +
            //'<img src="' + imageURL + '" style="width:30px;height:30px" class="modalImg">' +
            //'</div>' +
            //'<p class="font-montserrat bold" style="padding-left:35px!important;padding-top:5px!important;">' + userName + '</p>' +
            //'<div style="border:1px solid #e6e6e6 !important;position:relative;padding-top:20px;">' +
            //'<label class="via posted-comment" feed-id="' + feedId + '">' + comment + '</label><br/>' +
            //'<small class="text-muted timestamp">' + timestamp + '</small> <br/>' +
            //'<br />' +
            //'</div > <br />';



            $(".user-comment").val("");
            //$(".feedIdComment_" + feedId + " div.posted-comments").append(commentHtml);
            $(".posted-comments-modal").append(newCommentHtml);

            UpdateDateTimeFormat();
        },
        failure: function (resp) {
            console.log("failure");
        },
        error: function (resp) {
            console.log("error");
        }
    });

}
$('#ListFeed').on('click', '.feed-description', function (e) {
    var id = $(this).attr('feedIdForModal');
    $(".posted-comments-modal").empty();
    $(".new-comment-modal").empty();
    $('#modalSlideUp i.like').attr("feed-id", id);
    $('#modalSlideUp i.dislike').attr("feed-id", id);
    var finalCommentHtml = '';
    for (m = 0; m < modalListFeed.length; m++) {
        if (modalListFeed[m].feedId == id) {
            if (IsNullOrEmpty(modalListFeed[m].picture))
                $('#userPicModal').attr('src', "~/images/user.jpg");
            else {
                $('#userPicModal').attr('src', modalListFeed[m].picture);
            }
            $("#contentModal").html(modalListFeed[m].data);
            $("#userNameModal span").text(modalListFeed[m].firstName + " " + modalListFeed[m].lastName);
            $("#modalLikes").text(modalListFeed[m].likes);
            $("#modalDislikes").text(modalListFeed[m].dislikes);
            for (i = 0; i < modalListFeed[m].commentsViewModel.length; i++) {
                var commentItem = modalListFeed[m].commentsViewModel[i];
                var commentsHtml =
                    //'<div class="user-pic pull-left">' +
                    //'<img src="' + commentItem.profilePic + '" style="width:30px;height:30px" class="modalImg">' +
                    //'</div>' +
                    //'<p class="font-montserrat bold" style="padding-left:35px!important;padding-top:5px!important;">' + commentItem.userName + '</p>' +
                    //'<div style="border:1px solid #e6e6e6 !important;position:relative;padding-top:20px;">' +
                    //'<label class="via posted-comment" feed-id="' + commentItem.feedId + '">' + commentItem.comments + '</label><br/>' +
                    //'<small class="text-muted timestamp">' + commentItem.timestamp + '</small> <br/>' +
                    //'<br />' +
                    //'</div > <br />';


                    '<div class="form-group form-group-default not-allowed">' +
                    '<div class="row padding-10">' +
                    '<img src="' + commentItem.profilePic + '" style="width:30px;height:30px;" class="modalImg"/>' +
                    //'<p class="font-montserrat bold" style="padding-left:10px;">' + commentItem.userName + '</p></div>' +
                    '<label  style="padding-left:10px;">' + commentItem.userName + '</label>' +
                    '</div>' +
                    //'<div class=""col-md-12>' +
                    //'<label>' + commentItem.comments + '</input>' +
                    '<p class="font-montserrat" style="font-size:12px!important;">' + commentItem.comments + '</p>' +
                    //'</div>' +
                    '</div>';


                finalCommentHtml = finalCommentHtml + commentsHtml;
            }
            var newCommentDiv =
                //'<a href="javascript:;" class="user-pic pull-left">' +
                //'<img src="' + modalListFeed[m].picture + '" style="width:30px;height:30px" class="modalImg" id="UserImage">' +
                //'</a>' +
                //'<p class="font-montserrat bold" style="padding-left:35px!important;padding-top:5px!important;">' + modalListFeed[m].userName + '</p><br />' +
                '<div class="form-group form-group-default">' +
                '<label>' + Username + '</label>' +
                '<input type="text" class="form-control user-comment" placeholder="Write a comment" comment-feed-id="' + id + '" />' +
                '</div>';
            $(".posted-comments-modal").append(finalCommentHtml);
            $(".new-comment-modal").append(newCommentDiv);
        }
    }
    UpdateDateTimeFormat();
    $('#modalSlideUp').modal('show');
});