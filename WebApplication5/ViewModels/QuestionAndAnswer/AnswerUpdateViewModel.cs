﻿using System.ComponentModel.DataAnnotations;
using WebApplication5.Models;

namespace WebApplication5.ViewModels.QuestionAndAnswer
{
    public class AnswerUpdateViewModel
    {
        [Required]
        [MaxLength(ValidationConstants.MaxPostContentLength)]
        public string Content { get; set; }

        public string TruncatedContent { get; set; }

        public Question? AssociatedQuestion { get; set; }

        public Answer? OriginalAnswer { get; set; }
    }
}
