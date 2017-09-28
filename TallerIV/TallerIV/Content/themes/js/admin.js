$(function () {
    $(".numeric").numeric();
    $(".integer").numeric(false, function () { this.value = ""; this.focus(); });
    $(".positive").numeric({ negative: false }, function () { this.value = ""; this.focus(); });
    $(".positive-integer").numeric({ decimal: false, negative: false }, function () { this.value = ""; this.focus(); });
    $(".decimal-2-places").numeric({ decimalPlaces: 2 });
});