﻿using Bira.App.SchoolManager.Domain.Package;
using Bira.App.SchoolManager.Service.Interfaces;
using Bira.App.SchoolManager.Service.Notifications;
using FluentValidation;
using FluentValidation.Results;

namespace Bira.App.SchoolManager.Service.Services
{
    public abstract class BaseService
    {
        private readonly INotifier _notifier;

        protected BaseService(INotifier notifier)
        {
            _notifier = notifier;
        }

        protected void Notify(ValidationResult validationResult)
        {
            foreach (var error in validationResult.Errors)
            {
                Notify(error.ErrorMessage);
            }
        }

        protected void Notify(string message)
        {
            _notifier.Handle(new Notification(message));
        }

        protected bool RunValidation<TV, TE>(TV validation, TE entity) where TV : AbstractValidator<TE> where TE : EntityBase
        {
            var validator = validation.Validate(entity);

            if (validator.IsValid) return true;

            Notify(validator);

            return false;

        }
    }
}