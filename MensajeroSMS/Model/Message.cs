using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MensajeroSMS.Model.MasMensajes
{
    public class Message
    {

        private static readonly Dictionary<string, string> StatusDictionary = new Dictionary<string, string>()
        {
            { "S", "Enviado" },
            {"N", "No enviado"},
            {"U", "Listo para enviar"},
            {"B", "Enviando"},
            {"NIL", "Desconocido"}
        };

        public string Content { get; set; }
        public string Date { get; set; }

        public string Status
        {
            get { return _status; }
            set
            {

                var raw = value;

                string stat = "NIL";

                if (raw.Contains("S"))
                {
                    stat = "S";
                }
                else if (raw.Contains("N"))
                {
                    stat = "N";
                }
                else if (raw.Contains("U"))
                {
                    stat = "U";
                }
                else if (raw.Contains("B"))
                {
                    stat = "B";
                }

                _status = StatusDictionary[stat];

            }
        }

        private String _status;

        public static List<Message> GetFromResponse(String raw)
        {
            var rawMessages = raw.Split('\n').ToList();

            if (rawMessages.Count <= 3)
            {
                return new List<Message>();
            }

            rawMessages.RemoveAt(rawMessages.Count - 1);
            rawMessages.RemoveAt(0);

            return rawMessages.Select(rawMessage =>
                rawMessage.Split('|')).Select(messageComponents =>
                    new Message()
                    {
                        Content = messageComponents[0],
                        Date = messageComponents[2],
                        Status = messageComponents[3]
                    }
                ).ToList();
        }
    }

}
