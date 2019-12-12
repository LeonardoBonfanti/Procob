using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using ProcobCrawler.Enum;
using System;
using System.Collections.Generic;
using System.Threading;

namespace ProcobCrawler
{
    class Program
    {
        private static string urlProcob = "http://consulta.procob.com/pesquisa_v2/";
        private static ChromeDriver driver;

        private static List<string> contatos;

        private static int menu = (int)EnumMenu.DADOS_CADASTRAIS;
        private static int tab = (int)EnumDadosCadastrais.CPF_CNPJ_PELO_NOME;

        static void Main(string[] args)
        {
            initDriver();

            contatos = new List<string>();

            setupInit("israelrockenbach.adv@gmail.com", "escritorio1712");

            defineProcesso();
        }

        private static void initDriver()
        {
            ChromeOptions options = new ChromeOptions();
            options.AddArgument("--disable-notifications");
            options.AddArgument("start-maximized");

            driver = new ChromeDriver(options);
            driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(60);
        }

        /* Navega para página de login e efetua o login */
        private static void setupInit(string email, string password)
        {
            driver.Navigate().GoToUrl(urlProcob);

            try
            {
                string execField = string.Format("return document.querySelector('#formLogin > div:nth-child(1) > div > input').value = '{0}'", email);
                ((IJavaScriptExecutor)driver).ExecuteScript(execField);

                execField = string.Format("return document.querySelector('#formLogin > div:nth-child(2) > div > input').value = '{0}'", password);
                ((IJavaScriptExecutor)driver).ExecuteScript(execField);

                driver.FindElementByCssSelector("#formLogin > div:nth-child(3) > div:nth-child(1) > input").Click();

                Thread.Sleep(3000);
            }
            catch (Exception ex)
            {
                _ = ex.Message;
            }
        }

        private static void defineProcesso()
        {
            switch(menu)
            {
                case (int)EnumMenu.DADOS_CADASTRAIS:
                    {
                        DadosCadastrais dadosCadastrais = new DadosCadastrais(driver, tab, contatos);
                        dadosCadastrais.init();
                    }
                    break;
                case (int)EnumMenu.RESTRICOES_FINANCEIRAS:
                    break;
                case (int)EnumMenu.VEICULOS:
                    break;
                case (int)EnumMenu.MARKETING:
                    break;
                case (int)EnumMenu.SERVICOS_CARTORIAIS:
                    break;
                case (int)EnumMenu.ANTI_FRAUDE:
                    break;
            }
        }
    }
}
