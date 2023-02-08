using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiAfiliados.Models.HubsIntegracao.LojaIntegrada
{
    public class PedidosLojaIntegrada
    {
        public Cliente cliente { get; set; }
        public object cliente_obs { get; set; }
        public object cupom_desconto { get; set; }
        public DateTime data_criacao { get; set; }
        public DateTime data_expiracao { get; set; }
        public DateTime data_modificacao { get; set; }
        public Endereco_Entrega endereco_entrega { get; set; }
        public Envio[] envios { get; set; }
        public object id_anymarket { get; set; }
        public object id_externo { get; set; }
        public Iten[] itens { get; set; }
        public int numero { get; set; }
        public Pagamento[] pagamentos { get; set; }
        public string peso_real { get; set; }
        public string resource_uri { get; set; }
        public Situacao situacao { get; set; }
        public object utm_campaign { get; set; }
        public string valor_desconto { get; set; }
        public string valor_envio { get; set; }
        public string valor_subtotal { get; set; }
        public string valor_total { get; set; }
    }

    public class Cliente
    {
        public object cnpj { get; set; }
        public string cpf { get; set; }
        public object data_nascimento { get; set; }
        public string email { get; set; }
        public int id { get; set; }
        public string nome { get; set; }
        public object razao_social { get; set; }
        public string resource_uri { get; set; }
        public string sexo { get; set; }
        public string telefone_celular { get; set; }
        public object telefone_principal { get; set; }
    }

    public class Endereco_Entrega
    {
        public string bairro { get; set; }
        public string cep { get; set; }
        public string cidade { get; set; }
        public object cnpj { get; set; }
        public object complemento { get; set; }
        public string cpf { get; set; }
        public string endereco { get; set; }
        public string estado { get; set; }
        public int id { get; set; }
        public object ie { get; set; }
        public string nome { get; set; }
        public string numero { get; set; }
        public string pais { get; set; }
        public object razao_social { get; set; }
        public object referencia { get; set; }
        public object rg { get; set; }
        public string tipo { get; set; }
    }

    public class Situacao
    {
        public bool aprovado { get; set; }
        public bool cancelado { get; set; }
        public string codigo { get; set; }
        public bool final { get; set; }
        public int id { get; set; }
        public string nome { get; set; }
        public bool notificar_comprador { get; set; }
        public bool padrao { get; set; }
        public string resource_uri { get; set; }
    }

    public class Envio
    {
        public DateTime data_criacao { get; set; }
        public DateTime data_modificacao { get; set; }
        public Forma_Envio forma_envio { get; set; }
        public int id { get; set; }
        public object objeto { get; set; }
        public int prazo { get; set; }
        public string valor { get; set; }
    }

    public class Forma_Envio
    {
        public string code { get; set; }
        public int id { get; set; }
        public string nome { get; set; }
        public string tipo { get; set; }
    }

    public class Iten
    {
        public int altura { get; set; }
        public int disponibilidade { get; set; }
        public int id { get; set; }
        public int largura { get; set; }
        public int linha { get; set; }
        public string ncm { get; set; }
        public string nome { get; set; }
        public string pedido { get; set; }
        public string peso { get; set; }
        public string preco_cheio { get; set; }
        public string preco_custo { get; set; }
        public string preco_promocional { get; set; }
        public string preco_subtotal { get; set; }
        public string preco_venda { get; set; }
        public string produto { get; set; }
        public object produto_pai { get; set; }
        public int profundidade { get; set; }
        public string quantidade { get; set; }
        public string sku { get; set; }
        public string tipo { get; set; }
    }

    public class Pagamento
    {
        public object authorization_code { get; set; }
        public object banco { get; set; }
        public object bandeira { get; set; }
        public object codigo_retorno_gateway { get; set; }
        public Forma_Pagamento forma_pagamento { get; set; }
        public int id { get; set; }
        public object identificador_id { get; set; }
        public object mensagem_gateway { get; set; }
        public string pagamento_tipo { get; set; }
        public Parcelamento parcelamento { get; set; }
        public string transacao_id { get; set; }
        public string valor { get; set; }
        public string valor_pago { get; set; }
    }

    public class Forma_Pagamento
    {
        public string codigo { get; set; }
        public Configuracoes configuracoes { get; set; }
        public int id { get; set; }
        public string imagem { get; set; }
        public string nome { get; set; }
        public string resource_uri { get; set; }
    }

    public class Configuracoes
    {
        public bool ativo { get; set; }
        public bool disponivel { get; set; }
    }

    public class Parcelamento
    {
    }

}

