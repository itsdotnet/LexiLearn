using AutoMapper;
using LexiLearn.Domain.Entities.Categories;
using LexiLearn.Domain.Entities.Questions;
using LexiLearn.Domain.Entities.Quizzes;
using LexiLearn.Domain.Entities.Words;
using LexiLearn.Domain.Entity.User;
using LexiLearn.Service.DTOs.Categories;
using LexiLearn.Service.DTOs.Questions;
using LexiLearn.Service.DTOs.Quizzes;
using LexiLearn.Service.DTOs.Users;
using LexiLearn.Service.DTOs.Words;

namespace LexiLearn.Service.Mappers;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        //User

        CreateMap<User, UserCreationDto>().ReverseMap();
        CreateMap<User, UserResultDto>().ReverseMap();
        CreateMap<User, UserUpdateDto>().ReverseMap();
        CreateMap<UserResultDto, UserCreationDto>().ReverseMap();

        //Words

        CreateMap<Word, WordCreationDto>().ReverseMap();
        CreateMap<Word, WordResultDto>().ReverseMap();
        CreateMap<Word, WordUpdateDto>().ReverseMap();
        CreateMap<WordResultDto, WordCreationDto>().ReverseMap();

        //Categories

        CreateMap<WordCategory, CategoryCreationDto>().ReverseMap();
        CreateMap<WordCategory, CategoryResultDto>().ReverseMap();
        CreateMap<WordCategory, CategoryUpdateDto>().ReverseMap();

        CreateMap<QuizCategory, CategoryCreationDto>().ReverseMap();
        CreateMap<QuizCategory, CategoryResultDto>().ReverseMap();
        CreateMap<QuizCategory, CategoryUpdateDto>().ReverseMap();
        CreateMap<CategoryResultDto, CategoryCreationDto>().ReverseMap();

        //Quizzes

        CreateMap<Quiz, QuizCreationDto>().ReverseMap();
        CreateMap<Quiz, QuizResultDto>().ReverseMap();
        CreateMap<Quiz, QuizUpdateDto>().ReverseMap();
        CreateMap<QuizResultDto, QuizCreationDto>().ReverseMap();

        //Questions

        CreateMap<Question, QuestionCreationDto>().ReverseMap();
        CreateMap<Question, QuestionResultDto>().ReverseMap();
        CreateMap<Question, QuestionUpdateDto>().ReverseMap();
        CreateMap<QuestionResultDto, QuestionCreationDto>().ReverseMap();
    }
}
