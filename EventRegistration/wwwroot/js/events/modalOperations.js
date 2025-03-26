const addEventModal = document.getElementById('addEventModal');

let startDateInfo = '';

window.addEventListener('DOMContentLoaded', handlePhoneNumberFormat);
window.addEventListener('DOMContentLoaded', function () {
    const addEventBtn = document.getElementById('addEventBtn');
    addEventModal.addEventListener('hidden.bs.modal', () => {
        clearModal(addEventModal);
    });
    addEventBtn.addEventListener('click', addEvent);
});
function handleAddModalOpen(info, floor) {
    startDateInfo = info.dateStr;
    let timeValue = new Date(info.dateStr).toLocaleTimeString("bg-BG", {
        weekday: "long",
        year: "numeric",
        month: "2-digit",
        day: "2-digit",
        hour: "2-digit",
        minute: "2-digit",
        hour12: false
    });

    let cleanedString = timeValue.replace(" г.,", "");
    // Split date and time
    let [dayOfWeek, datePart, timePart] = cleanedString.split(" ");

    // Reformat date from DD.MM.YYYY → DD-MM-YYYY
    let formattedDate = datePart.split(".").join("-");

    //format year-month-day

    //console.log(timeValueForHTML);
    //console.log(timeValueForApi);
    //console.log(floor);
    document.querySelector('.event > input').value = 'рожден ден';
    const openModalButton = document.getElementById('openAddModal');
    const datePModal = document.getElementsByClassName('date-event-modal')[0];
    const floorPModal = document.getElementsByClassName('floor-event-modal')[0];

    datePModal.innerHTML = cleanedString;
    floorPModal.innerHTML = `${floor} етаж`;

    populateEndTimeDiv(info.dateStr);
    openModalButton.click();
}
function populateEndTimeDiv(inputDate) {
    const endTimeInput = document.querySelector('.end-time input');

    let startDate = new Date(inputDate);

    // Convert startTime to a Date object

    // Extract hours and minutes
    let hours = startDate.getHours();
    let minutes = startDate.getMinutes();

    // Increase hours by 3
    hours += 3;

    // Ensure the format is always HH:MM (24-hour format)
    let formattedHours = String(hours).padStart(2, '0'); // Ensures two-digit format
    let formattedMinutes = String(minutes).padStart(2, '0');

    // Set the new time in the input field
    endTimeInput.value = `${formattedHours}:${formattedMinutes}`;
}

function validateNumberInput(input) {
    if (input.value < 0) {
        input.value = 0;
    }
}

function handlePhoneNumberFormat() {
    const phoneNumberInput = document.querySelector('.phone-number>input');
    const formatP = document.getElementsByClassName('phone-format-message')[0];
    phoneNumberInput.addEventListener('focus', function () {
        formatP.innerHTML = 'Phone number format can be universal.';
        phoneNumberInput.style.outline = 'none';
    });
    phoneNumberInput.addEventListener('blur', function () {
        formatP.innerHTML = '';
        phoneNumberInput.style.outline = 'none';
    });

}


async function addEvent() {
    if (!validateForm(addEventModal, 'add')) {
        return;
    }
    const clientName = addEventModal.querySelector('.client > input').value;

    const eventEndTime = document.querySelector('.end-time > input').value;

    const startDate = new Date(startDateInfo);
    const endDate = new Date(startDate);
    const [endHours, endMinutes] = eventEndTime.split(":").map(Number);
    endDate.setHours(endHours, endMinutes, 0, 0);

    const eventType = addEventModal.querySelector('.event > input').value;
    const decorations = addEventModal.querySelector('.decoration > input').value;
    const phoneNumber = addEventModal.querySelector('.phone-number > input').value;
    const childrenMenu = addEventModal.querySelector('.children-count > input').value;
    const adultsMenu = addEventModal.querySelector('.adults-count > input').value;
    const moneyInAdvance = addEventModal.querySelector('.money > input').value;
    const floor = Number(addEventModal.querySelector('.floor-event-modal').innerHTML.split(' ')[0]);


    const data = {
        clientName,
        startDate,
        endDate,
        decorations,
        eventType,
        phoneNumber,
        childrenMenu,
        adultsMenu,
        moneyInAdvance,
        floor
    }

    const closeButton = addEventModal.querySelector('button.btn.btn-secondary');

    addEventBtn.disabled = true;
    closeButton.disabled = true;

    const messageElement = addEventModal.querySelector('.api-validation-message');

    fetch('/Events/AddEvent', {
        method: 'POST',
        headers: {
            'X-CSRF-VERIFICATION-TOKEN-E-Registration': document.getElementById('addEventAF').value,
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
                if (floor === 1) {
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
            messageElement.innerHTML = "Unexpected error occured in adding the event. Check your internet connection and try again.";
        }
    }).finally(() => {
        addEventBtn.disabled = false;
        closeButton.disabled = false;
    });

};