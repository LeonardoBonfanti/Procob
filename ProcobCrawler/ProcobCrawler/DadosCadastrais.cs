using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Remote;
using ProcobCrawler.Enum;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
                            driver.FindElementById("neto_cadastrais_3").Click();

                            searchDDDFONE();
                        }
                        break;
                    case (int)EnumDadosCadastrais.LISTA_TELEFONICA:
                        {
                            driver.FindElementById("neto_cadastrais_4").Click();

                            searchListaTelefonica();
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
                            response = ex.Message;
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

                //Pesquisar
                //driver.FindElementByCssSelector("input.btn.btn-primary.span4.botao-pesquisar-nome").Click();

                //Limpar
                //driver.FindElementByCssSelector("input.btn.btn-primary.span4.botao-limpar-nome").Click();

                //Recuperar a informação
                //Grava a informação
            }
            catch (Exception ex)
            {
                _ = ex.Message;
            }

            //Salvar a resposta
        }

        private void searchDDDFONE()
        {
            foreach (var tel in contatos)
            {
                try
                {
                    if (!string.IsNullOrEmpty(tel))
                    {
                        string telefone = "41995475503";
                        string ddd = telefone.Substring(0, 2);
                        telefone = telefone.Substring(2);

                        string execField = string.Format("return document.querySelector('#ddd').value = {0}", ddd);
                        ((IJavaScriptExecutor)driver).ExecuteScript(execField);

                        execField = string.Format("return document.querySelector('#fone').value = {0}", telefone);
                        ((IJavaScriptExecutor)driver).ExecuteScript(execField);
                    }

                    //driver.FindElementByCssSelector("div:nth-child(3) > button").Click();

                    //Recuperar a informação
                }
                catch (Exception ex)
                {
                    _ = ex.Message;
                }
            }

            //Salvar a resposta
        }

        private void searchListaTelefonica()
        {
            Thread.Sleep(2000);

            try
            {
                //Nome
                string execField = string.Format("return document.querySelector('div.span8 > div > input').value = '{0}'", "Pedro Pedro");
                ((IJavaScriptExecutor)driver).ExecuteScript(execField);

                //Idade Inicial
                execField = string.Format("return document.querySelector('div.span4 > div > input:nth-child(1)').value = {0}", "11");
                ((IJavaScriptExecutor)driver).ExecuteScript(execField);


                //Idade Final
                execField = string.Format("return document.querySelector('div.span4 > div > input:nth-child(2)').value = {0}", "30");
                ((IJavaScriptExecutor)driver).ExecuteScript(execField);

                //if(agrupar)
                //{
                    //Agrupar Resultado
                    execField = string.Format("return document.querySelector('div:nth-child(1) > div > div:nth-child(2) > div:nth-child(1) > input').checked = {0}", "true");
                    ((IJavaScriptExecutor)driver).ExecuteScript(execField);
                //}

                try
                {
                    bool endereco = true;

                    if (!endereco)
                    {
                        //Por Endereço
                        //driver.FindElementByCssSelector("#conteudo8 > form > div:nth-child(1) > div:nth-child(1) > div > div:nth-child(2) > div:nth-child(2) > input[type=radio]").Click();

                        //Endereço
                        driver.FindElementByCssSelector("div:nth-child(3) > div > div.span4 > input").Click();
                        execField = string.Format("return document.querySelector('div:nth-child(3) > div > div.span4 > input').value = '{0}'", "Endereço Teste");
                        ((IJavaScriptExecutor)driver).ExecuteScript(execField);

                        //NumeroInicial
                        execField = string.Format("return document.querySelector('div:nth-child(3) > div > div.span8 > div:nth-child(1) > input').value = {0}", 11);
                        ((IJavaScriptExecutor)driver).ExecuteScript(execField);

                        //NumeroFinal
                        execField = string.Format("return document.querySelector('div.span8 > div:nth-child(2) > input').value = {0}", 55);
                        ((IJavaScriptExecutor)driver).ExecuteScript(execField);

                        //Cidade
                        execField = string.Format("return document.querySelector('div.span4.box-sizing > input').value = '{0}'", "Curitiba");
                        ((IJavaScriptExecutor)driver).ExecuteScript(execField);

                        //UF
                        var obj = ((IJavaScriptExecutor)driver).ExecuteScript("return document.querySelector('div.span2.box-sizing > select').options");
                        var list = ((IEnumerable)obj).Cast<object>().ToList();

                        for (int i = 0; i < list.Count(); i++)
                        {
                            var item = (RemoteWebElement)list[i];
                            if (item.Text == "PR")
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
                    else
                    {
                        //Por CEP
                        driver.FindElementByCssSelector("#conteudo8 > form > div:nth-child(1) > div:nth-child(1) > div > div:nth-child(2) > div:nth-child(3) > input[type=radio]").Click();

                        //CEP
                        execField = string.Format("return document.querySelector('div:nth-child(4) > div > div.span4 > input').value = {0}", 82560020);
                        ((IJavaScriptExecutor)driver).ExecuteScript(execField);

                        //NumeroInicial 
                        execField = string.Format("return document.querySelector('div:nth-child(4) > div > div:nth-child(2) > input').value = {0}", 11);
                        ((IJavaScriptExecutor)driver).ExecuteScript(execField);

                        //NumeroFinal
                        execField = string.Format("return document.querySelector('div:nth-child(4) > div > div:nth-child(3) > input').value = {0}", 66);
                        ((IJavaScriptExecutor)driver).ExecuteScript(execField);
                    }
                }
                catch (Exception ex)
                {
                    _ = ex.Message;
                }

                //Tipo Telefone
                var obj2 = ((IJavaScriptExecutor)driver).ExecuteScript("return document.querySelector('div:nth-child(1) > select').options");
                var list2 = ((IEnumerable)obj2).Cast<object>().ToList();

                for (int i = 0; i < list2.Count(); i++)
                {
                    var item = (RemoteWebElement)list2[i];
                    if (item.Text == "Celular")
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

                //Fonética
                obj2 = ((IJavaScriptExecutor)driver).ExecuteScript("return document.querySelector('div:nth-child(2) > div:nth-child(2) > div:nth-child(2) > select').options");
                list2 = ((IEnumerable)obj2).Cast<object>().ToList();

                for (int i = 0; i < list2.Count(); i++)
                {
                    var item = (RemoteWebElement)list2[i];
                    if (item.Text == "Comparação Idêntica")
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

                //Tipo Nome
                obj2 = ((IJavaScriptExecutor)driver).ExecuteScript("return document.querySelector('div:nth-child(2) > div:nth-child(2) > div:nth-child(3) > select').options");
                list2 = ((IEnumerable)obj2).Cast<object>().ToList();

                for (int i = 0; i < list2.Count(); i++)
                {
                    var item = (RemoteWebElement)list2[i];
                    if (item.Text == "Semelhante")
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

                //Tipo Pessoa
                obj2 = ((IJavaScriptExecutor)driver).ExecuteScript("return document.querySelector('div:nth-child(3) > div:nth-child(1) > select').options");
                list2 = ((IEnumerable)obj2).Cast<object>().ToList();

                for (int i = 0; i < list2.Count(); i++)
                {
                    var item = (RemoteWebElement)list2[i];
                    if (item.Text == "PJ")
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

                //Sexo
                obj2 = ((IJavaScriptExecutor)driver).ExecuteScript("return document.querySelector('div:nth-child(3) > div:nth-child(2) > select').options");
                list2 = ((IEnumerable)obj2).Cast<object>().ToList();

                for (int i = 0; i < list2.Count(); i++)
                {
                    var item = (RemoteWebElement)list2[i];
                    if (item.Text == "Feminino")
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

                //Pesquisar
                //driver.FindElementByCssSelector("#conteudo8 > form > div:nth-child(2) > div:nth-child(1) > input").Click();

                //Recuperar a informação
                //Grava a informação
            }
            catch (Exception ex)
            {
                _ = ex.Message;
            }
        }
    }
}
