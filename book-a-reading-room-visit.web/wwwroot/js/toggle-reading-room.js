const roomSelectionForm = document.querySelector('#room-selection-form');

if (roomSelectionForm !== null) {
    const updateButton = document.querySelector('#room-selection-form > fieldset > button[type=submit]');
    const dateListContainer = document.querySelector('#date-selection > div');
    const dateList = document.querySelector('#date-selection > div > ul');
    const radioButtonsArray = Array.from(document.querySelectorAll('#room-selection-form input[type=radio]'));

    const hideUpdateButton = () => {
        updateButton.style.display = 'none';
    }

    const disableButtons = (radioButtonsArray) => {
        radioButtonsArray.forEach(button => {
            button.setAttribute('disabled', true);
        })
    }

    hideUpdateButton();

    roomSelectionForm.addEventListener('change', e => {
        const loadingMessageContainer = document.createElement('p');

        loadingMessageContainer.setAttribute('aria-live', 'polite');
        loadingMessageContainer.innerHTML = "Loading...";

        dateList.remove();
        dateListContainer.appendChild(loadingMessageContainer);

        e.currentTarget.submit();
        disableButtons(radioButtonsArray);
    })
}
