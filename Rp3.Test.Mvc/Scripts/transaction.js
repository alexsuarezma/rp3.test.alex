$(document).ready(function (){
    const transactionId = document.getElementById('transactionId').value

    if (transactionId > 0) {
        $('#exampleModal').modal('show')
    }

    document.getElementById('button-new').addEventListener('click', () => {
        document.getElementById('transactionId').value = ''
        document.getElementById('TransactionTypeId').value = ''
        document.getElementById('CategoryId').value = ''
        document.getElementById('RegisterDate').value = ''
        document.getElementById('ShortDescription').value = ''
        document.getElementById('Amount').value = ''
        document.getElementById('Notes').value = ''
    })

})

function filterFloat(evt, input) {
    // Barraespaciadora = 8, Enter = 13, ‘0? = 48, ‘9? = 57, ‘.’ = 46, ‘-’ = 43
    var key = window.Event ? evt.which : evt.keyCode;
    var chark = String.fromCharCode(key);
    var tempValue = input.value + chark;
    if (key >= 48 && key <= 57) {
        if (filter(tempValue) === false) {
            return false;
        } else {
            return true;
        }
    } else {
        if (key == 8 || key == 13 || key == 0) {
            return true;
        } else if (key == 44 || input.value==",") {
            if (filter(tempValue) === false) {
                return false;
            } else {
                return true;
            }
        } else {
            return false;
        }
    }
}

function filter(__val__) {
    var preg = /^([0-9]+\,?[0-9]{0,2})$/;
    if (preg.test(__val__) === true) {
        return true;
    } else {
        return false;
    }

}