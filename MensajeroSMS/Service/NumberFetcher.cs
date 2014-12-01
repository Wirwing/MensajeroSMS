using MensajeroSMS.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MensajeroSMS.Service
{
    class NumberFetcher
    {

        public static String FromContacts(List<Contacto> contacts)
        {
            StringBuilder builder = new StringBuilder();

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
