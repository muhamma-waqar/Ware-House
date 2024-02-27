﻿using Application.Common.Dependencies.DataAccess;
using Application.Common.Exceptions;
using Domain.Transactions;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Transactions.CreateTransaction
{
    public record CreateTransactionCommand : IRequest<int>
    {
        public int PartnerId { get; set; }
        public TransactionType TransactionType { get; set; }
        public TransactionLine[] TransactionLines { get; set; } = Array.Empty<TransactionLine>();
        public struct TransactionLine
        {
            public int ProductId { get; init; }
            public int ProductQuantity { get; init; }
        }
    }

    public class CreateTransactionCommandHandler : IRequestHandler<CreateTransactionCommand, int>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateTransactionCommandHandler(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

        public async Task<int> Handle(CreateTransactionCommand request, CancellationToken cancellationToken)
        {
            var partner = await _unitOfWork.Partners.GetByIdAsync(request.PartnerId)
                ?? throw new InputValidationException((nameof(request.PartnerId), $"Partner (id: {request.PartnerId}) was not found."));

            await _unitOfWork.BeginTransactionAsync();
            int createdTransactionId = 0;
            try
            {
                var orderedProductIds = request.TransactionLines.Select(x => x.ProductId).Distinct();
                var orderedProducts = await _unitOfWork.Products.GetFiltered(x => orderedProductIds.Contains(x.Id));

                var validLines = request.TransactionLines.Select(line =>
                (
                 product: orderedProducts.FirstOrDefault(p => p.Id == line.ProductId)
                 ?? throw new InputValidationException((nameof(line.ProductId), $"Product (id: {line.ProductId}) was not found.")),
                 qty: line.ProductQuantity
                ));

                var transaction = request.TransactionType switch
                {
                    TransactionType.Sales => partner.SellTo(validLines),
                    TransactionType.Procurement => partner.ProcureFrom(validLines),
                    _ => throw new InvalidEnumArgumentException($"No operation is defined for {nameof(TransactionType)} of '{request.TransactionType}'")
                };

                await _unitOfWork.SaveChanges();

                createdTransactionId = transaction.Id;
            }
            catch
            {
                await _unitOfWork.RollbackTransactionAsync();
                throw;
            }

            await _unitOfWork.CommitTransactionAsync();

            return createdTransactionId;
        }
    }
}
