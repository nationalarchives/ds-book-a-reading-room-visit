"use strict";

// Creating the home links

$.fn.mega_menu_enhancements = function () {

    // Mega menu button

    $('#mega-menu-pull-down, #mega-menu-mobile').each(function () {
        var $this = $(this);
        $this.on('click', function () {
            $('#nav').slideToggle('fast');
            $this.toggleClass('expanded');
        })
    });

    // Establishing toggle behaviour for links with .toggle-sub-menu

    $(document).on('click', '.toggle-sub-menu', function (e) {
        if ($(window).width() < 480) {
            var $this = $(this);
            e.preventDefault();
            $this.toggleClass('expanded').next().slideToggle('fast');
        }
    });

    // Replacing anchor-only links

    $('.mega-menu a[href="#"]').each(function () {
        var $this = $(this),
            text = $this.text();
        $this.replaceWith($('<div>', {
            'text': text,
            'class': 'toggle-sub-menu',
            'id': 'more-link'
        }));
    });

    return this.each(function () {
        var $this = $(this),
            $items = $this.next(),
            $link,
            $li;

        $this.addClass('toggle-sub-menu');


        $link = $('<a>', {
            'href': $this.attr('href'),
            'text': $this.text() + ' home'
        });

        $li = $('<li class="mobile-home-link">').append($link);

        $li.prependTo($items);

    })
};

$.fn.mega_menu_enhancements();