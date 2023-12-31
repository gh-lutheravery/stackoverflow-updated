﻿using System.ComponentModel.DataAnnotations;
using WebApplication5.Models;

namespace WebApplication5.ViewModels.QuestionAndAnswer
{
    public class AnswerCreateViewModel
    {
        [MinLength(ValidationConstants.MinPostContentLength)]
        [MaxLength(ValidationConstants.MaxPostContentLength)]
        public string Content { get; set; }

        public string TruncatedContent { get; set; }

        public int AssociatedQuestionId { get; set; }
    }
}
