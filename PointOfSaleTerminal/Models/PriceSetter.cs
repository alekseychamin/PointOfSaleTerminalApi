﻿using PointOfSaleTerminalApi.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PointOfSaleTerminalApi.Models
{
    public class PriceSetter : IPriceSetter
    {
        private readonly Dictionary<string, IVolumePrice> _prices = new();
        private readonly ILog _log;

        public PriceSetter(ILog log)
        {
            _log = log;
        }

        public Dictionary<string, IVolumePrice> Prices { get => _prices; }

        public void SetPricing(List<IVolumePrice> prices)
        {
            _ = prices ?? throw new ArgumentNullException(nameof(prices));

            foreach (var price in prices)
            {
                Validate(price);

                if (!_prices.ContainsKey(price.ProductCode))
                {
                    _prices.Add(price.ProductCode, price);
                }
                else
                {
                    _log.LogMessage($"{nameof(PointOfSaleTerminal)}: Can't set price. Such code of product: {price.ProductCode} is exist");
                }
            }
        }

        private void Validate(IVolumePrice price)
        {
            _ = price ?? throw new ArgumentNullException(nameof(price));

            if (!CheckRange.IsValid(price.PricePerUnit))
            {
                throw new ArgumentException(nameof(price.PricePerUnit));
            }

            if (!CheckRange.IsValid(price.PriceDiscount))
            {
                throw new ArgumentException(nameof(price.PriceDiscount));
            }

            if (!CheckRange.IsValid(price.VolumeDiscount))
            {
                throw new ArgumentException(nameof(price.VolumeDiscount));
            }
        }
    }
}