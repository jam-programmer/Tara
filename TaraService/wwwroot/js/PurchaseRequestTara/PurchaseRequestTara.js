function VerifyPurchase() {
    let orderId = document.getElementById("orderId").value;
    let terminal = document.getElementById("terminal").value;
    let body = {
        orderId: orderId,
        terminal: terminal
    }
    rest.postAsync("/Home/VerifyPurchase", null, body, function (isSuccess, response) {
        if (isSuccess) {
            if (response.isSuccess) {
                window.location = "/";
            } else {
                createToast(" سامانه تارا", response.data, "danger");
            }
        }
        else {
            createToast("خطای داخلی سامانه تارا", "لطفا به واحد پشتیبانی اطلاع دهید.", "danger");
        }
    });
}

function ReversePurchase() {
    let orderId = document.getElementById("orderId").value;
    let terminal = document.getElementById("terminal").value;
    let body = {
        orderId: orderId,
        terminal: terminal
    }
    rest.postAsync("/Home/ReversePurchase", null, body, function (isSuccess, response) {
        if (isSuccess) {
            if (response.isSuccess) {
                window.location = "/";
            } else {
                createToast(" سامانه تارا", response.data, "danger");
            }
        }
        else {
            createToast("خطای داخلی سامانه تارا", "لطفا به واحد پشتیبانی اطلاع دهید.", "danger");
        }
    });
}