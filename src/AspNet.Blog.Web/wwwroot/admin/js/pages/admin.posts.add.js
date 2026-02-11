jQuery(function ($) {

    var $postBody = $('#post-body');
    var $inputCategory = $('#inputCategory');

    $postBody.redactor({
        minHeight: 355,
        placeholder: 'Add content here...',
        imageUpload: $postBody.data('upload')
    });

    $('#inputTags').tagsInput({
        height: 'auto',
        width: 'auto'
    });

    //$inputCategory.autocomplete({
    //    source: $inputCategory.data('source'),
    //    appendTo: $inputCategory.parent()
    //});

    const config = {
        name: "inputCategory",
        data: {
            src: async (query) => {
                try {
                    // Fetch Data from external Source
                    const url = $inputCategory.data('source');
                    const source = await fetch(`${url}?term=${query}`);
                    // Data should be an array of `Objects` or `Strings`
                    const data = await source.json();

                    return data;
                } catch (error) {
                    return error;
                }
            }
        }
    };
    const autoCompleteJS = new autoComplete({ config });

});