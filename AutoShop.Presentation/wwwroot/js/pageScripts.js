const hideCardElement = $('#hideCardsId');
const showCardElement = $('#showCardsId');
const cardElement = $('.card');

const interval = 2000;

hideCardElement.click(function () {
    hideCardElement.hide(interval);
    showCardElement.show(interval);
    cardElement.hide(interval);
});

showCardElement.click(function () {
    hideCardElement.show(interval);
    showCardElement.hide(interval);
    cardElement.show(interval);
});