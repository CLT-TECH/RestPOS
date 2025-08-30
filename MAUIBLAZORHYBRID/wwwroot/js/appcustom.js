window.scrollToBottom = (elementId) => {
    const el = document.getElementById(elementId);
    if (el) {
        el.scrollTop = el.scrollHeight;
    }
};

window.focusElementById = (id) => {
    const el = document.getElementById(id);
    if (el) el.focus();
};

window.getFocusedElementId = function () {
    return document.activeElement ? document.activeElement.id : null;
};

window.removeFocus = function (id) {
    const el = document.getElementById(id);
    if (el) el.blur();
};