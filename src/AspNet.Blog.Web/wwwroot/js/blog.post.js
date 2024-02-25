jQuery(function ($) {

	function loadComments(url) {
		$.get(url)
			.done(function (result) {
				$("#load-comments-msg").remove();
				$(result).hide().appendTo("#comments-list").fadeIn();
				//$button.remove();
			})
			.fail(function () {
				alert("Cannot load comments!");
			});
	}

	//load comments
	$('#comments-list').on('click', '[data-action=load-comments]', function (e) {
		var $button = $(this);
		$button.button('loading');

		setTimeout(function () {
			loadComments($button.data('url'));
		}, 2000);
	});

	//submit form
	$('[data-action=add-comment]').on('click', function () {
		$('#form-add-comment').submit();
	});

	// init
	loadComments($('#comments-list').data('url'));

});