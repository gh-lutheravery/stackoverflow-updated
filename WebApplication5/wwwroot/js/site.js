// Requires jquery for vote count functions, and quill for init function



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


// Functions for modals used for various CRUD views
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



// Functions for voting in QuestionAnswer view
function handleQuestionVote(element, color, vote) {
    // vote is true when upvoting, and vice versa
    element.css('background-color', color);

    incrementVoteCount($('#ques-vote-count'), vote);

    // $.ajax({
    //     url: '@Url.Action("UpdateVote", "Question")',
    //     method: 'POST',
    //     data: { questionId: @Model.Question.Id, upVote: vote },
    //     contentType: "application/json; charset=utf-8",
    //     dataType: "json",
    //     error: function() {
    //         console.log('Error voting post.');
    //     }
    // });
}

function handleAnswerVote(element, color, vote) {
    element.css('background-color', color);

    let voteContainer = element.parent().parent();
    let voteCountEl = voteContainer.children('.ans-vote-count')[0];

    incrementVoteCount(voteCountEl, vote);

    // $.ajax({
    //     url: '@Url.Action("UpdateVote", "Answer")',
    //     method: 'POST',
    //     data: { answerId: @Model.Question.Id, upVote: vote },
    //     contentType: "application/json; charset=utf-8",
    //     dataType: "json",
    //     error: function() {
    //         console.log('Error voting post.');
    //     }
    // });
}

function incrementVoteCount(element, vote) {
    let voteCount = Number(element.text());

    if (vote) {
        element.text(
            String(voteCount + 1)
        );
    }
    else {
        element.text(
            String(voteCount - 1)
        );
    }
}

$('#ques-up-vote').click(function () {
    handleQuestionVote($(this), "#0066ffff", true)
});

$('#ques-down-vote').click(function () {
    handleQuestionVote($(this), "#42414cff", false)
});

$('.ans-up-vote').click(function () {
    handleAnswerVote($(this), "#0066ffff", true)
});

$('.ans-down-vote').click(function () {
    handleAnswerVote($(this), "#42414cff", false)
});
