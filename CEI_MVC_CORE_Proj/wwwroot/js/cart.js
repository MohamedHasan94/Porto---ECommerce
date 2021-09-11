/* [A] PRODUCTS DATA */
// Load products from the server via dynamic JS or AJAX

/* [B] PRODUCTS HTML GRID GENERATOR */


function addMultipleToCart() {

    var parentDiv = document.getElementById("productContainer");
    var id = parentDiv.dataset["id"];
    var name = document.querySelector("#productName").innerText;
    var img = document.querySelector("#productImg").src;
    var price = document.querySelector("#productPrice").innerText;
    var count = parseInt(document.querySelector("#purchaseQuantity").value);

    console.log(id, name, img, price);
    cart.add(id, name, img, price, count);

    setTimeout(() => { document.getElementById("cart-list").parentElement.classList.add("open") }, 100);

};


function addToCart(product) {

    var parentDiv = document.getElementById("P_" + product.dataset["productid"]);
    var id = product.dataset["productid"];
    var name = document.querySelector("#P_" + product.dataset["productid"] + " a h4").innerText;
    var img = document.querySelector("#P_" + product.dataset["productid"] + " .image").style.backgroundImage.match(/url\(["']?([^"']*)["']?\)/)[1];
    var price = document.querySelector("#P_" + product.dataset["productid"] + " a .flex-row span").innerText;

    console.log(id, name, img, price);
    cart.add(id, name, img, price);

    setTimeout(() => {  document.getElementById("cart-list").parentElement.classList.add("open") }, 100);

};
var htmlProductTemplate =

    `
<li class="item-bought" data-id="@productId" > <div class="item-bought-data"> 
<span>@productName</span>
<span> @productQtyX @productPrice </span>
</div> <div class="item-bought-img"> 
<img class="img-responsive imgSized"src="@productImg" alt="prewiew" width="100" style="max-height:100px;" height="auto">
</div></li>

`

/* [C] SHOPPING CART */
var cart = {
    data: null, // current shopping cart

    /* [C1] LOCALSTORAGE */
    load: function () {
        // load() : load previous shopping cart

        cart.data = localStorage.getItem("cart");
        if (cart.data == null) { cart.data = {}; }
        else { cart.data = JSON.parse(cart.data); }
    },

    save: function () {
        // save() : save current cart

        localStorage.setItem("cart", JSON.stringify(cart.data));
    },

    /* [C2] CART ACTIONS */
    add: function (_id, _name, _img, _price, count = 1) {
        // add() : add selected item to cart

        // Update current cart
        if (cart.data[_id] == undefined) {

            cart.data[_id] = {
                name: _name,
                img: _img,
                price: _price,
                qty: count
            };
        } else {
            cart.data[_id]['qty'] = parseInt(cart.data[_id]['qty']) + count;
        }

        // Save local storage + HTML update
        cart.save();
        cart.list();

    },

    list: function () {
        // list() : update HTML

        var container = document.getElementById("cart-list"),
            item = null, part = null, product = null;
        container.innerHTML = "";

        // Empty cart

        var isempty = function (obj) {
            for (var key in obj) {
                if (obj.hasOwnProperty(key)) { return false; }
            }
            return true;
        };
        if (isempty(cart.data)) {
            item = document.createElement("div");
            item.innerHTML = "Cart is empty";
            item.className += " alert alert-info";
            item.style.margin = "25px";
            container.appendChild(item);
        }

        // Not empty
        else {
            // List items
            var total = 0, subtotal = 0;
            for (var i in cart.data) {
                item = document.createElement("div");
                item.classList.add("c-item");
                item.classList.add("row");
                product = cart.data[i];


                container.innerHTML += htmlProductTemplate.replace("@productImg", product.img)
                  //  .replace("@productImg", "/images/productimages/thereAreFourRules.jpg")
                    .replace("@productName", product.name)
                    .replace("@productQty", product.qty)
                    .replace("@productPrice", product.price)
                    .replace("@productId", i)
                container.appendChild(item);
            }
          

            // EMPTY BUTTONS
            item = document.createElement("input");
            item.type = "button";
            item.value = "Clear";
            item.addEventListener("click", cart.reset);
            item.className += " btn-custom btn-custom-dark pull-left mt-4";
            container.appendChild(item);

            // CHECKOUT BUTTONS
            item = document.createElement("input");
            item.type = "button";
            item.value = "Checkout - " + "$";// + total;
            item.addEventListener("click", cart.checkout);
            item.className += " btn-custom btn-custom-blue pull-right mt-4";
            item.dataset.toggle = "modal";
            item.dataset.target = "#checkoutModal";

            container.appendChild(item);
          
        }
    },
    getPairs: function () {
        var a = [];
        for (var i in cart.data) {
            a.push({ productid: i, count: cart.data[i].qty });
        }
        return a;
    }
    ,
    change: function () {
        // change() : change quantity

        if (this.value == 0) {
            delete cart.data[this.dataset.id];
        } else {
            cart.data[this.dataset.id]['qty'] = this.value;
        }
        cart.save();
        cart.list();
    },
    forceReset: function () {
        // reset() : empty cart

        cart.data = {};
        cart.save();
        cart.list();
    },
    reset: function () {
        // reset() : empty cart

        if (confirm("Empty cart?")) {
            cart.data = {};
            cart.save();
            cart.list();
        }
    },
    listInTable: function () {

        var checkoutTemplate =
   `<tr data-id="productId" >
        <td><img class="checkout-img" src="productImg" alt="" /></td>
        <td>productName</td>
        <td>productQty</td>
        <td>productPrice</td>
        <td>productTotal</td>
        <td>paymentMethods</td>
    </tr>`
        var paymentMethodsTempalte = `<select  class="form-control" style="max-width:200px;">
        <option value="0" >OnDelivery</option>
        <option value="1" >Visa</option>
        <option value="2" >MasterCard</option>
        <option value="3" >Paypal</option>
        <option value="4" >Cheque</option>
        <option value="5" >DirectBankTransfer</option>
    </select>`

        var container = document.getElementById("contentOfTable"), item = null, part = null, product = null;
        container.innerHTML = "";
        var isempty = function (obj) {
            for (var key in obj) {
                if (obj.hasOwnProperty(key)) { return false; }
            }
            return true;
        };
        if (isempty(cart.data)) {
            item = document.createElement("div");
            item.innerHTML = "Cart is empty";
            item.className += " alert alert-info";
            item.style.margin = "25px";
            container.appendChild(item);
        }

        // Not empty
        else {
            // List items
            var total = 0, subtotal = 0;
            for (var i in cart.data) {
                item = document.createElement("div");
                item.classList.add("c-item");
                item.classList.add("row");
                product = cart.data[i];


                container.innerHTML += checkoutTemplate//.replace("productImg", product.img)
                    .replace("productImg", "/images/productimages/thereAreFourRules.jpg")
                    .replace("productName", product.name)
                    .replace("productQty", product.qty)
                    .replace("productPrice", product.price)
                    .replace("productId", i)
                    .replace("productTotal", parseInt(product.qty) * parseFloat(product.price.replace(" LE", "")))
                    .replace("paymentMethods", paymentMethodsTempalte);
                total += parseInt(product.qty) * parseFloat(product.price.replace(" LE", ""));

                container.appendChild(item);
            }
            document.getElementById("btnBuy").value = "Buy Now - " + "$" + total;


        }

    }

    ,

    checkout: function () {
        // checkout() : checkout the cart
        //location.href = "/Products/Checkout"
        var modalBody = document.getElementById("ModalContent");
        $("#loader").addClass("loading");

        if (parseValues()["qtys"].length == 0) return;

        $.ajax({
            url: `/Products/checkout`,
            type: "POST",
            data: {
                ordersData: parseValues()
            },
            cache: false,
            success: function (res) {
                $("#tableResult").replaceWith(res);
                $("#loader").removeClass("loading");
                //show mesge and redirect
            },
            error: function (xhr,message) {

                if (xhr.status == 401) {
                    $("#tableResult").replaceWith("");
                    document.getElementById("modal-msg").innerHTML += `<div class="alert alert-danger">Your have to login first </div>`
                    setTimeout(() => { document.location.href = "/Identity/Account/Login" }, 2000);
                }
                else {
                console.log(xhr.status, message) 
                }
            }
        });

    }
};

// Load previous cart and update HTML on load
window.addEventListener("load", function () {
    cart.load();
    cart.list();
});


function BuyItems() {

    var vals = parseValuesFromTable();
    if (vals.length == 0) return;
    $.ajax({
        url: `/Products/PlaceOrders`,
        type: "POST",
        data: {
            orders: vals
        },
        cache: false,
        success: function (res) {
            console.log(res);
            cart.forceReset();
            //show mesge and redirect
            $("#tableResult").replaceWith("");
            document.getElementById("modal-msg").innerHTML += `<div class="alert alert-info">Your order was placed successfuly, you can keep track of the order state in the orders page</div>`
            setTimeout(() => { document.location.href = "/products/index" }, 2000);
        },
        error: function (xhr, message) {
            if (xhr.status == 401) {
                $("#tableResult").replaceWith("");
                document.getElementById("modal-msg").innerHTML += `<div class="alert alert-danger">Your have to login first </div>`
                setTimeout(() => { document.location.href = "/Identity/Account/Login/index" }, 2000);
            }
            else {
                console.log(xhr.status, message)
            }
        }
    });

}

function parseValues() {
    var obj = {};
    obj.productids =[];
    obj.qtys =[];
    var qs = [];
    for (var i of Object.keys(cart.data) ) {
        obj.productids.push (i)
        obj.qtys.push (cart.data[i].qty)
    }
    return obj;
}
function parseValuesFromTable() {
    var arrOrders = [];
    $("#tableResult  .orderItem").each((i, a) =>{
        arrOrders.push({
            productId: $(a).data("id"),
            qty: $(a).data("qty"),
            PaymentMethod: $(a).find("select").val()
        })
    })
 
    return arrOrders;
}