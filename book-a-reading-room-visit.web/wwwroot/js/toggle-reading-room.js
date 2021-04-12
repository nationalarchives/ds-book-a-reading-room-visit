const roomSelectionForm = document.querySelector('#room-selection-form');
const updateButton = document.querySelector('#room-selection-form > fieldset > button[type=submit]')

if (roomSelectionForm !== null) {
    const hideUpdateButton = () => {
        updateButton.style.display = 'none';
    }

    hideUpdateButton();

    roomSelectionForm.addEventListener('change', e => {
        e.currentTarget.submit()
    })
}
