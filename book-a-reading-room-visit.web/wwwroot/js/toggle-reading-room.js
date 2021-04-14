const roomSelectionForm = document.querySelector('#room-selection-form');

if (roomSelectionForm !== null) {
    const updateButton = document.querySelector('#room-selection-form > fieldset > button[type=submit]');
    const dateListContainer = document.querySelector('#date-selection > div');
    const dateList = document.querySelector('#date-selection > div > ul');
    const radioButtons = document.querySelectorAll('#room-selection-form input[type=radio]');

    const hideUpdateButton = () => {
        updateButton.style.display = 'none';
    }

    const disableButtons = (radioButtons) => {
        Array.prototype.forEach.call(radioButtons, radioButton => {
            radioButton.setAttribute('disabled', true);
        });
    }

    hideUpdateButton();

    dateListContainer.setAttribute('aria-live', 'polite');
    dateListContainer.setAttribute('role', 'region');

    roomSelectionForm.addEventListener('change', e => {
        const loadingMessageContainer = document.createElement('p');

        loadingMessageContainer.innerHTML = "Loading available dates";
        loadingMessageContainer.className = "text-center";
        loadingMessageContainer.style.fontSize = "24px";

        dateListContainer.removeChild(dateList);
        dateListContainer.appendChild(loadingMessageContainer);

        e.currentTarget.submit();
        disableButtons(radioButtons);
    })
}
