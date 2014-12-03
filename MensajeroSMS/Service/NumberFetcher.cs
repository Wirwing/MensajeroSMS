using System;
using System.Collections.Generic;
using System.Text;
using MensajeroSMS.Model;

namespace MensajeroSMS.Service
{
    internal class NumberFetcher
    {
        public static String FromContacts(List<Contacto> contacts)
        {
            var builder = new StringBuilder();

            for (int i = 0; i < contacts.Count; i++)
            {
                builder.Append(contacts[i].Cellphone);

                if (i != contacts.Count - 1)
                    builder.Append(",");
            }

            return builder.ToString();
        }
    }
}