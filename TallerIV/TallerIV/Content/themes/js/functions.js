function showSuccess(message) {
    $('div#modal-success div.modal-body').html(message);
    $('div#modal-success').modal('show');
}

function hideSuccess() {
    $('div#modal-success div.modal-body').html("");
    $('div#modal-success').modal('hide');
}

function showError(message)
{
    $('div#modal-error div.modal-body').html(message);
    $('div#modal-error').modal('show');
}

function hideError() {
    $('div#modal-error div.modal-body').html("");
    $('div#modal-error').modal('hide');
}

function showLoading()
{
    $('div#modal-loading').modal('show');
}

function hideLoading() {
    $('div#modal-loading').modal('hide');
}

function showConfirm(message, callback) {
    $('div#modal-confirm div.modal-body').html(message);
    $('div#modal-confirm').modal('show');

    $('div#modal-confirm div.modal-footer a.btn-confirm').click(callback);
}

function hideConfirm() {
    $('div#modal-confirm div.modal-body').html("");
    $('div#modal-confirm').modal('hide');
}