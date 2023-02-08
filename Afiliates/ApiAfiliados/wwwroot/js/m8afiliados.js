
var data_layer_m8 = [];
var getUrlParameter = function getUrlParameter(sParam) {
    var sPageURL = window.location.search.substring(1),
        sURLVariables = sPageURL.split('&'),
        sParameterName,
        i;

    for (i = 0; i < sURLVariables.length; i++) {
        sParameterName = sURLVariables[i].split('=');

        if (sParameterName[0] === sParam) {
            return sParameterName[1] === undefined ? true : decodeURIComponent(sParameterName[1]);
        }
    }

    return false;
};


const executeM8fn = {

    init: function () {


        $m8a_publisher = undefined;

        getOld = this.getLocalM8();

        if (getOld != undefined && getOld != null) {
            $m8a_publisher = getOld.publisher;
        }

        if (getUrlParameter('m8psid')) {
            $m8a_publisher = getUrlParameter('m8psid');
            this.setCookieM8('m8a_publisher', $m8a_publisher, 30);
        }
        if ($m8a_publisher == undefined) {
            return;
        }
        data_layer_m8 = {
            'seller': $m8a_guid,
            'publisher': $m8a_publisher,
            'm8a_idproduct': m8a_idproduct,
            'm8a_page': $m8a_page,
            'type': $m8a_type,
            'utm_source': getUrlParameter('utm_source'),
            'utm_medium': getUrlParameter('utm_medium'),
            'utm_campaign': getUrlParameter('utm_campaign'),
            'm8a_sid': $m8a_publisher,
            'm8a_visity': $dvidity,

        };



        this.setCookieM8('m8a_afiliados', JSON.stringify(data_layer_m8), 100);
        this.setLocalM8(data_layer_m8);
        if ($m8a_page == "product") {
            if ($m8a_publisher != "")
                this.sendViewProduc();
        };
    },
    setCookieM8: function (cname, cvalue, exdays) {
        const d = new Date();
        d.setTime(d.getTime() + (exdays * 24 * 60 * 60 * 1000));
        let expires = "expires=" + d.toUTCString();
        document.cookie = cname + "=" + cvalue + ";" + expires + ";path=/; SameSite = Lax; ";
    },
    getCookieM8: function (cname) {
        const d = new Date();
        d.setTime(d.getTime() + (exdays * 24 * 60 * 60 * 1000));
        let expires = "expires=" + d.toUTCString();
        document.cookie = cname + "=" + cvalue + ";" + expires + ";path=/; SameSite = Lax; secure: true;";
    },
    setLocalM8: function (cvalue) {
        localStorage.setItem('m8afiliados', JSON.stringify(cvalue));
    },
    getLocalM8: function () {
        return JSON.parse(localStorage.getItem('m8afiliados'));
    },
    sendViewProduc: function () {



        $.ajax({
            type: 'POST',
            dataType: 'JSON',
            url: $m8a_loader + "/api/products/view",
            data: JSON.stringify(data_layer_m8),
            contentType: "application/json; charset=utf-8",
            success: function (datas) {
                console.log("visualizou");

                if (datas == true) {

                }

                else {


                }

            }
        });
        return;
    },
    sendOrder: function ($status, $paymode, $linkorder, $dataorder, $shipping, $totalpay, $referenceorder, $m8customer) {
        data_layer_m8 = this.getLocalM8();

        if (data_layer_m8 == undefined && data_layer_m8 == null) {
            return;
        }
        if (data_layer_m8.publisher == undefined && data_layer_m8.publisher == null) {
            return;
        }
        if (data_layer_m8.seller == undefined && data_layer_m8.seller == null) {
            return;
        }

        data_layer_m8['checkout'] = true;
        data_layer_m8['m8a_page'] = $m8a_page;

        let orders = {
            reference: $referenceorder, // ID order
            status: $status,

            paymode: $paymode,
            linkorder: $linkorder,
            cart: $dataorder,
            shipping: $shipping,
            totalpay: $totalpay,
            customer: $m8customer
        };

        if (data_layer_m8.orders == undefined) {
            data_layer_m8.orders = orders;
        }
        else {
            data_layer_m8.orders = orders;
        }




        $dvisti = data_layer_m8.m8a_visity;

        $.ajax({
            type: 'POST',
            dataType: 'JSON',
            url: $m8a_loader + "/api/orders/create/" + $dvisti,
            data: JSON.stringify(data_layer_m8),
            contentType: "application/json; charset=utf-8",
            success: function (datas) {
                console.log("visualizou");

                if (datas == true) {
                    console.log(datas);
                }

                else {
                    console.log(datas);

                }

            }
        });
        return;
    },
    addcart: function () {
        data_layer_m8['m8a_page'] = "addcart";

        $.ajax({
            type: 'POST',
            dataType: 'JSON',
            url: $m8a_loader + "/api/products/view",
            data: JSON.stringify(data_layer_m8),
            contentType: "application/json; charset=utf-8",
            success: function (datas) {
                console.log("visualizou");

                if (datas == true) {

                }

                else {


                }

            }
        });
        $.getJSON($m8a_loader + "/api/products/view", JSON.stringify(data_layer_m8));

        $.post($m8a_loader + "/api/products/view", { m8a_page: "addcart", seller: '"' + data_layer_m8['seller'] + '"', m8a_idproduct: '"' + data_layer_m8['m8a_idproduct'] + '"' });
        $.post($m8a_loader + "/api/products/view", JSON.stringify(data_layer_m8),
            function (data) {


            }, "json");
        $.post($m8a_loader + "/api/products/view", (data_layer_m8));
        $.getJSON($m8a_loader + "/api/products/view", JSON.stringify(data_layer_m8));


        $.ajax({
            type: 'POST',
            dataType: 'JSON',
            url: $m8a_loader + "/api/products/view",
            data: JSON.stringify(data_layer_m8),
            contentType: "application/json; charset=utf-8",
            success: function (datas) {
                console.log("visualizou");

                if (datas == true) {

                }

                else {


                }

            }
        });
        return;
    },

    paysplit: function (x, y) {
        return x / y;
    },

}


$(function () {
    executeM8fn.init();
});




function serializeForm(formData) {

    // Build the data object.
    const data = {};
    formData.forEach((value, key) => (data[key] = value));
    // Log the data.


}

function convertFormToJSON(form) {
    return $(form)
        .serializeArray()
        .reduce(function (json, { name, value }) {
            json[name] = value;
            return json;
        }, {});
}

function convertCurrency(value = 0) {
    var _pretotal = value;
    _pretotal = _pretotal.replace(".", "");
    _pretotal = _pretotal.replace(",", ".");
    return parseFloat(_pretotal);
}

