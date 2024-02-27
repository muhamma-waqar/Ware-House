﻿using Application.Common.Mapping;
using AutoMapper;
using Domain.Transactions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Transactions.GetTransactionDetails
{
    public record TransactionDetailsDto : IMapForm<Transaction>
    {
        public int Id { get; init; }

        public int TransactionType { get; init; }
        public DateTime CreateAt {  get; init; }
        public string CreatedBy { get; init; } = null!;
        public DateTime ModifiedAt { get; init; }
        public string? ModifiedBy { get; init; }

        public int PartnerId { get; init; }
        public string PartnerName { get; init; } = null!;
        public string PartnerAddress { get; init; } = null!;
        public bool PartnerIsDeleted { get; init; }
        public decimal TotalAmount { get; init; }
        public string TotalCurrencyCode { get; init; } = null!;
        public List<TransactionLineDto> TransactionLines { get; init; } = new();

        public struct TransactionLineDto
        {
            public int ProductId { get; init; }
            public string ProductName { get; init; }   
            public int Quantity { get; init; }
            public bool ProductIsDeleted { get; init; }
            public string UnitPrice { get; init; }
            public decimal UnitPriceAmount { get; init; }
            public string UnitPriceCurrencyCode { get; init; }
        }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Transaction, TransactionDetailsDto>()
                .ForMember(dest => dest.PartnerIsDeleted, cfg => cfg .MapFrom(src => src.Partner.DeleteAt != null));
            profile.CreateMap<TransactionLine, TransactionLineDto>()
                .ForMember(dest => dest.ProductIsDeleted, cfg => cfg
                .MapFrom(src => src.Product.DeleteAt != null));
        }
    }
}
