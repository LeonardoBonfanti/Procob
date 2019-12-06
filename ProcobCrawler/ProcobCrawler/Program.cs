using ProcobCrawler.Enum;
using System.Collections.Generic;

namespace ProcobCrawler
{
    class Program
    {
        static void Main(string[] args)
        {
            DriverNavigation navigation = new DriverNavigation();

            List<string> contatos = new List<string>();

            navigation.setupInit("israelrockenbach.adv@gmail.com", "escritorio1712", (int)EnumMenu.DADOS_CADASTRAIS, (int)EnumDadosCadastrais.CPF_CNPJ_COMPLETO, contatos);

            navigation.driverNavigation();

            //navigation.close();
        }
    }
}
