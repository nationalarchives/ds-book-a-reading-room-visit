if (window.innerWidth <= 480) {

    var details_elements = document.getElementsByTagName('details');

    Array.prototype.forEach.call(details_elements, function (item) {
        item.removeAttribute('open');
    });
}