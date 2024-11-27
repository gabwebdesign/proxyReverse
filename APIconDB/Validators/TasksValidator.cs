using APIconDB.Entities;
using FluentValidation;

namespace APIconDB.Validators;

public class TasksValidator:AbstractValidator<Tasks>
{
    public TasksValidator()
    {
        RuleFor(tasks => tasks.Title).NotEmpty().NotNull().WithName("Name");
        RuleFor(tasks => tasks.Description).NotEmpty().NotNull().WithName("Name");
        RuleFor(tasks => tasks.CreatedAt).NotEmpty().NotNull();
        RuleFor(tasks => tasks.DueDate).NotEmpty().NotNull();
        RuleFor(tasks => tasks.State).NotEmpty().NotNull();
    }
}