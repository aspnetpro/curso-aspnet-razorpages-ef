jQuery(function ($) {

	function loadComments(url) {
		$.get(url)
			.done(function (result) {
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

	loadComments($('#comments-list').data('url'));

});