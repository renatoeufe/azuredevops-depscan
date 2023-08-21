var element = document.getElementById("myChart");

var countUpdated = element.getAttribute("data-updated");
var countNotUpdated = element.getAttribute("data-not-updated");

new Chart("myChart", {
    type: "doughnut",
    data: {
        labels: ["Updated", "Not Updated"],
        datasets: [
            {
                backgroundColor: ["#20c997", "#dc3545"],
                data: [countUpdated, countNotUpdated]
            }
        ]
    },
    options: {
        title: {
            display: false,
            text: ""
        },
        plugins: {
            legend: {
                display: false
            }
        }
    }
});