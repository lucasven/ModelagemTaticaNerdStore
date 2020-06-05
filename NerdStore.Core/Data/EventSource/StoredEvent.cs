using System;
using System.Collections.Generic;
using System.Text;

namespace NerdStore.Core.Data.EventSource
{
    public class StoredEvent
    {
        public Guid Id { get; set; }
        public string Tipo { get; set; }
        public DateTime DataOcorrencia { get; set; }
        public string Dados { get; set; }
    }
}
