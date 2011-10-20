$(->
	$('.navigator .column li').live('click', (evt) -> 
		li = $(this) #.parents('li:first')
		li.addClass('active')
		
		col = li.parents('.column:first')
		col.find('.active').not(li).removeClass('active')

		a = li.find('a')
		href = a.attr('href')
		path = a.data('path')
		simpleUrl = window.location.origin + window.location.pathname + "?path=" + path

		window.history.replaceState({path: path}, "", simpleUrl ) if window.history.pushState?

		col.nextAll().remove()
		col.after($('<div class="column unloaded">').css('left', col.offset().left + col.outerWidth()).load(href, -> $(this).removeClass('unloaded')))

		false)
)