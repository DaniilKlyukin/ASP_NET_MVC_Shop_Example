let min_price = document.getElementById("min-price");
let max_price = document.getElementById("max-price");

function checkMinPrice() {
    if (parseInt(min_price.value) > parseInt(max_price.value)) {
        min_price.value = max_price.value;
    }
}

function checkMaxPrice() {
    if (parseInt(max_price.value) < parseInt(min_price.value)) {
        max_price.value = min_price.value;
    }
}