﻿namespace P01_BillsPaymentSystem.Initializer
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Data.Models;

    public static class CreditCardsGenerator
    {
        public static CreditCard[] GetCreditCards()
        {
            var creditCards = new[]
            {
                new CreditCard {  Limit =  4723.20M,   MoneyOwed = 928.64M,   ExpirationDate = new DateTime(2020, 09, 27)},
                new CreditCard {  Limit =  1156.51M,   MoneyOwed = 629.66M,   ExpirationDate = new DateTime(2020, 12, 30)},
                new CreditCard {  Limit =  7099.79M,   MoneyOwed = 325.07M,   ExpirationDate = new DateTime(2020, 01, 30)},
                new CreditCard {  Limit =  5939.89M,   MoneyOwed = 986.53M,   ExpirationDate = new DateTime(2022, 03, 23)},
                new CreditCard {  Limit =  5030.75M,   MoneyOwed = 809.71M,   ExpirationDate = new DateTime(2022, 05, 28)},
                new CreditCard {  Limit =  3164.05M,   MoneyOwed = 929.06M,   ExpirationDate = new DateTime(2021, 10, 03)},
                new CreditCard {  Limit =  9185.25M,   MoneyOwed = 347.44M,   ExpirationDate = new DateTime(2022, 08, 25)},
                new CreditCard {  Limit =  5686.16M,   MoneyOwed = 291.01M,   ExpirationDate = new DateTime(2021, 07, 02)},
                new CreditCard {  Limit =  1066.98M,   MoneyOwed = 984.03M,   ExpirationDate = new DateTime(2020, 04, 20)},
                new CreditCard {  Limit =  9661.24M,   MoneyOwed = 399.26M,   ExpirationDate = new DateTime(2023, 06, 16)},
                new CreditCard {  Limit =  4301.92M,   MoneyOwed = 125.88M,   ExpirationDate = new DateTime(2021, 11, 28)},
                new CreditCard {  Limit =  6315.12M,   MoneyOwed = 484.4M,   ExpirationDate = new DateTime(2021, 06, 16)},
                new CreditCard {  Limit =  7112.85M,   MoneyOwed = 696.94M,   ExpirationDate = new DateTime(2020, 07, 20)},
                new CreditCard {  Limit =  2335.65M,   MoneyOwed = 187.49M,   ExpirationDate = new DateTime(2019, 01, 12)},
                new CreditCard {  Limit =  4590.99M,   MoneyOwed = 212.71M,   ExpirationDate = new DateTime(2023, 12, 18)},
                new CreditCard {  Limit =  7626.1M,   MoneyOwed = 917.51M,   ExpirationDate = new DateTime(2022, 01, 30)},
                new CreditCard {  Limit =  1063.23M,   MoneyOwed = 258.69M,   ExpirationDate = new DateTime(2023, 01, 19)},
                new CreditCard {  Limit =  5477.21M,   MoneyOwed = 874.29M,   ExpirationDate = new DateTime(2023, 03, 31)},
                new CreditCard {  Limit =  4446.39M,   MoneyOwed = 855.38M,   ExpirationDate = new DateTime(2021, 06, 20)},
                new CreditCard {  Limit =  4913.79M,   MoneyOwed = 972.12M,   ExpirationDate = new DateTime(2021, 07, 23)},
                new CreditCard {  Limit =  5812.7M,   MoneyOwed = 231.69M,   ExpirationDate = new DateTime(2021, 06, 11)},
                new CreditCard {  Limit =  1327.8M,   MoneyOwed = 129.69M,   ExpirationDate = new DateTime(2022, 08, 30)},
                new CreditCard {  Limit =  3616.79M,   MoneyOwed = 262.07M,   ExpirationDate = new DateTime(2021, 04, 16)},
                new CreditCard {  Limit =  6932.0M,   MoneyOwed = 641.08M,   ExpirationDate = new DateTime(2019, 04, 26)},
                new CreditCard {  Limit =  1806.58M,   MoneyOwed = 503.63M,   ExpirationDate = new DateTime(2021, 02, 08)},
                new CreditCard {  Limit =  8492.67M,   MoneyOwed = 764.92M,   ExpirationDate = new DateTime(2019, 12, 13)},
                new CreditCard {  Limit =  9829.93M,   MoneyOwed = 920.78M,   ExpirationDate = new DateTime(2023, 05, 20)},
                new CreditCard {  Limit =  4414.12M,   MoneyOwed = 817.55M,   ExpirationDate = new DateTime(2021, 02, 19)},
                new CreditCard {  Limit =  8356.84M,   MoneyOwed = 887.69M,   ExpirationDate = new DateTime(2021, 07, 02)},
                new CreditCard {  Limit =  7315.5M,   MoneyOwed = 164.86M,   ExpirationDate = new DateTime(2023, 03, 04)},
                new CreditCard {  Limit =  1737.37M,   MoneyOwed = 363.56M,   ExpirationDate = new DateTime(2023, 09, 17)},
                new CreditCard {  Limit =  3769.68M,   MoneyOwed = 317.97M,   ExpirationDate = new DateTime(2020, 07, 23)},
                new CreditCard {  Limit =  7726.46M,   MoneyOwed = 307.29M,   ExpirationDate = new DateTime(2022, 11, 08)},
                new CreditCard {  Limit =  1873.56M,   MoneyOwed = 566.21M,   ExpirationDate = new DateTime(2019, 03, 08)},
                new CreditCard {  Limit =  5014.72M,   MoneyOwed = 268.12M,   ExpirationDate = new DateTime(2023, 03, 29)},
                new CreditCard {  Limit =  5635.63M,   MoneyOwed = 184.92M,   ExpirationDate = new DateTime(2021, 10, 26)},
                new CreditCard {  Limit =  6502.7M,   MoneyOwed = 611.71M,   ExpirationDate = new DateTime(2019, 06, 11)},
                new CreditCard {  Limit =  7649.73M,   MoneyOwed = 307.06M,   ExpirationDate = new DateTime(2019, 10, 09)},
                new CreditCard {  Limit =  2280.75M,   MoneyOwed = 433.7M,   ExpirationDate = new DateTime(2021, 11, 26)},
                new CreditCard {  Limit =  7571.08M,   MoneyOwed = 995.63M,   ExpirationDate = new DateTime(2020, 07, 19)},
                new CreditCard {  Limit =  9703.35M,   MoneyOwed = 445.71M,   ExpirationDate = new DateTime(2020, 01, 04)},
                new CreditCard {  Limit =  8753.69M,   MoneyOwed = 825.08M,   ExpirationDate = new DateTime(2020, 05, 20)},
                new CreditCard {  Limit =  1115.03M,   MoneyOwed = 766.62M,   ExpirationDate = new DateTime(2020, 12, 21)},
                new CreditCard {  Limit =  8864.92M,   MoneyOwed = 319.56M,   ExpirationDate = new DateTime(2023, 01, 06)},
                new CreditCard {  Limit =  1221.27M,   MoneyOwed = 515.64M,   ExpirationDate = new DateTime(2019, 08, 15)},
                new CreditCard {  Limit =  4503.07M,   MoneyOwed = 121.71M,   ExpirationDate = new DateTime(2020, 11, 25)},
                new CreditCard {  Limit =  7439.44M,   MoneyOwed = 728.27M,   ExpirationDate = new DateTime(2021, 04, 22)},
                new CreditCard {  Limit =  7802.07M,   MoneyOwed = 390.52M,   ExpirationDate = new DateTime(2021, 11, 06)},
                new CreditCard {  Limit =  6794.8M,   MoneyOwed = 643.88M,   ExpirationDate = new DateTime(2021, 10, 31)},
                new CreditCard {  Limit =  2717.68M,   MoneyOwed = 677.19M,   ExpirationDate = new DateTime(2023, 02, 05)},
                new CreditCard {  Limit =  7861.24M,   MoneyOwed = 791.52M,   ExpirationDate = new DateTime(2019, 09, 21)},
                new CreditCard {  Limit =  1344.57M,   MoneyOwed = 970.69M,   ExpirationDate = new DateTime(2023, 10, 27)},
                new CreditCard {  Limit =  4519.31M,   MoneyOwed = 274.0M,   ExpirationDate = new DateTime(2019, 04, 15)},
                new CreditCard {  Limit =  2141.46M,   MoneyOwed = 617.65M,   ExpirationDate = new DateTime(2023, 01, 03)},
                new CreditCard {  Limit =  8608.81M,   MoneyOwed = 857.63M,   ExpirationDate = new DateTime(2023, 08, 26)},
                new CreditCard {  Limit =  5823.21M,   MoneyOwed = 387.39M,   ExpirationDate = new DateTime(2020, 01, 17)},
                new CreditCard {  Limit =  4460.37M,   MoneyOwed = 915.87M,   ExpirationDate = new DateTime(2020, 06, 10)},
                new CreditCard {  Limit =  5711.15M,   MoneyOwed = 612.15M,   ExpirationDate = new DateTime(2019, 10, 02)},
                new CreditCard {  Limit =  1286.17M,   MoneyOwed = 107.29M,   ExpirationDate = new DateTime(2022, 04, 03)},
                new CreditCard {  Limit =  9028.46M,   MoneyOwed = 892.15M,   ExpirationDate = new DateTime(2023, 05, 29)},
                new CreditCard {  Limit =  7175.1M,   MoneyOwed = 245.18M,   ExpirationDate = new DateTime(2022, 06, 01)},
                new CreditCard {  Limit =  3587.07M,   MoneyOwed = 229.42M,   ExpirationDate = new DateTime(2023, 06, 04)},
                new CreditCard {  Limit =  7255.1M,   MoneyOwed = 697.88M,   ExpirationDate = new DateTime(2021, 06, 05)},
                new CreditCard {  Limit =  2198.94M,   MoneyOwed = 625.45M,   ExpirationDate = new DateTime(2021, 11, 10)},
                new CreditCard {  Limit =  4604.78M,   MoneyOwed = 402.63M,   ExpirationDate = new DateTime(2023, 05, 09)},
                new CreditCard {  Limit =  2249.79M,   MoneyOwed = 787.33M,   ExpirationDate = new DateTime(2019, 05, 27)},
                new CreditCard {  Limit =  1245.26M,   MoneyOwed = 822.15M,   ExpirationDate = new DateTime(2019, 11, 29)},
                new CreditCard {  Limit =  3038.03M,   MoneyOwed = 568.11M,   ExpirationDate = new DateTime(2021, 01, 31)},
                new CreditCard {  Limit =  9790.35M,   MoneyOwed = 265.74M,   ExpirationDate = new DateTime(2022, 10, 16)},
                new CreditCard {  Limit =  7327.08M,   MoneyOwed = 966.68M,   ExpirationDate = new DateTime(2021, 11, 06)},
                new CreditCard {  Limit =  4795.61M,   MoneyOwed = 728.74M,   ExpirationDate = new DateTime(2019, 09, 23)},
                new CreditCard {  Limit =  6306.69M,   MoneyOwed = 754.64M,   ExpirationDate = new DateTime(2019, 02, 08)},
                new CreditCard {  Limit =  3954.65M,   MoneyOwed = 446.87M,   ExpirationDate = new DateTime(2019, 10, 10)},
                new CreditCard {  Limit =  8025.08M,   MoneyOwed = 646.56M,   ExpirationDate = new DateTime(2019, 04, 21)},
                new CreditCard {  Limit =  9751.45M,   MoneyOwed = 597.94M,   ExpirationDate = new DateTime(2020, 09, 29)},
                new CreditCard {  Limit =  6996.11M,   MoneyOwed = 104.39M,   ExpirationDate = new DateTime(2023, 11, 22)},
                new CreditCard {  Limit =  9664.14M,   MoneyOwed = 307.09M,   ExpirationDate = new DateTime(2023, 01, 30)},
                new CreditCard {  Limit =  7587.13M,   MoneyOwed = 424.82M,   ExpirationDate = new DateTime(2019, 01, 08)},
                new CreditCard {  Limit =  3752.77M,   MoneyOwed = 191.6M,   ExpirationDate = new DateTime(2020, 02, 14)},
                new CreditCard {  Limit =  6415.03M,   MoneyOwed = 431.3M,   ExpirationDate = new DateTime(2019, 07, 04)},
                new CreditCard {  Limit =  7745.99M,   MoneyOwed = 408.28M,   ExpirationDate = new DateTime(2019, 05, 14)},
                new CreditCard {  Limit =  1610.66M,   MoneyOwed = 637.47M,   ExpirationDate = new DateTime(2022, 03, 20)},
                new CreditCard {  Limit =  4056.98M,   MoneyOwed = 302.79M,   ExpirationDate = new DateTime(2022, 07, 26)},
                new CreditCard {  Limit =  9568.77M,   MoneyOwed = 783.38M,   ExpirationDate = new DateTime(2023, 08, 17)},
                new CreditCard {  Limit =  6744.02M,   MoneyOwed = 785.04M,   ExpirationDate = new DateTime(2022, 03, 29)},
                new CreditCard {  Limit =  4918.56M,   MoneyOwed = 417.05M,   ExpirationDate = new DateTime(2022, 02, 22)},
                new CreditCard {  Limit =  5179.6M,   MoneyOwed = 301.17M,   ExpirationDate = new DateTime(2021, 12, 08)},
                new CreditCard {  Limit =  3864.64M,   MoneyOwed = 249.85M,   ExpirationDate = new DateTime(2023, 01, 29)},
                new CreditCard {  Limit =  7887.89M,   MoneyOwed = 422.58M,   ExpirationDate = new DateTime(2023, 05, 26)},
                new CreditCard {  Limit =  2844.79M,   MoneyOwed = 354.23M,   ExpirationDate = new DateTime(2022, 08, 19)},
                new CreditCard {  Limit =  6484.23M,   MoneyOwed = 887.22M,   ExpirationDate = new DateTime(2021, 06, 07)},
                new CreditCard {  Limit =  5739.82M,   MoneyOwed = 322.89M,   ExpirationDate = new DateTime(2021, 03, 07)},
                new CreditCard {  Limit =  9735.42M,   MoneyOwed = 561.25M,   ExpirationDate = new DateTime(2022, 10, 30)},
                new CreditCard {  Limit =  2985.83M,   MoneyOwed = 243.83M,   ExpirationDate = new DateTime(2021, 04, 27)},
                new CreditCard {  Limit =  4052.6M,   MoneyOwed = 694.82M,   ExpirationDate = new DateTime(2021, 08, 19)},
                new CreditCard {  Limit =  9657.44M,   MoneyOwed = 453.91M,   ExpirationDate = new DateTime(2021, 11, 01)},
                new CreditCard {  Limit =  9181.81M,   MoneyOwed = 767.25M,   ExpirationDate = new DateTime(2023, 07, 06)},
                new CreditCard {  Limit =  4865.24M,   MoneyOwed = 623.67M,   ExpirationDate = new DateTime(2022, 06, 06)},
                new CreditCard {  Limit =  8346.0M,   MoneyOwed = 961.43M,   ExpirationDate = new DateTime(2022, 08, 19)},
                new CreditCard {  Limit =  5502.27M,   MoneyOwed = 257.64M,   ExpirationDate = new DateTime(2023, 10, 18)}
            };

            return creditCards;
        }
    }
}