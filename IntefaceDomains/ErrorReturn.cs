using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntefaceDomains
{
    /// <summary>
    /// Return Errors
    /// </summary>
    public class ErrorReturn
    {

        public string Controller { get; set; }
        public string Action { get; set; }
        public string Code
        {

            get { return _Code; }

            set { _Code = value; }

        }

        private string _Code;
        private string msg;

        public string Error { get => GetError(); }

        private string GetError()
        {


            switch (this.Code)
            {
                ///Error Register
                case "reg-0001":
                    msg = "Verifique seus dados";
                    break;


                ///Error Login
                case "LO0001-01":
                    msg = "Verifique seus dados";
                    break;
                case "LO0001-02":
                    msg = "Usuário ou senha incorreta";
                    break;
                case "LO0001-03":
                    msg = "Erro ao executar login, verifique seus dados";
                    break;
                case "LO0001-04":
                    msg = "Usuário ou senha incorreta";
                    break;
                case "LO0001-05":
                    msg = "Erro ao recuperar usuário";
                    break;
                case "LO0001-06":
                    msg = "Identificamos inconsistência entre seu usuário e seu contrato. Por gentileza, entre em contato com nosso suporte técnico. Caso tenha esquecido sua senha clique em \"Esqueci minha senha\"";
                    break;
                case "LO0001-07":
                    msg = "Perfil com erro";
                    break;
                case "LO0001-08":
                    msg = "Chave não autorizada";
                    break;
                case "LO0001-09":
                    msg = "Erro com validade";
                    break;
                case "LO0001-10":
                    msg = "Erro ao gerar token";
                    break;
                case "LO0001-11":
                    msg = "Erro ao devolver usuário";
                    break;
                case "LO0001-10a":
                    msg = "A senha deve ter 6 caracteres";
                    break;
                case "LO0001-10b":
                    msg = "O usuário deve ser informado";
                    break;


                case "LO0002-01":
                    msg = "Token invalido";
                    break;
                case "LO0002-02":
                    msg = "Usuário não encontrado";
                    break;
                case "LO0002-03":
                    msg = "Usuário não autorizado";
                    break;

                ///Error Customer
                case "CU0001-01":
                    msg = "Erro ao busca lista e clientes";
                    break;


                //Error Publisher
                case "PU0003-01":
                    msg = "Cpf/CNPJ já cadastrado";
                    break;
                case "PU0003-02":
                    msg = "Por favor, verifique o CPF";
                    break;

                case "PU0003-03":
                    msg = "Informe uma senha";
                    break;
                case "PU0003-04":
                    msg = "Já existe um usuário com este Email";
                    break;

                case "PU0005-01":
                    msg = "Divulgador não encontrado";
                    break;

                case "PU0005-02":
                    msg = "Documento não pode ser vazio";
                    break;

                case "PU0005-03":
                    msg = "Documento não é válido";
                    break;

                case "PU0005-04":
                    msg = "CEP não é válido";
                    break;

                case "PU0005-05":
                    msg = "CEP  não pode estar vázio";
                    break;

                case "PU0006-03":
                    msg = "Senha diferente da senha atual";
                    break;
                    
                case "PU0007-03":
                    msg = "E-mail Já cadastrado";
                    break;
                case "UPP000-14":
                    msg = "O formato da imagem deve ser .jpg ou .jpeg";
                    break;
                ///Error Customer Create
                case "CU0004-01":
                    msg = "Erro ao obter contrato";
                    break;
                case "CU0004-02":
                    msg = "Alguns campos obrigatórios não foram enviados";
                    break;

                case "CU0004-03":
                    msg = "Alguns campos obrigatoórios não foram enviados";
                    break;
                case "CU0004-04":
                    msg = "Existe um cliente com este documento";
                    break;
                case "CU0004-05":
                    msg = "Problemas ao inserir nome da fantasia ao cliente";
                    break;
                case "CU0004-06":
                    msg = "Erro ao codificar cliente";
                    break;
                case "CU0004-07":
                    msg = "Erro ao adicionar cliente no banco de dados";
                    break;
                case "CU0004-08":
                    msg = "Erro ao criar telefone";
                    break;
                case "CU0004-09":
                    msg = "Erro ao criar telefone";
                    break;
                case "CU0004-10":
                    msg = "Erro ao criar telefone";
                    break;
                case "CU0004-11":
                    msg = "Erro ao criar email";
                    break;
                case "CU0004-12":
                    msg = "Erro ao criar email";
                    break;
                case "CU0004-13":
                    msg = "Erro ao criar email";
                    break;
                case "CU0004-14":
                    msg = "Erro ao salvar cliente";
                    break;
                case "CU0004-15":
                    msg = "Erro ao salvar cliente";
                    break;

                case "US0003-01":
                    msg = "Já existe um usuário com este Email";
                    break;



                case "US0000-01":
                    msg = "Usuário não validado ou e-mail não confirmado";
                    break;

                default:
                    msg = "Erro desconhecido";
                    break;
            }




            return msg;
        }
    }
}