document.addEventListener('DOMContentLoaded', async function () {
    let selectedDate = document.getElementById('datePicker').value;
    let calendar1El = document.getElementById('calendar1');
    let calendar2El = document.getElementById('calendar2');

    let calendar1 = new FullCalendar.Calendar(calendar1El, {
        initialView: 'timeGridDay',
        initialDate: selectedDate,
        slotDuration: '00:30:00',
        slotMinTime: "08:00:00",
        slotMaxTime: "23:00:00",
        slotLabelFormat: { hour: '2-digit', minute: '2-digit', hour12: false },
        events: async function () {
            let request = await fetch(`/Events/EventsByDate?date=${selectedDate}`);
            if (!request || !request.ok) {
                alert('Error in loading events. Please try again.');
                return;
            }
            let data = await request.json();
            let allEvents = JSON.parse(data);

            return allEvents.filter(e => e.floor === 1);

        },
        eventDidMount: function (info) {
            let startTime = new Date(info.event.start).toLocaleTimeString([], { hour: '2-digit', minute: '2-digit', hour12: false });
            let endTime = new Date(info.event.end).toLocaleTimeString([], { hour: '2-digit', minute: '2-digit', hour12: false });

            let additionalDetails = `
            <div class="fc-event-details">
                <strong>${info.event.title} ${startTime} - ${endTime}</strong><br>
                📞 ${info.event.extendedProps.phoneNumber} |  🎉 ${info.event.extendedProps.type} <br>
                👶 ${info.event.extendedProps.children} | 🧑 ${info.event.extendedProps.adult} <br>
                🎨: ${info.event.extendedProps.decorations}
            </div>
        `;

            info.el.innerHTML = additionalDetails; // Modify the event content
        },
        headerToolbar: {
            left: '',
            center: '',
            right: '',
        },
        dayHeaders: false,
        allDaySlot: false,
        contentHeight: 'auto',
        eventClick: function (info) {
            handleEditModalOpen(info);
        },
        dateClick: function (info) {
            handleAddModalOpen(info, 1);
        }
    });

    let calendar2 = new FullCalendar.Calendar(calendar2El, {
        initialView: 'timeGridDay',
        initialDate: selectedDate,
        slotDuration: '00:30:00',
        slotMinTime: "08:00:00",
        slotMaxTime: "23:00:00",
        slotLabelFormat: { hour: '2-digit', minute: '2-digit', hour12: false },
        events: async function () {
            let request = await fetch(`/Events/EventsByDate?date=${selectedDate}`);
            if (!request || !request.ok) {
                alert('Error in loading events. Please try again.');
                return;
            }
            let data = await request.json();
            let allEvents = JSON.parse(data);

            return allEvents.filter(e => e.floor === 2);
        },
        eventDidMount: function (info) {
            let startTime = new Date(info.event.start).toLocaleTimeString([], { hour: '2-digit', minute: '2-digit', hour12: false });
            let endTime = new Date(info.event.end).toLocaleTimeString([], { hour: '2-digit', minute: '2-digit', hour12: false });

            let additionalDetails = `
            <div class="fc-event-details">
                <strong>${info.event.title} ${startTime} - ${endTime}</strong><br>
                📞 ${info.event.extendedProps.phoneNumber} |  🎉 ${info.event.extendedProps.type} <br>
                👶 ${info.event.extendedProps.children} | 🧑 ${info.event.extendedProps.adult} <br>
                🎉 : ${info.event.extendedProps.decorations}
            </div>
        `;
            info.el.innerHTML = additionalDetails;
        },
        headerToolbar: {
            left: '',
            center: '',
            right: ''
        },
        dayHeaders: false,
        allDaySlot: false,
        contentHeight: 'auto',
        eventClick: function (info) {
            handleEditModalOpen(info);
        },
        dateClick: function (info) {
            handleAddModalOpen(info, 2);
        }
    });

    calendar1.render();
    calendar2.render();

    window.calendar1 = calendar1;
    window.calendar2 = calendar2;

    function updateURL(newDate) {
        const url = new URL(window.location);
        url.searchParams.set('date', newDate);
        window.history.pushState({}, '', url); // Update URL without reloading
    }

    function updateCalendars(newDate) {
        calendar1.gotoDate(newDate);
        calendar2.gotoDate(newDate);
        updateURL(newDate); 
    }

    document.getElementById('prevDayBtn').addEventListener('click', function () {
        let newDate = new Date(selectedDate);
        newDate.setDate(newDate.getDate() - 1); // Go to previous day
        selectedDate = newDate.toISOString().split('T')[0];
        updateCalendars(selectedDate);
        document.getElementById('datePicker').value = selectedDate; // Update input field
    });

    document.getElementById('nextDayBtn').addEventListener('click', function () {
        let newDate = new Date(selectedDate);
        newDate.setDate(newDate.getDate() + 1); // Go to next day
        selectedDate = newDate.toISOString().split('T')[0];
        updateCalendars(selectedDate);
        document.getElementById('datePicker').value = selectedDate; // Update input field
    });

    document.getElementById('datePicker').addEventListener('change', function () {
        selectedDate = this.value;
        updateCalendars(selectedDate);
    });

    //function test(info, floor) {
    //    console.log(info);
    //    console.log(floor);
    //}

});

