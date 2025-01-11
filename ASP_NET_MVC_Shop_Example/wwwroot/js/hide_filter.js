document.addEventListener("DOMContentLoaded", function () {
    const filterPanel = document.getElementById("filterPanel");
    const toggleButton = document.getElementById("toggleButton");

    toggleButton.addEventListener("click", function () {
        if (filterPanel.classList.contains("hidden")) {
            filterPanel.classList.remove("hidden");
            toggleButton.textContent = "Скрыть фильтры";
        } else {
            filterPanel.classList.add("hidden");
            toggleButton.textContent = "Показать фильтры";
        }
    });
});