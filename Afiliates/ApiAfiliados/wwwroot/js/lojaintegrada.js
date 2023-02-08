m8a_idproduct = 0;

$(function () {
    if ($("body").hasClass("pagina-produto")) {
        if (PRODUTO_ID != undefined && PRODUTO_ID > 0) {
            m8a_idproduct = PRODUTO_ID;
        }
        $m8a_page = "product";
    } else if ($("body").hasClass("carrinho-checkout")) {
        $m8a_page = "checkout";
        executeM8fn.init();
        executeM8fn.sendViewProduc();
    } else if ($("body").hasClass("pagina-carrinho")) {
        $m8a_page = "cart";
        executeM8fn.init();
        executeM8fn.sendViewProduc();
    } else if ($("body").hasClass("pagina-inicial")) {
        $m8a_page = "store";
        executeM8fn.init();
        //executeM8fn.sendViewProduc();
    } else if ($("body").hasClass("pagina-pedido-finalizado")) {

        $m8a_page = "purshcase";
        $pay_form = $("#img-forma-pagamento").attr("alt");
        $order_id = parseInt($(".numero-pedido").text());
        $order_total = parseFloat($('.tabela-carrinho tbody tr td .total .titulo').text().replace("R$ ", "").replace(",", "."));
        $order_shipping = parseFloat($('.tabela-carrinho tbody tr td .frete-preco .titulo').text().replace("R$ ", "").replace(",", "."));

        $m8customer = $(".account-info div a .link").text();

        $cart = [];

        arrstatus = {
            cancelado: "cancelado",
            pedido_efetuado: "aguardando",
            aguardando_pagamento: "aguardando",
            Entregue: "aprovado",
            entregue: "aprovado",
            Enviado: "aprovado",
            enviado: "aprovado",
            pago: "aprovado",
            aprovado: "aprovado",
            Aprovado: "aprovado",
            pagamento_devolvido: "bloqueado",
            pagamento_em_analise: "aguardando",
            pagamento_em_chargeback: "bloqueado",
            pagamento_em_disputa: "bloqueado",
            pronto_para_retirada: "aprovado",
            em_producao: "aprovado",
            producao: "aprovado",
            em_separacao: "aprovado",
            separacao: "aprovado"
        };


        for (i = 0; i < ga.q.length; i++) {

            if (ga.q[i][0] == "ecommerce:addItem") {

                $cart.push({
                    'id': null,
                    'sku': ga.q[i][1].sku,
                    'name': ga.q[i][1].name,
                    'price': ga.q[i][1].price,
                    'quantity': ga.q[i][1].quantity,
                });
            }
        }

        _status = arrstatus[pedido_status];

        executeM8fn.sendOrder(_status, $pay_form, url_check_pedido_status, $cart, $order_shipping, $order_total, $order_id, $m8customer);
        sendLojaintegrada($order_id);

    }

    function sendLojaintegrada($order_id) {

        if (data_layer_m8 == undefined && data_layer_m8 == null) {
            return;
        }
        if (data_layer_m8.publisher == undefined && data_layer_m8.publisher == null) {
            return;
        }
        if (data_layer_m8.seller == undefined && data_layer_m8.seller == null) {
            return;
        }


        $.ajax({
            type: 'POST',
            dataType: 'JSON',
            url: $m8a_loader + "/api/lojaintegrada/create/" + $order_id,
            data: JSON.stringify(data_layer_m8),
            contentType: "application/json; charset=utf-8",
            success: function (datas) {

                if (datas == true) {

                }

                else {


                }

            }
        });
        return;
    }

});


$(document).on("click", ".botao-comprar, .whatsapp-buy-button", function (e) { executeM8fn.addcart(); });

$(document).on("click", ".submit-email", function (e) { e.preventDefault(); executeM8fn.setCookieM8('customermailm8', $("#id_email_login").val(), 365); });

