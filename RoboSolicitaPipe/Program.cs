﻿
using RoboSolicitaPipe;
using RoboSolicitaPipe.Models;

Console.Title = "Robo Solicita Pipe Itau";

ConsoleColorHelper.ConsoleWrite("Robo Solicita Pepi Itau iniciado...");
ConsoleColorHelper.ConsoleWrite("Lendo arquivo de configuração");

var pathSettings = Path.Combine(Environment.CurrentDirectory, "appsettings.json");
var config = pathSettings.DeserializeJsonPath<Config>();

ConsoleColorHelper.ConsoleWrite("Lendo excel");

var pathExcel = config.CaminhoExcel;
var defaultUrlPipe = "https://itau.pipefy.com/public_api";
var tasks = new Task[6];

try
{    
    //tasks[0] = Task.Run(() => SoliciataReativacaoCota());
    //tasks[1] = Task.Run(() => SolicitacaoExtratos());
    //tasks[2] = Task.Run(() => SolicitacaoAlteracaoValorCarta());
    //tasks[3] = Task.Run(() => SolicitacaoBoletoLance());
    //tasks[4] = Task.Run(() => SolicitacaoFaturamentoEspecie());
    //tasks[5] = Task.Run(() => SolicitacaoFaturamentoCotaExcluidaGE());

    SoliciataReativacaoCota();
    SolicitacaoExtratos();
    SolicitacaoAlteracaoValorCarta();
    SolicitacaoBoletoLance();
    SolicitacaoFaturamentoEspecie();
    SolicitacaoFaturamentoCotaExcluidaGE();

    Task.WhenAll(tasks).Wait();
}
catch (Exception e)
{

    ConsoleColorHelper.ConsoleWriteException(e);
}
void SoliciataReativacaoCota()
{
    try
    {
        ConsoleColorHelper.ConsoleWrite("Iniciando metodo: Reativação de cota");
        var dadosExcel = pathExcel.LerArquivoExcel("Reativação de cota");

        ConsoleColorHelper.ConsoleWrite($"Reativação de cota: {dadosExcel.Count} dados encontrados!");
        foreach (var dado in dadosExcel)
        {
            var body = GenerateDefaultPublicFormJson("", "", "", "");
            var result = RequestHelper.CreateRequest<dynamic, dynamic>(defaultUrlPipe, HttpMethod.Post, body).GetAwaiter().GetResult();
        }
        ConsoleColorHelper.ConsoleWrite($"Reativação de cota: finalizado");
    }
    catch (Exception e)
    {

        ConsoleColorHelper.ConsoleWriteException(e);
    }
}

void SolicitacaoExtratos()
{
    try
    {
        ConsoleColorHelper.ConsoleWrite("Iniciando metodo: Solicitação de Extratos");
        var dadosExcel = pathExcel.LerArquivoExcel("Solicitação de Extratos");

        ConsoleColorHelper.ConsoleWrite($"Solicitação de Extratos: {dadosExcel.Count} dados encontrados!");
        foreach (var dado in dadosExcel)
        {
            dado[0] = "claudia.passos@consorciei.com.br";
            var body = GenerateDefaultPublicFormJson("Extrato", dado[0], dado[1], dado[2]);
            var result = RequestHelper.CreateRequest<dynamic, dynamic>(defaultUrlPipe, HttpMethod.Post, body).GetAwaiter().GetResult();
        }
        ConsoleColorHelper.ConsoleWrite($"Solicitação de Extratos: finalizado");
    }
    catch (Exception e)
    {
        ConsoleColorHelper.ConsoleWriteException(e);
    }
}

void SolicitacaoAlteracaoValorCarta()
{
    try
    {
        ConsoleColorHelper.ConsoleWrite("Iniciando metodo: Alteração do valor da carta");
        var dadosExcel = pathExcel.LerArquivoExcel("Alteração do valor da carta");

        ConsoleColorHelper.ConsoleWrite($"Alteração do valor da carta: {dadosExcel.Count} dados encontrados!");
        foreach (var dado in dadosExcel)
        {
            var body = GenerateValorCartaPublicFormJson("Alteração do valor da carta", dado[0], dado[1], dado[2], dado[3], dado[4], dado[5]);
            var result = RequestHelper.CreateRequest<dynamic, dynamic>(defaultUrlPipe, HttpMethod.Post, body).GetAwaiter().GetResult();
        }

        ConsoleColorHelper.ConsoleWrite($"Alteração do valor da carta: finalizado");
    }
    catch (Exception e)
    {

        ConsoleColorHelper.ConsoleWriteException(e);
    }
}

void SolicitacaoBoletoLance()
{
    try
    {
        ConsoleColorHelper.ConsoleWrite("Iniciando metodo: Boleto de lance");
        var dadosExcel = pathExcel.LerArquivoExcel("Boleto de lance");

        ConsoleColorHelper.ConsoleWrite($"Boleto de lance: {dadosExcel.Count} dados encontrados!");
        foreach (var dado in dadosExcel)
        {
            var body = GenerateDefaultPublicFormJson("Boleto de lance", dado[0], dado[1], dado[2]);
            var result = RequestHelper.CreateRequest<dynamic, dynamic>(defaultUrlPipe, HttpMethod.Post, body).GetAwaiter().GetResult();
        }

        ConsoleColorHelper.ConsoleWrite($"Boleto de lance: finalizado");
    }
    catch (Exception e)
    {

        ConsoleColorHelper.ConsoleWriteException(e);
    }
}

void SolicitacaoFaturamentoEspecie()
{
    try
    {
        ConsoleColorHelper.ConsoleWrite("Iniciando metodo: Faturamente em especie");
        var dadosExcel = pathExcel.LerArquivoExcel("Faturamente em especie");


        ConsoleColorHelper.ConsoleWrite($"Faturamente em especie: {dadosExcel.Count} dados encontrados!");
        foreach (var dado in dadosExcel)
        {
            var body = GenerateFaturamentePublicFormJson("Faturamente em especie", dado[0], dado[1], dado[2], dado[3], dado[4], dado[5]);
            var result = RequestHelper.CreateRequest<dynamic, dynamic>(defaultUrlPipe, HttpMethod.Post, body).GetAwaiter().GetResult();
        }

        ConsoleColorHelper.ConsoleWrite($"Faturamente em especie: finalizado");
    }
    catch (Exception e)
    {

        ConsoleColorHelper.ConsoleWriteException(e);
    }
}

void SolicitacaoFaturamentoCotaExcluidaGE()
{
    try
    {
        ConsoleColorHelper.ConsoleWrite("Iniciando metodo: Faturamento cota excluída GE");
        var dadosExcel = pathExcel.LerArquivoExcel("Faturamento cota excluída GE");

        ConsoleColorHelper.ConsoleWrite($"Faturamento cota excluída GE: {dadosExcel.Count} dados encontrados!");
        foreach (var dado in dadosExcel)
        {
            var body = GenerateDefaultPublicFormJson("Faturamento cota excluída GE", dado[0], dado[1], dado[2]);
            var result = RequestHelper.CreateRequest<dynamic, dynamic>(defaultUrlPipe, HttpMethod.Post, body).GetAwaiter().GetResult();
        }

        ConsoleColorHelper.ConsoleWrite($"Faturamento cota excluída GE: finalizado");
    }
    catch (Exception e)
    {

        ConsoleColorHelper.ConsoleWriteException(e);
    }
}

object GenerateDefaultPublicFormJson(string tipoSolicitacao, string email, string contrato, string cnpj)
{
    var publicForm = new
    {
        operation_name = "submitPublicForm",
        variables = new
        {
            formId = "z9sa6Tn_",
            filledFields = new object[]
            {
                new { fieldId = "parceira", fieldValue = "C." },
                new { fieldId = "tipo_de_solicita_o", fieldValue = tipoSolicitacao },
                new { fieldId = "e_mail_para_envio_do_boleto", fieldValue = email },
                new { fieldId = "contrato", fieldValue = contrato },
                new { fieldId = "cnpj_do_cliente", fieldValue = cnpj }
            },
            captchaToken = "dasd51sa6"
        },
        query = "mutation submitPublicForm($formId: ID!, $filledFields: [FilledField]!) {\n  submitPublicForm(input: {formId: $formId, filledFields: $filledFields}) {\n    repoItem {\n      id\n      uuid\n      __typename\n    }\n    __typename\n  }\n}\n"
    };

    return publicForm;
}

object GenerateFaturamentePublicFormJson(string tipoSolicitacao, string email, string contrato, string cnpj, string grupo, string cota, string dados)
{
    var publicFormFaturamento = new
    {
        operation_name = "submitPublicForm",
        variables = new
        {
            formId = "z9sa6Tn_",
            filledFields = new object[]
            {
                new { fieldId = "parceira", fieldValue = "C." },
                new { fieldId = "tipo_de_solicita_o", fieldValue = tipoSolicitacao },
                new { fieldId = "e_mail_para_envio_do_boleto", fieldValue = email },
                new { fieldId = "contrato", fieldValue = contrato },
                new { fieldId = "dados_banc_rios_para_transfer_ncia", fieldValue = dados },
                new { fieldId = "est_ciente_que_essa_solicita_o_s_poss_vel_para_cotas_contempladas_h_pelo_menos_180_dias_e_que_o_saldo_devedor_ser_quitado_com_o_valor_da_carta", fieldValue = new[] { "Sim" } },
                new { fieldId = "grupo_fat_esp_cie", fieldValue = cota },
                new { fieldId = "cota_fat_esp_cie", fieldValue = grupo },
                new { fieldId = "cnpj_do_cliente", fieldValue = cnpj }
            },
            captchaToken = ""
        },
        query = "mutation submitPublicForm($formId: ID!, $filledFields: [FilledField]!) {\n  submitPublicForm(input: {formId: $formId, filledFields: $filledFields}) {\n    repoItem {\n      id\n      uuid\n      __typename\n    }\n    __typename\n  }\n}\n"
    };

    return publicFormFaturamento;
}

object GenerateValorCartaPublicFormJson(string tipoSolicitacao, string email, string contrato, string cnpj, string valor, string grupo, string cota)
{
    var publicFormAlteracao = new
    {
        operation_name = "submitPublicForm",
        variables = new
        {
            formId = "z9sa6Tn_",
            filledFields = new object[]
            {
                new { fieldId = "parceira", fieldValue = "C." },
                new { fieldId = "tipo_de_solicita_o", fieldValue = tipoSolicitacao },
                new { fieldId = "e_mail_para_envio_do_boleto", fieldValue = email },
                new { fieldId = "contrato", fieldValue = contrato },
                new { fieldId = "qual_o_novo_valor_desejado", fieldValue = valor },
                new { fieldId = "grupo", fieldValue = grupo },
                new { fieldId = "cota", fieldValue = cota },
                new { fieldId = "cnpj_do_cliente", fieldValue = cnpj }
            },
            captchaToken = ""
        },
        query = "mutation submitPublicForm($formId: ID!, $filledFields: [FilledField]!) {\n  submitPublicForm(input: {formId: $formId, filledFields: $filledFields}) {\n    repoItem {\n      id\n      uuid\n      __typename\n    }\n    __typename\n  }\n}\n"
    };

    return publicFormAlteracao;
}
