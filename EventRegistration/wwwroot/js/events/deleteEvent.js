let temporaryEvent = {};
let temporaryId = '';
const deleteEventModal = document.getElementById('deleteEventModal');

window.addEventListener('DOMContentLoaded', function () {
    deleteEventModal.addEventListener('show.bs.modal', populateTemporaryEvent);
    const backToEditModalBtn = deleteEventModal.querySelector('.back-to-edit');
    backToEditModalBtn.addEventListener('click', () => {
        populateEditModalAndOpen(temporaryEvent);
    });
    const deleteEventBtn = deleteEventModal.querySelector('.delete-event-btn');
    deleteEventModal.addEventListener('hidden.bs.modal', cleanValidationMessage);
    deleteEventBtn.addEventListener('click', deleteEvent);
});

async function deleteEvent() {
    const messageElement = deleteEventModal.querySelector('.api-validation-message');
    messageElement.style.color = 'red';
    if (temporaryId === '') {
        messageElement.innerHTML = 'No id provided.';
    }

    const deleteEventBtn = deleteEventModal.querySelector('.delete-event-btn');
    const closeButton = deleteEventModal.querySelector('.btn-secondary');

    deleteEventBtn.disabled = true;
    closeButton.disabled = true;

    fetch('/Workers/DeleteEvent', {
        method: 'POST',
        headers: {
            'X-CSRF-VERIFICATION-TOKEN-E-Registration': document.getElementById('deleteEventAF').value,
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(temporaryId)
    }).then(response => {
        if (!response.ok) {
            throw new Error('Network error.');
        }
        return response.json()
    }).then(jsonResponse => {
        if (jsonResponse.success) {
            if (window.calendar1 && window.calendar2) {
                if (temporaryEvent.floor == 1) {
                    window.calendar1.refetchEvents();
                } else {
                    window.calendar2.refetchEvents();
                }
            }
            closeButton.disabled = false;
            closeButton.click();
        } else if (jsonResponse && jsonResponse.message) {
            messageElement.innerHTML = jsonResponse.message;
        } else {
            messageElement.innerHTML = "Unexpected error occured. Please try again.";
        }
    }).catch(error => {
        if (error.message == 'Network error.') {
            messageElement.innerHTML = "Network error.";
        } else {
            messageElement.innerHTML = "Unexpected error occured in deleting the event. Check your internet connection and try again.";
        }
    }).finally(() => {
        deleteEventBtn.disabled = false;
        closeButton.disabled = false;
    });
}
function cleanValidationMessage() {
    const validationMessage = deleteEventModal.querySelector('.api-validation-message');
    validationMessage.innerHTML = '';
}
function populateTemporaryEvent() {

    const formData = getValuesFromEditEventForm();
    temporaryEvent = {
        id: formData.idInput.value,
        date: formData.dateInput.value,
        clientName: formData.clientInput.value,
        eventType: formData.eventInput.value,
        decorations: formData.decorationsInput.value,
        phoneNumber: formData.phoneNumberInput.value,
        childrenCount: formData.childrenCountInput.value,
        adultCount: formData.adultCountInput.value,
        moneyInAdvance: formData.moneyInAdvanceInput.value,
        startDateValue: formData.startTimeInput.value,
        endDateValue: formData.endTimeInput.value,
        floor: formData.floorInput.value
    }

    temporaryId = temporaryEvent.id;
}

