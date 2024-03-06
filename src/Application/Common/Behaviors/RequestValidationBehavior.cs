using Application.Common.Exceptions;
using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Behaviors
{
    public class RequestValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;

        public RequestValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators;
        }

        public Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            if (_validators.Any())
            {
                var context = new ValidationContext<TRequest>(request);

                var results = Task.WhenAll(_validators.Select(v => v.ValidateAsync(context))).Result;

                var failures = results.Where(r => !r.IsValid).SelectMany(r => r.Errors).ToList();

                if (failures.Any())
                {
                    throw new InputValidationException(failures);
                }
            }

            return next();
        }
    }
}
