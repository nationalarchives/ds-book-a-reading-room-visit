var manage_details_elements = function () {

    var details_elements = document.getElementsByTagName('details'),
        summary_elements = document.getElementsByTagName('summary');

    if (window.innerWidth <= 480) {
        Array.prototype.forEach.call(details_elements, function (item) {
            item.removeAttribute('open');
            item.removeAttribute('tabindex');
        });
    } else {
        Array.prototype.forEach.call(details_elements, function (item) {
            item.setAttribute('open', '');

            Array.prototype.forEach.call(summary_elements, function (item) {
                item.setAttribute('tabindex', '-1');
            });
        });
    }
};

window.addEventListener('DOMContentLoaded', manage_details_elements);
window.addEventListener('resize', manage_details_elements);

document.addEventListener('click', function (event) {
    if (event.target.nodeName === 'SUMMARY' && window.innerWidth > 480) {
        event.preventDefault();
    }
});