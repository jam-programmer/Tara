 
//function PaymentCenter(accessCode, terminalCode) {
//    let model = {
//        accessCode: accessCode,
//        terminalCode: terminalCode
//    }
//    $.ajax({
//        url: '/PaymentCenter',
//        type: 'POST',
//        data: JSON.stringify(model),
//        contentType: "application/json",
//        success: function (data) {
//            console.log('دریافت داده:', data);
//        },
//        error: function (xhr, status, error) {
//            console.log('خطا در درخواست:', error);
//            console.log('کد وضعیت:', xhr.status);
//            console.log('متن خطا:', xhr.responseText);
//        }
//    });
//}