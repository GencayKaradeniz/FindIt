function ProductAdd() {
    var productName = $("#productName").val();
    var productBarcode = $("#productBarcode").val();
    var categoryID = $("#CategoryID").val();
    var SubCategoryID = $("#SubCategoryID").val();
    var productCost = $("#productCost").val();
    var productStock = $("#productStock").val();
    var productFeatures = $("#productFeatures").val();
    var singlePicture = "Images/" + document.getElementById("singlePicture").files[0].name;
    $.ajax({
        type: 'POST',
        url: '/ProductOperations/ProductAdd',
        data: JSON.stringify({
            productName: productName,
            productBarcode: productBarcode,
            categoryID: categoryID,
            SubCategoryID: SubCategoryID,
            productCost: productCost,
            productStock: productStock,
            productFeatures: productFeatures,
            singlePicture: singlePicture
        }),
        dataType: "json",
        contentType: 'application/json; charset=utf-8',
        success: function (result) {
            if (result == "OK") {
                alert("Ürün başarıyla eklendi.");
                location.reload();
            }
            else {
                alert("Ürün eklenemedi");
            }
        }
    });
}

function ProductSearch() {
    var barcode = $("#search").val();
    $.ajax({
        type: 'POST',
        url: '/ProductOperations/ProductSearch',
        data: JSON.stringify({
            barcode: barcode
        }),
        dataType: "json",
        contentType: 'application/json; charset=utf-8',
        success: function (result) {
            $("#productName").val(result.productName);
            $("#productBarcode").val(result.barcode);
            $("#categorySaved").html(result.categoryName);
            $("#subCategorySaved").html(result.subCategoryName);
            $("#productCost").val(result.price);
            $("#productStock").val(result.stock);
            $("#productFeatures").val(result.features);
        }
    });
}

function ProductDelete() {
    var barcode = $("#productBarcode").val();

    $.ajax({
        type: 'POST',
        url: '/ProductOperations/ProductDelete',
        data: JSON.stringify({
            barcode: barcode
        }),
        dataType: "json",
        contentType: 'application/json; charset=utf-8',
        success: function (result) {
            if (result == "OK") {
                alert("Ürün başarıyla silindi.");
                location.reload();
            }
            else {
                alert("Ürün silinirken bir hata oluştu.")
            }
        }
    });
}

function ProductUpdate() {
    var barcodeSearch = $("#search").val();
    var productName = $("#productName").val();
    var productBarcode = $("#productBarcode").val();
    var categoryID = $("#CategoryID").val();
    var SubCategoryID = $("#SubCategoryID").val();
    var productCost = $("#productCost").val();
    var productStock = $("#productStock").val();
    var productFeatures = $("#productFeatures").val();
    var singlePicture = "Images/" + document.getElementById("singlePicture").files[0].name;
    $.ajax({
        type: 'POST',
        url: '/ProductOperations/ProductUpdate',
        data: JSON.stringify({
            productName: productName,
            productBarcode: productBarcode,
            categoryID: categoryID,
            SubCategoryID: SubCategoryID,
            productCost: productCost,
            productStock: productStock,
            productFeatures: productFeatures,
            singlePicture: singlePicture,
            barcodeSearch: barcodeSearch
        }),
        dataType: "json",
        contentType: 'application/json; charset=utf-8',
        success: function (result) {
            if (result == "OK") {
                alert("Ürün başarıyla güncellendi.");
                location.reload();
            }
            else {
                alert("Ürün güncellenemedi");
            }
        }
    });
}