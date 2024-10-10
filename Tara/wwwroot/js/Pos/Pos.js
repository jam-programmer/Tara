function AddProduct() {
    let productName = document.getElementById("productName").value;
    let productCode = document.getElementById("productCode").value;
    let productGroup = $('#productGroup');
    let productMade = document.getElementById("productMade");
    let productCount = document.getElementById("productCount").value;
    let productFee = document.getElementById("productFee").value;
    let productUnit = document.getElementById("productUnit");
    if (!productName) {
        createToast("عدم تکمیل اطلاعات", "نام کالا را وارد کنید.", "warning"); return;
    }
    if (!productCode) {
        createToast("عدم تکمیل اطلاعات", "کد کالا را وارد کنید.", "warning"); return;
    }
   

    if (productGroup.val() === "0") {
        createToast("عدم تکمیل اطلاعات", "گروه کالا را انتخاب کنید", "warning"); return;
    }
    if (productMade.value === "4") {
        createToast("عدم تکمیل اطلاعات", "برند کالا را انتخاب کنید", "warning"); return;
    }
    if (!productCount) {
        createToast("عدم تکمیل اطلاعات", "تعداد را وارد کنید", "warning"); return;
    }
    if (!productFee) {
        createToast("عدم تکمیل اطلاعات", "مبلغ واحد را وارد کنید", "warning"); return;
    }
    if (productUnit.value === "0") {
        createToast("عدم تکمیل اطلاعات", "واحد شمارش را انتخاب کنید.", "warning"); return;
    }

    let body = document.getElementById("PosTableBody");

    var trId = generateCustomUID();




    let tr = document.createElement("tr");
    tr.id = "tr_" + trId;

    let tdProductName = document.createElement("td");
    tdProductName.innerText = productName;

    let tdProductGroup = document.createElement("td");
    tdProductGroup.innerText = productGroup.find("option:selected").text();

    let tdProductCount = document.createElement("td");
    tdProductCount.innerText = productCount;

    let tdProductFee = document.createElement("td");
    tdProductFee.innerText = productFee;

    let tdAction = document.createElement("td");
    let btnRemove = document.createElement("a");
    btnRemove.className = "btn btn-danger btn-sm text-white";
    btnRemove.innerText = "حذف کالا از لیست";
    btnRemove.onclick = function () {
        RemoveFromJson(trId);
    };


    tdAction.appendChild(btnRemove);
    tr.appendChild(tdProductName);
    tr.appendChild(tdProductGroup);
    tr.appendChild(tdProductCount);
    tr.appendChild(tdProductFee);
    tr.appendChild(tdAction);
    body.appendChild(tr);

    var product = {
        Key: trId,
        ProductName: productName,
        ProductCode: productCode,
        Count: productCount,
        Unit: productUnit.value,
        Made: productMade.value,
        Fee: productFee,
        Group: productGroup.val(),
        GroupTitle: productGroup.find("option:selected").text()
    }
    AddToJson(product);

    document.getElementById("productName").value = "";
    document.getElementById("productCode").value = "";
    productGroup.val("0").trigger('change');
    productMade.selectedIndex = 0;
    document.getElementById("productCount").value = "";
    document.getElementById("productFee").value = "";
    productUnit.selectedIndex = 0;

}


function AddToJson(product) {
    var json = document.getElementById("orderDetail").value;
    if (!json) {
        var obj = [];
        json = JSON.stringify(obj);
    }
    var data = JSON.parse(json);
    data.push(product);
    var updatedJson = JSON.stringify(data);
    document.getElementById("orderDetail").value = updatedJson;
}

function RemoveFromJson(Key) {

    var json = document.getElementById("orderDetail").value;

    var data = JSON.parse(json);
    var updatedJson = data.filter(item => item.Key !== Key);

    document.getElementById("orderDetail").value = JSON.stringify(updatedJson);

    var trItem = document.getElementById("tr_" + Key);
    if (trItem) {
        trItem.parentNode.removeChild(trItem);
    }
}

function RegisterOrder() {
   
    let barcode = document.getElementById("barcode");
    let dateTime = document.getElementById("orderDateTime");
    let detail = document.getElementById("orderDetail");
    let fullName = document.getElementById("cutomerName");
    let phoneNumber = document.getElementById("customerPhoneNumber");
    let terminal = document.getElementById("terminal");


    if (!barcode) {
        createToast("عدم تکمیل اطلاعات", "شناسه کیف پول تارا را وارد کنید.", "warning");
        return;
    }

    if (detail.value === "[]" || detail.value === "") {
        createToast("عدم تکمیل اطلاعات", "یک یا چند کالا وارد کنید.", "warning");
        return;
    }

    if (!fullName.value) {
        createToast("عدم تکمیل اطلاعات", "نام مشتری را واردکنید", "warning");
        return;
    }
    let body = {
        detail: detail.value,
        dateTime: dateTime.value,
        fullName: fullName.value,
        phoneNumber: phoneNumber.value,
        barcode: barcode.value,
        terminal: terminal.value
    }


    rest.postAsync("/RegisterOrder", null, body, function (isSuccess, response) {

        if (isSuccess) {

            if (response.isSuccess) {
                let btntara = document.getElementById("btnTara");
                let orderIdInput = document.getElementById("OrderId");
                orderIdInput.value = response.data;
                btntara.classList.remove("hide-btn-tara");
                btntara.classList.add("show-btn-tara");



            }

        } else {
            createToast("خطای داخلی سامانه تارا", "لطفا به واحد پشتیبانی اطلاع دهید.", "danger");

        }
    });
}

function RegisterTara() {
    let orderId = document.getElementById("OrderId").value;
    let terminal = document.getElementById("terminal").value;
    let url = "PurchaseRequestTara?terminal=" + terminal +"&orderId=" + orderId
    window.location = url;
}