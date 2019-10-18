using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace TeduCoreApp.Data.Enums
{
    public enum PaymentMethod
    {
        [Description("Trả tiền sau khi nhận hàng")]
        CashOnDelivery,
        [Description("PayPal")]
        PayPal,
      
    }
}
