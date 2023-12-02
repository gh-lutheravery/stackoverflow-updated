﻿using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using PagedList.Core.Mvc;
using System.Collections.Generic;
using System.Globalization;
using System.Security.Policy;
using WebApplication5.Controllers.DataServices;
using WebApplication5.Models;
using WebApplication5.ViewModels.Home;
using X.PagedList;

namespace WebApplication5.Controllers.BusinessControllers
{
    public class HomeBusinessController
    {
        private SOCloneContextService _context;
        private readonly int _pageSize = 10;

        private readonly string[] _sortTypes = new string[]
            { "Newest", "Unanswered" };

        private readonly string[] _filterTypes = new string[]
            { "NoAnswers", "NoAcceptedAnswers" };

        public HomeBusinessController(SOCloneContextService context) 
        { 
            _context = context;
        }

        // sorts/filters all questions from ef, randomizes sort of tags from ef and sets query variables
        public HomeViewModel PopulateHomeViewModel(int pageNumber, string? sortBy, string? filterBy)
        {
            HomeViewModel vm = new HomeViewModel();
            var allQuestions = _context.GetAllQuestions(true).ToList();

            if (!sortBy.IsNullOrEmpty() && _sortTypes.Contains(sortBy)) 
            {
                allQuestions = SortQuestions(allQuestions, sortBy);
            }

            if (!filterBy.IsNullOrEmpty() && _filterTypes.Contains(filterBy))
            {
                allQuestions = FilterQuestions(allQuestions, filterBy);
            }

            vm.Questions = allQuestions.ToPagedList(pageNumber, _pageSize);

            var tags = _context.GetAllTags(false)
                .Select(t => t.Title).ToList();
            vm.RandomTags = RandomizeTags(tags).Take(10).ToList();

            vm.SortBy = sortBy;
            vm.FilterBy = filterBy;

            return vm;
        }


        private List<Question> SortQuestions(List<Question> questions, string sortBy)
        {
            var newQuestions = new List<Question>();
            
            switch (sortBy)
            {
                case "Newest":
                    newQuestions = questions.OrderByDescending(q => q.DateCreated).ToList();
                    return newQuestions;

                case "Unanswered":
                    newQuestions = questions.OrderBy(q => q.AnswerCount).ToList();
                    return newQuestions;

                default:
                    break;
            }
            return questions;
        }


        private List<Question> FilterQuestions(List<Question> oldQuestions, string filterBy)
        {
            var newQuestions = new List<Question>();
            switch (filterBy)
            {
                case "NoAnswers":
                    foreach (var question in oldQuestions)
                    {
                        if (question.AnswerCount == 0)
                            newQuestions.Add(question);
                    }

                    return newQuestions;

                case "NoAcceptedAnswers":
                    foreach (var question in oldQuestions)
                    {
                        if (question.AcceptedAnswerId == 0 || question.AcceptedAnswerId == null)
                            newQuestions.Add(question);
                    }

                    return newQuestions;

                default:
                    break;
            }
            return oldQuestions;
        }

        private List<string> RandomizeTags(List<string> tags)
        {
            Random rand = new Random();
            var shuffled = tags.OrderBy(a => rand.Next()).ToList();
            return shuffled;
        }

        // search matching questions based on its title, randomizes sort of tags from ef and sets query variables
        public SearchViewModel PopulateSearchViewModel(int pageNumber, string searchTerm, string sortBy)
        {
            SearchViewModel vm = new SearchViewModel();

            var allQuestions = _context.GetAllQuestions(true).ToList();

            if (searchTerm.StartsWith("tag:"))
                allQuestions = allQuestions.Where(q => QuestionContains(q, string.Empty, searchTerm)).ToList();
            else
                allQuestions = allQuestions.Where(q => QuestionContains(q, searchTerm)).ToList();

            if (sortBy.IsNullOrEmpty())
                vm.Questions = SortQuestions(allQuestions, sortBy).ToPagedList(pageNumber, _pageSize);

            var tags = _context.GetAllTags(false)
                .Select(t => t.Title).ToList();
            vm.RandomTags = RandomizeTags(tags).Take(10).ToList();

            vm.SortBy = sortBy;
            vm.SearchQuery = searchTerm;

            return vm;
        }

        private bool QuestionContains(Question question, string searchTerm, string tagsString = "") 
        {
            if (!searchTerm.IsNullOrEmpty())
            {
                if (question.Title.ToLower().Contains(searchTerm.ToLower()))
                    return true;
            }

            if (!tagsString.IsNullOrEmpty())
            {
                string tagSearchTerm = tagsString.Split("tag:")[1];
                if (question.Tags.Where(t => t.Title.ToLower() == tagSearchTerm.ToLower()).Any())
                    return true;
            }
            
            return false;
        }
    }
}
