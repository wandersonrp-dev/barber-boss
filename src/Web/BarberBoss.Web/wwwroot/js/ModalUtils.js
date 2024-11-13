window.closeModal = (modalId) => {    
    var modal = document.getElementById(modalId);   
    if (modal) {
        modal.setAttribute('aria-hidden', 'true');
        modal.classList.add('hidden');
    }
};
