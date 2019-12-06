using System;
using System.Collections.Generic;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using ProcobCrawler.Enum;

namespace ProcobCrawler
{
    public class DriverNavigation
    {
        /* Link fixo de início de sessão */
        private string urlProcob = "http://consulta.procob.com/pesquisa_v2/";
        ChromeDriver driver;

        List<string> contatos;

        private string email, password;
        private int menu, tab;

        /* Construtor da classe DriverNavigation */
        public DriverNavigation()
        {
            ChromeOptions options = new ChromeOptions();
            options.AddArgument("--disable-notifications");
            options.AddArgument("start-maximized");

            driver = new ChromeDriver(options);
            driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(60);
        }

        /* Configuração inicial do Crawler */
        public void setupInit(string email, string password, int menu, int tab, List<string> contatos)
        {
            this.tab = tab;
            this.menu = menu;
            this.email = email;
            this.password = password;
            this.contatos = contatos;
        }

        /* Método que realiza a abertura do site */
        public void driverNavigation()
        {
            try
            {
                driver.Navigate().GoToUrl(urlProcob);

                makeLogin();

                defineProcess();

                search();
            }
            catch (Exception ex)
            {
                _ = ex.Message;
            }
        }

        /* Efetua o login */
        private void makeLogin()
        {
            try
            {
                string execField = string.Format("return document.querySelector('#formLogin > div:nth-child(1) > div > input').value = '{0}'", email);
                ((IJavaScriptExecutor)driver).ExecuteScript(execField);

                execField = string.Format("return document.querySelector('#formLogin > div:nth-child(2) > div > input').value = '{0}'", password);
                ((IJavaScriptExecutor)driver).ExecuteScript(execField);

                driver.FindElementByCssSelector("#formLogin > div:nth-child(3) > div:nth-child(1) > input").Click();
            }
            catch(Exception ex)
            {
                _ = ex.Message;
            }
        }

        /* Define o processo de pesquisa */
        private void defineProcess()
        {
            try
            {
                if(menu != (int)EnumMenu.DADOS_CADASTRAIS)
                {
                    //classe
                }

                if (tab != (int)EnumDadosCadastrais.CPF_CNPJ_COMPLETO)
                {

                }
            }
            catch(Exception ex)
            {
                _ = ex.Message;
            }
        }

        /* Laço de repetição para pesquisa da lista */
        private void search()
        {
            foreach(var cpf in contatos)
            {
                try
                {
                    if (!string.IsNullOrEmpty(cpf))
                    {
                        string execField = string.Format("return document.querySelector('#cpf-cnpj').value = '{0}'", cpf);
                        ((IJavaScriptExecutor)driver).ExecuteScript(execField);
                    }
                    else
                    {
                        string execField = string.Format("return document.querySelector('div.row-fluid > div:nth-child(3) > input').value = '{0}'", "");
                        ((IJavaScriptExecutor)driver).ExecuteScript(execField);
                    }

                    //driver.FindElementByCssSelector("div.row-fluid > div:nth-child(4) > input").Click();

                    //Recuperar a informação
                    //Salvar a resposta
                }
                catch (Exception ex)
                {
                    _ = ex.Message;
                }
            }
        }

        /* Método genérico para fechamento do navegador */
        public void close()
        {
            driver.Quit();
        }
    }
}
