(function(){function r(e,n,t){function o(i,f){if(!n[i]){if(!e[i]){var c="function"==typeof require&&require;if(!f&&c)return c(i,!0);if(u)return u(i,!0);var a=new Error("Cannot find module '"+i+"'");throw a.code="MODULE_NOT_FOUND",a}var p=n[i]={exports:{}};e[i][0].call(p.exports,function(r){var n=e[i][1][r];return o(n||r)},p,p.exports,r,e,n,t)}return n[i].exports}for(var u="function"==typeof require&&require,i=0;i<t.length;i++)o(t[i]);return o}return r})()({1:[function(require,module,exports){
"use strict";

var browser_can_print = typeof window.print === 'function';
var on_order_summary_page = !!document.querySelector('.order-summary');
var mount_node_exists = !!document.getElementById('print-your-order');

var add_print_button = function add_print_button() {
  var mount_node = document.getElementById('print-your-order');
  var print_button = document.createElement('button');
  print_button.className = 'button no-print';
  var button_content = document.createTextNode("Print this page");
  print_button.addEventListener('click', function () {
    window.print();
  });
  print_button.appendChild(button_content);
  mount_node.appendChild(print_button);
};

window.addEventListener('DOMContentLoaded', function () {
  if (browser_can_print && on_order_summary_page && mount_node_exists) {
    add_print_button();
  }
});

},{}]},{},[1])
//# sourceMappingURL=data:application/json;charset=utf-8;base64,eyJ2ZXJzaW9uIjozLCJzb3VyY2VzIjpbIm5vZGVfbW9kdWxlcy9icm93c2VyLXBhY2svX3ByZWx1ZGUuanMiLCJ3d3dyb290L2pzL3ByaW50LXlvdXItb3JkZXIuanMiXSwibmFtZXMiOltdLCJtYXBwaW5ncyI6IkFBQUE7OztBQ0FDLElBQU0saUJBQWlCLEdBQUcsT0FBTyxNQUFNLENBQUMsS0FBZCxLQUF3QixVQUFsRDtBQUNELElBQU0scUJBQXFCLEdBQUcsQ0FBQyxDQUFDLFFBQVEsQ0FBQyxhQUFULENBQXVCLGdCQUF2QixDQUFoQztBQUNBLElBQU0saUJBQWlCLEdBQUcsQ0FBQyxDQUFDLFFBQVEsQ0FBQyxjQUFULENBQXdCLGtCQUF4QixDQUE1Qjs7QUFFQSxJQUFNLGdCQUFnQixHQUFHLFNBQW5CLGdCQUFtQixHQUFNO0FBRTNCLE1BQU0sVUFBVSxHQUFHLFFBQVEsQ0FBQyxjQUFULENBQXdCLGtCQUF4QixDQUFuQjtBQUVBLE1BQU0sWUFBWSxHQUFHLFFBQVEsQ0FBQyxhQUFULENBQXVCLFFBQXZCLENBQXJCO0FBRUEsRUFBQSxZQUFZLENBQUMsU0FBYixHQUF5QixpQkFBekI7QUFFQSxNQUFNLGNBQWMsR0FBRyxRQUFRLENBQUMsY0FBVCxDQUF3QixpQkFBeEIsQ0FBdkI7QUFFQSxFQUFBLFlBQVksQ0FBQyxnQkFBYixDQUE4QixPQUE5QixFQUF1QyxZQUFNO0FBQ3pDLElBQUEsTUFBTSxDQUFDLEtBQVA7QUFDSCxHQUZEO0FBSUEsRUFBQSxZQUFZLENBQUMsV0FBYixDQUF5QixjQUF6QjtBQUVBLEVBQUEsVUFBVSxDQUFDLFdBQVgsQ0FBdUIsWUFBdkI7QUFDSCxDQWpCRDs7QUFtQkEsTUFBTSxDQUFDLGdCQUFQLENBQXdCLGtCQUF4QixFQUE0QyxZQUFNO0FBQzlDLE1BQUksaUJBQWlCLElBQUkscUJBQXJCLElBQThDLGlCQUFsRCxFQUFxRTtBQUNqRSxJQUFBLGdCQUFnQjtBQUNuQjtBQUNKLENBSkQiLCJmaWxlIjoiZ2VuZXJhdGVkLmpzIiwic291cmNlUm9vdCI6IiIsInNvdXJjZXNDb250ZW50IjpbIihmdW5jdGlvbigpe2Z1bmN0aW9uIHIoZSxuLHQpe2Z1bmN0aW9uIG8oaSxmKXtpZighbltpXSl7aWYoIWVbaV0pe3ZhciBjPVwiZnVuY3Rpb25cIj09dHlwZW9mIHJlcXVpcmUmJnJlcXVpcmU7aWYoIWYmJmMpcmV0dXJuIGMoaSwhMCk7aWYodSlyZXR1cm4gdShpLCEwKTt2YXIgYT1uZXcgRXJyb3IoXCJDYW5ub3QgZmluZCBtb2R1bGUgJ1wiK2krXCInXCIpO3Rocm93IGEuY29kZT1cIk1PRFVMRV9OT1RfRk9VTkRcIixhfXZhciBwPW5baV09e2V4cG9ydHM6e319O2VbaV1bMF0uY2FsbChwLmV4cG9ydHMsZnVuY3Rpb24ocil7dmFyIG49ZVtpXVsxXVtyXTtyZXR1cm4gbyhufHxyKX0scCxwLmV4cG9ydHMscixlLG4sdCl9cmV0dXJuIG5baV0uZXhwb3J0c31mb3IodmFyIHU9XCJmdW5jdGlvblwiPT10eXBlb2YgcmVxdWlyZSYmcmVxdWlyZSxpPTA7aTx0Lmxlbmd0aDtpKyspbyh0W2ldKTtyZXR1cm4gb31yZXR1cm4gcn0pKCkiLCLvu79jb25zdCBicm93c2VyX2Nhbl9wcmludCA9IHR5cGVvZiB3aW5kb3cucHJpbnQgPT09ICdmdW5jdGlvbic7XG5jb25zdCBvbl9vcmRlcl9zdW1tYXJ5X3BhZ2UgPSAhIWRvY3VtZW50LnF1ZXJ5U2VsZWN0b3IoJy5vcmRlci1zdW1tYXJ5Jyk7XG5jb25zdCBtb3VudF9ub2RlX2V4aXN0cyA9ICEhZG9jdW1lbnQuZ2V0RWxlbWVudEJ5SWQoJ3ByaW50LXlvdXItb3JkZXInKTtcblxuY29uc3QgYWRkX3ByaW50X2J1dHRvbiA9ICgpID0+IHtcblxuICAgIGNvbnN0IG1vdW50X25vZGUgPSBkb2N1bWVudC5nZXRFbGVtZW50QnlJZCgncHJpbnQteW91ci1vcmRlcicpO1xuXG4gICAgY29uc3QgcHJpbnRfYnV0dG9uID0gZG9jdW1lbnQuY3JlYXRlRWxlbWVudCgnYnV0dG9uJyk7XG5cbiAgICBwcmludF9idXR0b24uY2xhc3NOYW1lID0gJ2J1dHRvbiBuby1wcmludCc7XG5cbiAgICBjb25zdCBidXR0b25fY29udGVudCA9IGRvY3VtZW50LmNyZWF0ZVRleHROb2RlKFwiUHJpbnQgdGhpcyBwYWdlXCIpO1xuXG4gICAgcHJpbnRfYnV0dG9uLmFkZEV2ZW50TGlzdGVuZXIoJ2NsaWNrJywgKCkgPT4ge1xuICAgICAgICB3aW5kb3cucHJpbnQoKTtcbiAgICB9KTtcblxuICAgIHByaW50X2J1dHRvbi5hcHBlbmRDaGlsZChidXR0b25fY29udGVudCk7XG5cbiAgICBtb3VudF9ub2RlLmFwcGVuZENoaWxkKHByaW50X2J1dHRvbik7XG59O1xuXG53aW5kb3cuYWRkRXZlbnRMaXN0ZW5lcignRE9NQ29udGVudExvYWRlZCcsICgpID0+IHtcbiAgICBpZiAoYnJvd3Nlcl9jYW5fcHJpbnQgJiYgb25fb3JkZXJfc3VtbWFyeV9wYWdlICYmIG1vdW50X25vZGVfZXhpc3RzKSB7XG4gICAgICAgIGFkZF9wcmludF9idXR0b24oKTtcbiAgICB9XG59KTsiXX0=
