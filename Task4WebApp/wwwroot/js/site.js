let options = {
    animation: true,
    delay: 2000
};

function Toast(text) {
    var toastHTMLElement = document.getElementById("liveToast");
    var toastTextElement = document.getElementById("toastText");
    toastTextElement.innerHTML = text;
    var toastElement = new bootstrap.Toast(toastHTMLElement, options);
    toastElement.show();
}
let mainCheckBox = document.querySelector(".main-check");
function changeCheckboxes() {
    let checkBoxex = document.querySelectorAll(".table-checkbox");
    console.log(checkBoxex);
    
    for (var i = 0; i < checkBoxex.length; i++) {
        checkBoxex[i].checked = mainCheckBox.checked;
    }
}

function changeMainCheckBox() {
    mainCheckBox.checked = false;
}