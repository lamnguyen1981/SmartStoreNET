using System;

namespace SmartStore.CreditCardPay.Exceptions
{
    public  class HeartlandCustomErrorException: Exception
    {
        public int Code { get; set; }

        public string Title { get; set; }

        public string Detail { get; set; }

        public string Instance { get; set; }

        public string Info { get; set; }

        public string Type { get; set; }
    }
}