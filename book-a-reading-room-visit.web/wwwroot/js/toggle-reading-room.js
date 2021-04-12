const roomSelectionForm = document.querySelector('#room-selection-form');
const updateButton = document.querySelector('#room-selection-form > fieldset > button[type=submit]');
const dateListContainer = document.querySelector('#date-selection > div');
const dateList = document.querySelector('#date-selection > div > ul');

if (roomSelectionForm !== null) {
    const hideUpdateButton = () => {
        updateButton.style.display = 'none';
    }

    hideUpdateButton();

    roomSelectionForm.addEventListener('change', e => {
        const loadingMessageContainer = document.createElement('p');

        loadingMessageContainer.innerHTML = "Loading...";

        dateList.remove();
        dateListContainer.appendChild(loadingMessageContainer);

        e.currentTarget.submit();
    })
}
