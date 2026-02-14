jQuery(function ($) {

    var $inputCategory = $('#inputCategory');
    var $quillHolder = $('#quill-editor');
    var uploadUrl = $quillHolder.data('upload');

    var quill = new Quill('#quill-editor', {
        theme: 'snow',
        placeholder: 'Add content here...',
        modules: {
            toolbar: {
                container: [
                    [{ 'header': [2, 3, 4, false] }],
                    ['bold', 'italic', 'underline', 'strike'],
                    ['blockquote', 'code-block'],
                    [{ 'list': 'ordered' }, { 'list': 'bullet' }],
                    ['link', 'image'],
                    ['clean']
                ],
                handlers: {
                    image: function () {
                        var input = document.createElement('input');
                        input.setAttribute('type', 'file');
                        input.setAttribute('accept', 'image/*');
                        input.click();

                        input.onchange = function () {
                            var file = input.files[0];
                            var formData = new FormData();
                            formData.append('file', file);

                            $.ajax({
                                url: uploadUrl,
                                type: 'POST',
                                data: formData,
                                processData: false,
                                contentType: false,
                                success: function (response) {
                                    var range = quill.getSelection(true);
                                    quill.insertEmbed(range.index, 'image', response.filelink);
                                },
                                error: function () {
                                    alert('Upload failed.');
                                }
                            });
                        };
                    }
                }
            }
        }
    });

    var existingContent = $('#post-body').val();
    if (existingContent) {
        quill.root.innerHTML = existingContent;
    }

    $('#inputTags').tagsInput({
        height: 'auto',
        width: 'auto'
    });

    $inputCategory.autocomplete({
        source: $inputCategory.data('source')
    });

    $('#post-form').on('submit', function (e) {
        e.preventDefault();
        var form = this;
        var html = quill.root.innerHTML;
        $('#post-body').val(html);
        form.submit();
    });

});