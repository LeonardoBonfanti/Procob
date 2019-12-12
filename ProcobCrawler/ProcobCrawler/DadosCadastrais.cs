using MongoDB.Bson;
using Newtonsoft.Json.Linq;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Remote;
using ProcobCrawler.Enum;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading;

namespace ProcobCrawler
{
    public class DadosCadastrais
    {
        private int tab;
        ChromeDriver driver;
        List<string> contatos;

        List<List<string>> contatos2;

        public DadosCadastrais(ChromeDriver driver, int tab, List<string> contatos)
        {
            this.tab = tab;
            this.driver = driver;
            this.contatos = contatos;
            contatos2 = new List<List<string>>();
        }

        /* Define os métodos de varredura a partir da tab selecionada */
        public void init()
        {
            try
            {
                switch (tab)
                {
                    case (int)EnumDadosCadastrais.CPF_CNPJ_COMPLETO:
                        {
                            searchCPFCNPJCompleto();
                        }
                        break;
                    case (int)EnumDadosCadastrais.CPF_CNPJ_PELO_NOME:
                        {
                            driver.FindElementById("neto_cadastrais_2").Click();

                            searchCPFCNPJpeloNome();
                        }
                        break;
                    case (int)EnumDadosCadastrais.DDD_FONE:
                        {

                        }
                        break;
                    case (int)EnumDadosCadastrais.LISTA_TELEFONICA:
                        {

                        }
                        break;
                    case (int)EnumDadosCadastrais.QUADRO_SOCIETARIO:
                        {

                        }
                        break;
                    case (int)EnumDadosCadastrais.INFOBUSCA:
                        {

                        }
                        break;
                    case (int)EnumDadosCadastrais.PERFIL_CNPJ:
                        {

                        }
                        break;
                    case (int)EnumDadosCadastrais.PERFIL_SOCIO_ECONOMICO:
                        {

                        }
                        break;
                    case (int)EnumDadosCadastrais.PEP:
                        {

                        }
                        break;
                }
            }
            catch(Exception ex)
            {
                _ = ex.Message;
            }
        }


        private void searchCPFCNPJCompleto()
        {
            foreach (var cpf in contatos)
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
                }
                catch (Exception ex)
                {
                    _ = ex.Message;
                }
            }

            //Salvar a resposta
        }

        /* Pesquisa CPF e CNPJ pelo nome do registro */
        private void searchCPFCNPJpeloNome()
        {
            /*foreach(var contato in contatos)
            {

            }*/

            try
            {
                string execField = string.Format("return document.querySelector('#container_campo_nome_pelo_nome > input').value = '{0}'", "Pedro Pedro");
                ((IJavaScriptExecutor)driver).ExecuteScript(execField);

                //Respeitar primeiro nome
                execField = string.Format("return document.querySelector('label:nth-child(1) > input').checked = {0}", "true");
                ((IJavaScriptExecutor)driver).ExecuteScript(execField);


                //Buscar som semelhante
                execField = string.Format("return document.querySelector('label:nth-child(2) > input').checked = {0}", "true");
                ((IJavaScriptExecutor)driver).ExecuteScript(execField);

                //Estado                        
                var obj = ((IJavaScriptExecutor)driver).ExecuteScript("return document.querySelector('div:nth-child(2) > select:nth-child(1)').options");
                var list = ((IEnumerable)obj).Cast<object>().ToList();
                object response = null;

                for(int i = 0; i < list.Count(); i++)
                {
                    var item = (RemoteWebElement)list[i];
                    if (item.Text == "Paraná")
                    {
                        try
                        {
                            item.Click();
                        }
                        catch(Exception ex)
                        {
                            response = "fail";
                        }
                                
                        break;
                    }
                }

                //Cidade
                if(response == null)
                {
                    obj = ((IJavaScriptExecutor)driver).ExecuteScript("return document.querySelector('div:nth-child(2) > select:nth-child(2)').options");
                    list = ((IEnumerable)obj).Cast<object>().ToList();

                    for (int i = 0; i < list.Count(); i++)
                    {
                        var item = (RemoteWebElement)list[i];
                        if (item.Text == "Ampere")
                        {
                            try
                            {
                                item.Click();
                            }
                            catch (Exception ex)
                            {
                                _ = ex.Message;
                            }

                            break;
                        }
                    }
                }

                //Idade Ini
                obj = ((IJavaScriptExecutor)driver).ExecuteScript("return document.querySelector('select.selectIdadeInicial.span3.ng-pristine.ng-valid').options");
                list = ((IEnumerable)obj).Cast<object>().ToList();

                try
                {
                    ((RemoteWebElement)list[18]).Click();
                }
                catch (Exception ex)
                {
                    _ = ex.Message;
                }

                //Idade Fim
                obj = ((IJavaScriptExecutor)driver).ExecuteScript("return document.querySelector('select.selectIdadeFinal.span3.ng-pristine.ng-valid').options");
                list = ((IEnumerable)obj).Cast<object>().ToList();

                try
                {
                    ((RemoteWebElement)list[28]).Click();
                }
                catch (Exception ex)
                {
                    _ = ex.Message;
                }

                //Nascimento
                execField = string.Format("return document.querySelector('div:nth-child(3) > label > input').value = '{0}'", "1992-11-11");
                ((IJavaScriptExecutor)driver).ExecuteScript(execField);

                //Sexo
                obj = ((IJavaScriptExecutor)driver).ExecuteScript("return document.querySelector('div:nth-child(4) > select:nth-child(1)').options");
                list = ((IEnumerable)obj).Cast<object>().ToList();

                var elemento = (RemoteWebElement)list[1];

                try
                {
                    if (elemento.Text == "Feminino")
                        ((RemoteWebElement)list[1]).Click();
                    else
                        ((RemoteWebElement)list[2]).Click();
                }
                catch(Exception ex)
                {
                    _ = ex.Message;
                }

                //Tipo Pessoa
                obj = ((IJavaScriptExecutor)driver).ExecuteScript("return document.querySelector('div:nth-child(4) > select:nth-child(2)').options");
                list = ((IEnumerable)obj).Cast<object>().ToList();

                elemento = (RemoteWebElement)list[1];

                try
                {
                    if (elemento.Text == "Pessoa Jurídica")
                        ((RemoteWebElement)list[1]).Click();
                    else
                        ((RemoteWebElement)list[2]).Click();
                }
                catch (Exception ex)
                {
                    _ = ex.Message;
                }

                //driver.FindElementByCssSelector("div.row-fluid > div:nth-child(4) > input").Click();

                //Recuperar a informação
                //Grava a informação
            }
            catch (Exception ex)
            {
                _ = ex.Message;
            }

            //Salvar a resposta
        }
    }
}
