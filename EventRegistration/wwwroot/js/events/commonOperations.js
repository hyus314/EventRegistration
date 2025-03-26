function clearModal(modal) {
    const messages = modal.querySelectorAll('.message');
    const inputs = modal.querySelectorAll('input');
    const messageElement = modal.querySelector('p.api-validation-message');
    for (let i = 0; i < messages.length; i++) {
        messages[i].innerHTML = '';
    }

    for (let i = 0; i < inputs.length; i++) {
        inputs[i].value = '';
    }

    messageElement.innerHTML = '';
}

function validateForm(modal, modalName) {
    let isValid = true;
    modal.querySelectorAll(".input-row input[type='text']").forEach(input => {
        const message = input.parentElement.nextElementSibling;
        if (message && !message.classList.contains("decoration-message")) {
            if (input.value.trim() === "") {
                message.textContent = "This field cannot be empty!";
                message.style.color = "red";
                isValid = false;
            } else {
                message.textContent = "";
            }
        }
    });

    modal.querySelectorAll(".input-row input[type='number']").forEach(input => {
        const message = input.parentElement.nextElementSibling;
        if (input.value.trim() === "" || input.value < 0) {
            message.textContent = "Value must be 0 or greater!";
            message.style.color = "red";
            isValid = false;
        } else {
            message.textContent = "";
        }
    });

    const phoneInput = modal.querySelector(".phone-number input");
    const phoneMessage = modal.querySelector(".phone-validation-message");
    const phoneRegex = /^\+?\d{7,15}$/;

    if (!phoneRegex.test(phoneInput.value.trim())) {
        phoneMessage.textContent = "Invalid phone number!";
        phoneMessage.style.color = "red";
        isValid = false;
    } else {
        phoneMessage.textContent = "";
    }

    modal.querySelectorAll(".input-row input[type='time']").forEach(input => {
        const message = input.parentElement.nextElementSibling;
        if (input.value.trim() === "") {
            message.textContent = "This field cannot be empty!";
            message.style.color = "red";
            isValid = false;
        } else {
            message.textContent = "";
        }
    });

    if (modalName === 'edit') {

        const startTimeInput = editEventModal.querySelector('.start-time > input');
        const endTimeInput = editEventModal.querySelector('.end-time > input');
        const timeSoonerMessage = editEventModal.querySelector('.time-sooner-message');

        function timeToMinutes(timeString) {
            const [hours, minutes] = timeString.split(":").map(Number);
            return hours * 60 + minutes;
        }

        if (startTimeInput.value && endTimeInput.value) {
            const startMinutes = timeToMinutes(startTimeInput.value);
            const endMinutes = timeToMinutes(endTimeInput.value);

            if (endMinutes <= startMinutes) {
                timeSoonerMessage.textContent = "End time must be later than start time!";
                timeSoonerMessage.style.color = "red";
                isValid = false;
            } else {
                timeSoonerMessage.textContent = "";
            }
        }

        const floorInput = editEventModal.querySelector('.floor > input');
        const floorMessage = editEventModal.querySelector('.floor-message');
        console.log(floorInput.value);
        if (floorInput.value != 1 && floorInput.value != 2) {
            floorMessage.textContent = "Floor input cannot be different than 1 or 2!";
            floorMessage.style.color = "red";
            isValid = false;
        } else {
            timeSoonerMessage.textContent = "";
        }
    }
    return isValid;
}