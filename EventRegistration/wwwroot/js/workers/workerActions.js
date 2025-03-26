const editNameBtn = document.querySelector('#nameModal > div > div > div.modal-footer > button.btn.btn-primary');
const editEmailBtn = document.querySelector('#emailModal > div > div > div.modal-footer > button.btn.btn-primary');
const nameModal = document.getElementById('nameModal');
const emailModal = document.getElementById('emailModal');

nameModal.addEventListener('hidden.bs.modal', function () {
    document.querySelector('#nameModal > div > div > div.modal-body > div.message-line > p').innerHTML = '';
    document.querySelector('#nameModal > div > div > div.modal-body > div.edit-data > input').value = '';
});

emailModal.addEventListener('hidden.bs.modal', function () {
    document.querySelector('#emailModal > div > div > div.modal-body > div.message-line > p').innerHTML = '';
    document.querySelector('#emailModal > div > div > div.modal-body > div.edit-data > input').value = '';
});

editNameBtn.addEventListener('click', async function () {
    const newValue = document.querySelector('#nameModal > div > div > div.modal-body > div.edit-data > input').value;
    const messageElement = document.querySelector('#nameModal > div > div > div.modal-body > div.message-line > p');
    const messageDiv = document.querySelector('#nameModal > div > div > div.modal-body > div.message-line');
    messageElement.innerHTML = '';
    if (newValue === '' || newValue === null) {
        messageElement.innerHTML = 'Please enter a value.';
        return;
    }
    const nameModalElement = document.querySelector('#nameModal > div > div > div.modal-body > div.current-data > p');
    if (newValue === nameModalElement.innerHTML) {
        messageElement.innerHTML = 'The new username cannot be the same as the old one.';
        return;
    }

    const data = {
        newUsername: newValue,
        workerId: document.querySelector('#id').innerHTML
    }
    const editBtn = document.querySelector('#nameModal > div > div > div.modal-footer > button.btn.btn-primary');
    const closeBtn = document.querySelector('#nameModal > div > div > div.modal-footer > button.btn.btn-secondary');
    editBtn.disabled = true;
    closeBtn.disabled = true;

    messageElement.innerHTML = 'Please wait.';

    fetch('/Workers/EditName', {
        method: 'POST',
        headers: {
            'X-CSRF-VERIFICATION-TOKEN-E-Registration': document.getElementById('editWorkerNameAF').value,
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
            const nameElement = document.querySelector('body > div.worker-div > div.name-section > h1');
            nameModalElement.innerHTML = newValue;
            nameElement.innerHTML = newValue;
            closeBtn.disabled = false;
            closeBtn.click();
        } else if (jsonResponse && jsonResponse.errors) {
            appendErrors(jsonResponse.errors, messageDiv);
        } else {
            messageElement.innerHTML = "Unexpected error occured. Please try again.";
        }
    }).catch(error => {
        if (error.message == 'Network error.') {
            messageElement.innerHTML = "Network error.";
        } else {
            messageElement.innerHTML = "Unexpected error occured in editing the name. Check your internet connection and try again.";
        }
    }).finally(() => {
        editBtn.disabled = false;
        closeBtn.disabled = false;
    });
});

editEmailBtn.addEventListener('click', async function () {
    const newValue = document.querySelector('#emailModal > div > div > div.modal-body > div.edit-data > input').value;
    const messageElement = document.querySelector('#emailModal > div > div > div.modal-body > div.message-line > p');
    const messageDiv = document.querySelector('#emailModal > div > div > div.modal-body > div.message-line');
    messageElement.innerHTML = '';
    if (newValue === '' || newValue === null) {
        messageElement.innerHTML = 'Please enter a value.';
        return;
    }
    const emailModalElement = document.querySelector('#emailModal > div > div > div.modal-body > div.current-data > p');
    if (newValue === emailModalElement.innerHTML) {
        messageElement.innerHTML = 'The new email cannot be the same as the old one.';
        return;
    }

    const data = {
        newEmail: newValue,
        workerId: document.querySelector('#id').innerHTML
    }
    const editBtn = document.querySelector('#emailModal > div > div > div.modal-footer > button.btn.btn-primary');
    const closeBtn = document.querySelector('#emailModal > div > div > div.modal-footer > button.btn.btn-secondary');
    editBtn.disabled = true;
    closeBtn.disabled = true;

    messageElement.innerHTML = 'Please wait.';

    fetch('/Workers/EditEmail', {
        method: 'POST',
        headers: {
            'X-CSRF-VERIFICATION-TOKEN-E-Registration': document.getElementById('editWorkerEmailAF').value,
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
            const emailElement = document.querySelector('body > div.worker-div > div.email-section > h1');
            emailModalElement.innerHTML = newValue;
            emailElement.innerHTML = newValue;
            closeBtn.disabled = false;
            closeBtn.click();
        } else if (jsonResponse && jsonResponse.errors) {
            appendErrors(jsonResponse.errors, messageDiv);
        } else {
            messageElement.innerHTML = "Unexpected error occured. Please try again.";
        }
    }).catch(error => {
        if (error.message == 'Network error.') {
            messageElement.innerHTML = "Network error.";
        } else {
            messageElement.innerHTML = "Unexpected error occured in editing the email. Check your internet connection and try again.";
        }
    }).finally(() => {
        editBtn.disabled = false;
        closeBtn.disabled = false;
    });
});


function appendErrors(jsonErrors, messageDiv) {
    messageDiv.querySelector('p').innerHTML = '';
    messageDiv.querySelector('p').innerHTML = jsonErrors.join(" ");
}