using System;

namespace SGI_JMC.Models
{
    public class GuiaDeTransferencia
    {
        public DateTime DataEmissao { get; set; }
        public string NomeAluno { get; set; }
        public int numeroTransferencia { get; set; }
        public string codigoAutenticacaoTransferencia { get; set; }
    }
}
