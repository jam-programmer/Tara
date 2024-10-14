function createToast(toastTitle, message, status) {
    const toast = document.createElement('div');
    toast.className = 'bs-toast toast toast-placement-ex m-2 fade top-50 start-50 translate-middle show';
    toast.setAttribute('role', 'alert');
    toast.setAttribute('aria-live', 'assertive');
    toast.setAttribute('aria-atomic', 'true');
    toast.setAttribute('data-bs-delay', '2000');

    const toastHeader = document.createElement('div');
    toastHeader.className = 'toast-header bg-' + status;

    
    const titleElement = document.createElement('div');
    titleElement.className = 'me-auto fw-semibold';
    titleElement.innerText = toastTitle;

    const closeButton = document.createElement('button');
    closeButton.type = 'button';
    closeButton.className = 'btn-close';
    closeButton.setAttribute('data-bs-dismiss', 'toast');
    closeButton.setAttribute('aria-label', 'Close');

    toastHeader.appendChild(titleElement);
    toastHeader.appendChild(closeButton);

    const toastBody = document.createElement('div');
    toastBody.className = 'toast-body';
    toastBody.innerText = message;

    toast.appendChild(toastHeader);
    toast.appendChild(toastBody);
    document.body.appendChild(toast);
}

function generateCustomUID(length = 10) {
    const characters = 'ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789';
    let uid = '';
    for (let i = 0; i < length; i++) {
        uid += characters.charAt(Math.floor(Math.random() * characters.length));
    }
    return uid;
} 