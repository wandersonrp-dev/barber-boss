window.getUnmaskedPhoneValue = (elementId) => {    
    const unmaskedPhone = IMask(document.getElementById(elementId), { mask: "(00) 0 0000-0000" }).unmaskedValue

    if (!unmaskedPhone) return;
    
    return unmaskedPhone;
};
