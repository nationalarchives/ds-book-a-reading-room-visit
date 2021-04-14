const browser_can_print = typeof window.print === 'function';
const on_order_summary_page = !!document.querySelector('main.order-summary');
const mount_node_exists = !!document.getElementById('print-your-order');

const add_print_button = () => {

    const mount_node = document.getElementById('print-your-order');

    const print_button = document.createElement('button');

    print_button.className = 'button no-print';

    const button_content = document.createTextNode("Print this page");

    print_button.addEventListener('click', () => {
        window.print();
    });

    print_button.appendChild(button_content);

    mount_node.appendChild(print_button);
};

window.addEventListener('DOMContentLoaded', () => {
    if (browser_can_print && on_order_summary_page && mount_node_exists) {
        add_print_button();
    }
});