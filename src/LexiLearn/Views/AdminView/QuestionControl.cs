﻿using System;
using LexiLearn.Domain.Enums;
using LexiLearn.Service.DTOs.Questions;
using LexiLearn.Service.Services;

namespace LexiLearn.Views.AdminView
{
    public class QuestionControl
    {
        private readonly QuestionService questionService;

        public QuestionControl()
        {
            questionService = new QuestionService();
        }

        public async Task Start()
        {
            Console.WriteLine("Welcome to Question Management Console");

            while (true)
            {
                Console.WriteLine("\nOptions:");
                Console.WriteLine("1. Create Question");
                Console.WriteLine("2. Update Question");
                Console.WriteLine("3. Delete Question");
                Console.WriteLine("4. View All Questions");
                Console.WriteLine("5. View Questions by Word");
                Console.WriteLine("6. View Questions by Type");
                Console.WriteLine("7. Exit");

                Console.Write("Select an option: ");
                var option = Console.ReadLine();

                switch (option)
                {
                    case "1":
                        await CreateQuestion();
                        break;
                    case "2":
                        await UpdateQuestion();
                        break;
                    case "3":
                        await DeleteQuestion();
                        break;
                    case "4":
                        await ViewAllQuestions();
                        break;
                    case "5":
                        await ViewQuestionsByWord();
                        break;
                    case "6":
                        await ViewQuestionsByType();
                        break;
                    case "7":
                        return;
                    default:
                        Console.WriteLine("Invalid option. Please select a valid option.");
                        break;
                }
            }
        }

        private async Task CreateQuestion()
        {
            Console.Write("Enter question type (0 for EngToUzb, 1 for UzbToEng): ");
            var questionType = (QuestionType)Convert.ToInt32(Console.ReadLine());

            Console.Write("Enter word ID: ");
            var wordId = Convert.ToInt64(Console.ReadLine());

            var newQuestionDto = new QuestionCreationDto
            {
                Type = questionType,
                WordId = wordId
            };

            var response = await questionService.CreateAsync(newQuestionDto);

            if (response.StatusCode == 201)
            {
                Console.WriteLine("Question created successfully!");
            }
            else
            {
                Console.WriteLine($"Error creating question: {response.Message}");
            }
        }

        private async Task UpdateQuestion()
        {
            Console.Write("Enter question ID to update: ");
            var questionId = Convert.ToInt64(Console.ReadLine());

            Console.Write("Enter question type (0 for EngToUzb, 1 for UzbToEng): ");
            var questionType = (QuestionType)Convert.ToInt32(Console.ReadLine());

            Console.Write("Enter word ID: ");
            var wordId = Convert.ToInt64(Console.ReadLine());

            var newQuestionDto = new QuestionUpdateDto
            {
                Id = questionId,
                Type = questionType,
                WordId = wordId
            };

            var response = await questionService.UpdateAsync(newQuestionDto);

            if (response.StatusCode == 201)
            {
                Console.WriteLine("Question updated successfully!");
            }
            else
            {
                Console.WriteLine($"Error updating question: {response.Message}");
            }
        }

        private async Task DeleteQuestion()
        {
            Console.Write("Enter question ID to delete: ");
            var questionId = Convert.ToInt64(Console.ReadLine());

            var response = await questionService.DeleteAsync(questionId);

            if (response.StatusCode == 201)
            {
                Console.WriteLine("Question deleted successfully!");
            }
            else
            {
                Console.WriteLine($"Error deleting question: {response.Message}");
            }
        }

        private async Task ViewAllQuestions()
        {
            var response = await questionService.GetAllAsync();

            if (response.StatusCode == 200)
            {
                Console.WriteLine("All Questions:");
                foreach (var question in response.Data)
                {
                    Console.WriteLine($"Question ID: {question.Id}, Type: {question.Type}, Word ID: {question.WordId}");
                }
            }
            else
            {
                Console.WriteLine($"Error retrieving questions: {response.Message}");
            }
        }


        private async Task ViewQuestionsByWord()
        {
            Console.Write("Enter word ID: ");
            var wordId = Convert.ToInt64(Console.ReadLine());

            var response = await questionService.GetQuestionsByWordAsync(wordId);

            if (response.StatusCode == 200)
            {
                Console.WriteLine($"Questions for Word ID {wordId}:");
                foreach (var question in response.Data)
                {
                    Console.WriteLine($"Question ID: {question.Id}, Type: {question.Type}");
                }
            }
            else
            {
                Console.WriteLine($"Error retrieving questions: {response.Message}");
            }
        }


        private async Task ViewQuestionsByType()
        {
            // ... (similar to CreateQuestion)
        }
    }
}