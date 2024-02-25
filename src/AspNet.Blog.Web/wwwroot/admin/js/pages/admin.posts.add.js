jQuery(function ($) {

    var $postBody = $('#post-body');
    var $inputCategory = $('#inputCategory');

    $postBody.redactor({
        minHeight: 355,
        placeholder: 'Add content here...',
        imageUpload: $postBody.attr('data-upload')
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
        source: $inputCategory.attr('data-ajax-source')
    });

});