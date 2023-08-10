using LexiLearn.Domain.Entity.User;
using LexiLearn.Service.Services;

namespace LexiLearn.Views.UserView;

public class MainUserUI
{
    private QuestionService questionService;
    private QuizService quizService;
    private QuizHistoryService quizHistoryService;
    private UserService userService;
    private User currentUser;

    public MainUserUI(User user)
    {
        this.currentUser = user;
        this.questionService = new QuestionService();
        this.quizService = new QuizService();
        this.quizHistoryService = new QuizHistoryService();
        this.userService = new UserService();
    }






}
