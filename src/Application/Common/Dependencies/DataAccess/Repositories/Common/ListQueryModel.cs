using Application.Common.Exceptions;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Dependencies.DataAccess.Repositories.Common
{
    public class ListQueryModel<TDto> : IRequest<IListResponseModel<TDto>>
    {
        [Range(1, int.MaxValue, ErrorMessage = "The minimum page index is 1.")]
        public int PageIndex { get; set; } = 1;
        [Range(1, MAX_PAGESIZE)]
        public int PageSize { get; set; } = DEFAULT_PAGESIZE;

        public string OrderBy { get; set; } = "id";

        public string? Filter { get; set; } 
        private const int DEFAULT_PAGESIZE = 20;
        private const int MAX_PAGESIZE = 100;

        public void ThrowOrderByIncorrectException(Exception? innerException)
        {
            throw new InputValidationException(innerException, (PropertyName: nameof(OrderBy), ErrorMessage: $"The specified orderBy string '{OrderBy}' is invalid."));
        }

        public void ThrowFilterIncorrectException(Exception? innerException)
        {
            throw new InputValidationException(innerException,
                (
                    PropertyName: nameof(Filter),
                    ErrorMessage: $"The specified filter string '{Filter}' is invalid."
                )
            );
        }
    }
}
