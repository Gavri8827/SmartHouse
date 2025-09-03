using System;
using System.Collections.Generic;
using System.Text;

namespace SmartHouse.UtilitiesClass.Account
{
    public class Account
    {

        public string Electricity {  get; set; }

        public string ElectricityNumber { get; set; }

        public decimal ElectricityPayment { get; set; }

        public string ElectricityDate { get; set; }

        public string Water { get; set; }

        public string WaterNumber { get; set; }

        public decimal WaterPayment { get; set; }

        public string WaterDate { get; set; }

        public string GasCompany { get; set; }

        public string GasNumber { get; set; }

        public string GasPhoneNumber { get; set; }

        public decimal GasPayment { get; set; }

        public string GasDate { get; set; }

        public string FirebaseKey { get; set; }

    }
}
