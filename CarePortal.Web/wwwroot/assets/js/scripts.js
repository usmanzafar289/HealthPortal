$("#docSearchResults").on('click', '.doc-search-clickable-row', function () {
    debugger;
    var source = $(this).data("href");
    window.location = source;
});
// Initializes search overlay plugin.
// Replace onSearchSubmit() and onKeyEnter() with 
// your logic to perform a search and display results
//SearchDoctors("");
function SearchDoctors(searchText) {
    var allDoctors = {};
    var data = {
        searchText: searchText,
    };
    $.ajax({
        type: "POST",
        url: AppUrl + "/Search/SearchDoctors/",
        dataType: "json",
        data: data,
        success: function (resp) {
            debugger;
            allDoctors = resp.doctorsList;
            AddDoctors(allDoctors);
        },
        failure: function (resp) {
            debugger;
            console.log("failure");
        },
        error: function (resp) {
            debugger;
            console.log("error");
        }
    });
}
function AddDoctors(allDoctors) {
    $("#docSearchResults").empty();
    var finalDocDiv = '';
    for (var i = 0; i < allDoctors.length; i++) {
        var docItem = allDoctors[i];
        var docImage;
        if (IsNullOrEmpty(docItem.imageURL)) {
            docImage = "../images/user.jpg";
        }
        else {
            docImage = docItem.imageURL;
        }
        var docDiv = '<div class="col-md-6">' +
            '<div id="' + docItem.emailId + '" class="doc-search-clickable-row" data-href="' + AppUrl + '/Profile/Index?userId=' + docItem.userId + '">' +
            '<div>' +
            '<div class="thumbnail-wrapper d48 circular bg-success text-white inline m-t-10">' +
            '<div>' +
            '<img width="54" height="54" src="' + docImage + '" alt="">' +
            '</div>' +
            '</div>' +
            '<div class="p-l-10 inline p-t-5">' +
            '<h5 class="m-b-5"><span class="semi-bold result-name">' + docItem.firstName + ' ' + docItem.lastName + '</span></h5>' +
            '<p class="hint-text">' + docItem.emailId + '</p>' +
            '</div>' +
            '</div>' +
            '</div>' +
            '</div>';

        finalDocDiv = finalDocDiv + docDiv;
    }
    $("#docSearchResults").append(finalDocDiv);
}
$(".list-view-wrapper").scrollbar();

$('[data-pages="search"]').search({
    // Bind elements that are included inside search overlay
    searchField: '#overlay-search',
    closeButton: '.overlay-close',
    suggestions: '#overlay-suggestions',
    brand: '.brand',
    // Callback that will be run when you hit ENTER button on search box
    onSearchSubmit: function (searchString) {
        debugger;
        console.log("Search for: " + searchString);
        var searchField = $('#overlay-search');
        var searchResults = $('.search-results');
        SearchDoctors(searchString);
    },
    // Callback that will be run whenever you enter a key into search box. 
    // Perform any live search here.  
    //onKeyEnter: function (searchString) {
    //    console.log("Live search for: " + searchString);
    //    var searchField = $('#overlay-search');
    //    var searchResults = $('.search-results');
    //    SearchDoctors(searchField.val());
    //    /* 
    //        Do AJAX call here to get search results
    //        and update DOM and use the following block 
    //        'searchResults.find('.result-name').each(function() {...}'
    //        inside the AJAX callback to update the DOM
    //    */

    //    // Timeout is used for DEMO purpose only to simulate an AJAX call
    //    //clearTimeout($.data(this, 'timer'));
    //    //searchResults.fadeOut("fast"); // hide previously returned results until server returns new results
    //    //var wait = setTimeout(function () {

    //    //    searchResults.find('.result-name').each(function () {
    //    //        if (searchField.val().length != 0) {
    //    //            $(this).html(searchField.val());
    //    //            searchResults.fadeIn("fast"); // reveal updated results
    //    //        }
    //    //    });
    //    //}, 500);
    //    //$(this).data('timer', wait);
    //    searchResults.fadeOut("fast");
    //}
})


$('.panel-collapse label').on('click', function (e) {
    e.stopPropagation();
});