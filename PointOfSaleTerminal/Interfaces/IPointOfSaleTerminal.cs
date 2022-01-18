﻿using PointOfSaleTerminalApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PointOfSaleTerminalApi.Interfaces
{
    public interface IPointOfSaleTerminal
    {
        string ScanedCodes { get; }

        void Scan(string productCode);

        void SetPricing(List<IVolumePrice> prices);

        double CalculateTotal();
    }
}
