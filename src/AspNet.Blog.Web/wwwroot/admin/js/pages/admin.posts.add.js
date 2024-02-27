jQuery(function ($) {

    var $postBody = $('#post-body');
    var $inputCategory = $('#inputCategory');

    $postBody.redactor({
        minHeight: 355,
        placeholder: 'Add content here...',
        imageUpload: $postBody.data('upload')
    });

    //$('#post-form').validate({
    //    rules: {
    //        title: 'required',
    //        summary: 'required'
    //    }
    //});

    $('#inputTags').tagsInput({
        height: 'auto',
        width: 'auto'
    });

    $inputCategory.autocomplete({
        source: $inputCategory.data('source'),
        appendTo: '#post-form'
    });

});