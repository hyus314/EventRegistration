
const datePicker = document.getElementById('datePicker');
const prevDayButton = document.getElementById('prevDayBtn');
const nextDayButton = document.getElementById('nextDayBtn');

window.addEventListener('DOMContentLoaded', updateCurrentDate);
window.addEventListener('DOMContentLoaded', function () {
    prevDayButton.addEventListener('click', updateCurrentDate);
    nextDayButton.addEventListener('click', updateCurrentDate);
    datePicker.addEventListener('change', updateCurrentDate);
})
function updateCurrentDate() {
    let datePicker = document.getElementById("datePicker");
    let currentDateDiv = document.querySelector(".current-date-div");

    let selectedDate = new Date(datePicker.value);
    if (isNaN(selectedDate)) return; // Prevent errors when input is empty

    let options = { weekday: "long", day: "numeric", month: "long", year: "numeric" };
    let formattedDate = selectedDate.toLocaleDateString("bg-BG", options);

    currentDateDiv.textContent = formattedDate;
    document.title = `${formattedDate} - Event`;
}