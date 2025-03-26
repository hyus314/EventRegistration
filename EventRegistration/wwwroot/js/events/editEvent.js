const editEventModal = document.getElementById('editEventModal');
window.addEventListener('DOMContentLoaded', function () {
    const editEventBtn = document.getElementById('editEventBtn');
    editEventBtn.addEventListener('click', editEvent);
    editEventModal.addEventListener('hidden.bs.modal', () => {
        clearModal(editEventModal);
    });
});


async function editEvent() {
    if (!validateForm(editEventModal, 'edit')) {
        return;
    }

    const idInput = editEventModal.querySelector('.event-id');
    const dateInput = editEventModal.querySelector('.edit-date > input');
    const clientInput = editEventModal.querySelector('.client > input');
    const eventInput = editEventModal.querySelector('.event > input');
    const decorationsInput = editEventModal.querySelector('.decorations > input');
    const phoneNumberInput = editEventModal.querySelector('.phone-number > input');
    const childrenCountInput = editEventModal.querySelector('.children-count > input');
    const adultCountInput = editEventModal.querySelector('.adult-count > input');
    const moneyInAdvanceInput = editEventModal.querySelector('.money > input');
    const startTimeInput = editEventModal.querySelector('.start-time > input');
    const endTimeInput = editEventModal.querySelector('.end-time > input');
    const floorInput = editEventModal.querySelector('.floor > input');

    const data = {
        encryptedId: idInput.value,
        date: dateInput.value,
        clientName: clientInput.value,
        startDateValue: startTimeInput.value,
        endDateValue: endTimeInput.value,
        eventType: eventInput.value,
        phoneNumber: phoneNumberInput.value,
        childrenMenu: childrenCountInput.value,
        adultsMenu: adultCountInput.value,
        moneyInAdvance: moneyInAdvanceInput.value,
        decorations: decorationsInput.value,
        floor: floorInput.value
    }

    const editEventBtn = document.getElementById('editEventBtn');
    const closeButton = editEventModal.querySelector('button.btn.btn-secondary');

    const messageElement = editEventModal.querySelector('.api-validation-message');


    fetch('/Events/EditEvent', {
        method: 'POST',
        headers: {
            'X-CSRF-VERIFICATION-TOKEN-E-Registration': document.getElementById('editEventAF').value,
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(data)
    }).then(response => {
        if (!response.ok) {
            throw new Error('Network error.');
        }
        return response.json()
    }).then(jsonResponse => {
        if (jsonResponse.success) {
            if (window.calendar1 && window.calendar2) {
                window.calendar1.refetchEvents();
                window.calendar2.refetchEvents();
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
            messageElement.innerHTML = "Unexpected error occured in editing the event. Check your internet connection and try again.";
        }
    }).finally(() => {
        editEventBtn.disabled = false;
        closeButton.disabled = false;
    });


}
function handleEditModalOpen(info) {
    const [startDateValue, endDateValue] = getStartEndDateHours(info.event.start, info.event.end);
    const date = getDate(info.event.start);

    const event = {
        id: info.event.id,
        date,
        clientName: info.event.title, //
        startDateValue,
        endDateValue,
        eventType: info.event.extendedProps.type, //
        phoneNumber: info.event.extendedProps.phoneNumber, // 
        children: info.event.extendedProps.children, 
        adult: info.event.extendedProps.adult,
        moneyInAdvance: info.event.extendedProps.moneyInAdvance, //
        floor: info.event.extendedProps.floor, //
        decorations: info.event.extendedProps.decorations
    }
    populateEditModalAndOpen(event);
}

function getDate(infoEventDate) {
    const rawDate = new Date(infoEventDate);
    const day = rawDate.getDate();
    const month = rawDate.getUTCMonth() + 1;
    const year = rawDate.getFullYear();

    return `${year}-${month.toString().padStart(2, '0')}-${day.toString().padStart(2, '0')}`;
}

function getStartEndDateHours(infoEventStart, infoEventEnd) {
    const startDate = new Date(infoEventStart);
    const endDate = new Date(infoEventEnd);

    const startHours = startDate.getHours();
    const startMinutes = startDate.getMinutes();

    const endHours = endDate.getHours();
    const endMinutes = endDate.getMinutes();

    //console.log(startHours, startMinutes, endHours, endMinutes);

    let formattedStartHours = String(startHours).padStart(2, '0'); // Ensures two-digit format
    let formattedStartMinutes = String(startMinutes).padStart(2, '0');

    let formattedEndHours = String(endHours).padStart(2, '0'); // Ensures two-digit format
    let formattedEndMinutes = String(endMinutes).padStart(2, '0');

    const startDateValue = `${formattedStartHours}:${formattedStartMinutes}`;
    const endDateValue = `${formattedEndHours}:${formattedEndMinutes}`;

    return [startDateValue, endDateValue];
}

function getValuesFromEditEventForm() {

    return {
        idInput: editEventModal.querySelector('.event-id'),
        dateInput: editEventModal.querySelector('.edit-date > input'),
        clientInput: editEventModal.querySelector('.client > input'),
        eventInput: editEventModal.querySelector('.event > input'),
        decorationsInput: editEventModal.querySelector('.decorations > input'),
        phoneNumberInput: editEventModal.querySelector('.phone-number > input'),
        childrenCountInput: editEventModal.querySelector('.children-count > input'),
        adultCountInput: editEventModal.querySelector('.adult-count > input'),
        moneyInAdvanceInput: editEventModal.querySelector('.money > input'),
        startTimeInput: editEventModal.querySelector('.start-time > input'),
        endTimeInput: editEventModal.querySelector('.end-time > input'),
        floorInput: editEventModal.querySelector('.floor > input')
    }
}


function populateEditModalAndOpen(event) {
    const openEditModal = document.getElementById('openEditModal');

    const formData = getValuesFromEditEventForm();

    formData.idInput.value = event.id;
    formData.dateInput.value = event.date;
    formData.clientInput.value = event.clientName;
    formData.eventInput.value = event.eventType;
    formData.decorationsInput.value = event.decorations;
    formData.phoneNumberInput.value = event.phoneNumber;
    formData.childrenCountInput.value = event.children;
    formData.adultCountInput.value = event.adult;
    formData.moneyInAdvanceInput.value = event.moneyInAdvance;
    formData.startTimeInput.value = event.startDateValue;
    formData.endTimeInput.value = event.endDateValue;
    formData.floorInput.value = event.floor;

    openEditModal.click();
}

