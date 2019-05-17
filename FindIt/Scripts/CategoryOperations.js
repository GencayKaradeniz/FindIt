function CategoryAdd() {
    var categoryName = $("#categoryName").val();
    $.ajax({
        type:'POST',
        url: '/CategoryOperations/CategoryAdd',
        data: JSON.stringify({ categoryName: categoryName }),
        dataType: "json",
        contentType: 'application/json; charset=utf-8',
        success: function (result) {
            if (result == "OK") {
                alert("Kategori eklendi.");
                location.reload();
            }
            else {
                alert("Kategori eklenemedi");
            }
        }
    });
}

function CategoryDelete() {
    var categoryID = $("#deleteCategory").val();
    $.ajax({
        type: 'POST',
        url: '/CategoryOperations/CategoryDelete',
        data: JSON.stringify({ categoryID: categoryID }),
        dataType: "json",
        contentType: 'application/json; charset=utf-8',
        success: function (result) {
            if (result == "OK") {
                alert("Kategori silindi.");
                location.reload();
            }
            else {
                alert("Kategori silinemedi");
            }
        }
    });
}

function SubCategoryAdd() {
    var subCategoryName = $("#subCategory").val();
    var categoryID = $("#addSubCategory").val();
    $.ajax({
        type: 'POST',
        url: '/CategoryOperations/SubCategoryAdd',
        data: JSON.stringify({ subCategoryName: subCategoryName, categoryID: categoryID }),
        dataType: "json",
        contentType: 'application/json; charset=utf-8',
        success: function (result) {
            if (result == "OK") {
                alert("Alt Kategori eklendi.");
                location.reload();
            }
            else {
                alert("Alt Kategori eklenemedi");
            }
        }
    });
}

function SubCategoryDelete() {
    var subCategoryID = $("#deleteSubCategory").val();
    $.ajax({
        type: 'POST',
        url: '/CategoryOperations/SubCategoryDelete',
        data: JSON.stringify({ subCategoryID: subCategoryID }),
        dataType: "json",
        contentType: 'application/json; charset=utf-8',
        success: function (result) {
            if (result == "OK") {
                alert("Alt Kategori silindi.");
                location.reload();
            }
            else {
                alert("Alt Kategori silinemedi");
            }
        }
    });
}