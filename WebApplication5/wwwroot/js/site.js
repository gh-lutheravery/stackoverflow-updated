/// Requires jquery for vote count functions, and quill for init function


// slice content to display as plaintext later
function slicePostContent(text) {
    let slicedText = text.slice(0, 100);
    let pattern = /\r\n/gi;
    let cleanedText = slicedText.replaceAll(pattern, '');
    return cleanedText;
}



// toggles navbar contents for mobile
function toggleNavbarBurger(burger) {
    var target = burger.dataset.target;
    var targetElement = document.getElementById(target);
    targetElement.classList.toggle("is-active");
}

// init default settings for quill objects
function initDefaultQuill(containerId) {
    let quillObj = new Quill(containerId, {
        modules: {
            toolbar: [
                [{ header: [1, 2, false] }],
                ['bold', 'italic', 'underline', 'code-block'],
                [{ 'list': 'ordered' }, { 'list': 'bullet' }],
                [{ 'script': 'sub' }, { 'script': 'super' }],
                ['image']
            ]
        },
        theme: 'snow'
    });
    return quillObj;
}


/// Functions for modals used for various CRUD views
function openModal(modal) {
    modal.classList.add('is-active');
}

function closeModal(modal) {
    modal.classList.remove('is-active');
}

// Add a click event on buttons to open a specific modal
(document.querySelectorAll('.open-modal-trigger') || []).forEach((trigger) => {
    const modal = trigger.dataset.target;
    console.log(modal);
    const target = document.getElementById(modal);

    trigger.addEventListener('click', () => {
        openModal(target);
    });
});

// Add a click event on various child elements to close the parent modal
(document.querySelectorAll('.cancel-modal-trigger') || []).forEach((trigger) => {
    /*const target = close.closest('.modal');*/
    const modal = trigger.dataset.target;
    const target = document.getElementById(modal);

    trigger.addEventListener('click', () => {
        closeModal(target);
    });
});

