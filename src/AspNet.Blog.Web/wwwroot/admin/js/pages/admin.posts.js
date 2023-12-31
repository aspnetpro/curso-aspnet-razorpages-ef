jQuery(function ($) {

    var $postTable = $('#posts-table');

    $postTable.dataTable({
        "bProcessing": true,
        "bServerSide": true,
        "bAutoWidth": false,
        "sAjaxSource": $postTable.attr('data-ajax-source'),
        "aoColumns": [
            {
                "sTitle": "Title",
                "mDataProp": "title",
                "bSortable": false
            },
            {
                "sTitle": "Published On",
                "mDataProp": "publishedOn",
                "sWidth": "150px",
                "bSortable": false
            },
            {
                "sTitle": "",
                "mDataProp": "",
                "sWidth": "150px",
                "bSortable": false,
                "mRender": function (innerData, sSpecific, oData) {
                    var render = '<a class="btn btn-primary btn-sm" href="' + oData.editUrl + '">Edit</a>';
                    render += '&nbsp;';
                    render += '<a class="btn btn-danger btn-sm" href="' + oData.deleteUrl + '">Delete</a>';
                    return render;
                }
            }
        ]
    });

});