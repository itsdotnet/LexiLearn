﻿namespace LexiLearn.Service.DTOs.Quizzes.QuizHistory;

public class QuizHistoryCreationDto
{
    public long QuizId { get; set; }

    public long UserId { get; set; }

    public long Score { get; set; }
}